
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flexc.Core.Models;
using Flexc.Core.Services;
using Flexc.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Flexc.Core.Security;

/**
 *  User Management Controller providing registration and login functionality
 */
namespace Flexc.Web.Controllers
{
    public class UserController : BaseController
    {
        private readonly IConfiguration _config;
        private readonly IUserService _svc;

        public UserController(IUserService svc, IConfiguration config)
        {        
            _config = config;    
            _svc = svc;
        }

         public IActionResult Index()
        {
              var w = _svc.GetUsers();
            return View(w);
        }

        public IActionResult Login()
        {
            return View();
        }

         // GET/User/{id}
        public IActionResult Details(int id)
        {
            var user = _svc.GetUser(id);
            if (user == null)
            {
                Alert("User Not Found", AlertType.warning);  
                return RedirectToAction(nameof(Index));             
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] UserLoginViewModel m)
        {
            var user = _svc.Authenticate(m.Email, m.Password);
            // check if login was unsuccessful and add validation errors
            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Login Credentials");
                ModelState.AddModelError("Password", "Invalid Login Credentials");
                return View(m);
            }

            // Login Successful, so sign user in using cookie authentication
            await SignInCookie(user);

            Alert("Successfully Logged in", AlertType.info);

            return Redirect("/");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register([Bind("Name,Email,Password,PasswordConfirm,Role,Photourl")] UserRegisterViewModel m)       
        {
            if (!ModelState.IsValid)
            {
                return View(m);
            }
            // add user via service
            var user = _svc.AddUser(m.Name, m.Email,m.Password, m.Role);
            // check if error adding user and display warning
            if (user == null) {
                Alert("There was a problem Registering. Please try again", AlertType.warning);
                return View(m);
            }

            Alert("Successfully Registered. Now login", AlertType.info);

            return RedirectToAction(nameof(Login));
        }

        [Authorize]
        public IActionResult UpdateProfile()
        {
           // use BaseClass helper method to retrieve Id of signed in user 
            var user = _svc.GetUser(User.GetSignedInUserId());
            var userViewModel = new UserProfileViewModel { 
                Id = user.Id, 
                Name = user.Name, 
                Email = user.Email,                 
                Role = user.Role
            };
            return View(userViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateProfile([Bind("Id,Name,Email,Role")] UserProfileViewModel m)       
        {
            var user = _svc.GetUser(m.Id);
            // check if form is invalid and redisplay
            if (!ModelState.IsValid || user == null)
            {
                return View(m);
            } 

            // update user details and call service
            user.Name = m.Name;
            user.Email = m.Email;
            user.Role = m.Role;        
            var updated = _svc.UpdateUser(user);

            // check if error updating service
            if (updated == null) {
                Alert("There was a problem Updating. Please try again", AlertType.warning);
                return View(m);
            }

            Alert("Successfully Updated Account Details", AlertType.info);
            
            // sign the user in with updated details)
            await SignInCookie(user);

            return RedirectToAction("Index","Home");
        }

        // Change Password
        [Authorize]
        public IActionResult UpdatePassword()
        {
            // use BaseClass helper method to retrieve Id of signed in user 
            var user = _svc.GetUser(User.GetSignedInUserId());
            var passwordViewModel = new UserPasswordViewModel { 
                Id = user.Id, 
                Password = user.Password, 
                PasswordConfirm = user.Password, 
            };
            return View(passwordViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword([Bind("Id,OldPassword,Password,PasswordConfirm")] UserPasswordViewModel m)       
        {
            var user = _svc.GetUser(m.Id);
            if (!ModelState.IsValid || user == null)
            {
                return View(m);
            }  
            // update the password
            user.Password = m.Password; 
            // save changes      
            var updated = _svc.UpdateUser(user);
            if (updated == null) {
                Alert("There was a problem Updating the password. Please try again", AlertType.warning);
                return View(m);
            }

            Alert("Successfully Updated Password", AlertType.info);
            // sign the user in with updated details
            await SignInCookie(user);

            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        // Return not authorised and not authenticated views
        public IActionResult ErrorNotAuthorised() => View();
        public IActionResult ErrorNotAuthenticated() => View();

        // -------------------------- Helper Methods ------------------------------

        // Called by Remote Validation attribute on RegisterViewModel to verify email address is available
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyEmailAvailable(string email, int id)
        {
            // check if email is available, or owned by user with id 
            if (!_svc.IsEmailAvailable(email,id))
            {
                return Json($"A user with this email address {email} already exists.");
            }
            return Json(true);                  
        }

        // Called by Remote Validation attribute on ChangePassword to verify old password
        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyPassword(string oldPassword)
        {
            // use BaseClass helper method to retrieve Id of signed in user 
            var id = User.GetSignedInUserId();            
            // check if email is available, unless already owned by user with id
            var user = _svc.GetUser(id);
            if (user == null || !Hasher.ValidateHash(user.Password, oldPassword))
            {
                return Json($"Please enter current password.");
            }
            return Json(true);                  
        }

        // Sign user in using Cookie authentication scheme
        private async Task SignInCookie(User user)
        {
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                AuthBuilder.BuildClaimsPrincipal(user)
            );
        }
        

         public IActionResult MessageCreate(int id)
        {     
            var s = _svc.GetUser(id);
            // check the returned student is not null and if so alert
            if (s == null)
            {
                Alert($"User {id} not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // create a ticket view model and set StudentId (foreign key)
            var Message = new Message { UserId = id }; 

            return View( Message );
        }


         // POST /student/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MessageCreate([Bind("userId, Context")] Message t)
        {
            if (ModelState.IsValid)
            {                
                var message = _svc.CreateMessage(t.UserId,t.Id,t.Name, t.Context);
                Alert($"Ticket created successfully for student {t.UserId}", AlertType.info);
                return RedirectToAction(nameof(Details), new { Id = message.UserId });
            }
            // redisplay the form for editing
            return View(t);
        }

          //GET /Vehicle/Delete/{id}
            public IActionResult MessageDelete(int id)
            {
            //load the Vehicle using the service 
                var v= _svc.GetMessage(id);
                if (v ==null)
                {
                // Alert($"Vehicle {id} not found",AlertType.warning);
                    return RedirectToAction(nameof(Index));
                }
                // pass vehicle as parameter to the view
                return View(v); 
            }

            //POST /Vehicle/Delete/{id}
            [HttpPost]
            public IActionResult MessageDeleteConfirm(int id)
            {
                //complete Post action to save vehicle changes
                _svc.DeleteMessage(id);
                // pass vehicle as parameter to the view
            return RedirectToAction(nameof(Index));
            }


        /////////////////////////////////////////////////////
        //module methods
         // ============ User Module Management ===============
    
        // GET /student/moduleupdate/{id}
        [Authorize(Roles = "admin,manager")]
        public IActionResult ModuleUpdate(int id)
        {
            var sm = _svc.GetUserModule(id);  
            if (sm == null)
            {
                Alert("Student Module Not Found", AlertType.warning);
                return RedirectToAction(nameof(Details));
            }
            var vm = new UserModuleViewModel {
                UserId = sm.UserId,
                ModuleId = sm.ModuleId,
                Mark = sm.Mark
            };            
            return View( vm );
        }

        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        [ValidateAntiForgeryToken]
        public IActionResult ModuleUpdate([Bind("StudentId, ModuleId, Mark")] UserModuleViewModel sm)
        {
            if (ModelState.IsValid)
            {                
                _svc.UpdateUserModuleMark(sm.UserId, sm.ModuleId, sm.Mark);

                return RedirectToAction(nameof(Details), new { Id = sm.UserId });
            }
            // redisplay the form for editing
            return View(sm);
        }

        // GET /student/createmodule/{id}
        [Authorize(Roles = "admin,manager")]
        public IActionResult ModuleCreate(int id)
        {
            var sm = _svc.GetUser(id);  
            if (sm == null)
            {
                Alert("StudentNot Found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }  
            var modules = _svc.GetAvailableModulesForUser(id);  
            var vm = new UserModuleViewModel {
                Modules = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(modules,"Id","Title"),
                UserId = id
            };  
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        [ValidateAntiForgeryToken]
        public IActionResult ModuleCreate([Bind("StudentId, ModuleId, Mark")] UserModuleViewModel sm)
        {
            if (ModelState.IsValid)
            {                
                _svc.AddUserToModule(sm.UserId, sm.ModuleId, sm.Mark);
                _svc.UpdateUserModuleMark(sm.UserId, sm.ModuleId, sm.Mark);
                return RedirectToAction(nameof(Details), new { Id = sm.UserId });
            }
            // redisplay the form for editing  
            // note - we must re-create the selectlist and update view model Modules property
            //        this is because the form does not retain the select list values when posted to server
            var modules = _svc.GetAvailableModulesForUser(sm.UserId);
            sm.Modules = new SelectList(modules,"Id","Title"); 
            return View(sm);
        }

        [HttpPost]
        [Authorize(Roles = "admin,manager")]
        [ValidateAntiForgeryToken]
        public IActionResult ModuleDelete(int id)
        {
            var sm = _svc.GetUserModule(id);
            if (sm == null)
            {
                Alert("Student Modulet Found", AlertType.warning);
                return RedirectToAction(nameof(Index));   
            }   
            
            _svc.RemoveUserFromModule(sm.UserId,sm.ModuleId);
            return RedirectToAction(nameof(Details), new { Id = sm.UserId });
        }
        
    }
}
