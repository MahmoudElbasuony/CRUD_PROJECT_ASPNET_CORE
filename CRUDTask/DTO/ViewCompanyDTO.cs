using System;
using System.Collections.Generic;
using System.Text;

namespace APP.CRUDTask.DTO
{
    public class ViewCompanyDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string Code { get; set; }
        public byte[] CompanyPhoto { get; set; }
    }
}
