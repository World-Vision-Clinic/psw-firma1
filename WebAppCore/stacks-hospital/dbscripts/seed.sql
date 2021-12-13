\connect MyWebApi.Dev

-- Table: public.Questions-- DROP TABLE public."Questions";CREATE TABLE IF NOT EXISTS public."Questions"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "Answer" integer NOT NULL,    "IdSurvey" integer NOT NULL,    "Question" text COLLATE pg_catalog."default",    "Section" integer NOT NULL,    CONSTRAINT "PK_Questions" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."Questions"    OWNER to admin;

-- Table: public.Allergens-- DROP TABLE public."Allergens";CREATE TABLE IF NOT EXISTS public."Allergens"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "Name" text COLLATE pg_catalog."default",    CONSTRAINT "PK_Allergens" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."Allergens"    OWNER to admin;

-- Table: public.Doctors-- DROP TABLE public."Doctors";CREATE TABLE IF NOT EXISTS public."Doctors"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "FirstName" text COLLATE pg_catalog."default",    "LastName" text COLLATE pg_catalog."default",    "Type" integer NOT NULL,    CONSTRAINT "PK_Doctors" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."Doctors"    OWNER to admin;

-- Table: public.Patients-- DROP TABLE public."Patients";CREATE TABLE IF NOT EXISTS public."Patients"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "UserName" text COLLATE pg_catalog."default",    "Password" text COLLATE pg_catalog."default",    "FirstName" text COLLATE pg_catalog."default",    "LastName" text COLLATE pg_catalog."default",    "EMail" text COLLATE pg_catalog."default",    "Token" text COLLATE pg_catalog."default",    "Activated" boolean NOT NULL,    "Gender" integer NOT NULL,    "Jmbg" text COLLATE pg_catalog."default",    "DateOfBirth" timestamp without time zone NOT NULL,    "Country" text COLLATE pg_catalog."default",    "Address" text COLLATE pg_catalog."default",    "City" text COLLATE pg_catalog."default",    "Phone" text COLLATE pg_catalog."default",    "PreferedDoctor" integer NOT NULL,    "Weight" integer NOT NULL,    "Height" integer NOT NULL,    "BloodType" integer NOT NULL,    CONSTRAINT "PK_Patients" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."Patients"    OWNER to admin;

-- Table: public.PatientAllergens-- DROP TABLE public."PatientAllergens";CREATE TABLE IF NOT EXISTS public."PatientAllergens"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "PatientId" integer NOT NULL,    "AllergenId" integer NOT NULL,    CONSTRAINT "PK_PatientAllergens" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."PatientAllergens"    OWNER to admin;

-- Table: public.Appointments-- DROP TABLE public."Appointments";CREATE TABLE IF NOT EXISTS public."Appointments"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "PatientForeignKey" integer NOT NULL,    "DoctorForeignKey" integer NOT NULL,    "Type" integer NOT NULL,    "Date" timestamp without time zone NOT NULL,    "Time" interval NOT NULL,    "IsCancelled" boolean NOT NULL,    CONSTRAINT "PK_Appointments" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."Appointments"    OWNER to admin;

-- Table: public.Surveys-- DROP TABLE public."Surveys";CREATE TABLE IF NOT EXISTS public."Surveys"(    "IdSurvey" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "CreationDate" timestamp without time zone NOT NULL,    "IdAppointment" integer NOT NULL,    CONSTRAINT "PK_Surveys" PRIMARY KEY ("IdSurvey"),    CONSTRAINT "FK_Surveys_Appointments_IdAppointment" FOREIGN KEY ("IdAppointment")        REFERENCES public."Appointments" ("Id") MATCH SIMPLE        ON UPDATE NO ACTION        ON DELETE CASCADE)TABLESPACE pg_default;ALTER TABLE public."Surveys"    OWNER to admin;-- Index: IX_Surveys_IdAppointment-- DROP INDEX public."IX_Surveys_IdAppointment";CREATE INDEX "IX_Surveys_IdAppointment"    ON public."Surveys" USING btree    ("IdAppointment" ASC NULLS LAST)    TABLESPACE pg_default;

-- Table: public.AnsweredQuestions-- DROP TABLE public."AnsweredQuestions";CREATE TABLE IF NOT EXISTS public."AnsweredQuestions"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "SurveyForeignKey" integer NOT NULL,    "PatientForeignKey" integer NOT NULL,    "Question" text COLLATE pg_catalog."default",    "Section" integer NOT NULL,    "Answer" integer NOT NULL,    CONSTRAINT "PK_AnsweredQuestions" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."AnsweredQuestions"    OWNER to admin;

-- Table: public.Feedbacks-- DROP TABLE public."Feedbacks";CREATE TABLE IF NOT EXISTS public."Feedbacks"(    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),    "Content" text COLLATE pg_catalog."default",    "IsPublic" boolean NOT NULL,    "IsPublishable" boolean NOT NULL,    "IsAnonymous" boolean NOT NULL,    "Date" timestamp without time zone NOT NULL,    "UserName" text COLLATE pg_catalog."default",    CONSTRAINT "PK_Feedbacks" PRIMARY KEY ("Id"))TABLESPACE pg_default;ALTER TABLE public."Feedbacks"    OWNER to admin;


INSERT INTO public."Allergens" ("Id", "Name") VALUES (1, 'Allergen1');
INSERT INTO public."Allergens" ("Id", "Name") VALUES (2, 'Allergen2');
INSERT INTO public."Allergens" ("Id", "Name") VALUES (3, 'Allergen3');

INSERT INTO public."Doctors" ("Id", "FirstName", "LastName", "Type") VALUES (1, 'Marko', 'Oftalmologijević', 0);
INSERT INTO public."Doctors" ("Id", "FirstName", "LastName", "Type") VALUES (2, 'Petar', 'Kardiologijević', 1);
INSERT INTO public."Doctors" ("Id", "FirstName", "LastName", "Type") VALUES (3, 'Branislav', 'Radiologijević', 2);

INSERT INTO public."Patients" ("Id", "UserName", "Password", "EMail", "Token", "Activated", "DateOfBirth", "Gender", "Phone", "Jmbg", "LastName", "Height", "Weight", "BloodType", "Address", "City", "Country", "FirstName", "PreferedDoctor") VALUES (1, 'branko1', 'baki123', 'woxana2128@nefacility.com', 'eaf224c52496961284dc041901a3610d06cd6c0b563302aa70b9d29a04d87d08', true, '2000-11-01 00:00:00', 0, '063123123', '0111000800012', 'Branković', 182, 75, 0, 'Nikole Tesle 16', 'Novi Sad', 'Serbia', 'Branko', 1);
INSERT INTO public."Patients" ("Id", "UserName", "Password", "EMail", "Token", "Activated", "DateOfBirth", "Gender", "Phone", "Jmbg", "LastName", "Height", "Weight", "BloodType", "Address", "City", "Country", "FirstName", "PreferedDoctor") VALUES (2, 'petar', 'petar12345', 'peraperic@gmail.com', 'eaf224c52496fdfdfdc041901a3610d06cd6c0b563302aa70b9d29a04d87d08', false, '1977-07-03 00:00:00', 0, '063123456', '0111077800012', 'Petrović', 190, 100, 1, 'Bulevar Evrope 12', 'Novi Sad', 'Serbia', 'Petar', 2);

INSERT INTO public."PatientAllergens" ("Id", "PatientId", "AllergenId") VALUES (2, 1, 2);
INSERT INTO public."PatientAllergens" ("Id", "PatientId", "AllergenId") VALUES (3, 2, 1);
INSERT INTO public."PatientAllergens" ("Id", "PatientId", "AllergenId") VALUES (4, 2, 2);
INSERT INTO public."PatientAllergens" ("Id", "PatientId", "AllergenId") VALUES (1, 1, 1);

INSERT INTO public."Appointments" ("Id", "PatientForeignKey", "DoctorForeignKey", "Type", "Date", "Time", "IsCancelled") VALUES (1, 1, 1, 0, '2021-12-17 10:00:00', '00:00:00', false);
INSERT INTO public."Appointments" ("Id", "PatientForeignKey", "DoctorForeignKey", "Type", "Date", "Time", "IsCancelled") VALUES (2, 1, 2, 0, '2021-12-14 12:00:00', '00:00:00', false);
INSERT INTO public."Appointments" ("Id", "PatientForeignKey", "DoctorForeignKey", "Type", "Date", "Time", "IsCancelled") VALUES (3, 1, 1, 0, '2021-12-10 10:30:00', '00:00:00', false);
INSERT INTO public."Appointments" ("Id", "PatientForeignKey", "DoctorForeignKey", "Type", "Date", "Time", "IsCancelled") VALUES (4, 1, 1, 0, '2021-10-15 13:00:00', '00:00:00', false);

INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (1, 0, 1, 'Has doctor been polite to you?', 1);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (2, 0, 1, 'How would you rate the professionalism of doctor?', 1);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (3, 0, 1, 'How clearly did the doctor explain you your condition?', 1);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (4, 0, 1, 'How would you rate the doctor''s patience with you?', 1);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (5, 0, 1, 'What is your overall satisfaction with doctor?', 1);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (6, 0, 1, 'How easy is to use our application?', 0);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (7, 0, 1, 'How easy it was to schedule an appointment?', 0);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (8, 0, 1, 'What is an opportunity to recommend us to your friends and family?', 0);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (9, 0, 1, 'How satisfied are you with the services that the hospital provides you?', 0);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (10, 0, 1, 'What is your overall satisfaction with our hospital?', 0);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (11, 0, 1, 'How would you rate the kindness of our staff?', 2);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (12, 0, 1, 'How would you rate the professionalism of our staff?', 2);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (13, 0, 1, 'How clearly did the staff explain you some procedures of our hospital?', 2);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (14, 0, 1, 'How yould you rate to what extent staff was available to you during your visit to the hospital?', 2);
INSERT INTO public."Questions" ("Id", "Answer", "IdSurvey", "Question", "Section") VALUES (15, 0, 1, 'What is your overall satisfaction with our staff?', 2);

INSERT INTO public."Surveys" ("IdSurvey", "CreationDate", "IdAppointment") VALUES (14, '2021-12-12 22:55:39.342484', 3);
INSERT INTO public."Surveys" ("IdSurvey", "CreationDate", "IdAppointment") VALUES (15, '2021-12-12 22:56:57.025916', 4);

INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (181, 14, 1, 'Has doctor been polite to you?', 1, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (182, 14, 1, 'How would you rate the professionalism of doctor?', 1, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (183, 14, 1, 'How clearly did the doctor explain you your condition?', 1, 3);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (184, 14, 1, 'How would you rate the doctor''s patience with you?', 1, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (185, 14, 1, 'What is your overall satisfaction with doctor?', 1, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (186, 14, 1, 'How easy is to use our application?', 0, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (187, 14, 1, 'How easy it was to schedule an appointment?', 0, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (188, 14, 1, 'What is an opportunity to recommend us to your friends and family?', 0, 2);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (189, 14, 1, 'How satisfied are you with the services that the hospital provides you?', 0, 3);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (190, 14, 1, 'What is your overall satisfaction with our hospital?', 0, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (191, 14, 1, 'How would you rate the kindness of our staff?', 2, 2);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (192, 14, 1, 'How would you rate the professionalism of our staff?', 2, 2);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (193, 14, 1, 'How clearly did the staff explain you some procedures of our hospital?', 2, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (194, 14, 1, 'How yould you rate to what extent staff was available to you during your visit to the hospital?', 2, 3);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (195, 14, 1, 'What is your overall satisfaction with our staff?', 2, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (196, 15, 1, 'Has doctor been polite to you?', 1, 2);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (197, 15, 1, 'How would you rate the professionalism of doctor?', 1, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (198, 15, 1, 'How clearly did the doctor explain you your condition?', 1, 1);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (199, 15, 1, 'How would you rate the doctor''s patience with you?', 1, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (200, 15, 1, 'What is your overall satisfaction with doctor?', 1, 2);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (201, 15, 1, 'How easy is to use our application?', 0, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (202, 15, 1, 'How easy it was to schedule an appointment?', 0, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (203, 15, 1, 'What is an opportunity to recommend us to your friends and family?', 0, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (204, 15, 1, 'How satisfied are you with the services that the hospital provides you?', 0, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (205, 15, 1, 'What is your overall satisfaction with our hospital?', 0, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (206, 15, 1, 'How would you rate the kindness of our staff?', 2, 2);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (207, 15, 1, 'How would you rate the professionalism of our staff?', 2, 3);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (208, 15, 1, 'How clearly did the staff explain you some procedures of our hospital?', 2, 4);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (209, 15, 1, 'How yould you rate to what extent staff was available to you during your visit to the hospital?', 2, 5);
INSERT INTO public."AnsweredQuestions" ("Id", "SurveyForeignKey", "PatientForeignKey", "Question", "Section", "Answer") VALUES (210, 15, 1, 'What is your overall satisfaction with our staff?', 2, 2);

INSERT INTO public."Feedbacks" ("Id", "Content", "IsPublic", "IsPublishable", "IsAnonymous", "Date", "UserName") VALUES (1, 'Test feedback 1', true, true, false, '2021-11-24 19:18:28.036', 'Default User');
INSERT INTO public."Feedbacks" ("Id", "Content", "IsPublic", "IsPublishable", "IsAnonymous", "Date", "UserName") VALUES (2, 'Test feedback 2', false, false, false, '2021-11-24 19:18:39.337', 'Default User');

