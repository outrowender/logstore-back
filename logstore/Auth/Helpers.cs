namespace logstore.Auth
{
    public static class AuthHelpers
    {
        public static string getHashOfString(string value)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(value);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}