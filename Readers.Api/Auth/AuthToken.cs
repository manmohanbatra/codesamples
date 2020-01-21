namespace ReadersApi.Providers
{
    public class AuthToken
    {
        public long ExpiresIn { get; set; }

        public string AccessToken { get; set; }
    }
}