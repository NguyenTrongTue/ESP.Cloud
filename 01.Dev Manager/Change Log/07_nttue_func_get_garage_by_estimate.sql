
drop function if exists public.func_get_garage_by_estimate(p_car_id uuid, lat double precision, lng double precision);

CREATE OR REPLACE FUNCTION public.func_get_garage_by_estimate(p_car_id uuid, lat double precision, lng double precision)
 RETURNS TABLERETURNS TABLE(garage_id uuid, garage_name text, latitude numeric, longitude numeric, open_time time without time zone, close_time time without time zone, address text, garage_website text, phone text, distance double precision, total_rating bigint)
 LANGUAGE plpgsql
AS $function$
BEGIN    
   

END;
$function$;


