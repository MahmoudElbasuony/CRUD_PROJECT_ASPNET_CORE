using System;

namespace APP.Entities
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
