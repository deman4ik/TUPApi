using Newtonsoft.Json;

namespace tupapiService.Authentication
{
    public class AccessToken
    {
        public AccessToken(string token)
        {
            Token = token;
        }

        [JsonProperty("access_token")]
        public string Token { get; set; }
    }
}