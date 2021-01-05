
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using static System.Web.UI.WebControls.CustomValidator;


namespace Arkiva.ViewModels
{
    public class UploadDokument
    {
        

        public string Titull { get; set; }
        [Required(ErrorMessage = "Duhet të zgjidhni një lloj dokumenti!")]
        public int Lloji { get; set; }
        [StringLength(255, ErrorMessage = "{0}t duhet të përmbajnë deri në 255 shkronja gjithsej!")]
        public string Fusha { get; set; }
        [StringLength(10, ErrorMessage = "{0} duhet të përmbajë deri në 10 shkronja!")]
        public string Zyra { get; set; }
        [StringLength(10, ErrorMessage = "{0} duhet të përmbajë deri në 10 shkronja!")]
        public string Rafti { get; set; }
        [StringLength(10, ErrorMessage = "{0} duhet të përmbajë deri në 10 shkronja!")]
        public string Kutia { get; set; }
        [Required(ErrorMessage = "Duhet të zgjidhni të paktën një dokument!")]
        [DisplayName("Ngarko")]

        public HttpPostedFileBase[] Skedari { get; set; }
        [Required]
        public int Inspektimi { get; set; }
    }
}