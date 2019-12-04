using CayGiaPhaAPI.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Cors;
using System.Web.Mvc;

namespace CayGiaPhaAPI.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class UploadController : Controller
    {
        Context db = new Context();

        [HttpPost]
        public ActionResult Index(int personId, HttpPostedFileBase file)
        {
            var person = db.Person.Find(personId);
            if (person == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (file.ContentLength > 0)
            {
                string _fileName = Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), _fileName);
                file.SaveAs(_path);
                person.personImg = @"UploadedFiles/"+_fileName;
            }

            db.SaveChanges();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}