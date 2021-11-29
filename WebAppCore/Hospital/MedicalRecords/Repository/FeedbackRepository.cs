using Hospital.MedicalRecords.Model;
using Hospital.SharedModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hospital.MedicalRecords.Repository
{
    public class FeedbackRepository
    {
        private readonly HospitalContext _context;

        public FeedbackRepository(HospitalContext context)
        {
            _context = context;
        }

        public List<Feedback> GetAll()
        {
            return _context.Feedbacks.ToList();
        }

        public List<Feedback> GetPublished()
        {
            return _context.Feedbacks.Where(f => f.IsPublic).ToList();
        }

        public Feedback FindById(int id)
        {
            return _context.Feedbacks.Find(id);
        }

        public void Delete(Feedback feedback)
        {
            _context.Feedbacks.Remove(feedback);
            SaveSync();
        }

        public void AddFeedback(Feedback newFeedback)
        {
            _context.Feedbacks.Add(newFeedback);
            SaveSync();
        }

        public bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }

        public void Modify(Feedback feedback)
        {
            _context.Entry(feedback).State = EntityState.Modified;
        }

        public void SaveSync(){
            _context.SaveChanges();
        }

    }
}
