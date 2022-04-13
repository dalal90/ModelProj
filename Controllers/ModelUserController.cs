
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using ModelProject.Models;
using Microsoft.AspNetCore.Identity;
using System.IO;   

namespace ModelProject.Controllers
{
    public class ModelUserController : Controller
    {
        private MyContext _context;
        public ModelUserController(MyContext context)
        {
            _context = context;
        }

        // registration and login Form
        [HttpGet("model")]
        public ViewResult IndexM()
        {
            return View();
        }
        
        [HttpGet("model/dashboard")]
        public IActionResult Dashboard()
        {
            if (HttpContext.Session.GetInt32("uid") != null)
            {
            return View("Dashboard");
            }
            else
            {
            return RedirectToAction("Index", "ClientUser");
            }

        }
// registration post
        [HttpPost("model/processregister")]
        public IActionResult ProcessRegister(ModelUser newUser)
        {
            if (ModelState.IsValid)
            {
                //Confirm Email not already in use
                if (_context.ModelUsers.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("IndexM");
                }
                else
                {
                    //Create new user
                    // Hash PW
                    PasswordHasher<ModelUser> hasher = new PasswordHasher<ModelUser>();
                    newUser.Password = hasher.HashPassword(newUser, newUser.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    //Log User In (add to session)
                    HttpContext.Session.SetInt32("uid", newUser.ModelUserId);
                    //redirect to dashboard
                    return RedirectToAction("Dashboard");
                    }
                }
                    else
                    {
                    return View("IndexM");
                }
            }
            // login post
        [HttpPost("model/processlogin")]
        public IActionResult ProcessLogin(LoginModel logUser)
        {
            if (ModelState.IsValid)
            {
                // Find user in DB
                ModelUser userInDB = _context.ModelUsers.FirstOrDefault( u => u.Email == logUser.logEmail);
                if (userInDB == null)
                {
                    ModelState.AddModelError("logEmail", "Invalid Login Credentials");
                    return View("IndexM");
                }
                else
                {
                    PasswordHasher<LoginModel> hasher = new PasswordHasher<LoginModel>();
                    var result = hasher.VerifyHashedPassword(logUser, userInDB.Password, logUser.logPassword);

                    if (result == 0)
                    {
                        ModelState.AddModelError("logEmail", "Invalid Login Credentials");
                        return View("IndexM");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("uid", userInDB.ModelUserId);
                        //redirect to dashboard
                        return RedirectToAction("Dashboard");
                    }
                }
                
            }
            else 
            {
                return View("IndexM");
            }
        }

        // Model Application Form 
        [HttpGet("form")]
        public ViewResult Form()
        {
            return View();
        }
        [HttpPost("application")]
        public IActionResult CreatApp(App newApp)
        {
            if (ModelState.IsValid)
            {
                
                    newApp.ModelUserId = (int)HttpContext.Session.GetInt32("uid");
                    _context.Apps.Add(newApp);
                    _context.SaveChanges();
                    return Redirect($"model/{newApp.AppId}");
                
            }
            else 
            {
                return View("Form");
            }
        }
        
        [HttpGet("model/{aid}")]
        public ViewResult ModelDetail(int aid)
        {
            App thisApp = _context.Apps
            .Include( e => e.Creator)
            .FirstOrDefault(e => e.AppId == aid);
            return View("ModelDetail", thisApp);
        }

        [HttpGet("logout")]
        public RedirectToActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "ClientUser");
        }

    }
}

