using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.GraphicalEditor.Model
{
    class Renovation
    {
        public int Room1Id { get; set; }
        public int Room2Id { get; set; }
        public string NewRoomName1 { get; set; }
        public string NewRoomName2 { get; set; }

        public string NewRoomPurpose1 { get; set; }
        public string NewRoomPurpose2 { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public bool isMerge { get; set; }

    }
}
