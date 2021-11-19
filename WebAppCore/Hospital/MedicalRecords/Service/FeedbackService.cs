using Hospital.MedicalRecords.Model;
using Hospital.MedicalRecords.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.MedicalRecords.Service
{
    public class FeedbackService
    {
        private readonly FeedbackRepository _repo;

        public FeedbackService(FeedbackRepository repo)
        {
            _repo = repo;
        }

        public List<Feedback> GetPublished()
        {
            return _repo.GetPublished();
        }

        public List<Feedback> GetAll()
        {
            return _repo.GetAll();
        }

        public void AddFeedback(Feedback newFeedback)
        {
            _repo.AddFeedback(newFeedback);
        }

        public Feedback FindById(int id)
        {
            return _repo.FindById(id);
        }

        public void Delete(Feedback feedback)
        {
            _repo.Delete(feedback);
        }

        public bool FeedbackExists(int id)
        {
            return _repo.FeedbackExists(id);
        }

        public void Modify(Feedback feedback)
        {
            _repo.Modify(feedback);
        }

        public void SaveSync()
        {
            _repo.SaveSync();
        }

    }
}
