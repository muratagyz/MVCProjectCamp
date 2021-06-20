using BussinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjectCamp.Controllers
{
    public class WriterPanelController : Controller
    {
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        // GET: WriterPanel
        public ActionResult WriterProfile()
        {
            return View();
        }

        public ActionResult MyHeading()
        {
            var values = hm.GetListByWriter();
            return View(values);
        }
    }
}