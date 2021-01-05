/***
 *Microsoft .net Framework Version 4.8.04084
 * Microsift Visual Studio Community 2017 Version 15.9.23
 * ****/

using Arkiva.Models;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;




namespace Arkiva.Controllers
{
    [HandleError]
    [Authorize]
    public class HomeController : Controller
    {
      

        private ApplicationDbContext db = new ApplicationDbContext();
   
        /**
        * Programues:Erivsa Cakaj
        * Metoda: Index
        * Arsyeja:marrja e subjekteve nga db dhe afillimi i tyre
        * Pershkrimi: merr dhe listin subjektet
        * Return: view me subjektet
        **/


        public ActionResult Index()
        {
            dynamic allmodel = new ExpandoObject();
            allmodel.Subjekte = db.Subjekte.ToList();
            allmodel.LlojeDokumenti = db.LlojeDokumenti.ToList();
            allmodel.Shtuar = 0;
            ViewBag.Lloje = db.LlojeDokumenti.ToList();
            return View(allmodel);
        }
        

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShikoDokument
        * Arsyeja: Preview i dokumentave te uploduara ne faqe
        * Pershkrimi: Shfaq preview te nje dok
        * Return: Kthen File per preview
        **/
        [HttpGet]
        public ActionResult ShikoDokument(int dID)
        {
            try {
                byte[] byteArray;
                var dokumenti = (from d in db.Dokumenta
                                 where d.DokumentID == dID
                                 select d).SingleOrDefault();
                if (dokumenti != null)
                {
                    byteArray = dokumenti.Skedari;

                    var formati = dokumenti.Tipi;
                    var ext = dokumenti.Formati;
                    var titull = dokumenti.Titull;


                    return File(byteArray, formati);
                }
                else
                {
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }
        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShikoDokumentWordExcel
        * Arsyeja: Shfaqja(preview )e dokumenteve ne  word excel
        * Pershkrimi: shfaq dokumentin ne klikim te butonit select
        * Return: Kthen dokumetin per preview
        **/
        [HttpGet]
        public ActionResult ShikoDokumentWordExcel(int dID)
        {
            try {
                byte[] byteArray;
                var dokumenti = (from d in db.Dokumenta
                                 where d.DokumentID == dID
                                 select d).SingleOrDefault();
                if (dokumenti != null)
                {
                    byteArray = dokumenti.Skedari;

                    var formati = dokumenti.Tipi;
                    var ext = dokumenti.Formati;
                    var titull = dokumenti.Titull;
                    return File(byteArray, formati, titull + "." + ext);
                }
                else
                {
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: GetTeDhenaDokumenti
        * Arsyeja: Te dhena per preview-ne
        * Pershkrimi: merr fushat e indeksimit dhe vendndodhjen fizike
        * Parametrat: int dID - id dokumenti
        * Return: Kthen te dhenat e dokumentit per preview
        **/
        [HttpPost]
        public ActionResult GetTeDhenaDokumenti(int id)
        {
            try {
                int idAct = id;
                List<string> fushat = (from f in db.FushaIndeksimi
                                       join fi in db.FusheIndeksimiDokumente
                                       on f.FusheIndeksimiID equals fi.FusheIndeksimiID
                                       where fi.DokumentID == id
                                       select f.Emer).ToList();
                var vendi = (from d in db.Dokumenta
                             where d.DokumentID == id
                             select new { K = d.Kutia, Z = d.Zyra, R = d.Rafti }).SingleOrDefault();
                var dokumenti = (from d in db.Dokumenta
                                 where d.DokumentID == id
                                 select new { LLID = d.LlojDokumentiID, I = d.InspektimID, FormatiDok = d.Formati }).SingleOrDefault();
                List<Dokument> dokumentat = (from d in db.Dokumenta
                                             where d.LlojDokumentiID == dokumenti.LLID
                                             where d.InspektimID == dokumenti.I
                                             select d).ToList();
                string Kutia;
                string Zyra;
                string Rafti;
                Kutia = vendi.K;
                Zyra = vendi.Z;
                Rafti = vendi.R;

                dynamic DokTeDhena = new ExpandoObject();
                DokTeDhena.Fushat = fushat;
                DokTeDhena.K = Kutia;
                DokTeDhena.R = Rafti;
                DokTeDhena.Z = Zyra;
                DokTeDhena.Dokumenta = dokumentat;
                DokTeDhena.ActID = idAct;
                DokTeDhena.ActFormat = dokumenti.FormatiDok;
                return PartialView("_PreviewTeDhena", DokTeDhena);

            }

            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShkarkoDokument
        * Arsyeja: Shkarkimi inje dokumenti
        * Pershkrimi: Shkarkon dokumenti e perzgjedhur nga perdoruesi
        * Return: Kthen dokumetin per shkarkim
        **/
        [HttpGet]
        public ActionResult ShkarkoDokument(int id)
        {
            try {

                byte[] bytes;
                Dokument Dokumenti = (from d in db.Dokumenta
                                      where d.DokumentID == id
                                      select d).SingleOrDefault();
                bytes = Dokumenti.Skedari;
                string formati = Dokumenti.Tipi;
                string ext = Dokumenti.Formati;
                return File(bytes, formati, Dokumenti.Titull + "." + ext);



            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShkarkoDokumenta
        * Arsyeja: Shkarkim disa
        * Pershkrimi: Shkarkon disa dokumenta
        * Return: Kthen File zip per download
        **/
        [HttpGet]
        public ActionResult ShkarkoDokumenta(int[] dokID)
        {
            try
            {
                List<Dokument> dokumentat = new List<Dokument>();
                Dokument dok;
                int idd;
                for (var i = 0; i < dokID.Length; i++)
                {
                    idd = dokID[i];
                    dok = (from d in db.Dokumenta
                           where d.DokumentID == idd
                           select d).SingleOrDefault();
                    dokumentat.Add(dok);
                }
                using (var compressedFileStream = new MemoryStream())
                {

                    using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                    {
                        foreach (var dokument in dokumentat)
                        {
                            var zipEntry = zipArchive.CreateEntry(dokument.Titull + "." + dokument.Formati);

                            using (var originalFileStream = new MemoryStream(dokument.Skedari))
                            using (var zipEntryStream = zipEntry.Open())
                            {
                                originalFileStream.CopyTo(zipEntryStream);
                            }
                        }
                    }

                    return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "dokumenta.zip" };
                }
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }
        /**
   * Progrmues:Ervisa Cakaj
   * Metoda: UpdateDokument
   * Arsyeja: Modifikon dokument
   * Pershkrimi: Modifikon vendndodhjen fizike dhe fushat e indeksimit
   * Return: Kthen PartialView me dokumentat qe ndodhen ne te njejtin folder pas modifikimit
   **/
        public ActionResult UpdateDokument(string zyra, string rafti, string kutia, int id, string[] fushatShtim, string[] fushatHeqje)
        {
            try {
                Dokument d2 = (from d in db.Dokumenta
                               where d.DokumentID == id
                               select d).SingleOrDefault();
                d2.Zyra = zyra;
                d2.Kutia = kutia;
                d2.Rafti = rafti;
                db.SaveChanges();

                FusheIndeksimiDokument FID = new FusheIndeksimiDokument();
                string fushe;
                if (fushatHeqje != null)
                {
                    for (int i = 0; i < fushatHeqje.Length; i++)
                    {
                        fushe = fushatHeqje[i];
                        FID = (from fid in db.FusheIndeksimiDokumente
                               join f in db.FushaIndeksimi
                               on fid.FusheIndeksimiID equals f.FusheIndeksimiID
                               where fid.DokumentID == id
                               where f.Emer == fushe
                               select fid).SingleOrDefault();
                        db.FusheIndeksimiDokumente.Remove(FID);
                        db.SaveChanges();
                    }
                }

                List<FusheIndeksimi> fushaAktuale = new List<FusheIndeksimi>();
                FusheIndeksimi FusheERe = new FusheIndeksimi();
                FusheIndeksimiDokument FIDeRe = new FusheIndeksimiDokument();
                int IDnF;
                if (fushatShtim != null)
                {
                    for (int j = 0; j < fushatShtim.Length; j++)
                    {
                        fushe = fushatShtim[j];
                        fushaAktuale = (from f in db.FushaIndeksimi
                                        where f.Emer == fushe
                                        select f).ToList();
                        if (fushaAktuale.Count() == 0)
                        {
                            FusheERe.Emer = fushe;
                            db.FushaIndeksimi.Add(FusheERe);
                            db.SaveChanges();
                        }

                        IDnF = (from fuI in db.FushaIndeksimi
                                where fuI.Emer == fushe
                                select fuI.FusheIndeksimiID).SingleOrDefault();
                        FIDeRe.DokumentID = id;
                        FIDeRe.FusheIndeksimiID = IDnF;
                        db.FusheIndeksimiDokumente.Add(FIDeRe);
                        db.SaveChanges();

                    }
                }

                return ShfaqDokumentat(d2.LlojDokumentiID, d2.InspektimID);
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        private IIdentity GetIdentity()
        {
            return HttpContext.User.Identity;
        }


        [HttpPost]
        public JsonResult GetFushat(string Prefix)
        {

            var Fushat = (from f in db.FushaIndeksimi
                          where f.Emer.StartsWith(Prefix)
                          select new { f.Emer, f.FusheIndeksimiID });

            return Json(Fushat, JsonRequestBehavior.AllowGet);
        }

        /**
         * Progrmues:Ervisa Cakaj
         * Metoda: KrijoPathFiltrimi
         * Arsyeja:ker te krijuar pathin e navigimit ne faqe
         * Pershkrimi: Krijon pathin ne baze te fushave te plotesuara ne formen e filtrimi
         * Return: String - Kthen string me pathin e filtrimit
         **/
        public string KrijoPathFiltrimi(ViewModels.Filter filtri)
        {
           
            string Subjekti;
            string Inspektimi;
            string Lloji;
            string Fushat;
            string Dokumenti;
            string Zyra;
            string Rafti;
            string Kutia;
            string DataNga;
            string DataTek;
            StringBuilder SB = new StringBuilder();

            if (filtri.Subjekti != null)
            {
                Subjekti = filtri.Subjekti.ToString().Trim();
                SB.Append("Subjekti: " + Subjekti + " / ");
            }
            if (filtri.Inspektimi != null)
            {
                Inspektimi = filtri.Inspektimi.ToString().Trim();
                SB.Append("Inspektimi: " + Inspektimi + " / ");
            }
            if (filtri.Fusha != null)
            {
                Fushat = filtri.Fusha.ToString().Trim();
                SB.Append("Fushat: " + Fushat + " / ");
            }
            if (filtri.Dokumenti != null)
            {
                Dokumenti = filtri.Dokumenti.ToString().Trim();
                SB.Append("Dokumenti:" + Dokumenti + " / ");
            }
            if (filtri.Lloji != null)
            {
                Lloji = filtri.Lloji.ToString().Trim();
                string[] lloj = Lloji.Split('/');
                SB.Append("Lloji:");
                for (var i = 0; i < lloj.Length - 1; i++)
                {
                    SB.Append(lloj[i] + ",");

                }
                SB.Remove(SB.Length - 1, 1);
                SB.Append(" / ");

            }
            if (filtri.Zyra != null)
            {
                Zyra = filtri.Zyra.ToString().Trim();
                SB.Append("Zyra:" + Zyra + " / ");

            }
            if (filtri.Rafti != null)
            {
                Rafti = filtri.Rafti.ToString().Trim();
                SB.Append("Rafti:" + Rafti + " / ");

            }
            if (filtri.Kutia != null)
            {
                Kutia = filtri.Kutia.ToString().Trim();
                SB.Append("Kutia:" + Kutia + " / ");
            }
            if ((filtri.DataNga != null) && (filtri.DataTek != null))
            {
                DataNga = filtri.DataNga.ToString().Trim();
                DataTek = filtri.DataTek.ToString().Trim();
                SB.Append(DataNga + " - " + DataTek);
            }
            string pathi = SB.ToString();
            return pathi;

        }


        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: GetSubjekteByEmri
        * Arsyeja: filtron subjektet
        * Pershkrimi: Kerkon ne db Subjektet 
        * Return: list me subjekte e kerkuara
        **/
        public List<Subjekt> GetSubjekteByEmri(string sub)
        {
            var subjekte = from s in db.Subjekte
                           where s.Emer.Contains(sub)
                           select s;
            List<Subjekt> subjektet = subjekte.ToList();
            return subjektet;
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShkarkoSubjekte
        * Arsyeja: Shkarkim i subjekteve
        * Pershkrimi: Shkarkon disa subjekte
        * Return: Kthen File zip per shkarkimin e subjekteve
        **/
        [HttpGet]
        public ActionResult ShkarkoSubjekte(int[] subID)
        {
            try {
                using (var compressedFileStream = new MemoryStream())
                {

                    using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                    {
                        List<Dokument> dokumenta = new List<Dokument>();
                        int subjID;
                        for (var ind = 0; ind < subID.Length; ind++)
                        {
                            subjID = subID[ind];
                            var doks = (from d in db.Dokumenta
                                        join i in db.Inspektime
                                        on d.InspektimID equals i.InspektimID
                                        join ll in db.LlojeDokumenti
                                        on d.LlojDokumentiID equals ll.LlojDokumentiID
                                        join s in db.Subjekte
                                        on i.SubjektID equals s.SubjektID
                                        where s.SubjektID == subjID
                                        select new { Subjekti = s.Emer, Inspektimi = i.Emer, Lloji = ll.Emer, Titulli = d.Titull, Ext = d.Formati, Skedar = d.Skedari }).ToList();

                            if (doks.Count > 0)
                            {
                                foreach (var dokument in doks)
                                {

                                    var zipEntry = zipArchive.CreateEntry(dokument.Subjekti + "/" + dokument.Inspektimi + "/" + dokument.Lloji + "/" + dokument.Titulli + "." + dokument.Ext);


                                    using (var originalFileStream = new MemoryStream(dokument.Skedar))
                                    using (var zipEntryStream = zipEntry.Open())
                                    {
                                        originalFileStream.CopyTo(zipEntryStream);
                                    }
                                }
                            }

                        }

                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Subjektet.zip" };
                    }

                }

            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShkarkoInspektime
        * Arsyeja: Shkarkim i  inspektimeve
        * Pershkrimi: Shkarkon disa inspektime
        * Return: Kthen File zip per te bere shkarkimin e inspektimeve
        **/
        [HttpGet]
        public ActionResult ShkarkoInspektime(int[] insID)
        {
            try {
                using (var compressedFileStream = new MemoryStream())
                {

                    using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                    {
                        List<Dokument> dokumenta = new List<Dokument>();
                        int inspID;
                        for (var ind = 0; ind < insID.Length; ind++)
                        {
                            inspID = insID[ind];
                            var doks = (from d in db.Dokumenta
                                        join i in db.Inspektime
                                        on d.InspektimID equals i.InspektimID
                                        join ll in db.LlojeDokumenti
                                        on d.LlojDokumentiID equals ll.LlojDokumentiID
                                        where i.InspektimID == inspID
                                        select new { Inspektimi = i.Emer, Lloji = ll.Emer, Titulli = d.Titull, Ext = d.Formati, Skedar = d.Skedari }).ToList();

                            if (doks.Count > 0)
                            {
                                foreach (var dokument in doks)
                                {
                                    var zipEntry = zipArchive.CreateEntry(dokument.Inspektimi + "/" + dokument.Lloji + "/" + dokument.Titulli + "." + dokument.Ext);

                                    using (var originalFileStream = new MemoryStream(dokument.Skedar))
                                    using (var zipEntryStream = zipEntry.Open())
                                    {
                                        originalFileStream.CopyTo(zipEntryStream);
                                    }
                                }
                            }
                        }

                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Inspektimet.zip" };
                    }

                }

            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
         * Progrmues:Ervisa Cakaj
        * Metoda: ShkarkoLloje
        * Arsyeja: Shkarkimi disa llojeve 
        * Pershkrimi: Shkarkon disa lloje dokumetash ne baze te inspektimit
        * Return: Kthen File zip per shkarkim llojeve sipas inspektimit
        **/
        [HttpGet]
        public ActionResult ShkarkoLloje(int insID, int[] llojID)
        {
            try {
                using (var compressedFileStream = new MemoryStream())
                {
                    //Create an archive and store the stream in memory.
                    using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                    {
                        List<Dokument> dokumenta = new List<Dokument>();
                        int llojiID;
                        for (var ind = 0; ind < llojID.Length; ind++)
                        {
                            llojiID = llojID[ind];
                            var doks = (from d in db.Dokumenta
                                        join i in db.Inspektime
                                        on d.InspektimID equals i.InspektimID
                                        join ll in db.LlojeDokumenti
                                        on d.LlojDokumentiID equals ll.LlojDokumentiID
                                        where i.InspektimID == insID
                                        where ll.LlojDokumentiID == llojiID
                                        select new { Lloji = ll.Emer, Titulli = d.Titull, Ext = d.Formati, Skedar = d.Skedari }).ToList();

                            if (doks.Count > 0)
                            {
                                foreach (var dokument in doks)
                                {

                                    var zipEntry = zipArchive.CreateEntry(dokument.Lloji + "/" + dokument.Titulli + "." + dokument.Ext);

                                    using (var originalFileStream = new MemoryStream(dokument.Skedar))
                                    using (var zipEntryStream = zipEntry.Open())
                                    {
                                        originalFileStream.CopyTo(zipEntryStream);
                                    }
                                }
                            }
                        }

                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = "Llojet.zip" };
                    }

                }

            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShkarkoFolderZIP
        * Arsyeja: Shkarkim zip
        * Pershkrimi: Shkarkon zip 
        * Return: Kthen File zip per shkarkimin e disa elementeve
        **/
        [HttpGet]
        public ActionResult ShkarkoFolderZIP(int tipi, int idll, int idi, int ids)
        {
            try
            {
                if (tipi == 2)
                {
                    var doks = (from d in db.Dokumenta
                                where d.LlojDokumentiID == idll
                                where d.InspektimID == idi
                                select d).ToList();

                    var lloji = (from ll in db.LlojeDokumenti
                                 where ll.LlojDokumentiID == idll
                                 select ll.Emer).SingleOrDefault();
                    var llojjj = lloji.ToString();
                    using (var compressedFileStream = new MemoryStream())
                    {

                        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                        {
                            if (doks.Count > 0)
                            {
                                foreach (var dokument in doks)
                                {
                                    var zipEntry = zipArchive.CreateEntry(dokument.Titull + "." + dokument.Formati);

                                    using (var originalFileStream = new MemoryStream(dokument.Skedari))
                                    using (var zipEntryStream = zipEntry.Open())
                                    {
                                        originalFileStream.CopyTo(zipEntryStream);
                                    }
                                }
                            }
                        }


                        Response.AddHeader("Content-Disposition", "attachment; filename=" + lloji + ".zip");
                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = llojjj + ".zip" };
                    }
                }

                else if (tipi == 1)
                {

                    var doks = (from d in db.Dokumenta
                                join ll in db.LlojeDokumenti
                                on d.LlojDokumentiID equals ll.LlojDokumentiID
                                where d.InspektimID == idi
                                select new { Titulli = d.Titull, Ext = d.Formati, Tipi = d.Tipi, Skedar = d.Skedari, Lloji = ll.Emer }).ToList();
                    var inspektimi = (from i in db.Inspektime
                                      where i.InspektimID == idi
                                      select i.Emer).SingleOrDefault();
                    string inss = inspektimi.ToString();
                    using (var compressedFileStream = new MemoryStream())
                    {
                        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                        {
                            if (doks.Count > 0)
                            {
                                foreach (var dokument in doks)
                                {


                                    var zipEntry = zipArchive.CreateEntry(dokument.Lloji + "/" + dokument.Titulli + "." + dokument.Ext);

                                    using (var originalFileStream = new MemoryStream(dokument.Skedar))
                                    using (var zipEntryStream = zipEntry.Open())
                                    {

                                        originalFileStream.CopyTo(zipEntryStream);
                                    }
                                }
                            }
                        }


                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = inss + ".zip" };
                    }

                }
                else if (tipi == 0)
                {

                    var doks = (from d in db.Dokumenta
                                join i in db.Inspektime
                                on d.InspektimID equals i.InspektimID
                                join s in db.Subjekte
                                on i.SubjektID equals s.SubjektID
                                join ll in db.LlojeDokumenti
                                on d.LlojDokumentiID equals ll.LlojDokumentiID
                                where s.SubjektID == ids
                                select new { Inspektimi = i.Emer, Lloji = ll.Emer, Skedari = d.Skedari, Ext = d.Formati, Tipi = d.Tipi, Titulli = d.Titull }).ToList();
                    string Subjekti = (from s in db.Subjekte
                                       where s.SubjektID == ids
                                       select s.Emer).SingleOrDefault();
                    string sss = Subjekti.ToString();

                    using (var compressedFileStream = new MemoryStream())
                    {

                        using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                        {
                            if (doks.Count > 0)
                            {
                                foreach (var dokument in doks)
                                {


                                    var zipEntry = zipArchive.CreateEntry(dokument.Inspektimi + "/" + dokument.Lloji + "/" + dokument.Titulli + "." + dokument.Ext);


                                    using (var originalFileStream = new MemoryStream(dokument.Skedari))
                                    using (var zipEntryStream = zipEntry.Open())
                                    {

                                        originalFileStream.CopyTo(zipEntryStream);
                                    }
                                }
                            }
                        }

                        return new FileContentResult(compressedFileStream.ToArray(), "application/zip") { FileDownloadName = sss + ".zip" };
                    }

                }
                else
                {
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: ShfaqFolderat
        * Arsyeja: Kerkimi i folderave en databaze dhe shfaqja e tyre ne view
        * Pershkrimi: Merr elementet qe do te shfaqen ne db dhe i kthen ato tek partialView per t'i listuar
        * Return: PartialView -Kthen PartialView me folderat qe do te shfaqen ne te
        **/
        public ActionResult ShfaqFolderat(int ElemId, int niv)
        {
            try {
                dynamic PartialModel = new ExpandoObject();
                string path = "Subjektet";
                var pathID = new List<int>();

                if (niv == -1)
                {
                    var sub = from s in db.Subjekte
                              select s;
                    PartialModel.Foldera = sub.ToList();
                    PartialModel.Niveli = 0;
                    PartialModel.Path = path;
                    PartialModel.PathID = pathID;
                }

                else if (niv == 0)
                {

                    var ins = from i in db.Inspektime
                              join s in db.Subjekte
                              on i.SubjektID equals s.SubjektID
                              where s.SubjektID == ElemId
                              select i;
                    var Subj = (from s in db.Subjekte
                                where s.SubjektID == ElemId
                                select new { Emri = s.Emer, ID = s.SubjektID }).Single();
                    path += "/";
                    path += Subj.Emri;
                    pathID.Add(Subj.ID);
                    PartialModel.Foldera = ins.ToList();
                    PartialModel.Niveli = 1;
                    PartialModel.Path = path;
                    PartialModel.PathID = pathID;
                }
                else if (niv == 1)
                {

                    var lloje = from lli in db.LlojDokumentiInspektime
                                join ll in db.LlojeDokumenti
                                on lli.LlojDokumentiID equals ll.LlojDokumentiID
                                where lli.InspektimID == ElemId
                                select ll;
                    var Insp = (from s in db.Subjekte
                                join i in db.Inspektime
                                on s.SubjektID equals i.SubjektID
                                where i.InspektimID == ElemId
                                select new { EmriI = i.Emer, IDI = i.InspektimID, EmriS = s.Emer, IDS = s.SubjektID }).Single();
                    path += "/";
                    path += Insp.EmriS;
                    path += "/";
                    path += Insp.EmriI;
                    pathID.Add(Insp.IDS);
                    pathID.Add(Insp.IDI);
                    PartialModel.Foldera = lloje.ToList();
                    PartialModel.Niveli = 2;
                    PartialModel.Path = path;
                    PartialModel.PathID = pathID;
                }


                return PartialView("_Foldera", PartialModel);
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Metoda: ShfaqDokumentat
        * Arsyeja: Kerkimi ne db i dokumentave qe do te shfaqen dhe kthimi i tyre
        * Pershkrimi: Merr dokumentat qe do te shfaqen ne db dhe i kthen ato ne view
        * Return Kthen partial view me dokumeta
        **/
        public ActionResult ShfaqDokumentat(int LlojID, int InspID)
        {
            try {
                dynamic PartialModel = new ExpandoObject();
                string path = "Subjektet";
                var pathID = new List<int>();

                var dok = (from d in db.Dokumenta
                           join ldi in db.LlojDokumentiInspektime
                           on new { d.LlojDokumentiID, d.InspektimID } equals new { ldi.LlojDokumentiID, ldi.InspektimID }
                           where (ldi.LlojDokumentiID == LlojID)
                           where (ldi.InspektimID == InspID)
                           select d);
                var Insp = (from i in db.Inspektime
                            join s in db.Subjekte
                            on i.SubjektID equals s.SubjektID
                            where i.InspektimID == InspID
                            select new { EmerI = i.Emer, EmerS = s.Emer, IDS = s.SubjektID }).Single();
                var Lloj = (from ll in db.LlojeDokumenti
                            where ll.LlojDokumentiID == LlojID
                            select ll.Emer).Single();


                path += "/";
                path += Insp.EmerS;
                path += "/";
                path += Insp.EmerI;
                path += "/";
                path += Lloj;
                pathID.Add(Insp.IDS);
                pathID.Add(InspID);
                pathID.Add(LlojID);
                PartialModel.Foldera = dok.ToList();
                PartialModel.Niveli = 3;
                PartialModel.Path = path;
                PartialModel.PathID = pathID;

                return PartialView("_Foldera", PartialModel);
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: DeleteDok
        * Arsyeja: Fshirja e nje dokumenti
        * Pershkrimi: Fshin dokumentin e perzgjedhurnga perdoruesi ne raste se eshte perdorues krijues si dokumetit
        * Return: Kthen PartialView me dokumtate e tjere te mbetur
        **/
        public ActionResult DeleteDok(int id)
        {
            try {
                Dokument D = (from d in db.Dokumenta
                              where d.DokumentID == id
                              select d).Single();
                var SubjId = D.Inspektim.SubjektID;
                var Subj = D.Inspektim.Subjekt.Emer;
                var InspID = D.InspektimID;
                var Insp = D.Inspektim.Emer;
                var LlojID = D.LlojDokumentiID;
                var Lloj = D.LlojDokumenti.Emer;
                var pathID = new List<int>();
                string path = "Subjektet";
                dynamic PartialModel = new ExpandoObject();

                if (GetIdentity().Name == D.ApplicationUser.UserName)
                {
                    db.Dokumenta.Remove(D);
                    db.SaveChanges();
                    ViewBag.Message = "Dokumenti u fshi me sukses!";
                    List<Dokument> dokumenta = (from d in db.Dokumenta
                                                where d.InspektimID == InspID
                                                where d.LlojDokumentiID == LlojID
                                                select d).ToList();
                    if (dokumenta.Count() > 0)
                    {
                        path += "/";
                        path += Subj;
                        path += "/";
                        path += Insp;
                        path += "/";
                        path += Lloj;
                        pathID.Add(SubjId);
                        pathID.Add(InspID);
                        pathID.Add(LlojID);
                        PartialModel.Foldera = dokumenta.ToList();
                        PartialModel.Niveli = 3;
                        PartialModel.Path = path;
                        PartialModel.PathID = pathID;

                    }
                    else
                    {
                        LlojDokumentiInspektim llojins = (from ll in db.LlojDokumentiInspektime
                                                          where ll.InspektimID == InspID
                                                          where ll.LlojDokumentiID == LlojID
                                                          select ll).Single();
                        db.LlojDokumentiInspektime.Remove(llojins);
                        db.SaveChanges();

                        List<LlojDokumenti> llojePerInspektim = (from lli in db.LlojDokumentiInspektime
                                                                 join ll in db.LlojeDokumenti
                                                     on lli.LlojDokumentiID equals ll.LlojDokumentiID
                                                                 where lli.InspektimID == InspID
                                                                 select ll).ToList();
                        if (llojePerInspektim.Count() < 1)
                        {
                            Inspektim i1 = (from i in db.Inspektime
                                            where i.InspektimID == InspID
                                            where i.SubjektID == SubjId
                                            select i).Single();
                            db.Inspektime.Remove(i1);
                            db.SaveChanges();

                            List<Inspektim> Inspektimet = (from i in db.Inspektime
                                                           where i.SubjektID == SubjId
                                                           select i).ToList();
                            if (Inspektimet.Count > 0)
                            {
                                PartialModel.Foldera = Inspektimet.ToList();
                                path += "/";
                                path += Subj;
                                pathID.Add(SubjId);
                                PartialModel.Niveli = 1;
                                PartialModel.Path = path;
                                PartialModel.PathID = pathID;
                            }
                            else
                            {
                                var sub = from s in db.Subjekte
                                          select s;
                                PartialModel.Foldera = sub.ToList();
                                PartialModel.Niveli = 0;
                                PartialModel.Path = path;
                                PartialModel.PathID = pathID;
                            }
                        }
                        else
                        {
                            List<LlojDokumenti> llojet = (from ll in db.LlojeDokumenti
                                                          join lli in db.LlojDokumentiInspektime
                                                          on ll.LlojDokumentiID equals lli.LlojDokumentiID
                                                          where lli.InspektimID == InspID
                                                          select ll).ToList();
                            path += "/";
                            path += Subj;
                            path += "/";
                            path += Insp;
                            pathID.Add(SubjId);
                            pathID.Add(InspID);
                            PartialModel.Foldera = llojet.ToList();
                            PartialModel.Niveli = 2;
                            PartialModel.Path = path;
                            PartialModel.PathID = pathID;
                            ViewBag.Message = "Dokumenti u fshi me sukses!";
                        }

                    }
                    return PartialView("_Foldera", PartialModel);
                }
                else
                {
                    List<Dokument> dokumenta = (from d in db.Dokumenta
                                                where d.InspektimID == InspID
                                                where d.LlojDokumentiID == LlojID
                                                select d).ToList();
                    path += "/";
                    path += Subj;
                    path += "/";
                    path += Insp;
                    path += "/";
                    path += Lloj;
                    pathID.Add(SubjId);
                    pathID.Add(InspID);
                    pathID.Add(LlojID);
                    PartialModel.Foldera = dokumenta.ToList();
                    PartialModel.Niveli = 3;
                    PartialModel.Path = path;
                    PartialModel.PathID = pathID;
                    ViewBag.Message = "Dokumenti mund te fshihet vetem nga perdoruesi qe e ka krijuar ate!";
                    return PartialView("_Foldera", PartialModel);
                }
            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }


   

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: getInspektimetBySubjektOrNot
        * Arsyeja: Filtron inspektimet sipas emrit dhe sipas subjektit nese eshte percaktuar
        * Pershkrimi: Kerkon ne db Inspektimet me numrin e plotesuar dhe nese eshte plotesuar fusha e subjektit kerkon edhe sipas subjektit
        * Return:  kthen nje liste me inspektime sipas kerkimit
        **/
        public List<Inspektim> getInspektimetBySubjektOrNot(string sub, int? ins)
        {
            
            if (sub.IsNullOrWhiteSpace())
            {
                var inspektime = from i in db.Inspektime
                                 where i.Numer == ins
                                 select i;
                List<Inspektim> inspektimet = inspektime.ToList();
                return inspektimet;
            }
            else
            {
                var inspektime = from i in db.Inspektime
                                 join s in db.Subjekte
                                 on i.SubjektID equals s.SubjektID
                                 where i.Numer == ins
                                 where s.Emer.Contains(sub)
                                 select i;
                List<Inspektim> inspektimet = inspektime.ToList();
                return inspektimet;
            }

        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: GetLloje
        * Arsyeja: Per te marre Llojet sipas llojeve te perzgjedhura ne filter
        * Pershkrimi: Kerkon ne db te dhenat per llojet e perzgjedhura nga perdoruesi
        * Return: kthen listen me te dhenat e llojeve se bashku me inspektimet
        **/
        public List<LlojDokumentiInspektim> GetLloje(string llojet)
        {
            
            List<LlojDokumentiInspektim> lloje = new List<LlojDokumentiInspektim>();
            string[] lloj = llojet.Split('/');
            string l;
            for (var index = 0; index < lloj.Length - 1; index++)
            {
                l = lloj[index];
                List<LlojDokumentiInspektim> a = (from lld in db.LlojeDokumenti
                                                  join li in db.LlojDokumentiInspektime
                                                  on lld.LlojDokumentiID equals li.LlojDokumentiID
                                                  join i in db.Inspektime
                                                  on li.InspektimID equals i.InspektimID
                                                  where lld.Emer == l
                                                  select li).ToList();
                lloje.AddRange(a);
            }

            return lloje;

        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: GetLlojeBySubjektOrInspektim
        * Arsyeja: Per te marre Llojet sipas llojit, subjektit dhe inspektimit te perzgjedhura ne filter
        * Pershkrimi: Kerkon ne db te dhenat per llojet sipas inspektimit dhe subjektit
        * Return: kthen listen me te dhenat e llojeve se bashku me inspektimet
        **/
        public List<LlojDokumentiInspektim> GetLlojeBySubjektOrInspektim(string llojet, int? inspektimi, string subjekti)
        {
            
            List<LlojDokumentiInspektim> lloje = new List<LlojDokumentiInspektim>();
            string[] lloj = llojet.Split('/');
            string l;
            if (inspektimi == null)
            {
                for (var index = 0; index < lloj.Length - 1; index++)
                {
                    l = lloj[index];
                    List<LlojDokumentiInspektim> a = (from lld in db.LlojeDokumenti
                                                      join li in db.LlojDokumentiInspektime
                                                      on lld.LlojDokumentiID equals li.LlojDokumentiID
                                                      join i in db.Inspektime
                                                      on li.InspektimID equals i.InspektimID
                                                      join s in db.Subjekte
                                                      on i.SubjektID equals s.SubjektID
                                                      where lld.Emer == l
                                                      where s.Emer.Contains(subjekti)
                                                      select li).ToList();
                    lloje.AddRange(a);
                }
            }
            else if (subjekti.IsNullOrWhiteSpace() == true)
            {
                for (var index = 0; index < lloj.Length - 1; index++)
                {
                    l = lloj[index];
                    List<LlojDokumentiInspektim> a = (from lld in db.LlojeDokumenti
                                                      join li in db.LlojDokumentiInspektime
                                                      on lld.LlojDokumentiID equals li.LlojDokumentiID
                                                      join i in db.Inspektime
                                                      on li.InspektimID equals i.InspektimID
                                                      join s in db.Subjekte
                                                      on i.SubjektID equals s.SubjektID
                                                      where lld.Emer == l
                                                      where i.InspektimID == inspektimi
                                                      select li).ToList();
                    lloje.AddRange(a);
                }
            }
            else
            {
                for (var index = 0; index < lloj.Length - 1; index++)
                {

                    l = lloj[index];
                    List<LlojDokumentiInspektim> a = (from lld in db.LlojeDokumenti
                                                      join li in db.LlojDokumentiInspektime
                                                      on lld.LlojDokumentiID equals li.LlojDokumentiID
                                                      join i in db.Inspektime
                                                      on li.InspektimID equals i.InspektimID
                                                      join s in db.Subjekte
                                                      on i.SubjektID equals s.SubjektID
                                                      where lld.Emer == l
                                                      where i.InspektimID == inspektimi
                                                      where s.Emer.Contains(subjekti)
                                                      select li).ToList();

                    lloje.AddRange(a);
                }
            }



            return lloje;

        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: getDokumentaByEmri
        * Arsyeja: Per te filtruar dokumentat sipas emrit
        * Pershkrimi: Kerkon ne db dokumentat, emrat e te cileve permbajne tekstin e shkruar ne filter
        * Return:  Kthen listen me dokumenta sipas emrit
        **/
        public List<Dokument> getDokumentaByEmri(string emri)
        {
            var dokumenta = from d in db.Dokumenta
                            where d.Titull.Contains(emri)
                            select d;
            List<Dokument> dokumentat = dokumenta.ToList();
            return dokumentat;
        }

        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: getDokumentaByFushat
        * Arsyeja: Per te filtruar dokumentat sipas fushave te dokumentit (data, emri, fusha indeksimi, vendndodhje fizike)
        * Pershkrimi: Kerkon ne db dokumentat sipas fushave te tyre
        * Return: Kthen listen me dokumenta sipas fushave
        **/
        public List<Dokument> getDokumentaByFushat(string dt1, string dt2, string emri, string fusha, string zyra, string rafti, string kutia)
        {
           
            var dokumenta = from d in db.Dokumenta
                            select d;
            if (emri.IsNullOrWhiteSpace() == false)
            {
                dokumenta = dokumenta.Where(dok => dok.Titull.Contains(emri));
            }
            if (rafti.IsNullOrWhiteSpace() == false)
            {
                dokumenta = dokumenta.Where(dok => dok.Rafti.Contains(rafti));
            }
            if (kutia.IsNullOrWhiteSpace() == false)
            {
                dokumenta = dokumenta.Where(dok => dok.Kutia.Contains(kutia));
            }
            if (zyra.IsNullOrWhiteSpace() == false)
            {
                dokumenta = dokumenta.Where(dok => dok.Zyra.Contains(zyra));
            }
            if ((dt1.IsNullOrWhiteSpace() == false) && (dt2.IsNullOrWhiteSpace() == false))
            {
                CultureInfo fr = new CultureInfo("fr-FR");
                DateTime d1 = Convert.ToDateTime(dt1, fr);
                DateTime d2 = Convert.ToDateTime(dt2, fr);
                dokumenta = dokumenta.Where(dok => dok.DateRegjistrimi > d1);
                dokumenta = dokumenta.Where(dok => dok.DateRegjistrimi < d2);
            }


            if (fusha.IsNullOrWhiteSpace() == false)
            {

                List<Dokument> DD = new List<Dokument>();
                List<Dokument> doks = new List<Dokument>();
                string[] fu = fusha.Split(',');
                for (int t = 0; t < fu.Length; t++)
                {
                    string elem = fu[t].Trim();
                    doks = (from d in db.Dokumenta
                            join fid in db.FusheIndeksimiDokumente
                            on d.DokumentID equals fid.DokumentID
                            join fi in db.FushaIndeksimi
                            on fid.FusheIndeksimiID equals fi.FusheIndeksimiID
                            where fi.Emer.Contains(elem)
                            select d).ToList();
                    DD.AddRange(doks);
                }
                DD = DD.Distinct().ToList();
                var DokumentaALL = DD.Intersect(dokumenta).ToList();
                return DokumentaALL;
            }


            return dokumenta.ToList();
        }


        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: Filtro
        * Arsyeja: Per te shfaqur elementet sipas filtrit
        * Pershkrimi: Kontrollon cilat fusha te filtrit jane plotesuar dhe therret filtrin perkates.
        * Return: PartialView - Kthen listen me dokumenta/foldera sipas filtrit
        **/
        public ActionResult Filtro(ViewModels.Filter f)
        {
            try
            {
                var filterPath = KrijoPathFiltrimi(f);
                dynamic PartialModel = new ExpandoObject();
                PartialModel.Path = filterPath;

                bool vendndodhja = false;
                if ((f.Kutia != null) || (f.Rafti != null) || (f.Zyra != null))
                {
                    vendndodhja = true;
                }

                if ((f.Fusha == null) && (f.Dokumenti == null) && ((f.DataNga == null) || (f.DataTek == null)) && (vendndodhja == false) && (f.Lloji == null) && (f.Inspektimi == null) && (f.Subjekti == null))
                {
                    var pathID = new List<int>();
                    var sub = from s in db.Subjekte
                              select s;
                    PartialModel.Foldera = sub.ToList();
                    PartialModel.Niveli = 0;
                    PartialModel.Path = "Subjektet";
                    PartialModel.PathID = pathID;
                    return PartialView("_Foldera", PartialModel);
                }

                else if ((f.Fusha == null) && (f.Dokumenti == null) && ((f.DataNga == null) || (f.DataTek == null)) && (vendndodhja == false))
                {
                    if (f.Lloji == null)
                    {
                        if (f.Inspektimi == null)
                        {
                            List<Subjekt> subjektet = GetSubjekteByEmri(f.Subjekti);
                            PartialModel.Foldera = subjektet;
                            PartialModel.LlojFolderi = 0;
                            return PartialView("_FolderaFilter", PartialModel);
                        }
                        else
                        {
                            List<Inspektim> inspektimet = getInspektimetBySubjektOrNot(f.Subjekti, f.Inspektimi);
                            PartialModel.Foldera = inspektimet;
                            PartialModel.LlojFolderi = 1;
                            return PartialView("_FolderaFilter", PartialModel);
                        }
                    }
                    else
                    {
                        if ((f.Inspektimi == null) && (f.Subjekti == null))
                        {
                            List<LlojDokumentiInspektim> llojet = GetLloje(f.Lloji);
                            PartialModel.Foldera = llojet;
                            PartialModel.LlojFolderi = 2;
                            return PartialView("_FolderaFilter", PartialModel);
                        }
                        else
                        {
                            List<LlojDokumentiInspektim> llojet = GetLlojeBySubjektOrInspektim(f.Lloji, f.Inspektimi, f.Subjekti);
                            PartialModel.Foldera = llojet;
                            PartialModel.LlojFolderi = 2;
                            return PartialView("_FolderaFilter", PartialModel);
                        }

                    }
                }
                else if ((f.Subjekti == null) && (f.Inspektimi == null) && (f.Lloji == null))
                {
                    List<Dokument> dokumenta = getDokumentaByFushat(f.DataNga, f.DataTek, f.Dokumenti, f.Fusha, f.Zyra, f.Rafti, f.Kutia);
                    PartialModel.Foldera = dokumenta;
                    PartialModel.LlojFolderi = 3;
                    return PartialView("_FolderaFilter", PartialModel);
                }
                else
                {
                    List<Dokument> dokumentat = GetDokumentatByFilter(f.Subjekti, f.Inspektimi, f.Lloji, f.Dokumenti, f.Fusha, f.Zyra, f.Rafti, f.Kutia, f.DataNga, f.DataTek);
                    PartialModel.Foldera = dokumentat.ToList();
                    PartialModel.LlojFolderi = 4;
                    return PartialView("_FolderaFilter", PartialModel);
                }


            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }
        }

        public ActionResult Shto(ViewModels.UploadDokument UploadDok)
        {

            try
            {

                List<LlojDokumentiInspektim> llojInsp = (from lli in db.LlojDokumentiInspektime
                                                         where lli.InspektimID == UploadDok.Inspektimi
                                                         where lli.LlojDokumentiID == UploadDok.Lloji
                                                         select lli).ToList();
                if (llojInsp.Count() == 0)
                {
                    LlojDokumentiInspektim lli1 = new LlojDokumentiInspektim();
                    lli1.LlojDokumentiID = UploadDok.Lloji;
                    lli1.InspektimID = UploadDok.Inspektimi;
                    db.LlojDokumentiInspektime.Add(lli1);
                    db.SaveChanges();
                }

                foreach (HttpPostedFileBase fileup in UploadDok.Skedari)
                {



                    HttpPostedFileBase postedFile = fileup;
                    byte[] bytes;

                    using (Stream fs = postedFile.InputStream)
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            bytes = br.ReadBytes((Int32)fs.Length);
                        }
                        Dokument dok1 = new Dokument();
                        DateTime rn = DateTime.Now;
                        int index = postedFile.FileName.LastIndexOf(".");
                        string filenm = postedFile.FileName.ToString();
                        dok1.Titull = postedFile.FileName.Substring(0, index);
                        dok1.LlojDokumentiID = UploadDok.Lloji;
                        dok1.InspektimID = UploadDok.Inspektimi;
                        dok1.ApplicationUserID = GetIdentity().GetUserId();
                        dok1.DateRegjistrimi = rn;
                        dok1.Skedari = bytes;
                        dok1.Tipi = postedFile.ContentType;
                        dok1.Formati = postedFile.FileName.Substring(index + 1);
                        dok1.Rafti = UploadDok.Rafti;
                        dok1.Kutia = UploadDok.Kutia;
                        dok1.Zyra = UploadDok.Zyra;
                        db.Dokumenta.Add(dok1);
                        db.SaveChanges();

                        if (UploadDok.Fusha.IsNullOrWhiteSpace() == false)
                        {
                            List<FusheIndeksimi> fushaAktuale = new List<FusheIndeksimi>();
                            FusheIndeksimi FusheERe = new FusheIndeksimi();
                            List<FusheIndeksimiDokument> FDCheck = new List<FusheIndeksimiDokument>();
                            FusheIndeksimiDokument FD = new FusheIndeksimiDokument();
                            string elem = "";
                            string[] fu = UploadDok.Fusha.Split(',');
                            for (int t = 0; t < fu.Length - 1; t++)
                            {
                                elem = fu[t].Trim();
                                fushaAktuale = (from f in db.FushaIndeksimi
                                                where f.Emer.Equals(elem)
                                                select f).ToList();
                                if (fushaAktuale.Count() == 0)
                                {
                                    FusheERe.Emer = elem;
                                    db.FushaIndeksimi.Add(FusheERe);
                                    db.SaveChanges();
                                }
                            }
                            int idfushe;
                            for (int t = 0; t < fu.Length - 1; t++)
                            {
                                elem = fu[t].Trim();
                                idfushe = (from f in db.FushaIndeksimi
                                           where f.Emer.Equals(elem)
                                           select f.FusheIndeksimiID).SingleOrDefault();
                                FDCheck = (from fd in db.FusheIndeksimiDokumente
                                           where fd.DokumentID == dok1.DokumentID
                                           where fd.FusheIndeksimiID == idfushe
                                           select fd).ToList();
                                if (FDCheck.Count() == 0)
                                {

                                    FD.DokumentID = dok1.DokumentID;
                                    FD.FusheIndeksimiID = idfushe;
                                    db.FusheIndeksimiDokumente.Add(FD);
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }

                dynamic PartialModel = new ExpandoObject();
                string path = "Subjektet";
                var pathID = new List<int>();

                var dok = (from d in db.Dokumenta
                           join ldi in db.LlojDokumentiInspektime
                           on new { d.LlojDokumentiID, d.InspektimID } equals new { ldi.LlojDokumentiID, ldi.InspektimID }
                           where (ldi.LlojDokumentiID == UploadDok.Lloji)
                           where (ldi.InspektimID == UploadDok.Inspektimi)
                           select d);
                var Insp = (from i in db.Inspektime
                            join s in db.Subjekte
                            on i.SubjektID equals s.SubjektID
                            where i.InspektimID == UploadDok.Inspektimi
                            select i).Single();
                var Lloj = (from ll in db.LlojeDokumenti
                            where ll.LlojDokumentiID == UploadDok.Lloji
                            select ll.Emer).Single();

                path += "/";
                path += Insp.Subjekt.Emer;
                path += "/";
                path += Insp.Emer;
                path += "/";
                path += Lloj;
                pathID.Add(Insp.SubjektID);
                pathID.Add(Insp.InspektimID);
                pathID.Add(UploadDok.Lloji);
                PartialModel.Foldera = dok.ToList();
                PartialModel.Niveli = 3;
                PartialModel.Path = path;
                PartialModel.PathID = pathID;
                PartialModel.Shtuar = 1;
                ViewBag.Lloje = db.LlojeDokumenti.ToList();

                return View("Index", PartialModel);

            }
            catch (NullReferenceException)
            {
                return View("Error");
            }
            catch (DivideByZeroException)
            {
                return View("Error");
            }

        }
        /**
   * Progrmues:Ervisa Cakaj
   * Metoda: ErrorPage404
   * Arsyeja: ErrorPage404 ne raste se faqja e kerkuar nga perdoruesi nuk gjendet
   * Return:  ErrorPage404
   **/

        public ActionResult ErrorPage404()
        {
            return View();
        }

        /**
  * Progrmues:Ervisa Cakaj
  * Metoda: EXHandleErrorAttributeApproch
  * Arsyeja: Hanle error
  * Pershkrimi: për të trajtuar një exeption i cili është rrjedhoje e ActionMethod
  * Return:  EXHandleErrorAttributeApproch
  **/
        public ActionResult EXHandleErrorAttributeApproch()
        {
            throw new Exception("Error While Processing");
        }

        public string Nuk()
        {
            return "Dokumentet e ketij formati nuk mund te shihen si preview por mund te shihen duke u shkarkuar!";
        }


        /**
        * Progrmues:Ervisa Cakaj
        * Metoda: GetDokumentatByFilter
        * Arsyeja: Per te filtruar dokumentat sipas te gjithe fushave te filtrit
        * Pershkrimi: Kerkon ne db dokumentat sipas fushave te filtrimit
        * Return:  Kthen listen me dokumenta sipas filtrit
        **/
        public List<Dokument> GetDokumentatByFilter(string subjekti, int? inspektimi, string lloji, string dokumenti, string fusha, string zyra, string rafti, string kutia, string data1, string data2)
        {
            IQueryable<Dokument> alldok = from d in db.Dokumenta
                                          join f in db.FusheIndeksimiDokumente
                                          on d.DokumentID equals f.DokumentID
                                          join ll in db.LlojeDokumenti
                                          on d.LlojDokumentiID equals ll.LlojDokumentiID
                                          join lli in db.LlojDokumentiInspektime
                                          on ll.LlojDokumentiID equals lli.LlojDokumentiID
                                          join i in db.Inspektime
                                          on lli.InspektimID equals i.InspektimID
                                          select d;

            if (subjekti.IsNullOrWhiteSpace() == false)
            {
                alldok = alldok.Where(dok => dok.Inspektim.Subjekt.Emer.Contains(subjekti));
            }
            if (inspektimi.ToString().IsNullOrWhiteSpace() == false)
            {
                alldok = alldok.Where(dok => dok.Inspektim.Emer.Replace("Inspektim", "").Equals(inspektimi.ToString()));
            }
            if (dokumenti.IsNullOrWhiteSpace() == false)
            {
                alldok = alldok.Where(dok => dok.Titull.Contains(dokumenti));
            }
            if (rafti.IsNullOrWhiteSpace() == false)
            {
                alldok = alldok.Where(dok => dok.Rafti.Contains(rafti));
            }
            if (kutia.IsNullOrWhiteSpace() == false)
            {
                alldok = alldok.Where(dok => dok.Kutia.Contains(kutia));
            }
            if (zyra.IsNullOrWhiteSpace() == false)
            {
                alldok = alldok.Where(dok => dok.Zyra.Contains(zyra));
            }
            if ((data1.IsNullOrWhiteSpace() == false) && (data2.IsNullOrWhiteSpace() == false))
            {
                CultureInfo fr = new CultureInfo("fr-FR");
                DateTime d1 = Convert.ToDateTime(data1, fr);
                DateTime d2 = Convert.ToDateTime(data2, fr);
                alldok = alldok.Where(dok => dok.DateRegjistrimi > d1);
                alldok = alldok.Where(dok => dok.DateRegjistrimi < d2);
            }

            if ((lloji.IsNullOrWhiteSpace() == false) && (fusha.IsNullOrWhiteSpace() == false))
            {

                string[] lloj = lloji.Split('/');
                string l;
                List<Dokument> dokumentalloj = new List<Dokument>();
                for (var index = 0; index < lloj.Length - 1; index++)
                {
                    l = lloj[index];
                    List<Dokument> doklloj = (from d in db.Dokumenta
                                              where d.LlojDokumenti.Emer.Contains(l)
                                              select d).ToList();
                    dokumentalloj.AddRange(doklloj);
                }
                dokumentalloj = dokumentalloj.Distinct().ToList();

                List<Dokument> dokumentafushe = new List<Dokument>();
                List<Dokument> dokf = new List<Dokument>();
                string[] fu = fusha.Split(',');
                for (int t = 0; t < fu.Length; t++)
                {
                    string elem = fu[t].Trim();
                    dokf = (from d in db.Dokumenta
                            join fid in db.FusheIndeksimiDokumente
                            on d.DokumentID equals fid.DokumentID
                            join fi in db.FushaIndeksimi
                            on fid.FusheIndeksimiID equals fi.FusheIndeksimiID
                            where fi.Emer.Contains(elem)
                            select d).ToList();
                    dokumentafushe.AddRange(dokf);
                }
                dokumentafushe = dokumentafushe.Distinct().ToList();

                List<Dokument> dokumenta = alldok.ToList();
                dokumenta = dokumenta.Intersect(dokumentalloj).ToList();
                dokumenta = dokumenta.Intersect(dokumentafushe).ToList();
                return dokumenta.ToList();
            }
            else if (lloji.IsNullOrWhiteSpace() == false)
            {
                string[] lloj = lloji.Split('/');
                string l;
                List<Dokument> dokumentalloj = new List<Dokument>();
                for (var index = 0; index < lloj.Length - 1; index++)
                {
                    l = lloj[index];
                    List<Dokument> doklloj = (from d in db.Dokumenta
                                              where d.LlojDokumenti.Emer.Contains(l)
                                              select d).ToList();
                    dokumentalloj.AddRange(doklloj);
                }
                dokumentalloj = dokumentalloj.Distinct().ToList();
                List<Dokument> dokumenta = alldok.ToList();
                dokumenta = dokumenta.Intersect(dokumentalloj).ToList();
                return dokumenta;
            }
            else if (fusha.IsNullOrWhiteSpace() == false)
            {
                List<Dokument> dokumentafushe = new List<Dokument>();
                List<Dokument> dokf = new List<Dokument>();
                string elem;
                string[] fu = fusha.Split(',');
                for (int t = 0; t < fu.Length; t++)
                {
                    elem = fu[t].Trim();
                    dokf = (from d in db.Dokumenta
                            join fid in db.FusheIndeksimiDokumente
                            on d.DokumentID equals fid.DokumentID
                            join fi in db.FushaIndeksimi
                            on fid.FusheIndeksimiID equals fi.FusheIndeksimiID
                            where fi.Emer.Contains(elem)
                            select d).ToList();
                    dokumentafushe.AddRange(dokf);
                }
                dokumentafushe = dokumentafushe.Distinct().ToList();

                List<Dokument> dokk = alldok.ToList();
                dokk = dokk.Intersect(dokumentafushe).ToList();
                return dokk;
            }

            List<Dokument> dokumentat = alldok.ToList();
            dokumentat = dokumentat.Distinct().ToList();
            return dokumentat.ToList();
        }
    }
}