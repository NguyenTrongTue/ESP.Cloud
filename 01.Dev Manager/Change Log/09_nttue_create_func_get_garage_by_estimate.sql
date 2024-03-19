
drop function if exists func_get_garage_by_estimate(lat double precision, lng double precision, p_estimate_id uuid);

CREATE OR REPLACE FUNCTION public.func_get_garage_by_estimate(lat double precision, lng double precision, p_estimate_id uuid)
 RETURNS TABLE(garage_id uuid, garage_name text, latitude numeric, longitude numeric, open_time time without time zone, close_time time without time zone, address text, garage_website text, phone text, image text, distance double precision, total_rating integer, avg_rating double precision)
 LANGUAGE plpgsql
AS $function$
BEGIN    

    drop table if exists tmp_result;
    create TEMP table tmp_result(
        garage_id uuid,
        garage_name text,
        latitude numeric,
        longitude numeric,
        open_time time without time zone,
        close_time time without time zone,
        address text,
        garage_website text,
        phone text,
        image text,
        distance double precision
    );

    insert into tmp_result
    with tmp_garage as (
        select g.* from garage g
        left join tmp_estimate te on te.garage_id = g.garage_id
        where te.estimate_id = p_estimate_id
    )
    select *,
     6371 * acos(
             cos(radians(lat)) * cos(radians(g.latitude)) * cos(radians(g.longitude) - radians(lng)) +
             sin(radians(lat)) * sin(radians(g.latitude))
           ) AS distance 
    from tmp_garage g
    ;

    drop table if exists tmp_return;
    create TEMP table tmp_return (
        garage_id uuid,
        garage_name text,
        latitude numeric,
        longitude numeric,
        open_time time without time zone,
        close_time time without time zone,
        address text,
        garage_website text,
        phone text,
        image text,
        distance double precision,
        total_rating int, 
        avg_rating double precision
    );


    insert into tmp_return
    select 
        tr.*,
        coalesce(t.total_rating, 0) as total_rating,        
        coalesce(t.avg_rating, 0) as avg_rating
    from tmp_result tr
    left join LATERAL (
        select 
            distinct gr.garage_id,
            avg(gr.rating) over (partition by gr.garage_id) as avg_rating, 
            count(*) over (partition by gr.garage_id) as total_rating
        from public.garage_reviews gr
        where gr.garage_id = tr.garage_id 
    ) t on true;
    return query select * from tmp_return order by distance asc;
END;
$function$
;
