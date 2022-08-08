namespace TwitterWebAPI.Utilities
{
    public static class RandomPassword
    {
        public static string GenerateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%&*_-";
            Random random = new Random();
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }
    }
}
