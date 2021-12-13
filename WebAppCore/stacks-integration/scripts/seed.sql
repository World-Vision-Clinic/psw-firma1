-- Table: public.Credentials

-- DROP TABLE public."Credentials";

CREATE TABLE IF NOT EXISTS public."Credentials"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "PharmacyName" text COLLATE pg_catalog."default",
    "PharmacyLocalhost" text COLLATE pg_catalog."default",
    "ApiKey" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Credentials" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Credentials"
    OWNER to postgres;

-- Table: public.Patient

-- DROP TABLE public."Patient";

CREATE TABLE IF NOT EXISTS public."Patient"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "FirstName" text COLLATE pg_catalog."default",
    "LastName" text COLLATE pg_catalog."default",
    "PersonalID" text COLLATE pg_catalog."default",
    "IsGuest" boolean NOT NULL,
    "IsBlocked" boolean NOT NULL,
    CONSTRAINT "PK_Patient" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Patient"
    OWNER to postgres;


-- Table: public.Allergen

-- DROP TABLE public."Allergen";

CREATE TABLE IF NOT EXISTS public."Allergen"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Other" text COLLATE pg_catalog."default",
    "MedicineNames" text[] COLLATE pg_catalog."default",
    "IngredientNames" text[] COLLATE pg_catalog."default",
    CONSTRAINT "PK_Allergen" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Allergen"
    OWNER to postgres;


-- Table: public.Files

-- DROP TABLE public."Files";

CREATE TABLE IF NOT EXISTS public."Files"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" text COLLATE pg_catalog."default",
    "Extension" text COLLATE pg_catalog."default",
    "Path" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Files" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Files"
    OWNER to postgres;

-- Table: public.Medicines

-- DROP TABLE public."Medicines";

CREATE TABLE IF NOT EXISTS public."Medicines"
(
    "ID" text COLLATE pg_catalog."default" NOT NULL,
    "Name" text COLLATE pg_catalog."default",
    "DosageInMg" double precision NOT NULL,
    "Quantity" integer NOT NULL,
    "Price" double precision NOT NULL,
    "Description" text COLLATE pg_catalog."default",
    "ReplacementMedicineIDs" text[] COLLATE pg_catalog."default",
    CONSTRAINT "PK_Medicines" PRIMARY KEY ("ID")
)

TABLESPACE pg_default;

ALTER TABLE public."Medicines"
    OWNER to postgres;


-- Table: public.Ingredient

-- DROP TABLE public."Ingredient";

CREATE TABLE IF NOT EXISTS public."Ingredient"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Name" text COLLATE pg_catalog."default",
    "MedicineID" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Ingredient" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Ingredient_Medicines_MedicineID" FOREIGN KEY ("MedicineID")
        REFERENCES public."Medicines" ("ID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Ingredient"
    OWNER to postgres;
-- Index: IX_Ingredient_MedicineID

-- DROP INDEX public."IX_Ingredient_MedicineID";

CREATE INDEX "IX_Ingredient_MedicineID"
    ON public."Ingredient" USING btree
    ("MedicineID" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.MedicalRecords

-- DROP TABLE public."MedicalRecords";

CREATE TABLE IF NOT EXISTS public."MedicalRecords"
(
    "MedicalRecordID" text COLLATE pg_catalog."default" NOT NULL,
    "BloodType" integer NOT NULL,
    "AllergenId" integer,
    "PatientId" integer,
    "HealthCardNumber" text COLLATE pg_catalog."default",
    "ParentName" text COLLATE pg_catalog."default",
    "IsInsured" boolean NOT NULL,
    CONSTRAINT "PK_MedicalRecords" PRIMARY KEY ("MedicalRecordID"),
    CONSTRAINT "FK_MedicalRecords_Allergen_AllergenId" FOREIGN KEY ("AllergenId")
        REFERENCES public."Allergen" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_MedicalRecords_Patient_PatientId" FOREIGN KEY ("PatientId")
        REFERENCES public."Patient" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."MedicalRecords"
    OWNER to postgres;
-- Index: IX_MedicalRecords_AllergenId

-- DROP INDEX public."IX_MedicalRecords_AllergenId";

CREATE INDEX "IX_MedicalRecords_AllergenId"
    ON public."MedicalRecords" USING btree
    ("AllergenId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_MedicalRecords_PatientId

-- DROP INDEX public."IX_MedicalRecords_PatientId";

CREATE INDEX "IX_MedicalRecords_PatientId"
    ON public."MedicalRecords" USING btree
    ("PatientId" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.Appointment

-- DROP TABLE public."Appointment";

CREATE TABLE IF NOT EXISTS public."Appointment"
(
    "ID" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "DurationInHours" double precision NOT NULL,
    "DateTime" timestamp without time zone NOT NULL,
    "IDpatient" text COLLATE pg_catalog."default",
    "IDDoctor" text COLLATE pg_catalog."default",
    "IDAppointment" text COLLATE pg_catalog."default",
    "PatientsRecordMedicalRecordID" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Appointment" PRIMARY KEY ("ID"),
    CONSTRAINT "FK_Appointment_MedicalRecords_PatientsRecordMedicalRecordID" FOREIGN KEY ("PatientsRecordMedicalRecordID")
        REFERENCES public."MedicalRecords" ("MedicalRecordID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT
)

TABLESPACE pg_default;

ALTER TABLE public."Appointment"
    OWNER to postgres;
-- Index: IX_Appointment_PatientsRecordMedicalRecordID

-- DROP INDEX public."IX_Appointment_PatientsRecordMedicalRecordID";

CREATE INDEX "IX_Appointment_PatientsRecordMedicalRecordID"
    ON public."Appointment" USING btree
    ("PatientsRecordMedicalRecordID" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;


-- Table: public.Therapies

-- DROP TABLE public."Therapies";

CREATE TABLE IF NOT EXISTS public."Therapies"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "MedicineId" text COLLATE pg_catalog."default" DEFAULT 0,
    CONSTRAINT "PK_Therapies" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Therapies"
    OWNER to postgres;

-- Table: public.Examinations

-- DROP TABLE public."Examinations";

CREATE TABLE IF NOT EXISTS public."Examinations"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "MedicalRecordId" text COLLATE pg_catalog."default",
    "Diagnosis" text COLLATE pg_catalog."default",
    anamnesis text COLLATE pg_catalog."default",
    "appointmentID" integer,
    "dateOfExamination" timestamp without time zone NOT NULL DEFAULT '0001-01-01 00:00:00'::timestamp without time zone,
    "patientVisible" boolean NOT NULL DEFAULT false,
    "TherapyId" integer NOT NULL,
    CONSTRAINT "PK_Examinations" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Examinations_Appointment_appointmentID" FOREIGN KEY ("appointmentID")
        REFERENCES public."Appointment" ("ID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Examinations_MedicalRecords_MedicalRecordId" FOREIGN KEY ("MedicalRecordId")
        REFERENCES public."MedicalRecords" ("MedicalRecordID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_Examinations_Therapies_TherapyId" FOREIGN KEY ("TherapyId")
        REFERENCES public."Therapies" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE public."Examinations"
    OWNER to postgres;
-- Index: IX_Examinations_MedicalRecordId

-- DROP INDEX public."IX_Examinations_MedicalRecordId";

CREATE INDEX "IX_Examinations_MedicalRecordId"
    ON public."Examinations" USING btree
    ("MedicalRecordId" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Examinations_TherapyId

-- DROP INDEX public."IX_Examinations_TherapyId";

CREATE INDEX "IX_Examinations_TherapyId"
    ON public."Examinations" USING btree
    ("TherapyId" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_Examinations_appointmentID

-- DROP INDEX public."IX_Examinations_appointmentID";

CREATE INDEX "IX_Examinations_appointmentID"
    ON public."Examinations" USING btree
    ("appointmentID" ASC NULLS LAST)
    TABLESPACE pg_default;

-- Table: public.MedicineTherapys

-- DROP TABLE public."MedicineTherapys";

CREATE TABLE IF NOT EXISTS public."MedicineTherapys"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "MedicineID" text COLLATE pg_catalog."default",
    "DurationInDays" integer NOT NULL,
    "TimesPerDay" integer NOT NULL,
    "Description" text COLLATE pg_catalog."default",
    "TherapyId" integer NOT NULL,
    CONSTRAINT "PK_MedicineTherapys" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_MedicineTherapys_Medicines_MedicineID" FOREIGN KEY ("MedicineID")
        REFERENCES public."Medicines" ("ID") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE RESTRICT,
    CONSTRAINT "FK_MedicineTherapys_Therapies_TherapyId" FOREIGN KEY ("TherapyId")
        REFERENCES public."Therapies" ("Id") MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE public."MedicineTherapys"
    OWNER to postgres;
-- Index: IX_MedicineTherapys_MedicineID

-- DROP INDEX public."IX_MedicineTherapys_MedicineID";

CREATE INDEX "IX_MedicineTherapys_MedicineID"
    ON public."MedicineTherapys" USING btree
    ("MedicineID" COLLATE pg_catalog."default" ASC NULLS LAST)
    TABLESPACE pg_default;
-- Index: IX_MedicineTherapys_TherapyId

-- DROP INDEX public."IX_MedicineTherapys_TherapyId";

CREATE INDEX "IX_MedicineTherapys_TherapyId"
    ON public."MedicineTherapys" USING btree
    ("TherapyId" ASC NULLS LAST)
    TABLESPACE pg_default;


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
    "Posted" boolean NOT NULL,
    "PharmacyName" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_News" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."News"
    OWNER to postgres;

-- Table: public.Objections

-- DROP TABLE public."Objections";

CREATE TABLE IF NOT EXISTS public."Objections"
(
    "Id" text COLLATE pg_catalog."default" NOT NULL,
    "Content" text COLLATE pg_catalog."default",
    "PharmacyId" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Objections" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Objections"
    OWNER to postgres;


-- Table: public.Pharmacies

-- DROP TABLE public."Pharmacies";

CREATE TABLE IF NOT EXISTS public."Pharmacies"
(
    "Localhost" text COLLATE pg_catalog."default" NOT NULL,
    "Name" text COLLATE pg_catalog."default",
    "Key" text COLLATE pg_catalog."default",
    "Protocol" integer NOT NULL,
    "Address" text COLLATE pg_catalog."default",
    "City" text COLLATE pg_catalog."default",
    "Note" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Pharmacies" PRIMARY KEY ("Localhost")
)

TABLESPACE pg_default;

ALTER TABLE public."Pharmacies"
    OWNER to postgres;

-- Table: public.Replies

-- DROP TABLE public."Replies";

CREATE TABLE IF NOT EXISTS public."Replies"
(
    "Id" text COLLATE pg_catalog."default" NOT NULL,
    "ObjectionId" text COLLATE pg_catalog."default",
    "Content" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_Replies" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE public."Replies"
    OWNER to postgres;



DELETE FROM public."Allergen";
DELETE FROM public."Appointment";
DELETE FROM public."Credentials";
DELETE FROM public."Pharmacies";
DELETE FROM public."Examinations";
DELETE FROM public."Files";
DELETE FROM public."Ingredient";
DELETE FROM public."MedicalRecords";
DELETE FROM public."MedicineTherapys";
DELETE FROM public."Medicines";
DELETE FROM public."News";
DELETE FROM public."Objections";
DELETE FROM public."Patient";
DELETE FROM public."Replies";
DELETE FROM public."Therapies";

INSERT INTO public."Pharmacies"(
	"Localhost", "Name", "Key", "Protocol", "Address", "City", "Note")
	VALUES ('http://localhost:34616/', 'Jankovic', 'OrKFGLDOWKE/ZQMkvIU0bOzhQ30hmuIi5i7GxPrISI0=', 0, 'Svemirska', 'Novi Sad', '');

INSERT INTO public."Credentials"(
	"Id", "PharmacyName", "PharmacyLocalhost", "ApiKey")
	VALUES (1, 'Jankovic', 'http://localhost:34616/', 'KQuO1kJn189mfY30X6M1pony4sBOLPbhs0DQLt7NMeo=');













