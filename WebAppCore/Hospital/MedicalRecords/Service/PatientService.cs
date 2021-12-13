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


        public void AddPatient()
        {
            Patient newPatient = new Patient();
            newPatient.Activated = false;
            newPatient.Password = "123";
            newPatient.UserName = "ajajajajja";
            newPatient.EMail = "kedosok152@funboxcn.com";
            newPatient.Token = TokenizeSHA256(newPatient.UserName);
            _repo.AddPatient(newPatient);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                SendEmail(newPatient);
            }).Start();

        }

        public void RegisterPatient(Patient patient)
        {
            if (FindByUserName(patient.UserName) != null)
                return;

            patient.Activated = false;
            patient.Token = TokenizeSHA256(patient.UserName);
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

        public bool Block(Patient patient)
        {
            if (!IsBlockable(patient))
                return false;
            patient.IsBlocked = true;
            _repo.Modify(patient);
            SaveSync();
            return true;
        }

        public string TokenizeSHA256(string username) {  
  
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

        public Patient FindByToken(string token)
        {
            return _repo.FindByToken(token);
        }

        public Patient FindById(int id)
        {
            return _repo.FindById(id);
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
            patient.Activated = true;
            _repo.Modify(patient);
            SaveSync();
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }

    }
}
