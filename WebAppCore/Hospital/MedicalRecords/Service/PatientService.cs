using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Threading;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Model;

namespace Hospital.MedicalRecords.Service
{
    public class PatientService
    {

        private readonly IPatientRepository _repo;
        private readonly IAppointmentRepository _appointmentRepository;

        public PatientService(IPatientRepository repo, IAppointmentRepository appointmentRepository)
        {
            _repo = repo;
            _appointmentRepository = appointmentRepository;
        }

        public Patient LoginPatient(string username, string password)
        {
            Patient patient = _repo.FindActivatedByUserName(username);
            if (patient != null)
            {
                if (patient.Password.Equals(password))
                    return patient;
                else
                    return null;
            }
            else
                return null;
        }

        public void RegisterPatient(Patient patient)
        {
            if (FindByUserName(patient.UserName) != null)
                return;

            _repo.AddPatient(patient);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                SendEmail(patient);
            }).Start();
        }

        public bool IsBlockable(Patient patient)
        {
            List<Appointment> appointments = _appointmentRepository.GetByPatientId(patient.Id);
            int range = 30;
            int treshold = 3;
            DateTime startDate = DateTime.Now.AddDays(-range);
            int counter = 0;
            foreach (Appointment a in appointments)
            {
                if (a.Date > startDate && a.IsCancelled)
                    counter++;
            }
            return counter >= treshold;
        }

        public List<Patient> GetMaliciousPatients()
        {
            List<Patient> malicious = new List<Patient>();
            List<Patient> patients = _repo.GetAll();
            foreach (Patient p in patients)
            {
                if (IsBlockable(p))
                    malicious.Add(p);
            }
            return malicious;
        }

        public bool Block(Patient patient)
        {
            if (!IsBlockable(patient))
                return false;
            patient = new Patient(patient.Id, patient.UserName, patient.Password, patient.FullName, patient.EMail, patient.Activated, patient.Gender,
                patient.Jmbg, patient.DateOfBirth, patient.Residence, patient.Phone, patient.PreferedDoctor, patient.Weight, patient.Height, patient.BloodType, true, patient.Appointments);
            _repo.Modify(patient);
            SaveSync();
            return true;
        }

        public Patient FindByToken(string token)
        {
            return _repo.FindByToken(token);
        }

        public Patient FindById(int id)
        {
            return _repo.FindById(id);
        }

        public List<Patient> GetAll()
        {
            return _repo.GetAll();
        }
        
        public Patient FindByUserName(string username)
        {
            return _repo.FindByUserName(username);
        }

        public Patient FindByEmail(string email)
        {
            return _repo.FindByEmail(email);
        }

        public void SendEmail(Patient patient) {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("PatientServicePSWFirma1@gmail.com", "PSW!1234"),
                EnableSsl = true,
            };

            smtpClient.Send("PatientServicePSWFirma1@gmail.com", patient.EMail, "Account activation", "To activate your account please click the link below: \n\n" +
                "http://localhost:39901/api/Patients/activate?token=" + patient.Token);
        }

        public void Activate(Patient patient) {
            patient = new Patient(patient.Id, patient.UserName, patient.Password, patient.FullName, patient.EMail, true, patient.Gender,
                patient.Jmbg, patient.DateOfBirth, patient.Residence, patient.Phone, patient.PreferedDoctor, patient.Weight, patient.Height, patient.BloodType, patient.IsBlocked, patient.Appointments);
            _repo.Modify(patient);
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }

    }
}
