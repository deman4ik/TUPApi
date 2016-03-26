using Newtonsoft.Json;

namespace tupapiService.Authentication
{
    public class AccessToken
    {
        [JsonProperty("access_token")]
        public string Token { get; set; }

        public AccessToken(string token)
        {
            Token = token;
        }
    }
}