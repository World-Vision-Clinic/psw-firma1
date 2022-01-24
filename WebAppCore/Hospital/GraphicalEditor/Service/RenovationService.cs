using Hospital.GraphicalEditor.Model;
using Hospital.GraphicalEditor.Repository;
using Hospital.RoomsAndEquipment.Model;
using Hospital.RoomsAndEquipment.Service;
using Hospital.Schedule.Model;
using Hospital.Schedule.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.GraphicalEditor.Service
{
   
    public class RenovationService
    {
        private RenovationRepository repository;

        public RenovationService(RenovationRepository repos)
        {
            repository = repos;
        }

        public Renovation FindRenovationForRoom(int roomId)
        {
            return repository.FindRenovationForRoom(roomId);
        }

        public void Save(Renovation renovation)
        {
            repository.Save(renovation); 
        }

        public void Remove(int renovationId)
        {
            repository.Delete(renovationId);
        }

        public void DoSplit(RoomService roomService, Renovation renovation ) {
            Room room = roomService.GetById(renovation.Room1Id);
            roomService.splitRoom(room, renovation.NewRoomName1, renovation.NewRoomPurpose1, renovation.NewRoomName2, renovation.NewRoomPurpose2);
        }

        public int DoMerge(RoomService roomService, Renovation renovation)
        {
            Room room1 = roomService.GetById(renovation.Room1Id);
            Room room2 = roomService.GetById(renovation.Room2Id);
            Save(renovation);
            return roomService.mergeRooms(room1, room2, renovation.NewRoomName1, renovation.NewRoomPurpose1);
        }


        public List<Renovation> GetPassedRenovations()
        {
            return repository.GetPassedRenovations();
        }

        public Renovation GetById(int id)
        {
            return repository.GetByID(id);
        }

        public bool cancelRenovation(int id)
        {
            Renovation r = GetById(id);
            if (r == null)
                return false;
            return r.StartDate.AddHours(-24) >= new DateTime();
        }



        public RenovationPeriod getSuggestions(RoomService roomService, AppointmentService appointmentService, EquipmentService equipmentService, int room1Id, int room2Id, long startPeriodTimestamp, long endPeriodTimestamp, int durationInDays)
        {

            RenovationPeriod renovationPeriod = new RenovationPeriod();
            DateTime startDate = DateTimeOffset.FromUnixTimeSeconds(startPeriodTimestamp/1000).LocalDateTime;
            DateTime endDate = DateTimeOffset.FromUnixTimeSeconds(endPeriodTimestamp/1000).LocalDateTime;
            if (startDate.AddDays(durationInDays) > endDate)
                return null;
            startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, 8, 0, 0);
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, 18, 0, 0);
            DateTime workingStartDate = startDate;
            DateTime workingEndDate = workingStartDate.AddDays(durationInDays);

            List<Appointment> appointements = new List<Appointment>();
            appointmentService.GetAll().ForEach(e =>
            {
                if ((e.RoomId == room1Id || e.RoomId == room2Id) && startDate < e.Date && e.Date < endDate)
                    appointements.Add(e);
            });
            List<int> rooms = new List<int>();
            rooms.Add(room1Id);
            if(room2Id >= 0)
                rooms.Add(room2Id);

            List<Equipment> equipmentsInTransport = equipmentService.getAllInTransport(rooms);
            equipmentsInTransport = equipmentsInTransport.OrderBy(x => x.TransportStart).ToList<Equipment>();
            foreach (Equipment equipment in equipmentsInTransport)
            {
                if (workingStartDate >= equipment.TransportStart && equipment.TransportEnd <= workingEndDate)
                {
                    workingStartDate = equipment.TransportEnd.AddDays(1);
                    workingEndDate = workingStartDate.AddDays(durationInDays);
                    if (workingEndDate > endDate)
                        return null;
                }
            }
            renovationPeriod.StartDate = workingStartDate;
            renovationPeriod.EndDate = workingEndDate;

            return renovationPeriod;

        }
    }

  
} 
