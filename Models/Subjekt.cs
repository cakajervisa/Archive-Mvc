


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arkiva.Models
{
    public class Subjekt
    {
        [Index]
        public int SubjektID { get; set; }
        
        public string Emer { get; set; }
        public virtual ICollection<Inspektim> Inspektime { get; set; }

    }
}