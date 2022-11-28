using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using JustGoApi.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace JustGoApi.Handler
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ReuseDbContext _DBContext; 
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, 
            UrlEncoder encoder, ISystemClock clock,ReuseDbContext dbContext ) : base(options, logger, encoder, clock)
        {
            _DBContext = dbContext;
        }
    
        protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("No Header Found");
            var _haedervalue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(_haedervalue.Parameter);
            string credentials=Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(":");
                string username=array[0];
                string password=array[1];
                var user=this._DBContext.Users.FirstOrDefault(item=>item.Name == username && item.Password==password);
                if(user==null)
                  return AuthenticateResult.Fail("Unauthorized");

                var claim = new[]{new Claim(ClaimTypes.Name, username)};
                var identity=new ClaimsIdentity(claim,Scheme.Name);
                var principal=new ClaimsPrincipal(identity);
                var ticket=new AuthenticationTicket(principal,Scheme.Name);
               
                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

        }

    }
}
