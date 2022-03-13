using Hospital.GraphicalEditor.Model;
using Hospital.RoomsAndEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class LoginDTO
    {
        public string username{ get; set; }
        public string password { get; set; }

        public LoginDTO(){}

        public LoginDTO(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
    }
}
