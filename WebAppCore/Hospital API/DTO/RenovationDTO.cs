using Hospital.GraphicalEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.DTO
{
    public class RenovationDTO
    {
        public int id { get; set; }
        public int Room1Id { get; set; }
        public int Room2Id { get; set; }
        public string NewRoomName1 { get; set; }
        public string NewRoomName2 { get; set; }

        public string NewRoomPurpose1 { get; set; }
        public string NewRoomPurpose2 { get; set; }

        public long StartDateTimestamp { get; set; }
        public long EndDateTimeStamp { get; set; }

        public bool isMerge { get; set; }

        internal Renovation toRenovation()
        {
            Renovation renovation = new Renovation();
            renovation.isMerge = this.isMerge;
            renovation.Room1Id = this.Room1Id;
            renovation.Room2Id = this.Room2Id;
            renovation.NewRoomName1 = this.NewRoomName1;
            renovation.NewRoomName2 = this.NewRoomName2;
            renovation.NewRoomPurpose1 = this.NewRoomPurpose1;
            renovation.NewRoomPurpose2 = this.NewRoomPurpose2;
            renovation.StartDate = convertTimestampToDatetime(this.StartDateTimestamp);
            renovation.EndDate = convertTimestampToDatetime(this.EndDateTimeStamp);
            return renovation;
        }

        private DateTime convertTimestampToDatetime(long timestamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
