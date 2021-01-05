

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arkiva.Models
{
    public class Inspektim
    {
        [Index]
        public int InspektimID { get; set; }
        [Index]
        public int Numer { get; set; }
        public string Emer { get; set; }
        [Index]
        public int SubjektID { get; set; }
        public virtual Subjekt Subjekt { get; set; }
        public virtual ICollection<LlojDokumenti> LlojeDokumenti { get; set; }
        public virtual ICollection<Dokument> Dokumenta { get; set; }
     }
}