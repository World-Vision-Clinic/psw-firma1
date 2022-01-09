using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class EquipmentDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int amount { get; set; }
        public int roomId { get; set; }
        public string transportStart { get; set; }
        public string transportEnd { get; set; }

        public bool inTransport { get; set; }
    }
}
