using Hospital.Schedule.Model;
using Hospital.Schedule.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Schedule.Service
{
    public class ManagerService
    {
        private readonly ManagerRepository _repo;

        public ManagerService(ManagerRepository repo)
        {
            _repo = repo;
        }

        public Manager LoginManager(string username, string password)
        {
            Manager manager = _repo.FindByUserName(username);
            if (manager != null)
            {
                if (manager.Password.Equals(password))
                    return manager;
                else
                    return null;
            }
            else
                return null;
        }
    }
}
