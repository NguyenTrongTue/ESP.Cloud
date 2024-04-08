create or replace function sme.func_nttue_01(p_list_contract_id uuid[], p_is_update_all boolean)
Returns boolean
Language plpgsql
as $function$
Declare v_is_deduction boolean = false;
Declare p_voucher_arise_recorded_in int = 0;
Declare p_start_date date;
Begin
select sdo.option_value::bool from sme.sys_db_options sdo where sdo.option_id = ‘taxMethod’ limit 1 into v_is_deduction;

select sdo.option_value::bool from sme.sys_db_options sdo where sdo.option_id = VoucherariseRecordedin limit 1 into p_voucher_arise_recorded_in;

select sdo.option_value::bool from sme.sys_db_options sdo where sdo.option_id = startDate limit 1 into p_start_date;

drop table if exists tmp_contract;
Create temp table tmp_contract(
	contract_id uuid primary key,
project_id uuid,
is_arused boolean,
accoum_sale_amount_finance numeric,
close_amount numeric
);
insert into tmp_contract(
	contract_id ,
project_id,
is_arused,
accoum_sale_amount_finance,
close_amount 
) select c.contract_id, 
coalesce (c.project_id, c.contract_id),
c.is_arused,
c.accoum_sale_amount_finance,
c.close_amount 
From sme.contract c
Where (p_is_update_all = true or c.contract_id = any(p_list_contract_id) or c.project_id = any(p_list_contract_id));

with tmp_inv_detail as (
	select sai.refid,
	sai.inv_no,
	sai.inv_date,
	Case
		When v_is_deduction = false then sum(said.amount - said.dis_amount + said.vat_amout)
		Else sum(said.amount - said.dis_amount + said.vat_amout - coalesce(sai.deductions_tax_amount, 0))
	End as amount,
	Case
		When v_is_deduction = false then sum(said.amount_oc - said.dis_amount_oc + said.vat_amout_oc)
		Else sum(said.amount_oc - said.dis_amount_oc + said.vat_amout_oc - coalesce(sai.deductions_tax_amount_oc, 0))
	End as amount_oc,
	said.contract_id
	From tmp_contract t
	inner join sme.sa_inv_detail said on t.contract_id = said.contract_id
	inner join sme.sa_inv sai on sai.refid = said.refid
	left join sme.einvoice_status es on sai.refid = es.refid
	Where coalesce(said.is_combo, false) = false
	and sai.inv_date >= p_start_date
	and coalesce(es.is_inv_cancel, false) = false
	Group by sai.refid,
	sai.inv_no,
	sai.inv_date,
	said.contract_id
),
tmp_vocher_detail as (
	select sa.refid,
	sa.inv_no,
	sa.inv_date,
	Case
		When v_is_deduction = false then sum(case when sa.include_invoice = 1 then sad.amount - sad.dis_amount + sad.vat_amout else 0 end)
		Else sum(case when sa.include_invoice = 1 then sad.amount - sad.dis_amount + sad.vat_amout - coalesce(sad.deductions_tax_amount, 0) else 0 end)
	End as amount,
	Case
		When v_is_deduction = false then sum(case when sa.include_invoice = 1 then sad.amount_oc - sad.dis_amount_oc + sad.vat_amout_oc else 0 end)
		Else sum(case when sa.include_invoice = 1 then sad.amount_oc - sad.dis_amount_oc + sad.vat_amout_oc - coalesce(sad.deductions_tax_amount_oc, 0) else 0 end)
	End as amount_oc,
	t.contract_id
	From tmp_contract t
	inner join sme.sa_vocher_detail sad on t.contract_id = sad.contract_id
	inner join sme.sa_voucher sa on sa.refid = sad.refid
	left join sme.einvoice_status esi on sa.refid = esi.refid
	Where sad.is_combo = false
	and sa.include_invoice = 1
	and coalesce(esi.is_inv_cancel, false) = false
	Group by sa.refid,
	sa.inv_no,
	sa.inv_date,
	t.contract_id
)


insert into tmp_invoice_export
select tmp_inv_detail.refid, tmp_inv_detail.inv_no, tmp_inv_detail.inv_date, tmp_inv_detail.amount, tmp_inv_detail.amount_oc, tmp_inv_detail.contract_id
From tmp_inv_detail full outer join tmp_vocher_detail on tmp_inv_detail.refid = tmp_vocher_detail.refid and tmp_inv_detail.inv_date = tmp_vocher_detail.inv_date


truncate table tmp_invoice_export_result;
Insert into tmp_invoice_export_result
Select string_agg(t.inv_no,’, ’ order by t.inv_date, t.inv_no),
max(t.inv_date), sum(amount) amount, sum(amount_oc) amount_oc, contract_id
From tmp_invoice_export
Group by contract_id;

update sme.contract c1
set 
	total_sale_amount_finance = 
Case
	When tt.is_arused = true
	then (coalesce(tt.accum_sal_amount_finance, 0) + coalesce(sa.sale_amount, 0)- coalesce(sar.amount, 0) - (coalesce(sad.amount, 0) ))
Else
(coalesce(sa.sale_amount, 0) - coalesce(sar.amount, 0) - coalesce(sad.amount, 0) )
end,
total_sale_amount_oc_finance = 
Case
	When tt.is_arused = true
	then (coalesce(tt.accum_sal_amount_finance, 0) + coalesce(sa.sale_amount_oc, 0)- coalesce(sar.amount_oc, 0) - coalesce(sad.amount_oc, 0) )
Else
(coalesce(sa.sale_amount_oc, 0) - coalesce(sar.amount_oc, 0) - coalesce(sad.amount_oc, 0) )
End,
date_record_revenue_finance =
Case 
when sa.posted_date is not null then sa.posted_date
When tt.is_arused  = true and coalesce(tt.accum_sale_amount_finace, 0) > 0
then p_start_date - iNtERVaL ‘1 day’ else null end,
has_record_all_revenue_finance = case when tt.is_arused = true and (coalesce(tt.accum_sal_amount_finance, 0) + coalesce(sa.sale_amount_oc, 0)- coalesce(sar.amount_oc, 0) - coalesce(sad.amount_oc, 0) ) >= tt.close_amount then true
when tt.is_arused = false and (coalesce(sa.sale_amount_oc, 0)- coalesce(sar.amount_oc, 0) - coalesce(sad.amount_oc, 0) ) >= tt.close_amount then true
Else false end,
total_invoice_amount_finance = coalesce(tm.inv_date, c1.inv_date_last_year),
is_invoced  = case when (tm.inv_no is not null) or c1.inv_date_last_year is not null then true else false end from tmp_contract tt
Left join (
	select 
		sum(sad.amount - sad.discount_amount + sad.vat_amount) as sale_amount,
um(sad.amount_oc - sad.discount_amount_oc + sad.vat_amount_oc) as sale_amount_oc,
sad.contract_id,
max(sa.posted_date) as posted_date,
From tmp_contract t
inner join sme.sa_voucher_detail sad on t.contract_id = sad.contract_id
inner join sme.sa_voucher sa on sa.refid = sad.refid
Where is_combo = false
Group by sad.contract_id
) sa on sa.contract_id = tt.contract_id
		Left join (
	select  case when v_is_deduction = false then
		sum(srd.amount - srd.discount_amount + srd.vat_amount) 
Else  sum(srd.amount - srd.discount_amount + srd.vat_amount - coalesce(srd.deductions_tax_amount, 0)) end 
 as amount,
case when v_is_deduction = false then
		sum(srd.amount_oc - srd.discount_amount_oc + srd.vat_amount_oc) 
Else  sum(srd.amount_oc - srd.discount_amount_oc + srd.vat_amount_oc - coalesce(srd.deductions_tax_amount, 0)) end 
 as amount_oc,
sad.contract_id
From tmp_contract t
inner join sme.sa_return_detail srd on t.contract_id = srd.contract_id
Group by srd.contract_id
) sar on sar.contract_id = tt.contract_id

	Left join (
	select  case when v_is_deduction = false then
		sum(srd.amount - srd.discount_amount + srd.vat_amount) 
Else  sum(srd.amount - srd.discount_amount + srd.vat_amount - coalesce(srd.deductions_tax_amount, 0)) end 
 as amount,
case when v_is_deduction = false then
		sum(srd.amount_oc - srd.discount_amount_oc + srd.vat_amount_oc) 
Else  sum(srd.amount_oc - srd.discount_amount_oc + srd.vat_amount_oc - coalesce(srd.deductions_tax_amount, 0)) end 
 as amount_oc,
sad.contract_id
From tmp_contract t
inner join sme.sa_discount_detail srd on t.contract_id = srd.contract_id
Group by srd.contract_id
) sad on sad.contract_id = tt.contract_id;




















update sme.contract ct
set total_invoce_amount_finance = ct.total_invoice_amount_finance-ct.child_invoice_amount+rs.sub_invoice_amount,
total_invoce_amount_oc_finance = ct.total_invoice_amount_oc_finance-ct.child_invoice_amount_oc+rs.sub_invoice_oc_amount,
Child_invoice_amount =rs.sub_invoice_amount,
Child_invoice_amount_oc =rs.sub_invoice_oc_amount,
Balance_receipt_amount_finance = ct.close_amount-(ct.total_receipted-ct.child_receipted_amount+rs.sub_receipted_amount),
total_receipted_amount = ct.total_receipted_amount-ct.child_receipted_amount+rs.sub_receipted_amount,
total_receipted_amount_oc = ct.total_receipted_amount_oc-ct.child_receipted_amount_oc+rs.sub_receipted_amount_oc,
Child_receipted_amount = rs.sub_receipted_amount,
Child_receipted_amount_oc = rs.sub_receipted_amount_oc,
total_sale_amount_finance = rs.sub_total_sale_amount,
total_sale_amount_finance_oc = rs.sub_total_sale_amount_oc,
Date_record_revenue_finance = rs.sub_date_record_revenue_finance,
is_invoiced= case when rs.is_invoiced = 0 then false else true end,
has_recorded_all_revenue_finance = case when rs.sub_total_sale_amount >=ct.close_amount then true else false end
From (
select t.project_id,
sum(total_sale_amount_finance) as sub_total_sale_amount,
sum(total_sale_amount_finance_oc) as sub_total_sale_amount_oc,

sum(total_receipted_amount) as sub_receipted_amount,
sum(total_receipted_amount_oc) as sub_receipted_amount_oc,
sum(total_invoce_amount_finance) as sub_invoce_amount,
sum(total_invoce_amount_finance_oc) as sub_invoce_amount_oc,
max(Date_record_revenue_finance) as sub_date_record_revenue_finance,
min(case when is_invoiced then 1 else 0 end) as is_invoiced
From (select distinct is_invoiced from tmp_contract where project_id is not null)t
inner join sme.contract p on t.porject_id = p.project_id
Group by t.project_id) rs
Where ct.contract_id = rs.project_id and ct.is_project=true;

Return true
End $function$
;

