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
            allStatistics.Add(GetUseTimesOfDay());
            return allStatistics;
        }

        private static EventStatisticDTO GetSuccessful4StepAttempts()
        {
            EventService eventService = new EventService(new EventRepository());
            EventStatisticDTO statistic = new EventStatisticDTO("Successful Attempts", EventStatisticType.TABLE);
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
            EventStatisticDTO statistic = new EventStatisticDTO("Use Time", EventStatisticType.TABLE);
            List<Event> allDateNextEvents = eventService.GetAll().Where(p => String.Equals(p.Name, "DATE-NEXT")).ToList();
            double averageDateUseSeconds = allDateNextEvents.Average(p => p.TimeDifference.TotalSeconds);
            statistic.Data.Add(new EventStatisticDataPair("Date Selection", (float)averageDateUseSeconds));

            double averageSpecUseSeconds = eventService.GetAll().Where(p => String.Equals(p.Name, "SPEC-NEXT") || String.Equals(p.Name, "SPEC-BACK")).ToList().Sum(p => p.TimeDifference.TotalSeconds)
                / eventService.GetAll().Where(p => String.Equals(p.Name, "SPEC-NEXT") || String.Equals(p.Name, "SPEC-BACK")).ToList().Count();
            statistic.Data.Add(new EventStatisticDataPair("Specialist Selection", (float)averageSpecUseSeconds));

            double averageDoctorUseSeconds = eventService.GetAll().Where(p => String.Equals(p.Name, "DOC-NEXT") || String.Equals(p.Name, "DOC-BACK")).ToList().Sum(p => p.TimeDifference.TotalSeconds)
                / eventService.GetAll().Where(p => String.Equals(p.Name, "DOC-NEXT") || String.Equals(p.Name, "DOC-BACK")).ToList().Count();
            statistic.Data.Add(new EventStatisticDataPair("Doctor Selection", (float)averageDoctorUseSeconds));

            double averageTimeUseSeconds = eventService.GetAll().Where(p => String.Equals(p.Name, "END")).ToList().Sum(p => p.TimeDifference.TotalSeconds)
                / eventService.GetAll().Where(p => String.Equals(p.Name, "END")).ToList().Count();
            statistic.Data.Add(new EventStatisticDataPair("Time Selection", (float)averageTimeUseSeconds));

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

        private static EventStatisticDTO GetUseTimesOfDay()
        {
            EventService eventService = new EventService(new EventRepository());
            EventStatisticDTO statistic = new EventStatisticDTO("Times of Day");
            List<Event> allEndEvents = eventService.GetAll().Where(p => String.Equals(p.Name, "END")).ToList();
            double averageTimeOfDay = allEndEvents.Average(p => p.EventTime.TimeOfDay.TotalSeconds);
            double maxTimeOfDay = allEndEvents.Max(p => p.EventTime.TimeOfDay.TotalSeconds) + 0.1;
            double[] cutoffPoints = GenerateCutoffPoints(averageTimeOfDay, maxTimeOfDay);
            for (int i = 0; i < 4; i++)
            {
                int eventCount = allEndEvents
                    .Where(p => p.EventTime.TimeOfDay.TotalSeconds > cutoffPoints[i] && p.EventTime.TimeOfDay.TotalSeconds <= cutoffPoints[i + 1])
                    .Count();
                statistic.Data.Add(new EventStatisticDataPair(SecondsToHourAndMinuteString(Convert.ToInt32(cutoffPoints[i])) + "-" + SecondsToHourAndMinuteString(Convert.ToInt32(cutoffPoints[i+1])), eventCount));
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

        private static string SecondsToHourAndMinuteString(int seconds)
        {
            int hours = (seconds - seconds % 3600) / 3600;
            int minutes = (seconds % 3600) / 60;
            string hoursString = hours.ToString();
            string minutesString = minutes.ToString();
            if (hours < 10)
                hoursString = "0" + hoursString;
            if (minutes < 10)
                minutesString = "0" + minutesString;
            return hoursString + ":" + minutesString;
        }
    }
}
