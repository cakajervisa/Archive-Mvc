using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arkiva.ViewModels
{
    public class Filter
    {
       
        public string Subjekti { get; set; }
        public int? Inspektimi { get; set; }
        public string Lloji { get; set; }
        public int LlojiID { get; set; }     
        public string Dokumenti { get; set; }
        public string Fusha { get; set; }
        public string Zyra { get; set; }
        public string Rafti { get; set; }
        public string Kutia { get; set; }
        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])(\/|-|\.)(0[1-9]|1[0-2])(\/|-|\.)((19|20)\d\d))$", ErrorMessage = "Data nuk është në formatin e duhur!")]
        public string DataNga { get; set; }
        [RegularExpression(@"(((0|1)[0-9]|2[0-9]|3[0-1])(\/|-|\.)(0[1-9]|1[0-2])(\/|-|\.)((19|20)\d\d))$", ErrorMessage = "Data nuk është në formatin e duhur!")]
        public string DataTek { get; set; }

    }
}



