
using Xunit;
using Flexc.Core.Models;
using Flexc.Core.Services;


using Flexc.Data.Services;

namespace Flexc.Test
{
    public class ServiceTests
    {
        private IUserService service;

        public ServiceTests()
        {
            service = new UserServiceDb();
            service.Initialise();
        }

        [Fact]
        public void EmptyDbShouldReturnNoUsers()
        {
            // act
            var users = service.GetUsers();

            // assert
            Assert.Equal(0, users.Count);
        }
        
        [Fact]
        public void AddingUsersShouldWork()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
            service.AddUser("guest", "guest@mail.com", "guest", Role.guest);

            // act
            var users = service.GetUsers();

            // assert
            Assert.Equal(2, users.Count);
        }

        [Fact]
        public void UpdatingUserShouldWork()
        {
            // arrange
            var user = service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
            
            // act
            user.Name = "administrator";
            user.Email = "admin@mail.com";            
            var updatedUser = service.UpdateUser(user);

            // assert
            Assert.Equal("administrator", user.Name);
            Assert.Equal("admin@mail.com", user.Email);
        }

        [Fact]
        public void LoginWithValidCredentialsShouldWork()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin );
            
            // act            
            var user = service.Authenticate("admin@mail.com","admin");

            // assert
            Assert.NotNull(user);
           
        }

        [Fact]
        public void LoginWithInvalidCredentialsShouldNotWork()
        {
            // arrange
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin);

            // act      
            var user = service.Authenticate("admin@mail.com","xxx");

            // assert
            Assert.Null(user);
           
        }



        [Fact]
        public void AddworkoutAddAWorkout()
        {
            // Given
            service.AddUser("chest", "admin@mail.com", "admin", Role.admin);
            // When
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            // Then
            Assert.NotNull(workout);

            Assert.Equal(1,workout.Id);
            Assert.Equal("chest",workout.Name);
            Assert.Equal("creatorname",workout.Creator);
            Assert.Equal(System.DateTime.Now,workout.DateWorkout);
        }

        [Fact]
        public void AddworkoutAddAWorkouts()
        {
            // Given
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin);
            // When
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            var workout2 = service.AddWorkout(2,1,"name","creatorname",System.DateTime.Now);
            var workoutlist = service.GetWorkouts();
            // Then
            Assert.NotNull(workoutlist);
        }



        [Fact]
        public void GetAllworkoutsWhen0ShouldReturn0()
        {

             // Given
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin);
            // When
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            var workout2 = service.AddWorkout(2,1,"name","creatorname",System.DateTime.Now);
            var workoutlist = service.GetWorkouts();
       
            // When
            var count = workoutlist.Count;

            // Then
            Assert.Equal(2,count);
        }

        [Fact]
        public void GetAllworkoutsWhen2ShouldReturn2()
        {
            // Given
            var workouts = service.GetWorkouts();
            // When
            var count = workouts.Count;

            // Then
            Assert.Equal(0,count);
        }


        [Fact]
        public void deleteworkouttest()
        {
            // Given
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin);
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            var workout2 = service.AddWorkout(2,1,"name","creatorname",System.DateTime.Now);
            var workoutlist = service.GetWorkouts();
            // When
            service.DeleteWorkout(1);
        
            // Then
            Assert.Null(service.GetWorkoutById(1));
        }

        [Fact]
        public void EditWorkoutTest()
        {
            // Given
            service.AddUser("admin", "admin@mail.com", "admin", Role.admin);
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            var workout2 = service.AddWorkout(2,1,"name","creatorname",System.DateTime.Now);
            var workoutlist = service.GetWorkouts();
            // When
            workout2 = service.UpdateWorkout(workout);
            // Then
            Assert.Equal(workout,workout2);
            
        }

        [Fact]
        public void TestGetworkoutbyDate ()
        {
            // Given
                        service.AddUser("admin", "admin@mail.com", "admin", Role.admin);
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            var workout2 = service.AddWorkout(2,1,"name","creatorname",System.DateTime.Now);
            var workoutlist = service.GetWorkouts();

            // When
            var test = service.GetWorkoutdate(System.DateTime.Now);
            // Then
            Assert.NotNull(test);

            Assert.Equal(System.DateTime.Now,test.DateWorkout);
        }

      

        ///////////////////////////////////////////////////////////

        // Exersize tests

        [Fact]
        public void addExersizeTest()
        {
            // Given
                         var u4 = service.AddUser("admin", "admin@mail.com", "admin", Role.admin);
            var workout =  service.AddWorkout(1,1,"name","creatorname",System.DateTime.Now);
            var workout2 = service.AddWorkout(2,1,"name","creatorname",System.DateTime.Now);
            var workoutlist = service.GetWorkouts();
             // When
             var e1 = service.CreateExersize(u4.Id,workout.Id,"Preacher curl","Bicep",10,4,30,"url");
            // Then

            Assert.NotNull(e1);
        }











    }
}
