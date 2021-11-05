using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using api.Data;

namespace api.Models
{
    public class Request
    {      

        [Key]
        public long Id { get; set; }
        public long IdUser { get; set; }
        [MaxLength(15)]
        public string MethodDelivery { get; set; }
        [MaxLength(30)] 
        public string MethodPayment { get; set; }   
        public float Discount { get; set; } 
        [MaxLength(25)]
        public string Coupon { get; set; }
        public float Subtotal { get; set; }
        public float Total { get; set; }
        public virtual ICollection<RequestList> RequestList { get; set; }
        
        
    }
}