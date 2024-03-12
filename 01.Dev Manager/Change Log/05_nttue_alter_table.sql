    DROP TABLE public.booking_history_detail;

    DROP TABLE public.booking_history;

    CREATE TABLE public.booking_history (
        booking_history_id uuid NOT NULL,
        garage_id uuid NULL,
        user_id uuid NULL,
        cars_id uuid NULL,
        "comment" text NULL,
        first_name text NULL,
        last_name text NULL,
        email text NULL,
        phone text NULL,
        booking_date timestamp NULL,
        CONSTRAINT booking_history_pkey PRIMARY KEY (booking_history_id),
        CONSTRAINT pk_booking_history_cars FOREIGN KEY (cars_id) REFERENCES public.cars(cars_id),
        CONSTRAINT pk_booking_history_garage FOREIGN KEY (garage_id) REFERENCES public.garage(garage_id),
        CONSTRAINT pk_booking_history_user FOREIGN KEY (user_id) REFERENCES public."user"(user_id)
    );

    CREATE TABLE public.booking_history_detail (
        booking_history_detail_id uuid NOT NULL,
        booking_history_id uuid NULL,
        garage_services_id uuid NULL,
        quantity int4 NULL,
        total_amount numeric NULL,
        CONSTRAINT booking_history_detail_pkey PRIMARY KEY (booking_history_detail_id),
        CONSTRAINT pk_booking_history_detail_booking_history FOREIGN KEY (booking_history_id) REFERENCES public.booking_history(booking_history_id),
        CONSTRAINT pk_booking_history_detail_garage_services FOREIGN KEY (garage_services_id) REFERENCES public.garage_services(garage_services_id)
    );
