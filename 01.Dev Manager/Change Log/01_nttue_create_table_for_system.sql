drop table if exists public.garage_reviews;
drop table if exists public.booking_history_detail;
drop table if exists public.booking_history;
drop table if exists public.promo_garages;
drop table if exists public.garage_services;
drop table if exists public.garage;
drop table if exists public.user;
drop table if exists public.cars;

CREATE TABLE public.garage (
	garage_id uuid NOT NULL,
	garage_name text NULL,
	latitude numeric NULL,
	longitude numeric NULL,
	open_time time NULL,
	close_time time NULL,
	address text NULL,
	garage_website text NULL,
	phone text NULL,
	CONSTRAINT garage_pkey PRIMARY KEY (garage_id)
);
CREATE INDEX idx_garage_garage_name_btree ON public.garage USING btree (garage_name);

CREATE TABLE public."user" (
	user_id uuid NOT NULL,
	user_name text NULL,
	email text NULL,
	address text NULL,
	phone text NULL,
	password_hash bytea NULL,
	password_salt bytea NULL,
	avatar text NULL,
	potential bool NULL,
	CONSTRAINT user_pkey PRIMARY KEY (user_id)
);

CREATE TABLE public.cars (
	cars_id uuid NOT NULL,
	make text NULL,
	model text NULL,
	"year" int4 NULL,
	seats int4 NULL,
	description text NULL,
	CONSTRAINT cars_pkey PRIMARY KEY (cars_id)
);
CREATE INDEX idx_cars_make ON public.cars USING btree (make);

CREATE TABLE public.garage_services (
	garage_services_id uuid NOT NULL,
	garage_id uuid NULL,
	service_code text NULL,
	service_name text NULL,
	price numeric NULL,
	description text NULL,
	duaration int4 NULL,
	cars_id uuid NULL,
	CONSTRAINT garage_services_pkey PRIMARY KEY (garage_services_id),
	CONSTRAINT pk_garage_services_garage FOREIGN KEY (garage_id) REFERENCES public.garage(garage_id)
);
CREATE INDEX idx_garage_services_garage_id ON public.garage_services USING btree (garage_id);
CREATE INDEX idx_garage_services_service_code_btree ON public.garage_services USING btree (service_code);
CREATE INDEX idx_garage_services_service_name_btree ON public.garage_services USING btree (service_name);

CREATE TABLE public.garage_reviews (
	garage_reviews_id uuid NOT NULL,
	garage_id uuid NULL,
	user_id uuid NULL,
	"comment" text NULL,
	rating int4 NULL,
	image text NULL,
	CONSTRAINT garage_reviews_pkey PRIMARY KEY (garage_reviews_id),
	CONSTRAINT pk_garage_reviews_garage FOREIGN KEY (garage_id) REFERENCES public.garage(garage_id),
	CONSTRAINT pk_garage_reviews_user FOREIGN KEY (user_id) REFERENCES public."user"(user_id)
);
CREATE TABLE public.booking_history (
	booking_history_id  uuid NOT NULL,
    garage_id uuid NULL,
    user_id uuid NULL,
    cars_id uuid null,
    comment text null, 
    booking_date timestamp null,    
	CONSTRAINT booking_history_pkey PRIMARY KEY (booking_history_id),
    CONSTRAINT pk_booking_history_garage FOREIGN KEY (garage_id) REFERENCES public.garage(garage_id),
    CONSTRAINT pk_booking_history_user FOREIGN KEY (user_id) REFERENCES public.user(user_id),
    CONSTRAINT pk_booking_history_cars FOREIGN KEY (cars_id) REFERENCES public.cars(cars_id)
);

CREATE TABLE public.booking_history_detail (
	booking_history_detail_id  uuid NOT NULL,
    booking_history_id uuid NULL,
    garage_services_id uuid NULL,
    quantity int null, 
    total_amount numeric null,    
	CONSTRAINT booking_history_detail_pkey PRIMARY KEY (booking_history_detail_id),
    CONSTRAINT pk_booking_history_detail_booking_history FOREIGN KEY (booking_history_id) REFERENCES public.booking_history(booking_history_id),
    CONSTRAINT pk_booking_history_detail_garage_services FOREIGN KEY (garage_services_id) REFERENCES public.garage_services(garage_services_id)
);


CREATE TABLE public.promo_garages (
	promo_garages_id  uuid NOT NULL,
    garage_id uuid null,
    garage_services_id uuid NOT NULL,   
    percent_discount decimal NULL,
    start_date date, 
    end_date date,
	CONSTRAINT promo_garages_pkey PRIMARY KEY (promo_garages_id),
    CONSTRAINT pk_promo_garages_garage FOREIGN key(garage_id) REFERENCES public.garage(garage_id),
    CONSTRAINT pk_promo_garages_garage_services FOREIGN key(garage_services_id) REFERENCES public.garage_services(garage_services_id)
);