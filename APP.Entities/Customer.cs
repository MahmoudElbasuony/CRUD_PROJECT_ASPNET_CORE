using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int? MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; }

    }
}
