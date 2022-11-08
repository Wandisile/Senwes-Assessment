using SenwesAssignment_API.Models;
using System.ComponentModel.DataAnnotations;

namespace SenwesAssignment_API.Confuguration
{
    public class AppSettings
    {
        public LoginDetails LoginDetails { get; set; }
        public JWT JWT { get; set; }
    }

    public class LoginDetails
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class JWT
    {
        public string ValidAudience { get; set; }

        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
