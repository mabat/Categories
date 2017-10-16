using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using Categories.Models;
using System.Net;

namespace Categories.Controllers
{
    public class HomeController : Controller
    {
        // lista kategorija
        private static List<Cathegory> kategorije = new List<Cathegory>();

        // dohvati kategorije
        public JsonResult Index()
        {
            return Json(kategorije, JsonRequestBehavior.AllowGet);
        }

        // upload i ucitavanje datoteke u listu kategorija
        public ActionResult UploadFile(HttpPostedFileBase uploadedFile)
        {
            if (uploadedFile == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            try
            {
                kategorije.Clear();

                StreamReader sr = new StreamReader(uploadedFile.InputStream);

                // sadrzaj csv datoteke
                string csvData = sr.ReadToEnd();

                // redci
                foreach (string row in csvData.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        Cathegory cat = new Cathegory
                        {
                            CathegoryID = Convert.ToInt32(row.Split(',')[0]),
                            ParentID = !(row.Split(',')[1]).Equals("") ? Convert.ToInt32(row.Split(',')[1]) : (int?)null,
                            Name = row.Split(',')[2],
                            Children = new List<Cathegory>()
                        };
                        // poziv rekurzivne metode za svaki novi Cathegory objekt. 
                        // metoda dodaje novi objekt u Children listu od Parent kategorije, a ako objekt nema ParentID onda ga dodaje u listu kategorije "root"
                        FillCategories(kategorije, cat);
                    }
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // rekurzivno ucitavanje csv datoteke
        public void FillCategories(List<Cathegory> kategorije, Cathegory cat)
        {
            if (cat.ParentID == null)
            {
                kategorije.Add(cat);
                return;
            }

            foreach (var c in kategorije)
            {
                if (c.CathegoryID == cat.ParentID)
                {
                    c.Children.Add(cat);
                    return;
                }
                else
                {
                    FillCategories(c.Children, cat);
                }
            }
        }
    }
}