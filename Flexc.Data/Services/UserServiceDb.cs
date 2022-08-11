
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
            return ctx.Users
             .Include(s => s.Messages)
              .Include(s=> s.Meals).ThenInclude(sm => sm.Foods)

             .Include(s=> s.Workouts).ThenInclude(sm => sm.Exersizes)
             .Include(s=> s.UserModules).ThenInclude(sv => sv.Module)


             .FirstOrDefault(s => s.Id == id);
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
            User.PhotoUrl = updated.PhotoUrl; 

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


       /* public User AddworkoutPlan(int userId, int targetUserId){

           var Account =  GetUser(userId);
           var targetAccount = GetUser(targetUserId);

           targetAccount.WorkoutPlan = Account.Workouts;


           ctx.SaveChanges();
           return targetAccount;
        }

         public User AddMealPlan(int userId, int targetUserId){

           var Account =  GetUser(userId);
           var targetAccount = GetUser(targetUserId);

          // targetAccount.MealPlan = Account.Meals;


           ctx.SaveChanges();
           return targetAccount;
        }*/
 

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
            
            return ctx.Meals.Include(s=>s.User).ThenInclude(sm=>sm.Foods)
            .FirstOrDefault(v => v.Id == id);
        }

        public Meal GetMealdate(DateTime date){
             return ctx.Meals.Include(s=>s.Foods).FirstOrDefault(v => v.DateMeal == date);
        }

        

        public Meal AddMeal (   int userId,int id, string name, int totalCalories, string photoUrl){
             var M = GetUser(userId);
            var exists = GetMealById(id);
            if(exists != null )
            {
                return null;
            }

            var m = new Meal
            {
                UserId = userId,
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
       public Food CreateFood(int UserId, int mealId,string name,int weight,
         int calories, string FoodPhotoUrl){
            var M = GetMealById(mealId);
            var f = new Food{
                 UserId = UserId,
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
            public IList<Workout> GetWorkouts()
        {
            return ctx.Workouts.Include(s=>s.Exersizes).ToList();
        }


        
        public Workout GetWorkoutById(int id){
            return ctx.Workouts.Include(s=>s.Exersizes).FirstOrDefault(v => v.Id == id);
        }

        public Workout GetWorkoutdate(DateTime date){
             return ctx.Workouts.Include(s=>s.Exersizes).FirstOrDefault(v => v.DateWorkout == date);
        }

        

        public Workout AddWorkout (  int userId, int id, string name, string Creator, DateTime DateWorkout){
             var M = GetUser(userId);
            var exists = GetWorkoutById(id);
            if(exists != null )
            {
                return null;
            }

            var w = new Workout
            {
                UserId = userId,
                Id = id,
                Name = name,
                Creator = Creator,
                DateWorkout = DateTime.Now,
               
            };
            ctx.Workouts.Add(w);
            ctx.SaveChanges();
            return w;
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
             public IList<Exersize> GetExersizes()
        {
            return ctx.Exersizes.Include(s=>s.Workout).ToList();
        }
         public Exersize CreateExersize(int UserId,int Id,string exName,string MuscleGroup,
         int Reps, int Sets,int Weight, string ExPhotoUrl){
            var M = GetWorkoutById(Id);
            var f = new Exersize{
                UserId=UserId,
                ExName = exName,
                MuscleGroup = MuscleGroup,
                Reps = Reps,
                Sets = Sets,
                Weight = Weight, 
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

         // ----------------------- Meassage activities 
         public Message CreateMessage(int Id,int UserId, string name, string Context){
            var user = GetUser(UserId);
            if(user == null)return null;

            var Message = new Message
            {
                Name = name,
                Context = Context,
                UserId = UserId,
                CreatedOn = DateTime.Now,
                Active = true,
            };
               ctx.Messages.Add(Message);
            ctx.SaveChanges(); // write to database
            return Message;


         }
       public Message GetMessage(int id){
              return ctx.Messages
                     .Include(t => t.User)
                     .FirstOrDefault(t => t.Id == id);
        }
       public Message CloseMessage(int id, string resolution){
             var message = GetMessage(id);
            // if ticket does not exist or is already closed return null
            if (message == null || !message.Active) return null;
            
            // ticket exists and is active so close
            message.Active = false;
            message.Resolution = resolution;
            message.RepliedOn = DateTime.Now;

           
            ctx.SaveChanges(); // write to database
            return message;
        }
        
        public bool  DeleteMessage(int id ){ // find ticket
            var ticket = GetMessage(id);
            if (ticket == null) return false;
            
            // remove ticket 
            var result = ctx.Messages.Remove(ticket);
            
            ctx.SaveChanges();
            return true;
        }
        public IList<Message> GetAllMessage(){
                return ctx.Messages
                     .Include(t => t.User)
                     .ToList();
        }
        public IList<Message> GetOpenMessage(){
            // return open tickets with associated students
            return ctx.Messages
                     .Include(t => t.User) 
                     .Where(t => t.Active)
                     .ToList();
        }         
        public IList<Message> SearchMessage(TicketRange range, string query){

            // ensure query is not null    
            query = query == null ? "" : query.ToLower();

            // search ticket issue, active status and student name
            var results = ctx.Messages
                            .Include(t => t.User)
                            .Where(t => (t.Context.ToLower().Contains(query) || 
                                         t.User.Name.ToLower().Contains(query)
                                        ) &&
                                        (range == TicketRange.OPEN && t.Active ||
                                         range == TicketRange.READ && !t.Active ||
                                         range == TicketRange.ALL
                                        ) 
                            ).ToList();
            return  results;

        }

          // ========================= Module Management ========================
     
        public Module AddModule(string title)
        {
            var m = new Module { Title = title };
            ctx.Modules.Add(m);
            ctx.SaveChanges();

            return m;
        }

        public UserModule AddUserToModule(int userId, int moduleId, int mark)
        {
            // check if this User module already exists and return null if found
            var sm = ctx.UserModules
                       .FirstOrDefault(o => o.UserId == userId && 
                                            o.ModuleId == moduleId);
            if (sm != null)  {  return null;  }

            // locate the User and the module
            var s = ctx.Users.FirstOrDefault(s => s.Id == userId);
            var m = ctx.Modules.FirstOrDefault(m => m.Id == moduleId);
            // if either don't exist then return null
            if (s == null || m == null) { return null;  }

            // create the User module and add to database
            var nsm = new UserModule { UserId = s.Id, ModuleId = m.Id, Mark = mark };
            ctx.UserModules.Add(nsm);
            ctx.SaveChanges();
            return nsm;
        }

        public bool RemoveUserFromModule(int userId, int moduleId)
        {
            // check User is taking the module
            var sm = ctx.UserModules.FirstOrDefault(
                m => m.UserId == userId && m.ModuleId == moduleId
            );
            if (sm == null) {  return false;  }
            
            // remove the User module
            ctx.UserModules.Remove(sm);
            ctx.SaveChanges();
            return true;
        }
        
        public UserModule UpdateUserModuleMark(int userId, int moduleId, int mark)
        {
            var user = GetUser(userId);
            // check the User exists
            if (user == null)
            {
                return null; // no such User
            }
            var sm = user.UserModules.FirstOrDefault(o => o.ModuleId == moduleId);
            if (sm == null)
            {
                return null; // no such User module
            }

            // update User module mark
            sm.Mark = mark;

            // calculate sum of module marks and count number of modules
            var sum = user.UserModules.Sum(sm => sm.Mark);
            var count = user.UserModules.Count();
            var avg = count == 0 ? 0 : sum / count;

            // set the User Profile Grade with average of module grades or 0 if no modules
            user.Grade = avg; 

            ctx.SaveChanges();
            return sm;
        }

        public UserModule GetUserModule(int id)
        {
            return ctx.UserModules.FirstOrDefault(sm => sm.Id == id);
        }

        public IList<Module> GetAvailableModulesForUser(int id)
        {
            var user = GetUser(id);
            var sm = user.UserModules.ToList();
            var modules = ctx.Modules.ToList();
            return modules.Where(m => sm.Any(x => x.ModuleId != m.Id)).ToList();
            //return db.Modules.ToList();
        }
       
    }
}