using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Integration.Pharmacy.Model
{
    public class Reply
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; } 
        public string ObjectionId { get; set; }
        public string Content { get; set; }

        public Reply() { }
    }
}
