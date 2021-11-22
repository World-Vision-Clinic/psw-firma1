﻿// <auto-generated />
using System;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    [DbContext(typeof(HospitalContext))]
    [Migration("20211122225305_QuestionsUpdate")]
    partial class QuestionsUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<bool>("isAnonymous")
                        .HasColumnType("boolean");

                    b.Property<bool>("isPublic")
                        .HasColumnType("boolean");

                    b.Property<bool>("isPublishable")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<string>("BloodType")
                        .HasColumnType("text");

                    b.Property<string>("ContactPhone")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("DoctorName")
                        .HasColumnType("text");

                    b.Property<string>("EMail")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Residence")
                        .HasColumnType("text");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.AnsweredSurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Answer")
                        .HasColumnType("integer");

                    b.Property<int>("PatientForeignKey")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("Section")
                        .HasColumnType("integer");

                    b.Property<int>("SurveyForeignKey")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AnsweredQuestions");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("DoctorForeignKey")
                        .HasColumnType("integer");

                    b.Property<int>("PatientForeignKey")
                        .HasColumnType("integer");

                    b.Property<TimeSpan>("Time")
                        .HasColumnType("interval");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Appointments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DoctorForeignKey = 0,
                            PatientForeignKey = 0,
                            Time = new TimeSpan(0, 0, 0, 0, 0),
                            Type = 0
                        });
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Survey", b =>
                {
                    b.Property<int>("IdSurvey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("IdAppointment")
                        .HasColumnType("integer");

                    b.HasKey("IdSurvey");

                    b.HasIndex("IdAppointment");

                    b.ToTable("Surveys");

                    b.HasData(
                        new
                        {
                            IdSurvey = 1,
                            CreationDate = new DateTime(2021, 11, 22, 23, 53, 5, 49, DateTimeKind.Local).AddTicks(1413),
                            IdAppointment = 1
                        });
                });

            modelBuilder.Entity("Hospital.Schedule.Model.SurveyQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Answer")
                        .HasColumnType("integer");

                    b.Property<int>("IdSurvey")
                        .HasColumnType("integer");

                    b.Property<string>("Question")
                        .HasColumnType("text");

                    b.Property<int>("Section")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Questions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "Has doctor been polite to you?",
                            Section = 1
                        },
                        new
                        {
                            Id = 2,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How would you rate the professionalism of doctor?",
                            Section = 1
                        },
                        new
                        {
                            Id = 3,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How clearly did the doctor explain you your condition?",
                            Section = 1
                        },
                        new
                        {
                            Id = 4,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How would you rate the doctor's patience with you?",
                            Section = 1
                        },
                        new
                        {
                            Id = 5,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "What is your overall satisfaction with doctor?",
                            Section = 1
                        },
                        new
                        {
                            Id = 6,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How easy is to use our application?",
                            Section = 0
                        },
                        new
                        {
                            Id = 7,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How easy it was to schedule an appointment?",
                            Section = 0
                        },
                        new
                        {
                            Id = 8,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "What is an opportunity to recommend us to your friends and family?",
                            Section = 0
                        },
                        new
                        {
                            Id = 9,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How satisfied are you with the services that the hospital provides you?",
                            Section = 0
                        },
                        new
                        {
                            Id = 10,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "What is your overall satisfaction with our hospital?",
                            Section = 0
                        },
                        new
                        {
                            Id = 11,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How would you rate the kindness of our staff?",
                            Section = 2
                        },
                        new
                        {
                            Id = 12,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How would you rate the professionalism of our staff?",
                            Section = 2
                        },
                        new
                        {
                            Id = 13,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How clearly did the staff explain you some procedures of our hospital?",
                            Section = 2
                        },
                        new
                        {
                            Id = 14,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "How yould you rate to what extent staff was available to you during your visit to the hospital?",
                            Section = 2
                        },
                        new
                        {
                            Id = 15,
                            Answer = 0,
                            IdSurvey = 1,
                            Question = "What is your overall satisfaction with our staff?",
                            Section = 2
                        });
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Survey", b =>
                {
                    b.HasOne("Hospital.Schedule.Model.Appointment", "Appointment")
                        .WithMany("Surveys")
                        .HasForeignKey("IdAppointment")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Appointment", b =>
                {
                    b.Navigation("Surveys");
                });
#pragma warning restore 612, 618
        }
    }
}
