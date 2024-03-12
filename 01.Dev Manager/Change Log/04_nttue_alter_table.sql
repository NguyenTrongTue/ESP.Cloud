ALTER TABLE public."user" DROP column if exists user_name;
ALTER TABLE public."user" DROP COLUMN if exists avatar;
ALTER TABLE public."user" ADD column if not exists fullname text NULL;
