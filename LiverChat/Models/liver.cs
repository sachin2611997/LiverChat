using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LiverChat.Models
{
    [Table("liver")]
    [DataContract]
    public class liver
    {   [Key]
        public string STATE { get; set; }
        [DataMember(Name = "y")]
        public string N_CLINEXT { get; set; }

         public string TMH_RX { get; set; }
    }

    public class LiverModel
    {
        public double Statecount { get; set; }
        public string State { get; set; }
        public double Percentage { get; set; }
        public int othercount { get; set; }
    }
}