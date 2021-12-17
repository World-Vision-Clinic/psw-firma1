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
            _patientService = new PatientService(new PatientRepository(new Hospital.SharedModel.HospitalContext()));
            _managerService = new ManagerService(new ManagerRepository(new Hospital.SharedModel.HospitalContext()));
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO loginInfo)
        {
            if (_patientService.LoginPatient(loginInfo.username,loginInfo.password)) 
            {
                string jwt = GenerateJWTForPatient(loginInfo);
                return Ok("{\"token\":\"" + jwt+ "\"}");
            }
            else if (_managerService.LoginManager(loginInfo.username, loginInfo.password))
            {
                string jwt = GenerateJWTForManager(loginInfo);
                return Ok("{\"token\":\"" + jwt + "\"}");
            }
            return Unauthorized();
        }

        private string GenerateJWTForPatient(LoginDTO login) 
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer:"PSW",
                audience: "PSW-audience",
                claims: new[] {
                    new Claim("username",login.username),
                    new Claim("role","Patient")
                },
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: new SigningCredentials(
                        key: new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET)),
                        algorithm: SecurityAlgorithms.HmacSha256
                    )
                );

            return (new JwtSecurityTokenHandler()).WriteToken(token);
        }

        private string GenerateJWTForManager(LoginDTO login)
        {
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: "PSW",
                audience: "PSW-audience",
                claims: new[] {
                    new Claim("username",login.username),
                    new Claim("role","Manager")
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
