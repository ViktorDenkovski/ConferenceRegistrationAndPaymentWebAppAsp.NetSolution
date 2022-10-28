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
    public class HomeController : Controller
    {
        public ActionResult IndexReset()
        {
            
                using (var newContext = new UcesniciContext())
                {
                    // get all Persons with FirstName equals name
                    var personsToUpdate = newContext.Ucesnici.Where(m => (m.Ucestvo >= 0) || (m.Smestuvanje >= 0) || (m.NacVecera >= 0) || (m.Provizija >= 0) || (m.DopoTrosoci >= 0));

                    // update LastName for all Persons in personsToUpdate
                    foreach (Ucesnici p in personsToUpdate)
                    {
                        p.Ucestvo = 0;
                        p.Smestuvanje = 0;
                        p.NacVecera = 0;
                        p.Provizija = 0;
                        p.DopoTrosoci = 0;

                        p.Pecateno = "0";

                        p.sertifikat = "0";

                        p.NacinUplata = "Neplateno";
                        p.NacinUplataS = "Neplateno";
                        p.NacinUplataN = "Neplateno";
                        p.NacinUplataP = "Neplateno";
                        p.NacinUplataD = "Neplateno";

                        
                    }
                    newContext.SaveChanges();
                }
                ViewBag.Resetiranje = "Базата со податоци е ресетирана!";

                return View();
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Апликација за регистрирање на учесници на конференција - DVM 2014 v.2.1.2015";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Информации за лицето одговорно за изработка на оваа апликација.";

            return View();
        }

        public ActionResult Certificate(string SearchUID, string SearchString, string searchPecateno="0")
        {
            ViewBag.Message = "Факултет за ветеринарна медицина - Скопје.";

            if (SearchUID == "")
            {
                var PecatenoLst = new List<string>();
                var PecatenoQry = from d in db.Ucesnici orderby d.sertifikat select d.sertifikat;

                PecatenoLst.AddRange(PecatenoQry.Distinct());
                ViewBag.searchPecateno = new SelectList(PecatenoLst);

                var temp = from m in db.Ucesnici
                           where ((string.IsNullOrEmpty(SearchString) ? true : m.ImePrezime.Contains(SearchString)) &&
                           (string.IsNullOrEmpty(searchPecateno) ? true : m.sertifikat == searchPecateno) && ((m.Ucestvo >= 0) || (m.Smestuvanje >= 0) || (m.NacVecera >= 0) || (m.Provizija >= 0) || (m.DopoTrosoci >= 0)))
                           select m;

                var ucesnici1 = temp.ToList();

                var model = new SearchModel();
                model.Ucesnici = ucesnici1.ToList();

                return View(model);
            }
            else
            {
                int UID = Convert.ToInt32(SearchUID);

                var PecatenoLst = new List<string>();
                var PecatenoQry = from d in db.Ucesnici orderby d.sertifikat select d.sertifikat;

                PecatenoLst.AddRange(PecatenoQry.Distinct());
                ViewBag.searchPecateno = new SelectList(PecatenoLst);

                var temp = from m in db.Ucesnici
                           where ((string.IsNullOrEmpty(SearchString) ? true : m.ImePrezime.Contains(SearchString)) && (string.IsNullOrEmpty(SearchUID) ? true : m.UcesnikID.Equals(UID)) &&
                           (string.IsNullOrEmpty(searchPecateno) ? true : m.sertifikat == searchPecateno) && ((m.Ucestvo > 0) || (m.Smestuvanje > 0) || (m.NacVecera > 0) || (m.Provizija > 0) || (m.DopoTrosoci > 0)))
                           select m;

                var ucesnici1 = temp.ToList();

                var model = new SearchModel();
                model.Ucesnici = ucesnici1.ToList();

                return View(model);
            }
        }
        private UcesniciContext db = new UcesniciContext();

        [HttpGet]
        public ActionResult Edit(int id)
        {

            using (var newContext = new UcesniciContext())
            {
                // get all Persons with FirstName equals name
                var personsToUpdate = newContext.Ucesnici.Where(m => (m.UcesnikID == id));

                foreach (Ucesnici p in personsToUpdate)
                {
                    if (p.sertifikat == "0")
                    {
                        p.sertifikat = "1";
                    }
                    else
                    {
                        p.sertifikat = "0";
                    }
                }
                newContext.SaveChanges();
                return RedirectToAction("Certificate");
            }
        }
    }
}
