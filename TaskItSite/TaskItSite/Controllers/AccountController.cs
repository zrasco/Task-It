using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskItSite.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace TaskItSite.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {

            return View();
            //return Challenge("Google");
        }

        [HttpPost]
        public async Task<IActionResult> Login(string idtoken)
        {
            // idtoken contains the google ID token associated with the account. Use the ID token to get a google username

            User loginUser = new User();
            string username = GetUserDetails(idtoken);

            if (LoginUser(loginUser.username, "blah"))
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginUser.username)
            };

                var userIdentity = new ClaimsIdentity(claims, "login");

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync("CookieAuthentication", principal);

                //Just redirect to our index after logging in. 
                return Redirect("/");
            }
            return View();
        }

        private bool LoginUser(string username, string password)
        {
            //As an example. This method would go to our data store and validate that the combination is correct. 
            //For now just return true. 
            return true;
        }

        private const string GoogleApiTokenInfoUrl = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={0}";

        public string GetUserDetails(string providerToken)
        {
            var httpClient = new HttpClient();
            var requestUri = new Uri(string.Format(GoogleApiTokenInfoUrl, providerToken));

            HttpResponseMessage httpResponseMessage;
            try
            {
                httpResponseMessage = httpClient.GetAsync(requestUri).Result;
            }
            catch (Exception ex)
            {
                return null;
            }

            if (httpResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

            var response = httpResponseMessage.Content.ReadAsStringAsync().Result;
            var googleApiTokenInfo = JsonConvert.DeserializeObject<Dictionary<string, string>>(response);

            return "test";

            /*
            if (!SupportedClientsIds.Contains(googleApiTokenInfo.aud))
            {
                //Log.WarnFormat("Google API Token Info aud field ({0}) not containing the required client id", googleApiTokenInfo.aud);
                return null;
            }

            return new x 
            {
                Email = googleApiTokenInfo.email,
                FirstName = googleApiTokenInfo.given_name,
                LastName = googleApiTokenInfo.family_name,
                Locale = googleApiTokenInfo.locale,
                Name = googleApiTokenInfo.name,
                ProviderUserId = googleApiTokenInfo.sub
            };
            */
        }
    }
}