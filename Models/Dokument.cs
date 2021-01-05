

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Arkiva.Models
{
    public class Dokument
    {
        [Index]
        public int DokumentID { get; set; }
        
        public string Titull { get; set; }
        
        public string Zyra { get; set; }
      
        public string Rafti { get; set; }
     
        public string Kutia { get; set; }
        [Index]
        [DisplayFormat(DataFormatString = "{0:MMM dd, yyyy}")]
        public DateTime DateRegjistrimi { get; set; }
        public string Path { get; set; }
        public string Tipi { get; set; }
        public string Formati { get; set; }
        public byte[] Skedari { get; set; }
        public string ApplicationUserID { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        [Index]
        public int LlojDokumentiID { get; set; }
        public virtual LlojDokumenti LlojDokumenti { get; set; }
        [Index]
        public int InspektimID { get; set; }
        public virtual Inspektim Inspektim { get; set; }
        public virtual ICollection<FusheIndeksimi> FushaIndeksimi { get; set; }


      
        public string GetImazhi()
        {
            if (this.Formati == "pdf")
            {
                return "doc.png";
            }
            else if ((this.Formati == "xls") || (this.Formati == "xlsx"))
            {
                return "doc.png";
            }
             else if ((this.Formati == "doc") || (this.Formati == "docx"))
            {
                return "doc.png";
            }
            else if ((this.Formati == "jpeg") || (this.Formati == "jpg") || (this.Formati == "jfif") || (this.Formati == "png") || (this.Formati == "gif") || (this.Formati == "bmp") || (this.Formati == "tiff") || (this.Formati == "hdr"))
            {
                return "doc.png";
            }
            else if ((this.Formati == "ppt") || (this.Formati == "pptx"))
            {
                return "doc.png";
            }
            else if ((this.Formati == "rar") || (this.Formati == "zip") || (this.Formati == "zipx"))
            {
                return "doc.png";
            }
            else if ((this.Formati == "mp3") || (this.Formati == "3gp") || (this.Formati == "wma") || (this.Formati == "wav"))
            {
                return "doc.png";
            }
            else if ((this.Formati == "mp4") || (this.Formati == "flv") || (this.Formati == "avi") || (this.Formati == "mov") || (this.Formati == "wmv"))
            {
                return "doc.png";
            }
            else
            {
                return "doc.png";
            }
        }
    }
}