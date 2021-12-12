-- Table: public.Credentials

-- DROP TABLE public."Credentials";

CREATE TABLE IF NOT EXISTS public."Credentials"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "HospitalName" text COLLATE pg_catalog."default",
    "HospitalLocalhost" text COLLATE pg_catalog."default",
    "ApiKey" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Credentials" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Credentials"
    OWNER to postgres;

-- Table: public.Hospitals

-- DROP TABLE public."Hospitals";

CREATE TABLE IF NOT EXISTS public."Hospitals"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" text COLLATE pg_catalog."default",
    "Key" text COLLATE pg_catalog."default",
    "Localhost" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Hospitals" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Hospitals"
    OWNER to postgres;

-- Table: public.Medicines

-- DROP TABLE public."Medicines";

CREATE TABLE IF NOT EXISTS public."Medicines"
(
    "MedicineId" bigint NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 9223372036854775807 CACHE 1 ),
    "MedicineName" text COLLATE pg_catalog."default",
    "Manufacturer" text COLLATE pg_catalog."default",
    "SideEffects" text COLLATE pg_catalog."default",
    "Usage" text COLLATE pg_catalog."default",
    "Weigth" double precision NOT NULL,
    "MainPrecautions" text COLLATE pg_catalog."default",
    "PotentialDangers" text COLLATE pg_catalog."default",
    "Quantity" integer NOT NULL,
    "Price" double precision NOT NULL DEFAULT 0.0,
    CONSTRAINT "PK_Medicines" PRIMARY KEY ("MedicineId")
)

TABLESPACE pg_default;

ALTER TABLE public."Medicines"
    OWNER to postgres;

-- Table: public.News

-- DROP TABLE public."News";

CREATE TABLE IF NOT EXISTS public."News"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Title" text COLLATE pg_catalog."default",
    "Content" text COLLATE pg_catalog."default",
    "FromDate" timestamp without time zone NOT NULL,
    "ToDate" timestamp without time zone NOT NULL,
    "IdEncoded" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_News" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."News"
    OWNER to postgres;

-- Table: public.Objections

-- DROP TABLE public."Objections";

CREATE TABLE IF NOT EXISTS public."Objections"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "HospitalId" text COLLATE pg_catalog."default",
    "Content" text COLLATE pg_catalog."default",
    "IdEncoded" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Objections" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Objections"
    OWNER to postgres;

-- Table: public.Replies

-- DROP TABLE public."Replies";

CREATE TABLE IF NOT EXISTS public."Replies"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "ObjectionId" text COLLATE pg_catalog."default",
    "Content" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Replies" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Replies"
    OWNER to postgres;

-- Table: public.Replies

-- DROP TABLE public."Replies";

CREATE TABLE IF NOT EXISTS public."Replies"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "ObjectionId" text COLLATE pg_catalog."default",
    "Content" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Replies" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Replies"
    OWNER to postgres;

-- Table: public.SubstituteMedicines

-- DROP TABLE public."SubstituteMedicines";

CREATE TABLE IF NOT EXISTS public."SubstituteMedicines"
(
    "MedicineId" bigint NOT NULL,
    "SubstituteId" bigint NOT NULL,
    CONSTRAINT "PK_SubstituteMedicines" PRIMARY KEY ("MedicineId", "SubstituteId"),
    CONSTRAINT "FK_SubstituteMedicines_Medicines_MedicineId" FOREIGN KEY ("MedicineId")
        REFERENCES public."Medicines" ("MedicineId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT "FK_SubstituteMedicines_Medicines_SubstituteId" FOREIGN KEY ("SubstituteId")
        REFERENCES public."Medicines" ("MedicineId") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."SubstituteMedicines"
    OWNER to postgres;
-- Index: IX_SubstituteMedicines_SubstituteId

-- DROP INDEX public."IX_SubstituteMedicines_SubstituteId";

CREATE INDEX "IX_SubstituteMedicines_SubstituteId"
    ON public."SubstituteMedicines" USING btree
    ("SubstituteId" ASC NULLS LAST)
    TABLESPACE pg_default;


DELETE FROM public."Credentials";
DELETE FROM public."Hospitals";
DELETE FROM public."Medicines";
DELETE FROM public."News";
DELETE FROM public."Objections";
DELETE FROM public."Replies";

INSERT INTO public."Credentials"(
	"Id", "HospitalName", "HospitalLocalhost", "ApiKey")
	VALUES (1, 'World Vision Clinic', 'http://localhost:43818', 'qvHNrR8Qy8opuGnfHBPseBzCjBPdd7oZgFL3UvKKBGo=');

INSERT INTO public."Hospitals"(
	"Id", "Name", "Key", "Localhost")
	VALUES (1,'World Vision Clinic','YtmmHLff/L72Db/KxqK/xJVUMzbt5xUiGn6IHgdf13Y=','http://localhost:43818');

INSERT INTO public."Medicines"(
	"MedicineId", "MedicineName", "Manufacturer", "SideEffects", "Usage", "Weigth", "MainPrecautions", "PotentialDangers", "Quantity", "Price")
	VALUES (5,'Aspirin','Galenika','None','3 times a day', 200.00,'None','None',21,250.00);

INSERT INTO public."News"(
	"Id", "Title", "Content", "FromDate", "ToDate", "IdEncoded")
	VALUES (1,'Maske na popustu', 'Sve maske snizene', TO_TIMESTAMP('2021-12-16 00:00:00', 'YYYY-MM-DD HH24:MI:SS'), TO_TIMESTAMP('2021-12-19 00:00:00', 'YYYY-MM-DD HH24:MI:SS'),'NqZuWmdjua/RgKMP4wVyMKkeRDYMSvhOYAA6PFpn6HA=');


INSERT INTO public."Objections"(
	"Id", "HospitalId", "Content", "IdEncoded")
	VALUES (1,'http://localhost:43818', 'Radnici nisu ljubazni', 'tCNbgflULFw2eCqc5zDsTTrx9YbZoYDlOZl0JAHhPEU=');
