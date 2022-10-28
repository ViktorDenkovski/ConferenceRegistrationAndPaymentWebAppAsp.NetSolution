using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using dvm2014.Models;
using RazorPDF;
using PagedList;
using PagedList.Mvc;

namespace dvm2014.Controllers
{
    public class UcesniciController : Controller
    {
        public ActionResult Print(int id = 0)
        {
            Ucesnici ucesnici = db.Ucesnici.Find(id);
            if (ucesnici == null)
            {
                return HttpNotFound();
            }
            var pdfResult = new PdfResult(ucesnici, "Print");

            return pdfResult;

        }

        public ActionResult Print2()
        {
            
            List<Ucesnici> ucesnici = new List<Ucesnici>();
            ucesnici = db.Ucesnici.ToList();
            
            return new RazorPDF.PdfResult(ucesnici, "Print2");
        }

        //[HttpPost]
        //public ActionResult Print2(Ucesnici ucesnici)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Ucesnici.Add(ucesnici);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(ucesnici);
        //}
        public ActionResult SearchIndex(string searchString,
            string searchValuta, string searchDrzavjanin, string searchPecateno,
            string searchUplataSite, string searchUplataUcestvo, string searchUplataDopo,
            string searchUplataSmestuvanje, string searchNacinUplata, string searchNacinUplataS, 
            string searchNacinUplataP, string searchNacinUplataN,
            string searchUplataNacVecera, string searchUplataProvizija, string searchNacinUplataD)
        {
            
            var ValutaLst = new List<string>();
            var DrzavjaninLst = new List<string>();
            var PecatenoLst = new List<string>();
            var NacinUplataLst = new List<string>();
            var NacinUplataSLst = new List<string>();
            var NacinUplataNLst = new List<string>();
            var NacinUplataPLst = new List<string>();
            var NacinUplataDLst = new List<string>();

            var ValutaQry = from d in db.Ucesnici orderby d.Valuta select d.Valuta;
            var DrzavjaninQry = from d in db.Ucesnici orderby d.Drzavjanin select d.Drzavjanin;
            var PecatenoQry = from d in db.Ucesnici orderby d.Pecateno select d.Pecateno;
            var NacinUplataQry = from d in db.Ucesnici orderby d.NacinUplata select d.NacinUplata;
            var NacinUplataSQry = from d in db.Ucesnici orderby d.NacinUplataS select d.NacinUplataS;
            var NacinUplataNQry = from d in db.Ucesnici orderby d.NacinUplataN select d.NacinUplataN;
            var NacinUplataPQry = from d in db.Ucesnici orderby d.NacinUplataP select d.NacinUplataP;
            var NacinUplataDQry = from d in db.Ucesnici orderby d.NacinUplataD select d.NacinUplataD;

            ValutaLst.AddRange(ValutaQry.Distinct());
            ViewBag.searchValuta = new SelectList(ValutaLst);

            DrzavjaninLst.AddRange(DrzavjaninQry.Distinct());
            ViewBag.searchDrzavjanin = new SelectList(DrzavjaninLst);

            PecatenoLst.AddRange(PecatenoQry.Distinct());
            ViewBag.searchPecateno = new SelectList(PecatenoLst);

            NacinUplataLst.AddRange(NacinUplataQry.Distinct());
            ViewBag.searchNacinUplata = new SelectList(NacinUplataLst);

            NacinUplataSLst.AddRange(NacinUplataSQry.Distinct());
            ViewBag.searchNacinUplataS = new SelectList(NacinUplataSLst);

            NacinUplataNLst.AddRange(NacinUplataNQry.Distinct());
            ViewBag.searchNacinUplataN = new SelectList(NacinUplataNLst);

            NacinUplataPLst.AddRange(NacinUplataPQry.Distinct());
            ViewBag.searchNacinUplataP = new SelectList(NacinUplataPLst);

            NacinUplataDLst.AddRange(NacinUplataDQry.Distinct());
            ViewBag.searchNacinUplataD = new SelectList(NacinUplataDLst);

       
                var temp = from m in db.Ucesnici
                           where ((string.IsNullOrEmpty(searchString) ? true : m.ImePrezime.Contains(searchString)) &&
                                  (string.IsNullOrEmpty(searchValuta) ? true : m.Valuta == searchValuta) &&
                                  (string.IsNullOrEmpty(searchDrzavjanin) ? true : m.Drzavjanin == searchDrzavjanin) &&
                                  (string.IsNullOrEmpty(searchPecateno) ? true : m.Pecateno == searchPecateno) &&

                                  (string.IsNullOrEmpty(searchNacinUplata) ? true : m.NacinUplata == searchNacinUplata) &&
                                  (string.IsNullOrEmpty(searchNacinUplataS) ? true : m.NacinUplataS == searchNacinUplataS) &&
                                  (string.IsNullOrEmpty(searchNacinUplataN) ? true : m.NacinUplataN == searchNacinUplataN) &&
                                  (string.IsNullOrEmpty(searchNacinUplataP) ? true : m.NacinUplataP == searchNacinUplataP) &&
                                  (string.IsNullOrEmpty(searchNacinUplataD) ? true : m.NacinUplataD == searchNacinUplataD))
                           select m;
            
                var ucesnici1 = temp.ToList();
            
                if (searchUplataSite != "true")
                {
                    
                    // i kombinacii za 1 opcija
                    if (searchUplataUcestvo == "true")
                    {
                        ucesnici1 = ucesnici1.Where(x => x.Ucestvo > 0).ToList();
                        ViewData["Foo"] = "u";
                    }
                    else
                        if (searchUplataSmestuvanje == "true")
                        {
                            ucesnici1 = ucesnici1.Where(x => x.Smestuvanje > 0).ToList();
                            ViewData["Foo"] = "s";
                        }
                        else
                            if (searchUplataNacVecera == "true")
                            {
                                ucesnici1 = ucesnici1.Where(x => x.NacVecera > 0).ToList();
                                ViewData["Foo"] = "n";
                            }
                            else
                                if (searchUplataProvizija == "true")
                                {
                                    ucesnici1 = ucesnici1.Where(x => x.Provizija > 0).ToList();
                                    ViewData["Foo"] = "p";
                                }
                                else
                                    if (searchUplataDopo == "true")
                                    {
                                        ucesnici1 = ucesnici1.Where(x => x.DopoTrosoci > 0).ToList();
                                        ViewData["Foo"] = "d";
                                    }

                    // i kombinacii za 2 opcii
                    if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true"))
                    {
                        ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0)).ToList();
                        ViewData["Foo"] = "us";
                    }
                    else
                        if ((searchUplataUcestvo == "true") && (searchUplataNacVecera == "true"))
                        {
                            ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.NacVecera > 0)).ToList();
                            ViewData["Foo"] = "un";
                        }
                        else
                            if ((searchUplataUcestvo == "true") && (searchUplataProvizija == "true"))
                            {
                                ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Provizija > 0)).ToList();
                                ViewData["Foo"] = "up";
                            }
                            else
                                if ((searchUplataUcestvo == "true") && (searchUplataDopo == "true"))
                                {
                                    ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.DopoTrosoci > 0)).ToList();
                                    ViewData["Foo"] = "ud";
                                }
                                else
                                    if ((searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true"))
                                    {
                                        ucesnici1 = ucesnici1.Where(x => (x.Smestuvanje > 0) || (x.NacVecera > 0)).ToList();
                                        ViewData["Foo"] = "sn";
                                    }
                                    else
                                        if ((searchUplataSmestuvanje == "true") && (searchUplataProvizija == "true"))
                                        {
                                            ucesnici1 = ucesnici1.Where(x => (x.Smestuvanje > 0) || (x.Provizija > 0)).ToList();
                                            ViewData["Foo"] = "sp";
                                        }
                                        else
                                            if ((searchUplataSmestuvanje == "true") && (searchUplataDopo == "true"))
                                            {
                                                ucesnici1 = ucesnici1.Where(x => (x.Smestuvanje > 0) || (x.DopoTrosoci > 0)).ToList();
                                                ViewData["Foo"] = "sd";
                                            }
                                            else
                                                if ((searchUplataNacVecera == "true") && (searchUplataProvizija == "true"))
                                                {
                                                    ucesnici1 = ucesnici1.Where(x => (x.NacVecera > 0) || (x.Provizija > 0)).ToList();
                                                    ViewData["Foo"] = "np";
                                                }
                                                else
                                                    if ((searchUplataNacVecera == "true") && (searchUplataDopo == "true"))
                                                    {
                                                        ucesnici1 = ucesnici1.Where(x => (x.NacVecera > 0) || (x.DopoTrosoci > 0)).ToList();
                                                        ViewData["Foo"] = "nd";
                                                    }
                                                    else
                                                        if ((searchUplataProvizija == "true") && (searchUplataDopo == "true"))
                                                        {
                                                            ucesnici1 = ucesnici1.Where(x => (x.Provizija > 0) || (x.DopoTrosoci > 0)).ToList();
                                                            ViewData["Foo"] = "pd";
                                                        }
                    
                    // i kombinacii so 3 opcii
                    if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true"))
                    {
                        ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.NacVecera > 0)).ToList();
                        ViewData["Foo"] = "usn";
                    }
                    else
                        if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataProvizija == "true"))
                        {
                            ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.Provizija > 0)).ToList();
                            ViewData["Foo"] = "usp";
                        }
                        else
                            if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataDopo == "true"))
                            {
                                ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.DopoTrosoci > 0)).ToList();
                                ViewData["Foo"] = "usd";
                            }
                            else
                                if ((searchUplataUcestvo == "true") && (searchUplataNacVecera == "true") && (searchUplataProvizija == "true"))
                                {
                                    ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.NacVecera > 0) || (x.Provizija > 0)).ToList();
                                    ViewData["Foo"] = "unp";
                                }
                                else
                                    if ((searchUplataUcestvo == "true") && (searchUplataNacVecera == "true") && (searchUplataDopo == "true"))
                                    {
                                        ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.NacVecera > 0) || (x.DopoTrosoci > 0)).ToList();
                                        ViewData["Foo"] = "und";
                                    }
                                    else
                                        if ((searchUplataUcestvo == "true") && (searchUplataProvizija == "true") && (searchUplataDopo == "true"))
                                        {
                                            ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Provizija > 0) || (x.DopoTrosoci > 0)).ToList();
                                            ViewData["Foo"] = "upd";
                                        }
                                        else
                                            if ((searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true") && (searchUplataProvizija == "true"))
                                            {
                                                ucesnici1 = ucesnici1.Where(x => (x.Smestuvanje > 0) || (x.NacVecera > 0) || (x.Provizija > 0)).ToList();
                                                ViewData["Foo"] = "snp";
                                            }
                                            else
                                                if ((searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true") && (searchUplataDopo == "true"))
                                                {
                                                    ucesnici1 = ucesnici1.Where(x => (x.Smestuvanje > 0) || (x.NacVecera > 0) || (x.DopoTrosoci > 0)).ToList();
                                                    ViewData["Foo"] = "snd";
                                                }
                                                else
                                                    if ((searchUplataSmestuvanje == "true") && (searchUplataProvizija == "true") && (searchUplataDopo == "true"))
                                                    {
                                                        ucesnici1 = ucesnici1.Where(x => (x.Smestuvanje > 0) || (x.Provizija > 0) || (x.DopoTrosoci > 0)).ToList();
                                                        ViewData["Foo"] = "spd";
                                                    }
                                                    else
                                                        if ((searchUplataNacVecera == "true") && (searchUplataProvizija == "true") && (searchUplataDopo == "true"))
                                                        {
                                                            ucesnici1 = ucesnici1.Where(x => (x.NacVecera > 0) || (x.Provizija > 0) || (x.DopoTrosoci > 0)).ToList();
                                                            ViewData["Foo"] = "npd";
                                                        }

                    // i kombinacii za 4 opcii
                    if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true") && (searchUplataProvizija == "true"))
                    {
                        ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.NacVecera > 0) || (x.Provizija > 0)).ToList();
                        ViewData["Foo"] = "usnp";
                    }
                    else
                        if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true") && (searchUplataDopo == "true"))
                        {
                            ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.NacVecera > 0) || (x.DopoTrosoci > 0)).ToList();
                            ViewData["Foo"] = "usnd";
                        }
                        else
                            if ((searchUplataUcestvo == "true") && (searchUplataDopo == "true") && (searchUplataNacVecera == "true") && (searchUplataProvizija == "true"))
                            {
                                ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.DopoTrosoci > 0) || (x.NacVecera > 0) || (x.Provizija > 0)).ToList();
                                ViewData["Foo"] = "unpd";
                            }
                            else
                                if ((searchUplataDopo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataNacVecera == "true") && (searchUplataProvizija == "true"))
                                {
                                    ucesnici1 = ucesnici1.Where(x => (x.DopoTrosoci > 0) || (x.Smestuvanje > 0) || (x.NacVecera > 0) || (x.Provizija > 0)).ToList();
                                    ViewData["Foo"] = "snpd";
                                }
                                else
                                    if ((searchUplataUcestvo == "true") && (searchUplataSmestuvanje == "true") && (searchUplataDopo == "true") && (searchUplataProvizija == "true"))
                                    {
                                        ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.DopoTrosoci > 0) || (x.Provizija > 0)).ToList();
                                        ViewData["Foo"] = "uspd";
                                    }
                }
                else
                    // i kombinacija so 5 opcii
                    ucesnici1 = ucesnici1.Where(x => (x.Ucestvo > 0) || (x.Smestuvanje > 0) || (x.NacVecera > 0) || (x.Provizija > 0) || (x.DopoTrosoci > 0)).ToList();

                //ucesnici1 = ucesnici1.Where(x => (x.NacinUplata == "Vo gotovo")).ToList();
                
                var model = new SearchModel();
                model.Ucesnici = ucesnici1.ToList();

                return View(model);

        }

        private UcesniciContext db = new UcesniciContext();

        
        //
        // GET: /Ucesnici/

        public ActionResult Index(int? page)
        {
            var ucesnici = from m in db.Ucesnici orderby m.UcesnikID descending select m;
           
            return View(ucesnici.ToList().ToPagedList(page ?? 1, 8));
        }

        //
        // GET: /Ucesnici/Details/5

        public ActionResult Details(int id = 0)
        {
            Ucesnici ucesnici = db.Ucesnici.Find(id);
            if (ucesnici == null)
            {
                return HttpNotFound();
            }
            return View(ucesnici);
        }

        //
        // GET: /Ucesnici/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ucesnici/Create

        [HttpPost]
        public ActionResult Create(Ucesnici ucesnici)
        {
            if (ModelState.IsValid)
            {
                db.Ucesnici.Add(ucesnici);
                db.SaveChanges();
               // return RedirectToAction("Index");
                var pdfResult = new PdfResult(ucesnici, "Print");

                return pdfResult;
            }

            return View(ucesnici);
        }

        //
        // GET: /Ucesnici/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ucesnici ucesnici = db.Ucesnici.Find(id);
            if (ucesnici == null)
            {
                return HttpNotFound();
            }
            return View(ucesnici);
          
        }

        //
        // POST: /Ucesnici/Edit/5

        [HttpPost]
        public ActionResult Edit(Ucesnici ucesnici)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(ucesnici).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //return View(ucesnici);
            if (ModelState.IsValid)
            {
                //db.Ucesnici.Add(ucesnici);
                db.Entry(ucesnici).State = EntityState.Modified;
                db.SaveChanges();
                // return RedirectToAction("Index");
                var pdfResult = new PdfResult(ucesnici, "Print");

                return pdfResult;
            }

            return View(ucesnici);
        }

        //
        // GET: /Ucesnici/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ucesnici ucesnici = db.Ucesnici.Find(id);
            if (ucesnici == null)
            {
                return HttpNotFound();
            }
            return View(ucesnici);
        }

        //
        // POST: /Ucesnici/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ucesnici ucesnici = db.Ucesnici.Find(id);
            db.Ucesnici.Remove(ucesnici);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}