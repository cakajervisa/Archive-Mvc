
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Arkiva.Models
{
    public class LlojDokumenti
    {
        [Index]
        public int LlojDokumentiID { get; set; }
        
        public string Emer { get; set; }
        public virtual ICollection<Inspektim> Inspektime { get; set; }
        public virtual ICollection<Dokument> Dokumenta { get; set; }
    }
}