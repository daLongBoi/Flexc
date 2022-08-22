
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
            var u1 = svc.AddUser("Administrator", "admin@mail.com", "admin", Role.admin);
            var u2 = svc.AddUser("Manager", "manager@mail.com", "manager", Role.manager);
            var u3 = svc.AddUser("Guest", "guest@mail.com", "guest", Role.guest);
            var u4 = svc.AddUser("Trainer", "Trainer@mail.com", "Trainer", Role.manager);
            var u5 = svc.AddUser("ExampleUser", "ExampleUser@mail.com", "User", Role.user);

            //trainer views 
            var m1 = svc.AddMeal(u4.Id, 1, "Breakfast", 500, "templateurl");
            var m2 = svc.AddMeal(u4.Id, 2, "Lunch", 300, "templateurl");
            var m3 = svc.AddMeal(u4.Id, 3, "Dinner", 1000, "templateurl");
            var m4 = svc.AddMeal(u4.Id, 2, "Snack", 500, "templateurl");
            var m5 = svc.AddMeal(u4.Id, 5, "Breakfast", 500, "templateurl");

            var f1 = svc.CreateFood(u4.Id, m1.Id, "Tomato", 100, 100, "url");
            var f2 = svc.CreateFood(u4.Id, m1.Id, "Tomato", 100, 100, "url");
            var f3 = svc.CreateFood(u4.Id, m1.Id, "Tomato", 100, 100, "url");
            var f4 = svc.CreateFood(u4.Id, m1.Id, "Tomato", 100, 100, "url");

            var f5 = svc.CreateFood(u4.Id, m2.Id, "Tomato", 100, 100, "url");
            var f6 = svc.CreateFood(u4.Id, m2.Id, "Tomato", 100, 100, "url");
            var f7 = svc.CreateFood(u4.Id, m2.Id, "Tomato", 100, 100, "url");
            var f8 = svc.CreateFood(u4.Id, m3.Id, "Tomato", 100, 100, "url");



            var w1 = svc.AddWorkout(u4.Id, 1, "Arms", "pt name", DateTime.Today);
            var w2 = svc.AddWorkout(u4.Id, 2, "Back - biceps", "pt name", DateTime.Today.AddDays(-1));
            var w3 = svc.AddWorkout(u4.Id, 3, "Chest - triceps", "pt name", DateTime.Today.AddDays(-2));
            var w4 = svc.AddWorkout(u4.Id, 4, "Legs", "pt name", DateTime.Today.AddDays(-3));
            var w5 = svc.AddWorkout(u4.Id, 5, "shoulders", "pt name", DateTime.Today.AddDays(-4));
            var w6 = svc.AddWorkout(u4.Id, 5, "Cardio", "pt name", DateTime.Today.AddDays(-4));

            var w7 = svc.AddWorkout(u4.Id, 1, "Arms", "pt name", DateTime.Today);
            var w8 = svc.AddWorkout(u4.Id, 2, "Back - biceps", "pt name", DateTime.Today.AddDays(-1));
            var w9 = svc.AddWorkout(u4.Id, 3, "Chest - triceps", "pt name", DateTime.Today.AddDays(-2));
            var w10 = svc.AddWorkout(u4.Id, 4, "Legs", "pt name", DateTime.Today.AddDays(-3));
            var w11 = svc.AddWorkout(u4.Id, 5, "shoulders", "pt name", DateTime.Today.AddDays(-4));
            var w12 = svc.AddWorkout(u4.Id, 5, "Cardio", "pt name", DateTime.Today.AddDays(-4));




            var e1 = svc.CreateExersize(u4.Id, m1.Id, "Preacher curl", "Bicep", 10, 4, 30, "url");
            var e2 = svc.CreateExersize(u4.Id, m1.Id, "Dumbell curl", "Bicep", 10, 4, 12, "url");
            var e3 = svc.CreateExersize(u4.Id, m1.Id, "Hammer curl", "Bicep", 10, 4, 14, "url");
            var e4 = svc.CreateExersize(u4.Id, m1.Id, "Consentration curl", "Bicep", 10, 4, 20, "url");

            var e6 = svc.CreateExersize(u4.Id, m1.Id, "Skull crushers", "Tricep", 10, 4, 40, "url");
            var e7 = svc.CreateExersize(u4.Id, m1.Id, "Close grip Bench Press", "Tricep", 10, 4, 36, "url");
            var e8 = svc.CreateExersize(u4.Id, m1.Id, "Overhead Rope pulls", "Tricep", 10, 4, 30, "url");
            var e9 = svc.CreateExersize(u4.Id, m1.Id, "Tricep pulldown", "Tricep", 10, 4, 20, "url");



            //User view 


            var um1 = svc.AddMeal(u5.Id, 1, "Breakfast", 500, "templateurl");
            var um2 = svc.AddMeal(u5.Id, 2, "Lunch", 300, "templateurl");
            var um3 = svc.AddMeal(u5.Id, 3, "Dinner", 1000, "templateurl");
            var um4 = svc.AddMeal(u5.Id, 2, "Snack", 500, "templateurl");
            var tm5 = svc.AddMeal(u5.Id, 5, "Breakfast", 500, "templateurl");

            var uf1 = svc.CreateFood(u5.Id, m1.Id, "Tomato", 100, 100, "url");
            var uf2 = svc.CreateFood(u5.Id, m1.Id, "Tomato", 100, 100, "url");
            var uf3 = svc.CreateFood(u5.Id, m1.Id, "Tomato", 100, 100, "url");
            var uf4 = svc.CreateFood(u5.Id, m1.Id, "Tomato", 100, 100, "url");

            var uf5 = svc.CreateFood(u5.Id, m2.Id, "Tomato", 100, 100, "url");
            var uf6 = svc.CreateFood(u5.Id, m2.Id, "Tomato", 100, 100, "url");
            var uf7 = svc.CreateFood(u5.Id, m2.Id, "Tomato", 100, 100, "url");
            var uf8 = svc.CreateFood(u5.Id, m3.Id, "Tomato", 100, 100, "url");



            var uw1 = svc.AddWorkout(u5.Id, 1, "Arms", "pt name", DateTime.Today);
            var uw2 = svc.AddWorkout(u5.Id, 2, "Back - biceps", "pt name", DateTime.Today.AddDays(-1));
            var uw3 = svc.AddWorkout(u5.Id, 3, "Chest - triceps", "pt name", DateTime.Today.AddDays(-2));
            var uw4 = svc.AddWorkout(u5.Id, 4, "Legs", "pt name", DateTime.Today.AddDays(-3));
            var uw5 = svc.AddWorkout(u5.Id, 5, "shoulders", "pt name", DateTime.Today.AddDays(-4));
            var uw6 = svc.AddWorkout(u5.Id, 5, "Cardio", "pt name", DateTime.Today.AddDays(-4));

            var uw7 = svc.AddWorkout(u5.Id, 1, "Arms", "pt name", DateTime.Today);
            var uw8 = svc.AddWorkout(u5.Id, 2, "Back - biceps", "pt name", DateTime.Today.AddDays(-1));
            var uw9 = svc.AddWorkout(u5.Id, 3, "Chest - triceps", "pt name", DateTime.Today.AddDays(-2));
            var uw10 = svc.AddWorkout(u5.Id, 4, "Legs", "pt name", DateTime.Today.AddDays(-3));
            var uw11 = svc.AddWorkout(u5.Id, 5, "shoulders", "pt name", DateTime.Today.AddDays(-4));
            var uw12 = svc.AddWorkout(u5.Id, 5, "Cardio", "pt name", DateTime.Today.AddDays(-4));


            var ue1 = svc.CreateExersize(u5.Id, m1.Id, "Preacher curl", "Bicep", 10, 4, 30, "url");
            var ue2 = svc.CreateExersize(u5.Id, m1.Id, "Dumbell curl", "Bicep", 10, 4, 12, "url");
            var ue3 = svc.CreateExersize(u5.Id, m1.Id, "Hammer curl", "Bicep", 10, 4, 14, "url");
            var ue4 = svc.CreateExersize(u5.Id, m1.Id, "Consentration curl", "Bicep", 10, 4, 20, "url");

            var ue6 = svc.CreateExersize(u5.Id, m1.Id, "Skull crushers", "Tricep", 10, 4, 40, "url");
            var ue7 = svc.CreateExersize(u5.Id, m1.Id, "Close grip Bench Press", "Tricep", 10, 4, 36, "url");
            var ue8 = svc.CreateExersize(u5.Id, m1.Id, "Overhead Rope pulls", "Tricep", 10, 4, 30, "url");
            var ue9 = svc.CreateExersize(u5.Id, m1.Id, "Tricep pulldown", "Tricep", 10, 4, 20, "url");

            var ms1 = svc.CreateMessage(u5.Id, 1, "client", "Hi Admin, the following workout has been recommended to you please find details in your wokrouts section");
            var ms2 = svc.CreateMessage(u1.Id, 2, "trainer", "Hi insert name here, thef ollowing workout has been recommended to you please find details below");




            // create some modules
            var G1 = svc.AddModule("Fatloss");
            var G2 = svc.AddModule("Muscle Gain");
            var G3 = svc.AddModule("General Fitness");



            // tests for modules
            svc.AddUserToModule(u1.Id, m1.Id, 60);

            // tests for modules
            svc.AddUserToModule(u2.Id, m2.Id, 72);
            // tests for modules
            svc.AddUserToModule(u3.Id, m3.Id, 56);

            // tests for modules 
            svc.AddUserToModule(u4.Id, m1.Id, 71);
            svc.AddUserToModule(u4.Id, m2.Id, 79);
            svc.AddUserToModule(u4.Id, m3.Id, 69);

            //tester for creating events 
            svc.CreateEvent(1, 3, "example title", "2022-08-15");




        }
    }
}
