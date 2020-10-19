using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Api.Authentication
{
    public class CastomClaimTypes
    {
        public const string Login = "http://schemas.microsoft.com/ws/2008/06/identity/claims/login";
        public const string UserId = "http://schemas.microsoft.com/ws/2008/06/identity/claims/userid";
        public const string Email = "http://schemas.microsoft.com/ws/2008/06/identity/claims/emal";
    }
}
