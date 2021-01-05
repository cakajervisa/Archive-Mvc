

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arkiva.Models
{
    public class FusheIndeksimi
    {
        [Index]
        public int FusheIndeksimiID { get; set; }
        
        public string Emer { get; set; }
        public virtual ICollection<Dokument> Dokumenta { get; set; }

    }
}