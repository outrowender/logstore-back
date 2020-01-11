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
            var password = "WenderPatrick1122$";
            var password2 = "WenderPatrick1122$";

            var hash = AuthHelpers.getHashOfString(password);

            Assert.Equal(AuthHelpers.getHashOfString(password2), hash);

        }

        [Fact]
        public void different_string_has_diferrent_hash()
        {
            var password = "WenderPatrick1122$";
            var password2 = "wenderpatrick1122$";

            var hash = AuthHelpers.getHashOfString(password);

            Assert.NotEqual(AuthHelpers.getHashOfString(password2), hash);

        }
    }
}
