using System;

namespace tupapi.Shared.DataObjects
{
    public class PostResponse
    {
        public string Id { get; set; }
        public string Sas { get; set; }

        public override string ToString()
        {
            return "# Post Id:" + Environment.NewLine + Id + Environment.NewLine +
                   "# Sas:" + Environment.NewLine + Sas;
        }
    }
}