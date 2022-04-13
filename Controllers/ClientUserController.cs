using Microsoft.AspNetCore.Mvc;
using ModelProject.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ModelProject.Controllers
{
    public class ClientUserController : Controller
    {
        private MyContext _context;
        public ClientUserController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public ViewResult Index()
        {
            return View();
        }

        [HttpGet("client/dashboard")]
        public ViewResult Dashboard()
        {
            return View();
        }
        
        // registration and login Form
        [HttpGet("client")]
        public ViewResult IndexC()
        {
            return View("IndexC");
        }

        // registration post
        [HttpPost("client/processregister")]
        public IActionResult ProcessRegister(ClientUser newUser)
        {
            if (ModelState.IsValid)
            {
                //Confirm Email not already in use
                if (_context.ClientUsers.Any(u => u.Email == newUser.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use");
                    return View("IndexC");
                }
                else
                {
                    //Create new user
                    // Hash PW
                    PasswordHasher<ClientUser> hasher = new PasswordHasher<ClientUser>();
                    newUser.Password = hasher.HashPassword(newUser, newUser.Password);
                    _context.Add(newUser);
                    _context.SaveChanges();
                    //Log User In (add to session)
                    HttpContext.Session.SetInt32("uid", newUser.UserId);
                    //redirect to dashboard
                    return RedirectToAction("Dashboard");

                }
            }
            else
            {
                return View("IndexC");
            }
        }

        // login post
        [HttpPost("client/processlogin")]
        public IActionResult ProcessLogin(LoginClient logUser)
        {
            if (ModelState.IsValid)
            {
                // Find user in DB
                ClientUser userInDB = _context.ClientUsers.FirstOrDefault( u => u.Email == logUser.LoginEmail);
                if (userInDB == null)
                {
                    ModelState.AddModelError("logEmail", "Invalid Login Credentials");
                    return View("IndexC");
                }
                else
                {
                    PasswordHasher<LoginClient> hasher = new PasswordHasher<LoginClient>();

                    var result = hasher.VerifyHashedPassword(logUser, userInDB.Password, logUser.LoginPassword);

                    if (result == 0)
                    {
                        ModelState.AddModelError("logEmail", "Invalid Login Credentials");
                        return View("IndexC");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("uid", userInDB.UserId);
                        //redirect to dashboard
                        return RedirectToAction("Dashboard");
                    }
                }
                
            }
            else 
            {
                return View("IndexC");
            }
        }
    }
}