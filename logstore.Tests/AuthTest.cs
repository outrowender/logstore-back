using System;
using logstore.Auth;
using Xunit;


namespace logstore.Tests
{
    public class AuthTest
    {

        [Fact]
        public void can_hash_twice_same_string()
        {
            // Arrange
            var password = "WenderPatrick1122$";
            var password2 = "WenderPatrick1122$";

            // Act
            var hash = AuthHelpers.getHashOfString(password);

            //Assert
            Assert.Equal(AuthHelpers.getHashOfString(password2), hash);

        }

        [Fact]
        public void different_string_has_diferrent_hash()
        {
            //Arrange
            var password = "WenderPatrick1122$";
            var password2 = "wenderpatrick1122$";

            //act
            var hash = AuthHelpers.getHashOfString(password);

            //assert
            Assert.NotEqual(AuthHelpers.getHashOfString(password2), hash);

        }
    }
}
