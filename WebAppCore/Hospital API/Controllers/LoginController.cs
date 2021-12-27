using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using Hospital_API.DTO;
using Hospital.MedicalRecords.Service;
using Hospital.MedicalRecords.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Hospital.Schedule.Service;
using Hospital.Schedule.Repository;
using Hospital.Schedule.Model;

namespace Hospital_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private string SECRET = "ecc0024b9feaa167cf8c5bc4819bc03aa8ed88d86524bd647db6f3363dfabd13";
        public PatientService _patientService;
        public ManagerService _managerService;

        public LoginController()
        {
            IAppointmentRepository _appointmentRepository = new AppointmentRepository(new HospitalContext());
            _patientService = new PatientService(new PatientRepository(new HospitalContext()), _appointmentRepository);
            _managerService = new ManagerService(new ManagerRepository(new HospitalContext()));
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginInfo)
        {
            Patient patient = _patientService.LoginPatient(loginInfo.username, loginInfo.password);
            Manager manager = _managerService.LoginManager(loginInfo.username, loginInfo.password);
            if (patient != null) 
            {
                string jwt = GenerateJWT(patient.UserName,patient.Id,"Patient");
                return Ok("{\"token\":\"" + jwt+ "\"}");
            }
            else if (manager != null)
            {
                string jwt = GenerateJWT(manager.UserName, manager.Id, "Manager");
                return Ok("{\"token\":\"" + jwt + "\"}");
            }
            return Unauthorized();
        }

        private string GenerateJWT(string username,int id,string role) 
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "PSW",
                audience: "PSW-audience",
                claims: new[] {
                    new Claim("username",username),
                    new Claim("id",id.ToString()),
                    new Claim("role",role)
                },
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET)),
                        algorithm: SecurityAlgorithms.HmacSha256
                    )
                );

            return (new JwtSecurityTokenHandler()).WriteToken(token);
        }

    }
}
