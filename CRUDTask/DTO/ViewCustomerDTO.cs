using System;
using System.Collections.Generic;
using System.Text;

namespace APP.CRUDTask.DTO
{
    public class ViewCustomerDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public int? MobileNo { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
