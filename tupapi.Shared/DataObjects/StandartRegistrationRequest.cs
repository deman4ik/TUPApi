using Newtonsoft.Json;

namespace tupapi.Shared.DataObjects
{
    public class StandartRegistrationRequest
    {
        #region Infrastructe

        [JsonIgnore] private const string email = "email";
        [JsonIgnore] private const string name = "name";
        [JsonIgnore] private const string password = "password";

        public StandartRegistrationRequest()
        {
            EmailPropertyName = email;

            NamePropertyName = name;

            PasswordPropertyName = password;
        }


        [JsonIgnore]
        public string EmailPropertyName { get; }

        [JsonIgnore]
        public string NamePropertyName { get; }

        [JsonIgnore]
        public string PasswordPropertyName { get; }

        #endregion

        [JsonProperty(PropertyName = email)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = name)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = password)]
        public string Password { get; set; }
    }
}