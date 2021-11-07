using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using api.Data;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class RequestList
    {

     

        [Key]
        public long Id { get; set; }
        [ForeignKey("Request")]
        public long RequestId { get; set; }
        public virtual Request Request { get; set; }
        [MaxLength(100)]
        public string Product { get; set; }
        [MaxLength(50)]
        public string Type { get; set; }
        public float Value { get; set; }
        
    }
}