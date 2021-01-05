

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arkiva.Models
{
    public class LlojDokumentiInspektim
    {
        [Key]
        [Column(Order = 1)]
        [Index]
        public int LlojDokumentiID { get; set; }
        [Key]
        [Column(Order = 2)]
        [Index]
        public int InspektimID { get; set; }
        public virtual LlojDokumenti LlojDokumenti { get; set; }

        public virtual Inspektim Inspektim { get; set; }

    }
}