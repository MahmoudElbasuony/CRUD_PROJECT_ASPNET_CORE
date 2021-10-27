using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.CRUDTask.DTO
{
    public class AddCustomerDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 10)]
        public string Job { get; set; }
        public int? MobileNo { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 10)]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        [Required]
        public int CompanyId { get; set; }
    }
}
