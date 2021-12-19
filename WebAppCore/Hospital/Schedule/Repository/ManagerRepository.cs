using Hospital.Schedule.Model;
using Hospital.SharedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.Schedule.Repository
{
    public class ManagerRepository
    {

        private readonly HospitalContext _context;

        public ManagerRepository(HospitalContext context)
        {
            _context = context;
        }

        public Manager FindByUserName(string username)
        {
            try
            {
                return _context.Managers.FirstOrDefault(p => String.Equals(p.UserName, username));
            }
            catch
            {
                return null;
            }
        }

        public void AddManager(Manager newManager)
        {
            _context.Managers.Add(newManager);
            SaveSync();
        }
        public void SaveSync()
        {
            _context.SaveChanges();
        }
    }
}
