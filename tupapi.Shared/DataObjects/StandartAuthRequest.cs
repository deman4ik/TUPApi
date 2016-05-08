using System;
using Newtonsoft.Json;

namespace tupapi.Shared.DataObjects
{
    public class StandartAuthRequest
    {
        [JsonProperty(PropertyName = nameof(Email))]
        public string Email { get; set; }

        [JsonProperty(PropertyName = nameof(Name))]
        public string Name { get; set; }

        [JsonProperty(PropertyName = nameof(Password))]
        public string Password { get; set; }

        public override string ToString()
        {
            return "# Email:" + Environment.NewLine + Email + Environment.NewLine +
                   "# Name:" + Environment.NewLine + Name + Environment.NewLine +
                   "# Password:" + Environment.NewLine + Password;
        }
    }
}