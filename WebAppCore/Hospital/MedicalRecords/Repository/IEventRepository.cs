﻿using Hospital.MedicalRecords.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public interface IEventRepository
    {
        public List<Event> GetAll();
        public Event GetById(int eventId);
        public void Save(Event newEvent);
        public Event getLastEvent();
    }
}
