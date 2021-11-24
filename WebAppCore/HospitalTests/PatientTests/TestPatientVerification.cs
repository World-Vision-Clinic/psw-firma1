using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital_API.Verification;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;

namespace HospitalTests.PatientTests
{
    public class TestPatientVerification
    {
        PatientVerification _verification;

        private void setupVerification()
        {
            var stubPatientRepository = new Mock<IPatientRepository>();
            Patient nullPatient = null;
            Patient truePatient = new Patient();
            truePatient.UserName = "branko1";
            stubPatientRepository.Setup(m => m.FindByUserName("branko")).Returns(nullPatient);
            stubPatientRepository.Setup(m => m.FindByUserName(truePatient.UserName)).Returns(truePatient);

            var stubDoctorRepository = new Mock<IDoctorRepository>();
            List<Doctor> doctors = new List<Doctor>();
            Doctor trueDoctor1 = new Doctor();
            trueDoctor1.Id = 0;
            Doctor trueDoctor2 = new Doctor();
            trueDoctor2.Id = 4;
            doctors.Add(trueDoctor1);
            doctors.Add(trueDoctor2);
            stubDoctorRepository.Setup(m => m.GetAvailableDoctors()).Returns(doctors);

            var stubAllergenRepository = new Mock<IAllergenRepository>();
            List<Allergen> allergens = new List<Allergen>();
            Allergen trueAllergen1 = new Allergen();
            trueAllergen1.Id = 0;
            Allergen trueAllergen2 = new Allergen();
            trueAllergen2.Id = 1;
            Allergen trueAllergen3 = new Allergen();
            trueAllergen3.Id = 2;
            allergens.Add(trueAllergen1);
            allergens.Add(trueAllergen2);
            allergens.Add(trueAllergen3);
            stubAllergenRepository.Setup(m => m.GetAllergens()).Returns(allergens);

            PatientService patientService = new PatientService(stubPatientRepository.Object);
            DoctorService doctorService = new DoctorService(stubDoctorRepository.Object);
            AllergenService allergenService = new AllergenService(stubAllergenRepository.Object);
            _verification = new PatientVerification(patientService, doctorService, allergenService);
        }

        private PatientRegisterDTO GenerateValidBranko()
        {
            setupVerification();

            PatientRegisterDTO patient = new PatientRegisterDTO();
            patient.UserName = "branko";
            patient.FirstName = "Branko";
            patient.LastName = "Branković";
            patient.Password = "baki123"; //Za sad plaintext
            patient.EMail = "branko.brankovic@gmail.com";
            patient.Gender = "Male";
            patient.Jmbg = "0111000800012";
            patient.DateOfBirth = new DateTime(2000, 11, 1, 0, 0, 0);
            patient.Country = "Serbia";
            patient.Address = "Nikole Tesle 16";
            patient.City = "Novi Sad";
            patient.Phone = "063123123";
            patient.Allergens = new List<int>();
            patient.Allergens.Add(0);
            patient.Allergens.Add(2);
            patient.PreferedDoctor = 0;
            patient.Weight = 75;
            patient.Height = 182;
            patient.BloodType = "A";
            return patient;
        }

        [Fact]
        public void Test_patient_valid()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            Assert.True(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_is_null()
        {
            setupVerification();
            PatientRegisterDTO patient = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_username_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.UserName = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_username_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.UserName = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_username_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.UserName = "brankoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo123";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_username_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.UserName = "branko*";
            Assert.False(_verification.Verify(patient));
        }

        //TODO: Username unique?

        [Fact]
        public void Test_patient_first_name_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.FirstName = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_first_name_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.FirstName = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_first_name_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.FirstName = "Brankoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_first_name_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.FirstName = "Branko1";
            Assert.False(_verification.Verify(patient));
        }

        //Last Name ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_last_name_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.LastName = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_last_name_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.LastName = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_last_name_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.LastName = "Brankoooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooovic";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_last_name_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.FirstName = "Brankovic1";
            Assert.False(_verification.Verify(patient));
        }

        //Email ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_email_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.EMail = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_email_is_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.EMail = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_email_is_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.EMail = "branko.brankovicccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc@gmail.com";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_email_bad_format()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.EMail = "branko.brankovicgmail.com";
            Assert.False(_verification.Verify(patient));
        }

        //Gender ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_gender_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Gender = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_gender_is_invalid()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Gender = "";
            Assert.False(_verification.Verify(patient));
        }

        //Jmbg ------------------------------------------------------------------------------------
        [Fact]
        public void Test_patient_jmbg_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Jmbg = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_jmbg_is_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Jmbg = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_jmbg_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Jmbg = "123123123123123123123";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_jmbg_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Jmbg = "01110a0800012";
            Assert.False(_verification.Verify(patient));
        }

        //Date of Birth ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_date_of_birth_in_future()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.DateOfBirth = DateTime.Today.AddDays(1);
            Assert.False(_verification.Verify(patient));
        }

        //Country ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_country_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Country = null;
            Assert.False(_verification.Verify(patient));
        }

        //Remove after Valid Countries DB is implemented <<<<<<<<<<<<<<<
        [Fact]
        public void Test_patient_country_is_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Country = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_country_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Country = "Serbiaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_country_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Country = "Ser?ia";
            Assert.False(_verification.Verify(patient));
        }
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

        //TODO: Valid Countries (Is In Database) + IsEmpty
        /*
        [Fact]
        public void Test_patient_country_is_valid()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Country = "Mozambique";
            Assert.False(_verification.Verify(patient));
        }
        */

        //Address ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_address_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Address = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_address_is_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Address = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_address_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Address = "Nikoleeeeeeeeeeeeeeeeeeeeeee Tesleeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee 16";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_address_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Address = "N*kole Te?le 16";
            Assert.False(_verification.Verify(patient));
        }

        //Phone ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_phone_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Phone = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_phone_is_empty()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Phone = "";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_phone_too_long()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Phone = "063123123123123123123123123123123123123123123123123123123123123123123123";
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_phone_illegal_characters()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Phone = "063i23123";
            Assert.False(_verification.Verify(patient));
        }

        //Allergens ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_allergens_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Allergens = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_allergens_dont_exist()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Allergens = new List<int>();
            patient.Allergens.Add(2);
            patient.Allergens.Add(12);
            Assert.False(_verification.Verify(patient));
        }

        //Prefered Doctor ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_prefered_doctor_negative() //TODO: Prebaciti u UINT, i testirati sta se desi kada se posalje negativan broj u POST-u
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.PreferedDoctor = -1;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_prefered_doctor_exists()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.PreferedDoctor = 9999;
            Assert.False(_verification.Verify(patient));
        }
        

        //Weight ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_weight_negative()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Weight = -1;
            Assert.False(_verification.Verify(patient));
        }

        //Height ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_height_negative()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.Height = -1;
            Assert.False(_verification.Verify(patient));
        }

        //BloodType ------------------------------------------------------------------------------------

        [Fact]
        public void Test_patient_blood_type_is_null()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.BloodType = null;
            Assert.False(_verification.Verify(patient));
        }

        [Fact]
        public void Test_patient_blood_type_is_invalid()
        {
            PatientRegisterDTO patient = GenerateValidBranko();
            patient.BloodType = "";
            Assert.False(_verification.Verify(patient));
        }
    }
}