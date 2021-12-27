using Integration.Partnership.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration_API.Dto
{
    public class TenderCreationDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<TenderItem> TenderItems { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
