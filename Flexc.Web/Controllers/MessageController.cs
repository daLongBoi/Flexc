using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Flexc.Core.Models;
using Flexc.Core.Services;
using Flexc.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

using Flexc.Web.ViewModels;
namespace Flexc.Web.Controllers;


    public class MessageController : BaseController
    {
                private IUserService svc;


                 public MessageController(IUserService ss)
            {
                svc = ss;
            } 
            // provided via the DI
             public IActionResult Index(MessageSearchViewModel m)
        {
            m.Messages = svc.SearchMessage(m.Range, m.Query);
            return View(m);
        }

         // GET/POST /ticket/index  
        //[AcceptVerbs("GET","POST")]
        public IActionResult IndexExtended(MessageSearchViewModel m)
        {            
            // perform search query and assign results to viewmodel Tickets property
            m.Messages = svc.SearchMessage(m.Range, m.Query);

            // build custom alert message if post
            if (Request.Method == "POST") 
            {
                var alert = $"{m.Messages.Count} result(s) found searching '{m.Range}' Tickets";
                if (m.Query != null && m.Query != "")
                {
                    alert += $" for '{m.Query}'"; 
                }

                // display alert
                Alert(alert, AlertType.info);
            }

            return View(m);
        } 
         // display page containg JS query 
        public IActionResult Query()
        {
            return View();
        }
        // display page containing Vue query
        public IActionResult VQuery()
        {
            return View();
        }

        // GET/ticket/{id}
        public IActionResult Details(int id)
        {
            var ticket = svc.GetMessage(id);
            if (ticket == null)
            {
                Alert("Ticket Not Found", AlertType.warning);  
                return RedirectToAction(nameof(Index));             
            }

            return View(ticket);
        }


        // POST /Message/close/{id}
        [HttpPost]
        [Authorize(Roles="admin,manager")]
        public IActionResult Close([Bind("Id, Resolution")] Message t)
        {
            // close ticket via service
            var Message = svc.CloseMessage(t.Id, t.Resolution);
            if (Message == null)
            {
                Alert("Message Not Found", AlertType.warning);                               
            }
            else
            {
                Alert($"Message {t.Id } closed", AlertType.info);  
            }

            // redirect to the index view
            return RedirectToAction(nameof(Index));
        }
       






            //launches create view
             public IActionResult Create()
            {
                var Users = svc.GetUsers();
            // populate viewmodel select list property
            // display blank form to create a vehicle
            var tvm = new MessageCreateViewModel {
                
                Users = new SelectList(Users,"Id","Name") 
            };

        
                return View(tvm);
            }
            



            //create on click action 
             // create /Vehicle/Post
             [HttpPost]
             public IActionResult Create(MessageCreateViewModel v)
            {
                
                if (ModelState.IsValid)
                {
                    var message = svc.CreateMessage(v.UserId, v.Name,v.Context);

                    Alert($"Message Created", AlertType.info);
                    return RedirectToAction(nameof(Index));
                }

                //redisplay the form for editing as there are errors
                return View(v);
            }
            

            //get/Meals view
 

        

            //GET /Vehicle/edit/{id}

            public IActionResult Edit(int id)
            {
                //load the Vehicle using the service 
                var v= svc.GetMessage(id);

                // TBC check if s is null and return NotFound()
                if (v ==null)
                {
                    return NotFound();
                }
                // pass Meal as parameter to the view
                return View(v);
            }

            //POST /Vehicle/edit/{id}
            [HttpPost]
            public IActionResult Edit(int Id, Workout v)
            {
                //complete Post action to save meal changes
                if(ModelState.IsValid)
                {
                var Vehicle = svc.UpdateWorkout(v);

                return RedirectToAction(nameof(Index));  
                }
                // pass vehicle as parameter to the view
                return View(v);
            }

             //GET /Vehicle/Delete/{id}
            public IActionResult Delete(int id)
            {
            //load the Vehicle using the service 
                var v= svc.GetMessage(id);
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
            public IActionResult DeleteConfirm(int id)
            {
                //complete Post action to save vehicle changes
                svc.DeleteWorkout(id);
                // pass vehicle as parameter to the view
            return RedirectToAction(nameof(Index));
            }


        


    }





