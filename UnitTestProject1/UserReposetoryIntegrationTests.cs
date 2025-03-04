using Entities;
using Microsoft.EntityFrameworkCore;
using Reposetories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests;
using Xunit;
namespace Test
{
    public class UserReposetoryIntegrationTests:IClassFixture<DatabaseFixure>
    {
        private readonly MyShop327707238Context _context;
        private readonly UserReposetory _reposetory;

        public UserReposetoryIntegrationTests(DatabaseFixure fixture)
        {
            _context = fixture.Context; 
            _reposetory = new UserReposetory(_context);
        }

        [Fact]
        public async Task Get_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = new User { Email = "test@example.com", Password = "password123", FirstName = "John", LastName = "Doe" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Act

            var retrievedUser = await _reposetory.GetById(user.Id); 

           // var retrievedUser = await _context.Users.FindAsync(user.Id);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Email, retrievedUser.Email);
            Assert.Equal(user.FirstName, retrievedUser.FirstName);
            Assert.Equal(user.LastName, retrievedUser.LastName);
        }

        [Fact]
        public async Task Get_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            //var retrievedUser = await _context.Users.FindAsync(-1); // מזהה לא קיים
            var retrievedUser = await _reposetory.GetById(-1);
            // Assert
            Assert.Null(retrievedUser);
        }

        [Fact]
        public async Task Post_ShouldAddUser_WhenUserIsValid()
        {
            // Arrange
            var user = new User { Email = "newuser@example.com", Password = "securepassword" };

            // Act
            // var addedUser = await _context.Users.AddAsync(user);
             var addedUser = await _reposetory.Add(user);


            //await _context.SaveChangesAsync();

            // Assert
            Assert.NotNull(addedUser);
            Assert.Equal(user.Email, addedUser.Email);
            Assert.True(addedUser.Id > 0); // נניח שהמזהה יוקצה אוטומטית
        }

        [Fact]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            var user = new User { Email = "testuser@example.com", Password = "securepassword" };

            //_context.Users.Add(user);
            //await _context.SaveChangesAsync();

            // Act
            //var loggedInUser = await _context.Users
            //    .FirstOrDefaultAsync(u => u.Password == user.Password && u.Email == user.Email);
            var loggedInUser =await _reposetory.Login("testuser@example.com", "securepassword");
            // Assert
            Assert.NotNull(loggedInUser);
            Assert.Equal(user.Email, loggedInUser.Email.Trim());
            Assert.Equal(user.Password, loggedInUser.Password.Trim());
        }

        [Fact]
        public async Task Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Act
            var loggedInUser =  await _reposetory.Login("Ttestuser@example.com", "securepassword");

            // Assert
            Assert.Null(loggedInUser);
        }

    }
}



