﻿using Hospital.RepositoryInterfaces;
using Hospital.ShiftsAndVacations.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.ShiftsAndVacations.Repository.RepositoryInterfaces
{
    public interface IVacationRepository : IGenericRepository<Vacation>
    {
        List<Vacation> getDoctorsVacations(int doctorId);
    }
}
