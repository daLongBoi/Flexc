
using Flexc.Core.Models;

namespace Flexc.Core.Services
{
    // This interface describes the operations that a UserService class implementation should provide
    public interface IUserService
    {
        // Initialise the repository - only to be used during development 
        void Initialise();

        // ---------------- User Management --------------
        IList<User> GetUsers();
        User GetUser(int id);
        User GetUserByEmail(string email);
        bool IsEmailAvailable(string email, int userId);
        User AddUser(string name, string email, string password, Role role);
        User UpdateUser(User user);
        bool DeleteUser(int id);
        User Authenticate(string email, string password);

        /*  public User AddworkoutPlan(int userId, int targetUserId);

         public User AddMealPlan(int userId, int targetUserId);*/



        // ---------------- meals management --------------

        public IList<Meal> GetMeals();

        public Meal GetmealByName(string MealName);

        public Meal GetMealById(int id);

        public Meal GetMealdate(DateTime date);

        public Meal AddMeal(int userId, int Id, string Name, int TotalCalories, string photoUrl);
        public Meal UpdateMeal(Meal updated);
        public bool DeleteMeal(int id);
        // ---------------- food management --------------
        public Food CreateFood(int UserId, int mealId, string name, int weight,
         int calories, string FoodPhotoUrl);

        public bool DeleteFood(int id);
        public Food GetFoodById(int id);

        public Food getFoodByName(string Name);

        public IList<Food> GetAllFood();
        public IList<Food> SearchFoods(string name, string query);

        //-------------------Workout management--------------------
        public IList<Workout> GetWorkouts();

        public Workout GetWorkoutById(int id);

        public Workout GetWorkoutdate(DateTime date);



        public Workout AddWorkout(int userId, int id, string name, string Creator, DateTime DateWorkout);
        public Workout UpdateWorkout(Workout updated);
        public bool DeleteWorkout(int id);

        // ----------------------- Exersize activities 
        public IList<Exersize> GetExersizes();
        public Exersize CreateExersize(int UserId, int Id, string exName, string MuscleGroup,
        int Reps, int Sets, int weight, string ExPhotoUrl);

        public bool DeleteExersize(int id);
        public Exersize GetExersizeById(int id);

        public Exersize getExersizeByName(string Name);

        public IList<Exersize> GetAllExersizes();
        public IList<Exersize> SearchExersizes(string name, string query);


        // ----------------------- Meassage activities 
        Message CreateMessage(int UserId, int id, string name, string issue);
        Message GetMessage(int id);
        Message CloseMessage(int id, string resolution);
        bool DeleteMessage(int id);
        IList<Message> GetAllMessage();
        IList<Message> GetOpenMessage();
        IList<Message> SearchMessage(TicketRange range, string query);


        // -------------- Module Management -----------------
        Module AddModule(string name);
        UserModule AddUserToModule(int userId, int moduleId, int mark);
        bool RemoveUserFromModule(int userId, int moduleId);
        UserModule GetUserModule(int id);
        IList<Module> GetAvailableModulesForUser(int id);
        UserModule UpdateUserModuleMark(int userId, int moduleId, int mark);

        // ------------- Event Management ------------------

        public Event CreateEvent(int id, int UserId, string Title, string Startdate);

        public Event GetEvent(int id);

        public IList<Event> GetEvents();



    }

}
