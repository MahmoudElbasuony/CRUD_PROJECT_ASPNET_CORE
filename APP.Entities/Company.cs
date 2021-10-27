using System;
using System.Collections.Generic;
using System.Text;

namespace APP.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string Code { get; set; }
        public byte[] CompanyPhoto { get; set; }
    }
}
