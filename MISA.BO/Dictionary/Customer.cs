using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.BO
{
   public class Customer
    {
        [Key]
        public Guid CID { get; set; }
        public string CCode { get; set; }
        public string CName { get; set; }
        public string CCompany { get; set; }
        public string CTaxCode { get; set; }
        public string CAddress { get; set; }
        public string CPhone { get; set; }
        public string CEmail { get; set; }
        public string CMemberNum { get; set; }
        public string CMemberType { get; set; }
        public decimal CDebit { get; set; }
        public string CDescription { get; set; }
        public bool Is5Food { get; set; }
        public bool IsFollow { get; set; }
    }
}
