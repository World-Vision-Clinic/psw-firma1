using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;
using System.Threading;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Model;

namespace Hospital.MedicalRecords.Service
{
    public class PatientService
    {

        private readonly IPatientRepository _repo;

        public PatientService(IPatientRepository repo)
        {
            _repo = repo;
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
