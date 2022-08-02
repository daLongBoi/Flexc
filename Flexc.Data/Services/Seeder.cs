
using Flexc.Core.Models;
using Flexc.Core.Services;

namespace Flexc.Data.Services
{
    public static class Seeder
    {
        // use this class to seed the database with dummy 
        // test data using an IUserService 
         public static void Seed(IUserService svc)
        {
            svc.Initialise();

            // add users
            svc.AddUser("Administrator", "admin@mail.com", "admin", Role.admin);
            svc.AddUser("Manager", "manager@mail.com", "manager", Role.manager);
            svc.AddUser("Guest", "guest@mail.com", "guest", Role.guest); 


            var m1 =  svc.AddMeal(1,"Breakfast",500,"templateurl");
            var m2 = svc.AddMeal(2,"Lunch",300, "templateurl");
            var m3 =  svc.AddMeal(3,"Dinner",1000, "templateurl");
            var m4 = svc.AddMeal(4,"Snack",500,"templateurl");
            var m5 = svc.AddMeal(5,"Breakfast",500,"templateurl");

            var f1 = svc.CreateFood(m1.Id,"Tomato",100,100,"url");
            var f2 = svc.CreateFood(m1.Id,"Tomato",100,100,"url");
            var f3 = svc.CreateFood(m1.Id,"Tomato",100,100,"url");
            var f4 = svc.CreateFood(m1.Id,"Tomato",100,100,"url");

            var f5 = svc.CreateFood(m2.Id,"Tomato",100,100,"url");
            var f6 = svc.CreateFood(m2.Id,"Tomato",100,100,"url");
            var f7 = svc.CreateFood(m2.Id,"Tomato",100,100,"url");
            var f8 = svc.CreateFood(m3.Id,"Tomato",100,100,"url");


        }
    }
}
