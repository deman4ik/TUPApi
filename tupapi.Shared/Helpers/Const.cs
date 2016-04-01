namespace tupapiService.Helpers
{
    public static class Const
    {
        // Authentication Providers
        public const string Standart = "STANDART";
        public const string Instagram = "INSTAGRAM";
        public const string Facebook = "FB";
        public const string Vkontakte = "VK";

        //Regex
        public const string EmailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
           @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public const string NameRegex = @"([a-z][0-9a-z])\w+";


    }
}