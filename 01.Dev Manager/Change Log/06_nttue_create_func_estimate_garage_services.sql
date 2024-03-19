
ALTER TABLE public.garage ADD column if not exists image text NULL;

drop table if exists tmp_estimate;
CREATE TABLE public.tmp_estimate (
    estimate_id uuid,
	garage_id uuid
);


drop function if exists public.func_estimate_garage_services(p_car_id uuid, t_services_code jsonb);

CREATE OR REPLACE FUNCTION public.func_estimate_garage_services(p_car_id uuid, t_services_code jsonb)
 RETURNS TABLE(max_price numeric, min_price numeric, total_locations int, estimate_id uuid)
 LANGUAGE plpgsql
AS $function$
declare v_estimate_id uuid = uuid_generate_v4();
BEGIN    
    DROP TABLE IF EXISTS tmp_service_codes;

    CREATE TEMP TABLE tmp_service_codes(service_code text);

    INSERT INTO tmp_service_codes(service_code)
    SELECT  
    (json_data->>'service_code')::TEXT AS text
    FROM 
    (
        SELECT jsonb_array_elements(t_services_code) AS json_data
    ) AS t;

    drop table if exists tmp_result;
     CREATE TEMP TABLE tmp_result(        
        garage_id uuid,
        total_price numeric
    );


    INSERT into tmp_result(garage_id, total_price)  
    select distinct  gs.garage_id, sum(gs.price) over (partition by gs.garage_id) as total_price  
    from tmp_service_codes tsc
    left join garage_services gs on gs.service_code = tsc.service_code
    where gs.cars_id = p_car_id   ;

    -- insert vào bảng tạm estimate rồi trả về backend estimate_id để called khi lấy danh sách các garage
    
    delete from tmp_estimate;
    insert into tmp_estimate(
        estimate_id, 
        garage_id
    )
    select v_estimate_id, garage_id from tmp_result;

    drop table if exists tmp_return;
    CREATE TEMP TABLE tmp_return(        
        max_price numeric,
        min_price numeric,
        total_garage int,
        estimate_id uuid
    );
    insert into tmp_return
    select max(total_price), min(total_price), count(garage_id), v_estimate_id from tmp_result;

    return query select * from tmp_return;

END;
$function$;



