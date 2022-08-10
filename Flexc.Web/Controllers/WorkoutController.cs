using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Flexc.Core.Models;
using Flexc.Core.Services;
using Flexc.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Authorization;

using Flexc.Web.ViewModels;
namespace Flexc.Web.Controllers;


    public class WorkoutController : BaseController
    {
                private IUserService svc;


                 public WorkoutController(IUserService ss)
            {
                svc = ss;
            } 
            // provided via the DI



            //launches create view
             public IActionResult Create()
            {
            // display blank form to create a vehicle
                return View();
            }



            //create on click action 
             // create /Vehicle/Post
             [HttpPost]
             public IActionResult Create(Workout v)
            {
                
                if (ModelState.IsValid)
                {
                    var Workout = svc.AddWorkout(v.UserId,v.Id,v.Name,v.Creator,v.DateWorkout);
                    return RedirectToAction(nameof(Index));
                }

                //redisplay the form for editing as there are errors
                return View(v);
            }
            

            //get/Meals view
            public IActionResult Index(){
        
            var w = svc.GetWorkouts();
            return View(w);
            }


            //get/ workout details 
            public IActionResult Details(int id)
            {
                var v = svc.GetWorkoutById(id);
                 if (v ==null)
                {
                    Alert("Meal Not Found", AlertType.warning);
                    return NotFound();
                }

                // pass Vehicle as parameter to the view
                return View(v);
            }

            //GET /Vehicle/edit/{id}

            public IActionResult Edit(int id)
            {
                //load the Vehicle using the service 
                var v= svc.GetWorkoutById(id);

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
                var v= svc.GetWorkoutById(id);
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
            [Authorize(Roles="admin")]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteConfirm(int id, int userId)
            {
                //complete Post action to save Workout changes
                svc.DeleteWorkout(id);
                // pass parameter to the view
                Alert("Workout deleted successfully", AlertType.info);
            return RedirectToAction(nameof(Details), new {Id = userId});
            }


        


    }





