using BussinessLayer.Concrete;
using DataAccessLayer.Concraee;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using FluentValidation.Results;
using BussinessLayer.ValidationRules;

namespace MVCProjectCamp.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        WriterManager wm = new WriterManager(new EfWriterDal());
        Context c = new Context();

        // GET: WriterPanel
        [HttpGet]
        public ActionResult WriterProfile()
        {
            string p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var writerValue = wm.GetByID(writerIdInfo);
            return View(writerValue);
        }
        [HttpPost]
        public ActionResult WriterProfile(Writer p)
        {
            WriterValidator writerValidator = new WriterValidator();
            ValidationResult results = writerValidator.Validate(p);
            if (results.IsValid)
            {
                wm.WriterUpdate(p);
                return RedirectToAction("WriterProfile");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult MyHeading(string p)
        {
            p = (string)Session["WriterMail"];
            var writerIdInfo = c.Writers.Where(x => x.WriterMail == p).Select(y => y.WriterID).FirstOrDefault();
            var values = hm.GetListByWriter(writerIdInfo);
            return View(values);
        }

        [HttpGet]
        public ActionResult NewHeading()
        {
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            return View();
        }

        [HttpPost]
        public ActionResult NewHeading(Heading p)
        {
            string _mail = (string)Session["WriterMail"];
            var writerMailInfo = c.Writers.Where(x => x.WriterMail == _mail).Select(y => y.WriterID).FirstOrDefault();
            p.HeadingDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.WriterID = writerMailInfo;
            p.HeadingStatus = true;
            hm.HeadingAdd(p);
            return RedirectToAction("MyHeading");
        }
        [HttpGet]
        public ActionResult EditHeading(int id)
        {
            ViewBag.Id = id;
            List<SelectListItem> valuecategory = (from x in cm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.CategoryName,
                                                      Value = x.CategoryID.ToString()
                                                  }).ToList();
            ViewBag.vlc = valuecategory;
            var headingvalue = hm.GetByID(id);
            return View(headingvalue);
        }

        [HttpPost]
        public ActionResult EditHeading(Heading p)
        {
            hm.HeadingUpdate(p);
            return RedirectToAction("MyHeading");
        }
        public ActionResult DeleteHeading(int id)
        {
            var headingvalue = hm.GetByID(id);
            hm.HeadingDelete(headingvalue);
            return RedirectToAction("MyHeading");
        }

        public ActionResult AllHeading(int p = 1)
        {
            var headings = hm.GetList().ToPagedList(p, 4);
            return View(headings);
        }
    }
}