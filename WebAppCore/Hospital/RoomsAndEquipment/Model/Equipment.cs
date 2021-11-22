using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.RoomsAndEquipment.Model
{

   public enum EquipmentType
    {
        STATIC,
        DYNAMIC
    }

    public class Equipment
    {
        public int id { get; set; }
        private string name;
        public string Name{
            get{return name;}
            set{name=value;}
        }
        private EquipmentType type;
        public EquipmentType Type {
            get{return type;}
            set{type=value;}
        }
        private int amount;
        public int Amount{   
            get { return amount; } 
            set { amount=value; }       
        }
        private int roomId;
        public int RoomId
        {
            get { return roomId; }
            set { roomId = value; }
        }

        public Equipment()
        {

        }

    }
}
