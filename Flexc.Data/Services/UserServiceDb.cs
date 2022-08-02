
using Flexc.Core.Models;
using Flexc.Core.Services;
using Flexc.Core.Security;
using Flexc.Data.Repositories;
using Microsoft.EntityFrameworkCore;
namespace Flexc.Data.Services
{
    public class UserServiceDb : IUserService
    {
        private readonly DatabaseContext  ctx;

        public UserServiceDb()
        {
            ctx = new DatabaseContext(); 
        }

        public void Initialise()
        {
           ctx.Initialise(); 
        }

        // ------------------ User Related Operations ------------------------

        // retrieve list of Users
        public IList<User> GetUsers()
        {
            return ctx.Users.ToList();
        }

        // Retrive User by Id 
        public User GetUser(int id)
        {
            return ctx.Users.FirstOrDefault(s => s.Id == id);
        }

        // Add a new User checking a User with same email does not exist
        public User AddUser(string name, string email, string password, Role role)
        {     
            var existing = GetUserByEmail(email);
            if (existing != null)
            {
                return null;
            } 

            var user = new User
            {            
                Name = name,
                Email = email,
                Password = Hasher.CalculateHash(password), // can hash if required 
                Role = role              
            };
            ctx.Users.Add(user);
            ctx.SaveChanges();
            return user; // return newly added User
        }

        // Delete the User identified by Id returning true if deleted and false if not found
        public bool DeleteUser(int id)
        {
            var s = GetUser(id);
            if (s == null)
            {
                return false;
            }
            ctx.Users.Remove(s);
            ctx.SaveChanges();
            return true;
        }

        // Update the User with the details in updated 
        public User UpdateUser(User updated)
        {
            // verify the User exists
            var User = GetUser(updated.Id);
            if (User == null)
            {
                return null;
            }
            // verify email address is registered or available to this user
            if (!IsEmailAvailable(updated.Email, updated.Id))
            {
                return null;
            }
            // update the details of the User retrieved and save
            User.Name = updated.Name;
            User.Email = updated.Email;
            User.Password = Hasher.CalculateHash(updated.Password);  
            User.Role = updated.Role; 

            ctx.SaveChanges();          
            return User;
        }

        // Find a user with specified email address
        public User GetUserByEmail(string email)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email);
        }

        // Verify if email is available or registered to specified user
        public bool IsEmailAvailable(string email, int userId)
        {
            return ctx.Users.FirstOrDefault(u => u.Email == email && u.Id != userId) == null;
        }

        public IList<User> GetUsersQuery(Func<User,bool> q)
        {
            return ctx.Users.Where(q).ToList();
        }

        public User Authenticate(string email, string password)
        {
            // retrieve the user based on the EmailAddress (assumes EmailAddress is unique)
            var user = GetUserByEmail(email);

            // Verify the user exists and Hashed User password matches the password provided
            return (user != null && Hasher.ValidateHash(user.Password, password)) ? user : null;
            //return (user != null && user.Password == password ) ? user: null;
        }





        // meals methods ------------------------------------------


        //method to get the meals
            public IList<Meal> GetMeals()
        {
            return ctx.Meals.Include(s=>s.Foods).ToList();
        }

        // method to find a meal by the name 
          public Meal GetmealByName(string MealName)
        {
            return ctx.Meals.FirstOrDefault(v => v.Name == MealName );
        }
         // ---------------- meals management --------------


        public Meal GetMealById(int id){
            return ctx.Meals.Include(s=>s.Foods).FirstOrDefault(v => v.Id == id);
        }

        public Meal GetMealdate(DateTime date){
             return ctx.Meals.Include(s=>s.Foods).FirstOrDefault(v => v.DateMeal == date);
        }

        

        public Meal AddMeal (   int id, string name, int totalCalories, string photoUrl){
            var exists = GetMealById(id);
            if(exists != null )
            {
                return null;
            }

            var m = new Meal
            {
                Id = id,
                Name = name,
                DateMeal = DateTime.Now,
                TotalCalories = totalCalories  
            };
            ctx.Meals.Add(m);
            ctx.SaveChanges();
            return m;
        }
        public Meal UpdateMeal(Meal updated)
        {
            var m = GetMealById(updated.Id);

             if(m == null )
            {
                return null;
            }
                m.Id = updated.Id;
                m.Name = updated.Name;
                m.TotalCalories = updated.TotalCalories;  
            
      
            ctx.SaveChanges();
            return m;
        }



        public bool DeleteMeal(int id){
        
            //load the Vehicle 
            var v = GetMealById(id);

            //verify the Vehicle exists
            if(v== null)
            {
                return false;
            }
        
            //delete code
            ctx.Meals.Remove(v);
            ctx.SaveChanges();
            return true;
        }

        
        // ---------------- food management --------------
       public Food CreateFood(int mealId,string name,int weight,
         int calories, string FoodPhotoUrl){
            var M = GetMealById(mealId);
            var f = new Food{
                MealId = mealId,
                Name = name,
                Weight = weight,
                Calories = calories,
                FoodPhotoUrl = FoodPhotoUrl
            };
            M.Foods.Add(f);
            ctx.SaveChanges();
            return f;
            

         }
    
        public bool DeleteFood(int id){
              //load the Vehicle 
            var v = GetMealById(id);

            //verify the Vehicle exists
            if(v== null)
            {
                return false;
            }
        
            //delete code
            ctx.Meals.Remove(v);
            ctx.SaveChanges();
            return true;
        }
        public Food GetFoodById(int id){
            return ctx.Foods.Include(m=> m.Meal).FirstOrDefault(v => v.Id == id);
        }

        public Food getFoodByName(string Name){
                 return ctx.Foods.FirstOrDefault(v => v.Name == Name);
        }
       
        public IList<Food> GetAllFood(){
            return ctx.Foods
            .Include(t =>t.Meal).ToList();

        }  
        public IList<Food> SearchFoods(string name, string query){
        
            // ensure query is not null    
            query = query == null ? "" : query.ToLower();

            // search Vehicle issue, active status and Vehicle name
            var results = ctx.Foods
                            .Include(t => t.Meal)
                            .Where(t => (t.Name.ToLower().Contains(query)) 
                            ).ToList();
            return  results;  
        }

        // ----------------------- workout activities 
        
        public Workout GetWorkoutById(int id){
            return ctx.Workouts.Include(s=>s.Exersizes).FirstOrDefault(v => v.Id == id);
        }

        public Workout GetWorkoutdate(DateTime date){
             return ctx.Workouts.Include(s=>s.Exersizes).FirstOrDefault(v => v.DateWorkout == date);
        }

        

        public Workout AddWorkout (   int id, string name, String Creator, string DateWorkout){
            var exists = GetMealById(id);
            if(exists != null )
            {
                return null;
            }

            var m = new Workout
            {
                Id = id,
                Name = name,
                Creator = Creator,
                DateWorkout = DateTime.Now,
               
            };
            ctx.Workouts.Add(m);
            ctx.SaveChanges();
            return m;
        }
        public Workout UpdateWorkout(Workout updated)
        {
            var m = GetWorkoutById(updated.Id);

             if(m == null )
            {
                return null;
            }
                m.Id = updated.Id;
                m.Name = updated.Name;
                m.Creator = updated.Creator;  
            
      
            ctx.SaveChanges();
            return m;
        }



        public bool DeleteWorkout(int id){
        
            //load the Vehicle 
            var v = GetWorkoutById(id);

            //verify the Vehicle exists
            if(v== null)
            {
                return false;
            }
        
            //delete code
            ctx.Workouts.Remove(v);
            ctx.SaveChanges();
            return true;
        }

         // ----------------------- Exersize activities 
         public Exersize CreateExersize(int Id,string exName,string MuscleGroup,
         int Reps, int Sets, string ExPhotoUrl){
            var M = GetWorkoutById(Id);
            var f = new Exersize{
                ExName = exName,
                MuscleGroup = MuscleGroup,
                Reps = Reps,
                Sets = Sets,
                ExPhotoUrl = ExPhotoUrl
            };
            M.Exersizes.Add(f);
            ctx.SaveChanges();
            return f;
            

         }
    
        public bool DeleteExersize(int id){
              //load the Vehicle 
            var v = GetExersizeById(id);

            //verify the Vehicle exists
            if(v== null)
            {
                return false;
            }
        
            //delete code
            ctx.Exersizes.Remove(v);
            ctx.SaveChanges();
            return true;
        }
        public Exersize GetExersizeById(int id){
            return ctx.Exersizes.Include(m=> m.Workout).FirstOrDefault(v => v.Id == id);
        }

        public Exersize getExersizeByName(string Name){
                 return ctx.Exersizes.FirstOrDefault(v => v.ExName == Name);
        }
       
        public IList<Exersize> GetAllExersizes(){
            return ctx.Exersizes
            .Include(t =>t.Workout).ToList();

        }  
        public IList<Exersize> SearchExersizes(string name, string query){
        
            // ensure query is not null    
            query = query == null ? "" : query.ToLower();

            // search Vehicle issue, active status and Vehicle name
            var results = ctx.Exersizes
                            .Include(t => t.Workout)
                            .Where(t => (t.ExName.ToLower().Contains(query)) 
                            ).ToList();
            return  results;  
        }
   
    }
}