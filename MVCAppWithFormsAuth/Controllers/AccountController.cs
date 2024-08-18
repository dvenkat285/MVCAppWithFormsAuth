using MVCAppWithFormsAuth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCAppWithFormsAuth.Controllers
{
    public class AccountController : Controller
    {
        MVCDBEntities dc = new MVCDBEntities();
        [HttpGet]
       public ViewResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    Password = model.Password,
                    Email = model.Email,
                    Mobile = model.Mobile,
                    Status = true
                };
                dc.Users.Add(user);
                dc.SaveChanges();
                return RedirectToAction("Login");
            }
            else
                return View("Register", model); //Register is optional cuz default is callback is register only and only model is enough
        }
        [HttpGet]
        public ViewResult Login()
        {
            return View(); 
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var record = from U in dc.Users where U.UserId == model.UserId && U.Password == model.Password select U;
                if (record.Count() > 0)
                {
                    Session["UserKey"] = Guid.NewGuid();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Credentials.");
                    return View(model);
                }
            }
            else
                return View(model);
        }

    }
}