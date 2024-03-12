drop function if exists public.func_caculate_distance(lat double precision, lng double precision);
CREATE OR REPLACE FUNCTION public.func_caculate_distance(lat double precision, lng double precision)
 RETURNS TABLE(garage_id uuid, garage_name text, latitude numeric, longitude numeric, open_time time without time zone, close_time time without time zone, address text, garage_website text, phone text, distance double precision)
 LANGUAGE plpgsql
AS $function$
BEGIN
  RETURN QUERY
    SELECT *,
           6371 * acos(
             cos(radians(lat)) * cos(radians(g.latitude)) * cos(radians(g.longitude) - radians(lng)) +
             sin(radians(lat)) * sin(radians(g.latitude))
           ) AS distance
    FROM public.garage as g
    ORDER BY distance
    LIMIT 20;

  RETURN;
END;
$function$
;



drop function if exists public.func_get_garage_by_coordinate(lat double precision, lng double precision, sort_by text, car_type text, p_open_time text, list_vervice_names text, take integer, skip integer);

CREATE OR REPLACE FUNCTION public.func_get_garage_by_coordinate(lat double precision, lng double precision, sort_by text, car_type text, p_open_time text, list_vervice_names text, take integer, skip integer)
 RETURNS TABLE(garage_id uuid, garage_name text, latitude numeric, longitude numeric, open_time time without time zone, close_time time without time zone, address text, garage_website text, phone text, distance double precision, total_rating bigint)
 LANGUAGE plpgsql
AS $function$
DECLARE 
    whereText text;
    dynamicSql text;
BEGIN    

    whereText := list_vervice_names || ' AND ' || car_type || ' AND ' || p_open_time;
  

    dynamicSql := 
        'WITH temp_result AS (' ||
        '    SELECT g.*, ' ||
        '        coalesce(sum(gr.rating), 0) as total_rating ' ||
        '    FROM public.func_caculate_distance(' || lat || ', ' || lng || ') g ' ||
        '    LEFT  JOIN LATERAL (select garage_id, cars_id, service_name from garage_services) gs ON gs.garage_id = g.garage_id ' ||
        '    JOIN public.cars c ON c.cars_id = gs.cars_id ' ||
        '    LEFT JOIN public.garage_reviews gr ON gs.garage_id = g.garage_id ' ||
        '    WHERE ' || whereText || ' ' ||
        '    GROUP BY g.garage_id, g.garage_name,g.latitude ,g.longitude ,g.open_time ,g.close_time ,g.address ,g.garage_website ,g.phone ,g.distance '||
        '    LIMIT ' || take || ' ' ||
        '    OFFSET ' || skip || ' ' ||
        ') ' ||
        'SELECT * FROM temp_result ' ||
        'ORDER BY ' || sort_by;

     RAISE NOTICE 'dynamicSql: %', dynamicSql;
     RAISE NOTICE 'whereText: %', whereText;
     RAISE NOTICE 'sort_by: %', sort_by;

    return query EXECUTE dynamicSql;
END;
$function$
;
