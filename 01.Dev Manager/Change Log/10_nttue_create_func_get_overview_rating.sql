DROP FUNCTION IF EXISTS public.func_get_overview_rating(p_make text);

CREATE OR REPLACE FUNCTION public.func_get_overview_rating(p_make text)
RETURNS TABLE(make text, cars_id uuid, model text, "year" int, rating int, total_rating int)
LANGUAGE plpgsql
AS $function$
DECLARE
    v_cars_id uuid;
BEGIN    
    IF p_make = '' THEN 
        SELECT cr.cars_id INTO v_cars_id FROM car_review cr 
        GROUP BY cr.cars_id 
        ORDER BY AVG(cr.rating) DESC, COUNT(*) DESC 
        LIMIT 1;

        RETURN QUERY
        SELECT cr.make, cr.cars_id, cr.model, cr."year", cr.rating, 
               COUNT(*) OVER (PARTITION BY cr.rating ORDER BY cr.rating DESC)::int AS total_rating 
        FROM car_review cr 
        WHERE cr.cars_id = v_cars_id;
    ELSE 
        RETURN QUERY
        SELECT distinct cr.make, NULL::uuid AS cars_id, NULL::text AS model, NULL::int AS "year", cr.rating, 
               COUNT(*) OVER (PARTITION BY cr.rating, cr.make ORDER BY cr.rating DESC)::int AS total_rating 
        FROM car_review cr 
        WHERE cr.make = p_make;
    END IF;
END;
$function$;
