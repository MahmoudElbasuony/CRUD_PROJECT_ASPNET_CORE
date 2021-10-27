using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APP.CRUDTask.DTO
{
    public class AddCompanyDTO
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 10)]
        public string Name { get; set; }
        public int? PhoneNumber { get; set; }
        [Required(AllowEmptyStrings = false)]
        [StringLength(2, MinimumLength = 1)]
        public string Code { get; set; }
    }
}
