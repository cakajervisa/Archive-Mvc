

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Arkiva.Models
{
    public class FusheIndeksimiDokument
    {
        [Key]
        public int FusheIndeksimiDokumentID { get; set; }

        [Index]
        public int DokumentID { get; set; }
        [Index]
        public int FusheIndeksimiID { get; set; }
        public virtual Dokument Dokument{ get; set; }

        public virtual FusheIndeksimi FusheIndeksimi { get; set; }
    }
}