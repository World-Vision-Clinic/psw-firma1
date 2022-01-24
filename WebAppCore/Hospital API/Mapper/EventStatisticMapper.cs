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
            allStatistics.Add(GetUseTimes());
            allStatistics.Add(GetUserAges());
            return allStatistics;
        }

        private static EventStatisticDTO GetSuccessful4StepAttempts()
        {
            EventService eventService = new EventService(new EventRepository());
            EventStatisticDTO statistic = new EventStatisticDTO("Successful Attempts");
            List<Event> allEvents = eventService.GetAll();
            float startCount = allEvents
                .Where(p => String.Equals(p.Name, "START"))
                .Count();
            float endCount = allEvents
                .Where(p => String.Equals(p.Name, "END"))
                .Count();
            float successRate = 0;
            if(startCount != 0)
            {
                successRate = (endCount / startCount) * 100;
            }
            statistic.Data.Add(new EventStatisticDataPair("Successful", successRate));
            statistic.Data.Add(new EventStatisticDataPair("Unsuccessful", (100 - successRate)));
            return statistic;
        }

        private static EventStatisticDTO GetUseTimes()
        {
            EventService eventService = new EventService(new EventRepository());
            EventStatisticDTO statistic = new EventStatisticDTO("Use Time");
            List<Event> allEndEvents = eventService.GetAll().Where(p => String.Equals(p.Name, "END")).ToList();
            double averageUseSeconds = allEndEvents.Average(p => p.TimeDifference.TotalSeconds);
            double maxUseSeconds = allEndEvents.Max(p => p.TimeDifference.TotalSeconds) + 0.1;
            double[] cutoffPoints = GenerateCutoffPoints(averageUseSeconds, maxUseSeconds);
            for (int i = 0; i < 4; i++)
            {
                int eventCount = allEndEvents
                    .Where(p => p.TimeDifference.TotalSeconds > cutoffPoints[i] && p.TimeDifference.TotalSeconds <= cutoffPoints[i + 1])
                    .Count();
                statistic.Data.Add(new EventStatisticDataPair(cutoffPoints[i].ToString() + "-" + cutoffPoints[i+1].ToString(), eventCount));
            }
            return statistic;
        }

        private static EventStatisticDTO GetUserAges()
        {
            EventService eventService = new EventService(new EventRepository());
            EventStatisticDTO statistic = new EventStatisticDTO("User Age");
            List<Event> allEndEvents = eventService.GetAll().Where(p => String.Equals(p.Name, "END")).ToList();
            double averageUserAge = allEndEvents.Average(p => p.PatientAge);
            double maxUserAge = allEndEvents.Max(p => p.PatientAge) + 0.1;
            double[] cutoffPoints = GenerateCutoffPoints(averageUserAge, maxUserAge);
            for (int i = 0; i < 4; i++)
            {
                int eventCount = allEndEvents
                    .Where(p => p.PatientAge > cutoffPoints[i] && p.PatientAge <= cutoffPoints[i + 1])
                    .Count();
                statistic.Data.Add(new EventStatisticDataPair(cutoffPoints[i].ToString() + "-" + cutoffPoints[i + 1].ToString(), eventCount));
            }
            return statistic;
        }

        private static double[] GenerateCutoffPoints(double average, double max)
        {
            double[] cutoffPoints = new double[5];
            cutoffPoints[0] = 0;
            cutoffPoints[1] = Math.Truncate((average / 2) * 100) / 100;
            cutoffPoints[2] = Math.Truncate(average * 100) / 100;
            cutoffPoints[3] = Math.Truncate(((average + max) / 2) * 100) / 100;
            cutoffPoints[4] = Math.Truncate(max * 100) / 100;
            return cutoffPoints;
        }
    }
}
