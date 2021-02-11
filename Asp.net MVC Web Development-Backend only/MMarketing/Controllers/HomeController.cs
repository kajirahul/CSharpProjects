using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TweetSharp;
using MMarketing.Models;
using System.ComponentModel.DataAnnotations;
using Facebook;
using System.Web.Security;

namespace MMarketing.Controllers
{
    
    public class HomeController : Controller
    {

        
        
        //[HttpGet]
        // public ActionResult TwitterAuth()
        // {
        //     string key = "pW5HqWhWNuTwe6q9wuknbYhni";
        //     string secret = "AGgwXXiMRLHGl0KpmjgjuFH3Kt2vmoWYkidc8O3GwUclY7PvRA";
        //     string token = "1314355076262133760-vgWCDIOZsD4CJLRI5raYYrYIbwcKqp";
        //     TwitterService service = new TwitterService(key, secret , token);
        //     OAuthRequestToken requestToken = service.GetRequestToken("https://localhost:44366/Home/TwitterCallback");
        //     Uri uri = service.GetAuthenticationUrl(requestToken);
        //     return Redirect(uri.ToString());
        // }
        
        public ActionResult TwitterCallback(string oauth_token, string oauth_verifier)
        {
            var requesttoken = new OAuthRequestToken { Token = oauth_token };
           string key = "xNKsNyv31ybiN5IsMrmr0r80L";
           string secret = "WKZ527rn7A5bD9uzpjfVwd8mJ25RClWnBzowmHkgWf6ea5g68l";
            
            try
            {
                TwitterService service = new TwitterService(key, secret);
                OAuthAccessToken accesstoken = service.GetAccessToken(requesttoken, oauth_verifier);
                service.AuthenticateWith(accesstoken.Token, accesstoken.TokenSecret);
                VerifyCredentialsOptions option = new VerifyCredentialsOptions();
                TwitterUser user = service.VerifyCredentials(option);
                
                return View();
           }
           catch (Exception)
           {
               throw;
           }

        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Unsubscribe(string Email)
        { 
            using(ApplicationDbContext db = new ApplicationDbContext())
            {
              var MarketingEmails = db.mMarketings.Where(a => a.UserEmail == Email).ToList();
                if (MarketingEmails.Count() != 0 && Email != null)
                {
                    foreach(var MarketingEmail in MarketingEmails)
                    {
                        if(MarketingEmail.Subscribe == false)
                        {
                           
                            db.mMarketings.Attach(MarketingEmail);
                            MarketingEmail.Subscribe = true;
                            db.SaveChanges();
                        }
                    }
                }
            }
            return View();
        }
        //[HttpGet]
        //public ActionResult ReplyComment(string pageID, string postID, string commentID)
        //{

        //}

        [HttpPost]
        public ActionResult Unsubscribe(MarketingViewModel Model)
        {
           
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }


}
    
