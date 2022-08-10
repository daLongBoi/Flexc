
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
            

            
            var w1 =  svc.AddWorkout(1,"Arms","pt name",DateTime.Today);
            var w2 = svc.AddWorkout(2,"Back - biceps","pt name",DateTime.Today.AddDays(-1));
            var w3 =  svc.AddWorkout(3,"Chest - triceps","pt name",DateTime.Today.AddDays(-2));
            var w4 =  svc.AddWorkout(4,"Legs","pt name",DateTime.Today.AddDays(-3));
            var w5 =  svc.AddWorkout(5,"shoulders","pt name",DateTime.Today.AddDays(-4));
            var w =  svc.AddWorkout(5,"Cardio","pt name",DateTime.Today.AddDays(-4));


            var e1 = svc.CreateExersize(m1.Id,"Preacher curl","Bicep",10,4,30,"url");
            var e2 = svc.CreateExersize(m1.Id,"Dumbell curl","Bicep",10,4,12,"url");
            var e3 = svc.CreateExersize(m1.Id,"Hammer curl","Bicep",10,4,14,"url");
            var e4 = svc.CreateExersize(m1.Id,"Consentration curl","Bicep",10,4,20,"url");

            var e6 = svc.CreateExersize(m1.Id,"Skull crushers","Tricep",10,4,40,"url");
            var e7 = svc.CreateExersize(m1.Id,"Close grip Bench Press","Tricep",10,4,36,"url");
            var e8 = svc.CreateExersize(m1.Id,"Overhead Rope pulls","Tricep",10,4,30,"url");
            var e9 = svc.CreateExersize(m1.Id,"Tricep pulldown","Tricep",10,4,20,"url");
        }
    }
}
