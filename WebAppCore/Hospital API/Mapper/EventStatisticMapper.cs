using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using Hospital.MedicalRecords.Service;
using Hospital_API.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital_API.Mapper
{
    public class EventStatisticMapper
    {
        public static List<EventStatisticDTO> GetAllEventStatistics()
        {
            List<EventStatisticDTO> allStatistics = new List<EventStatisticDTO>();
            allStatistics.Add(GetSuccessful4StepAttempts());
            return allStatistics;
        }

        private static EventStatisticDTO GetSuccessful4StepAttempts()
        {
            EventService eventService = new EventService(new EventRepository());
            EventStatisticDTO statistic = new EventStatisticDTO("Successful Attempts");
            List<Event> allEvents = eventService.GetAll();
            int startCount = allEvents
                .Where(p => String.Equals(p.Name, "START"))
                .Count();
            int endCount = allEvents
                .Where(p => String.Equals(p.Name, "END"))
                .Count();
            float successRate = 0;
            if(startCount != 0)
            {
                successRate = (float)(endCount / startCount) * 100;
            }
            statistic.Data.Add(new EventStatisticDataPair("Successful", successRate));
            statistic.Data.Add(new EventStatisticDataPair("Unsuccessful", (100 - successRate)));
            return statistic;
        }
    }
}
