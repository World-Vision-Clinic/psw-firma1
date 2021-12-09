using System;
using System.Collections.Generic;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Hospital.Migrations
{
    [DbContext(typeof(HospitalContext))]
    partial class HospitalContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.Area", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.Building", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("Areaid")
                        .HasColumnType("integer");

                    b.Property<string>("Info")
                        .HasColumnType("text");

                    b.Property<int>("MapPositionId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.HasIndex("Areaid");

                    b.ToTable("Buildings");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Info = "Gynecology",
                            MapPositionId = 1,
                            Name = "Hospital I"
                        },
                        new
                        {
                            id = 2,
                            Info = "",
                            MapPositionId = 2,
                            Name = "Hospital II"
                        });
                });

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.Floor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("BuildingId")
                        .HasColumnType("integer");

                    b.Property<string>("Info")
                        .HasColumnType("text");

                    b.Property<string>("Level")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Floors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BuildingId = 1,
                            Level = "Ground floor"
                        },
                        new
                        {
                            Id = 2,
                            BuildingId = 1,
                            Level = "First floor"
                        });
                });

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.FloorLabel", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("FloorId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .HasColumnType("text");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("FloorLabels");

                    b.HasData(
                        new
                        {
                            id = 1,
                            FloorId = 1,
                            Text = "ENTRANCE",
                            X = 50,
                            Y = 80
                        });
                });

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.MapPosition", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("MapPositions");

                    b.HasData(
                        new
                        {
                            id = 1,
                            Height = 180,
                            Width = 520,
                            X = 30,
                            Y = 20
                        },
                        new
                        {
                            id = 2,
                            Height = 180,
                            Width = 520,
                            X = 30,
                            Y = 460
                        });
                });

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.OutsideDoor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("IsVertical")
                        .HasColumnType("boolean");

                    b.Property<int>("MapPositionId")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("OutsideDoors");

                    b.HasData(
                        new
                        {
                            id = 1,
                            IsVertical = true,
                            MapPositionId = 1,
                            X = 545,
                            Y = 80
                        },
                        new
                        {
                            id = 2,
                            IsVertical = false,
                            MapPositionId = 1,
                            X = 260,
                            Y = 195
                        },
                        new
                        {
                            id = 3,
                            IsVertical = true,
                            MapPositionId = 2,
                            X = 545,
                            Y = 505
                        },
                        new
                        {
                            id = 4,
                            IsVertical = false,
                            MapPositionId = 2,
                            X = 260,
                            Y = 455
                        });
                });

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.Parking", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.ToTable("Parkings");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Allergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<List<string>>("IngredientNames")
                        .HasColumnType("text[]");

                    b.Property<List<string>>("MedicineNames")
                        .HasColumnType("text[]");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Doctor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Examination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Diagnosis")
                        .HasColumnType("text");

                    b.Property<string>("MedicalRecordId")
                        .HasColumnType("text");

                    b.Property<int>("TherapyId")
                        .HasColumnType("integer");

                    b.Property<string>("anamnesis")
                        .HasColumnType("text");

                    b.Property<int?>("appointmentId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("dateOfExamination")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("patientVisible")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("MedicalRecordId");

                    b.HasIndex("TherapyId");

                    b.HasIndex("appointmentId");

                    b.ToTable("Examinations");
                });

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

                    b.Property<bool>("IsAnonymous")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublishable")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("MedicineID")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("MedicineID");

                    b.ToTable("Ingredient");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.MedicalRecord", b =>
                {
                    b.Property<string>("MedicalRecordID")
                        .HasColumnType("text");

                    b.Property<int?>("AllergenId")
                        .HasColumnType("integer");

                    b.Property<string>("HealthCardNumber")
                        .HasColumnType("text");

                    b.Property<bool>("IsInsured")
                        .HasColumnType("boolean");

                    b.Property<string>("ParentName")
                        .HasColumnType("text");

                    b.Property<int?>("PatientId")
                        .HasColumnType("integer");

                    b.HasKey("MedicalRecordID");

                    b.HasIndex("AllergenId");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalRecords");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Medicine", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<double>("DosageInMg")
                        .HasColumnType("double precision");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.Property<List<string>>("ReplacementMedicineIDs")
                        .HasColumnType("text[]");

                    b.HasKey("ID");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.MedicineTherapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("DurationInDays")
                        .HasColumnType("integer");

                    b.Property<string>("MedicineID")
                        .HasColumnType("text");

                    b.Property<int>("TherapyId")
                        .HasColumnType("integer");

                    b.Property<int>("TimesPerDay")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("MedicineID");

                    b.HasIndex("TherapyId");

                    b.ToTable("MedicineTherapy");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("Activated")
                        .HasColumnType("boolean");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("BloodType")
                        .HasColumnType("integer");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EMail")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Jmbg")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("PreferedDoctor")
                        .HasColumnType("integer");

                    b.Property<string>("Token")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.Property<int>("Weight")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.PatientAllergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AllergenId")
                        .HasColumnType("integer");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("PatientAllergens");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Therapy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("MedicineId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Therapies");
                });

            modelBuilder.Entity("Hospital.RoomsAndEquipment.Model.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<bool>("InTransport")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TransportEnd")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TransportStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("AllEquipment");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 15,
                            InTransport = false,
                            Name = "Bandage",
                            RoomId = 15,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 3,
                            InTransport = false,
                            Name = "Operating table",
                            RoomId = 23,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            Amount = 11,
                            InTransport = false,
                            Name = "Infusion",
                            RoomId = 1,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 17,
                            InTransport = false,
                            Name = "Bandage",
                            RoomId = 2,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        },
                        new
                        {
                            Id = 5,
                            Amount = 2,
                            InTransport = false,
                            Name = "Operating table",
                            RoomId = 2,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 6,
                            Amount = 23,
                            InTransport = false,
                            Name = "Infusion",
                            RoomId = 23,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        },
                        new
                        {
                            Id = 7,
                            Amount = 15,
                            InTransport = false,
                            Name = "Bandage",
                            RoomId = 3,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        },
                        new
                        {
                            Id = 8,
                            Amount = 1,
                            InTransport = false,
                            Name = "Operating table",
                            RoomId = 3,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 9,
                            Amount = 11,
                            InTransport = false,
                            Name = "Syringe",
                            RoomId = 3,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        },
                        new
                        {
                            Id = 10,
                            Amount = 7,
                            InTransport = false,
                            Name = "Bed",
                            RoomId = 4,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 11,
                            Amount = 4,
                            InTransport = false,
                            Name = "Chair",
                            RoomId = 16,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 12,
                            Amount = 11,
                            InTransport = false,
                            Name = "Bed",
                            RoomId = 5,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 13,
                            Amount = 6,
                            InTransport = false,
                            Name = "Chair",
                            RoomId = 17,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 0
                        },
                        new
                        {
                            Id = 14,
                            Amount = 25,
                            InTransport = false,
                            Name = "Bandage",
                            RoomId = 5,
                            TransportEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            TransportStart = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Type = 1
                        });
                });

            modelBuilder.Entity("Hospital.RoomsAndEquipment.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Css")
                        .HasColumnType("text");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<bool>("DoorExist")
                        .HasColumnType("boolean");

                    b.Property<int>("DoorX")
                        .HasColumnType("integer");

                    b.Property<int>("DoorY")
                        .HasColumnType("integer");

                    b.Property<int>("FloorId")
                        .HasColumnType("integer");

                    b.Property<int>("Height")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Purpose")
                        .HasColumnType("text");

                    b.Property<bool>("Vertical")
                        .HasColumnType("boolean");

                    b.Property<int>("Width")
                        .HasColumnType("integer");

                    b.Property<int>("X")
                        .HasColumnType("integer");

                    b.Property<int>("Y")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 148,
                            DoorY = 285,
                            FloorId = 1,
                            Height = 190,
                            Name = "OPERATING ROOM 1",
                            Purpose = "",
                            Vertical = true,
                            Width = 150,
                            X = 0,
                            Y = 150
                        },
                        new
                        {
                            Id = 2,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 220,
                            DoorY = 248,
                            FloorId = 1,
                            Height = 100,
                            Name = "OPERATING ROOM 2",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 160,
                            Y = 150
                        },
                        new
                        {
                            Id = 3,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 370,
                            DoorY = 248,
                            FloorId = 1,
                            Height = 100,
                            Name = "OPERATING ROOM 3",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 320,
                            Y = 150
                        },
                        new
                        {
                            Id = 4,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 520,
                            DoorY = 248,
                            FloorId = 1,
                            Height = 100,
                            Name = "ROOM 1",
                            Purpose = "",
                            Vertical = false,
                            Width = 170,
                            X = 480,
                            Y = 150
                        },
                        new
                        {
                            Id = 5,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 680,
                            DoorY = 248,
                            FloorId = 1,
                            Height = 100,
                            Name = "ROOM 2",
                            Purpose = "",
                            Vertical = false,
                            Width = 180,
                            X = 660,
                            Y = 150
                        },
                        new
                        {
                            Id = 6,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 728,
                            DoorY = 290,
                            FloorId = 1,
                            Height = 100,
                            Name = "OFFICE 1",
                            Purpose = "",
                            Vertical = true,
                            Width = 110,
                            X = 730,
                            Y = 260
                        },
                        new
                        {
                            Id = 7,
                            Css = "staircase",
                            DoctorId = -1,
                            DoorExist = false,
                            DoorX = 728,
                            DoorY = 290,
                            FloorId = 1,
                            Height = 90,
                            Name = "LIFT",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 690,
                            Y = 370
                        },
                        new
                        {
                            Id = 8,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 728,
                            DoorY = 485,
                            FloorId = 1,
                            Height = 60,
                            Name = "TOILET",
                            Purpose = "",
                            Vertical = true,
                            Width = 110,
                            X = 730,
                            Y = 470
                        },
                        new
                        {
                            Id = 9,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 728,
                            DoorY = 555,
                            FloorId = 1,
                            Height = 60,
                            Name = "TOILET",
                            Purpose = "",
                            Vertical = true,
                            Width = 110,
                            X = 730,
                            Y = 540
                        },
                        new
                        {
                            Id = 10,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 148,
                            DoorY = 419,
                            FloorId = 1,
                            Height = 190,
                            Name = "OPERATING ROOM 4",
                            Purpose = "",
                            Vertical = true,
                            Width = 150,
                            X = 0,
                            Y = 410
                        },
                        new
                        {
                            Id = 11,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 200,
                            DoorY = 498,
                            FloorId = 1,
                            Height = 100,
                            Name = "ROOM 3",
                            Purpose = "",
                            Vertical = false,
                            Width = 100,
                            X = 160,
                            Y = 500
                        },
                        new
                        {
                            Id = 12,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 315,
                            DoorY = 498,
                            FloorId = 1,
                            Height = 100,
                            Name = "ROOM 4",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 270,
                            Y = 500
                        },
                        new
                        {
                            Id = 13,
                            Css = "room room-cadetblue",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 475,
                            DoorY = 498,
                            FloorId = 1,
                            Height = 100,
                            Name = "ROOM 5",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 430,
                            Y = 500
                        },
                        new
                        {
                            Id = 14,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 100,
                            DoorY = 248,
                            FloorId = 2,
                            Height = 100,
                            Name = "DOCTOR'S OFFICE 1",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 0,
                            Y = 150
                        },
                        new
                        {
                            Id = 15,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 260,
                            DoorY = 248,
                            FloorId = 2,
                            Height = 100,
                            Name = "DOCTOR'S OFFICE 2",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 160,
                            Y = 150
                        },
                        new
                        {
                            Id = 16,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 420,
                            DoorY = 248,
                            FloorId = 2,
                            Height = 100,
                            Name = "DOCTOR'S OFFICE 3",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 320,
                            Y = 150
                        },
                        new
                        {
                            Id = 17,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 595,
                            DoorY = 248,
                            FloorId = 2,
                            Height = 100,
                            Name = "DOCTOR'S OFFICE 4",
                            Purpose = "",
                            Vertical = false,
                            Width = 170,
                            X = 480,
                            Y = 150
                        },
                        new
                        {
                            Id = 18,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 680,
                            DoorY = 248,
                            FloorId = 2,
                            Height = 100,
                            Name = "ROOM 1",
                            Purpose = "",
                            Vertical = false,
                            Width = 180,
                            X = 660,
                            Y = 150
                        },
                        new
                        {
                            Id = 19,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = false,
                            DoorX = 728,
                            DoorY = 290,
                            FloorId = 2,
                            Height = 104,
                            Name = "STAIRS",
                            Purpose = "",
                            Vertical = true,
                            Width = 70,
                            X = 770,
                            Y = 258
                        },
                        new
                        {
                            Id = 20,
                            Css = "staircase",
                            DoctorId = -1,
                            DoorExist = false,
                            DoorX = 728,
                            DoorY = 290,
                            FloorId = 2,
                            Height = 90,
                            Name = "LIFT",
                            Purpose = "",
                            Vertical = false,
                            Width = 150,
                            X = 690,
                            Y = 370
                        },
                        new
                        {
                            Id = 21,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 728,
                            DoorY = 485,
                            FloorId = 2,
                            Height = 60,
                            Name = "TOILET",
                            Purpose = "",
                            Vertical = true,
                            Width = 110,
                            X = 730,
                            Y = 470
                        },
                        new
                        {
                            Id = 22,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 728,
                            DoorY = 555,
                            FloorId = 2,
                            Height = 60,
                            Name = "TOILET",
                            Purpose = "",
                            Vertical = true,
                            Width = 110,
                            X = 730,
                            Y = 540
                        },
                        new
                        {
                            Id = 23,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 148,
                            DoorY = 419,
                            FloorId = 2,
                            Height = 190,
                            Name = "OPERATING ROOM 2",
                            Purpose = "",
                            Vertical = true,
                            Width = 150,
                            X = 0,
                            Y = 410
                        },
                        new
                        {
                            Id = 24,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 200,
                            DoorY = 498,
                            FloorId = 2,
                            Height = 100,
                            Name = "ROOM 2",
                            Purpose = "",
                            Vertical = false,
                            Width = 100,
                            X = 160,
                            Y = 500
                        },
                        new
                        {
                            Id = 25,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 350,
                            DoorY = 498,
                            FloorId = 2,
                            Height = 100,
                            Name = "OPERATING ROOM 3",
                            Purpose = "",
                            Vertical = false,
                            Width = 300,
                            X = 270,
                            Y = 500
                        },
                        new
                        {
                            Id = 26,
                            Css = "room",
                            DoctorId = -1,
                            DoorExist = true,
                            DoorX = 400,
                            DoorY = 398,
                            FloorId = 2,
                            Height = 100,
                            Name = "OPERATING ROOM 1",
                            Purpose = "",
                            Vertical = false,
                            Width = 580,
                            X = 0,
                            Y = 300
                        });
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
                            CreationDate = new DateTime(2021, 11, 30, 20, 18, 8, 180, DateTimeKind.Local).AddTicks(3591),
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

            modelBuilder.Entity("Hospital.GraphicalEditor.Model.Building", b =>
                {
                    b.HasOne("Hospital.GraphicalEditor.Model.Area", "Area")
                        .WithMany()
                        .HasForeignKey("Areaid");

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Examination", b =>
                {
                    b.HasOne("Hospital.MedicalRecords.Model.MedicalRecord", null)
                        .WithMany("Examination")
                        .HasForeignKey("MedicalRecordId");

                    b.HasOne("Hospital.MedicalRecords.Model.Therapy", "therapy")
                        .WithMany()
                        .HasForeignKey("TherapyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hospital.Schedule.Model.Appointment", "appointment")
                        .WithMany()
                        .HasForeignKey("appointmentId");

                    b.Navigation("appointment");

                    b.Navigation("therapy");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Ingredient", b =>
                {
                    b.HasOne("Hospital.MedicalRecords.Model.Medicine", null)
                        .WithMany("Ingredient")
                        .HasForeignKey("MedicineID");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.MedicalRecord", b =>
                {
                    b.HasOne("Hospital.MedicalRecords.Model.Allergen", "Allergen")
                        .WithMany()
                        .HasForeignKey("AllergenId");

                    b.HasOne("Hospital.MedicalRecords.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId");

                    b.Navigation("Allergen");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.MedicineTherapy", b =>
                {
                    b.HasOne("Hospital.MedicalRecords.Model.Medicine", "Medicine")
                        .WithMany()
                        .HasForeignKey("MedicineID");

                    b.HasOne("Hospital.MedicalRecords.Model.Therapy", null)
                        .WithMany("Medicine")
                        .HasForeignKey("TherapyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Medicine");
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

            modelBuilder.Entity("Hospital.MedicalRecords.Model.MedicalRecord", b =>
                {
                    b.Navigation("Examination");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Medicine", b =>
                {
                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("Hospital.MedicalRecords.Model.Therapy", b =>
                {
                    b.Navigation("Medicine");
                });

            modelBuilder.Entity("Hospital.Schedule.Model.Appointment", b =>
                {
                    b.Navigation("Surveys");
                });
#pragma warning restore 612, 618
        }
    }
}