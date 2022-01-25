﻿using Hospital.MedicalRecords.Repository;
using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Service;
using Hospital.Seedwork;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Hospital.MedicalRecords.Model
{
    public enum Gender
    {
        Male,
        Female
    }

    public enum BloodType
    {
        A,
        B,
        AB,
        O
    }
    public class Patient : Entity
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public FullName FullName { get; private set; }
        public string EMail { get; private set; }
        public string Token { get; private set; }
        public bool Activated { get; private set; }
        public Gender Gender { get; private set; }
        public string Jmbg { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public Residence Residence { get; private set; }
        public string Phone { get; private set; }
        public int PreferedDoctor { get; private set; }
        public int Weight { get; private set; }
        public int Height { get; private set; }
        public BloodType BloodType { get; private set; }
        public bool IsBlocked { get; private set; }
        public List<Appointment> Appointments { get; set; }
        public string ProfileImage { get; set; }

        public Patient() { }

        public Patient(int id, string username, string password, FullName fullName, string email, bool activated,
            Gender gender, string jmbg, DateTime dateOfBirth, Residence residence, string phone, int preferedDoctor, int weight, int height, BloodType bloodType, bool isBlocked, List<Appointment> appointments, string profileImage)
        {
            this.Id = id;
            this.UserName = username;
            this.Password = password;
            this.FullName = fullName;
            this.EMail = email;
            this.Token = TokenizeSHA256(this.UserName);
            this.Activated = activated;
            this.Gender = gender;
            this.Jmbg = jmbg;
            this.DateOfBirth = dateOfBirth;
            this.Residence = residence;
            this.Phone = phone;
            this.PreferedDoctor = preferedDoctor;
            this.Weight = weight;
            this.Height = height;
            this.BloodType = bloodType;
            this.IsBlocked = isBlocked;
            this.Appointments = appointments;
            this.ProfileImage = profileImage;
        }
        public Patient(string username, string password, FullName fullName, string email, bool activated, 
            Gender gender, string jmbg, DateTime dateOfBirth, Residence residence, string phone, int preferedDoctor, int weight, int height, BloodType bloodType, bool isBlocked, List<Appointment> appointments, string profileImage)
        {
            this.UserName = username;
            this.Password = password;
            this.FullName = fullName;
            this.EMail = email;
            this.Token = TokenizeSHA256(this.UserName);
            this.Activated = activated;
            this.Gender = gender;
            this.Jmbg = jmbg;
            this.DateOfBirth = dateOfBirth;
            this.Residence = residence;
            this.Phone = phone;
            this.PreferedDoctor = preferedDoctor;
            this.Weight = weight;
            this.Height = height;
            this.BloodType = bloodType;
            this.IsBlocked = isBlocked;
            this.Appointments = appointments;
            this.ProfileImage = profileImage;
        }

        public bool AddAppointment(Appointment appointment)
        {
            if (!Activated || appointment.Date < DateTime.Now)
                return false;

            appointment.PatientForeignKey = Id;
            appointment.Length = new TimeSpan(0, 30, 0);
            
            AppointmentService _appointmentService = new AppointmentService(new AppointmentRepository(new SharedModel.HospitalContext()), new DoctorRepository(new SharedModel.HospitalContext()));
            if (_appointmentService.GetByDateAndDoctor(appointment.Date, appointment.Length, appointment.DoctorForeignKey) != null)
                return false;

            if (Appointments == null)
                Appointments = new List<Appointment>();
            Appointments.Add(appointment);

            PatientRepository patientRepository = new PatientRepository();
            patientRepository.SaveSync();

            return true;
        }

        public static string TokenizeSHA256(string username)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(username));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
