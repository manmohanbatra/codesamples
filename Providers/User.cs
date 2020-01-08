namespace ReadersApi.Providers
{
    public class Reader
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class User : Reader
    {
        public string Role { get; set; }
    }

    public class Roles
    {
        public const string Admin = "Admin";
        public const string Editor = "Editor";
        public const string Reader = "Reader";
        public const string None = "None";
    }
}