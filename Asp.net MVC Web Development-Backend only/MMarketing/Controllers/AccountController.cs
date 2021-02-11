using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.PeerToPeer.Collaboration;
using System.Runtime.Serialization.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Office2013.PowerPoint;
using DocumentFormat.OpenXml.Presentation;
using Facebook;
//using LinqToTwitter;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Facebook;
using MMarketing.Helper;
//using MMarketing.Helper;
using MMarketing.Models;
using Newtonsoft.Json;
using static MMarketing.Helper.FacebookApi;
using static MMarketing.Models.FacebookPlay;
//using static MMarketing.Helper.FacebookApi;

namespace MMarketing.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        ApplicationDbContext db;
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //Login with Facebook login javascript 

        public class AuthResponse
        {
            public string accessToken { get; set; }
            public string expiresIn { get; set; }
            public string reauthorize_required_in { get; set; }
            public string signedRequest { get; set; }
            public string userID { get; set; }
        }

        public class FBView
        {
            public string Status { get; set; }
            public AuthResponse AuthResponse { get; set; }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> FBLogin(FBView view)
        {

            //DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(View));
            return View(view);

        }



        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
          
             db = new ApplicationDbContext();
           


            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            if (loginInfo.Login.LoginProvider == "Twitter")
            {
                List<Models.MarketingViewModel> infoEmail = db.mMarketings.Where(x => x.UserEmail == loginInfo.Email).ToList();

                if (infoEmail.Count == 0)
                {
                    {
                        db.mMarketings.Add(new MMarketing.Models.MarketingViewModel
                        {
                            UserName = loginInfo.DefaultUserName,
                            UserEmail = loginInfo.Email,
                            Subscribe = true,
                           // CommentID = ""

                        }) ;
                        db.SaveChanges();
                    }
                }

            }
            if (loginInfo.Login.LoginProvider == "Google")

            {
                List<Models.MarketingViewModel> listEmail = db.mMarketings.Where(x => x.UserEmail == loginInfo.Email).ToList();
                if (listEmail.Count == 0)
                {
                    db.mMarketings.Add(new MMarketing.Models.MarketingViewModel
                    {
                        UserName = loginInfo.DefaultUserName,
                        UserEmail = loginInfo.Email,
                        Subscribe = true,
                       // CommentID = ""
                        
                    });
                    db.SaveChanges();
                }
            }
            // ClaimsIdentity ext = await AuthenticationManager.GetExternalIdentityAsync(DefaultAuthenticationTypes.ExternalCookie);
            //var email = ext.Claims.First(x => x.Type.Contains("emailaddress")).Value;
            //var access_token = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "FacebookAccessToken");
            //FacebookObj getPageId = await new FacebookApi(access_token.Value, null, null).QueryPages();
            //FacebookObj getPostId = await new FacebookApi(access_token.Value, getPageId.data.First().id, null).QueryPost();
            //FacebookAccount getPageToken = await new FacebookApi(access_token.Value, getPageId.data.First().id, getPostId.data.First().id).QueryPageToken();
            //FacebookObj getCommentId = await new FacebookApi(access_token.Value, getPageId.data.First().id, getPostId.data.First().id).QueryComments();
            //var replyComment = new FacebookApi(getPageToken.access_token, getPageId.data.First().id, getPostId.data.First().id).ReplyComment(getCommentId.data.First().id, "Superman is the fastest!");
            
            if (loginInfo.Login.LoginProvider == "Facebook")
            {
                
                //var First_Name = loginInfo.ExternalIdentity.Claims.First(x => x.Type == "urn:facebook:first_name").Value;
                //var Last_Name = loginInfo.ExternalIdentity.Claims.First(x => x.Type == "urn:facebook:last_name").Value;
                //var email = loginInfo.ExternalIdentity.Claims.First(x => x.Type == "urn:facebook:email").Value;
                var access_token = loginInfo.ExternalIdentity.Claims.First(c => c.Type == "FacebookAccessToken");
                var db_access_token = db._Tokens.FirstOrDefault();
               
                try
                {
                    
                    if (db_access_token == null)
                    {
                        db._Tokens.Add(new Access_Token { ACcess_TOken = access_token.Value });
                        await db.SaveChangesAsync();

                    }
                    else
                    {
                        if (db_access_token.ACcess_TOken != access_token.Value)
                        {
                            db._Tokens.Remove(db_access_token);
                            await db.SaveChangesAsync();
                            db._Tokens.Add(new Access_Token { ACcess_TOken = access_token.Value });
                            await db.SaveChangesAsync();
                        }


                    }

                    
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }


                List<Models.MarketingViewModel> listEmail = db.mMarketings.Where(x => x.UserEmail == loginInfo.Email).ToList();
                if (listEmail.Count == 0)
                {

                    db.mMarketings.Add(new MMarketing.Models.MarketingViewModel
                    {
                        UserName = loginInfo.DefaultUserName,
                        UserEmail = loginInfo.Email,
                        Subscribe = true,
                       // CommentID = ""

                    }) ;
                    db.SaveChanges();
                }
                
              
                };
            
            
            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel {Email = loginInfo.Email });
            }
           
        }
       
      





        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        /*********************************************************************************
         ********------------GET ACTION TO SEE ALL THE POSTS IN THE PAGE-------***********
         *********************************************************************************
       */
        [HttpGet]
     public ActionResult FacebookReply()
        {
            string app_id = "379050216797422";
            string app_secret = "e0fb119bdbb7dd6d5096b525d9da115d";
            if (Request["code"] == null)
            {
                var redirecturl = string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}", app_id, Request.Url.AbsoluteUri);
                Response.Redirect(redirecturl); 
            }
            else
            {
                AccessToken at = new AccessToken();
                string url = string.Format("https://graph.facebook.com/v8.0/oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}", app_id, Request.Url.AbsoluteUri, app_secret, Request["code"].ToString());
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string result = reader.ReadToEnd();
                    at = JsonConvert.DeserializeObject<AccessToken>(result);
                    ViewBag.access_token = at.access_token;
                }
               FacebookObj PageIds =  new FacebookApi(at.access_token, null, null).QueryPages();
                ViewBag.PageIds = PageIds.data;
            }
            return View();
        }

        /*********************************************************************************
         ********------------POST ACTION TO REPLY THE POSTS IN THE PAGE-------************
        **********************************************************************************
       */
        [HttpPost]
       public ActionResult FacebookReply (string message, string access_token)
        {
            
            if (message == null)
            {
                return RedirectToAction("Contact", "Home");
            }
            else
            {
                FacebookObj pageID = new FacebookApi(access_token, null, null).QueryPages();
                ViewBag.PageIds = pageID.data;
                foreach (var pageId in pageID.data)
                {
                    FacebookObj postID = new FacebookApi(access_token, pageId.id, null).QueryPost();
                    foreach (var postId in postID.data)
                    {
                        FacebookAccount pageToken = new FacebookApi(access_token, pageId.id, postId.id).QueryPageToken();
                        FacebookObj commentID = new FacebookApi(access_token, pageId.id, postId.id).QueryComments();
                        using (ApplicationDbContext db = new ApplicationDbContext())
                            foreach (var comment in commentID.data)
                            {
                                var replycomments = db.mMarketings.Where(a => a.CommentID == comment.id).ToList();
                                if (replycomments.Count==0)
                                {
                                    var replyComment = new FacebookApi(pageToken.access_token, pageId.id, postId.id).ReplyComment(comment.id, message);
                                    db.mMarketings.Add(new MarketingViewModel { CommentID = comment.id });
                                    db.SaveChanges();
                                }
                            }
                    }
                }
            }
            return View();
        }


        /*********************************************************************************
         ********------------PARTIAL VIEW TO SEE ALL THE POSTS IN THE PAGE-------*********
         *********************************************************************************
        */
        [HttpGet]
        public PartialViewResult FacebookpagePost(string access_token, string pageID)
        {
            FacebookObj Post =  new FacebookApi(access_token, pageID, null).QueryPost();
            List<FacebookAccount> PostList = Post.data;
            return PartialView("_FacebookPagePosts", PostList);
        }


        /*********************************************************************************
        ********------------Getting webhook set-up-------*********
        *********************************************************************************
       */
        [HttpGet]
        [AllowAnonymous]
        public int FacebookWebhook(Hub hub)
        {
           
            int challenge = 0;
            if (hub.mode == "subscribed" && hub.verify_token =="qw12rt34as12df34")
            {
                challenge = hub.challenge;
            }
            else
            {
                challenge = 01;
            }
            return hub.challenge;
        }

        //[HttpGet]
        //[AllowAnonymous]
        //public int FacebookWebhooks (Hub hub)
        //{
        //    string app_id = "379050216797422";
        //    string app_secret = "e0fb119bdbb7dd6d5096b525d9da115d";
        //    Hubb.challenge = hub.challenge;
        //    if (hub.mode == "subscribed" && hub.verify_token == "qw12rt34as12df34")
        //    {
        //        Hubb.mode = hub.mode;
        //        Hubb.verify_token = hub.verify_token;
        //        if (Request["code"] == null)
        //        {
        //            var redirecturl = string.Format("https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}", app_id, Request.Url.AbsoluteUri);
        //            Response.Redirect(redirecturl);
        //        }
        //    }
        //    if (Request["code"] != null)
        //    {
        //        Access_Token a = new Access_Token();

        //    }
        //    }



        //    return challenge;
        

        /*********************************************************************************
        ********------------Feed method using facebook commentID-------*********
        *********************************************************************************
       */

        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpStatusCodeResult> FacebookWebhook(Feed feed)
        {
           db = new ApplicationDbContext();
            try
            {
                string access_token = db._Tokens.FirstOrDefault().ACcess_TOken;
                foreach(var e in feed.entry)
                {
                    foreach (var c in e.changes)
                    {
                        string pageId = e.id;
                        string postId = c.value.post_id;
                        string commentID = c.value.comment_id;
                        var commented = db.Comment_IDss.Where(a => a.Comment_IDs == commentID).ToList();
                        if (commented.Count == 0)
                        {
                            string message = "Ola";
                            FacebookAccount pageToken =  new FacebookApi(access_token, pageId, null).QueryPageToken();
                            if (commentID != null && c.value.message != null && c.value.message != message)
                            {
                                var commentReply = new FacebookApi(pageToken.access_token, pageId, postId).ReplyComment(commentID, message);
                               await db.SaveChangesAsync();
                                var privateReply = new FacebookApi(pageToken.access_token, pageId, postId).PrivateReply(commentID);
                            }
                        } 
                    }
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception )
            {
                throw;
            }
        } 






        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]

        public ActionResult ExternalLoginFailure()
        {
            return View();
        }
      
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
    
        
           

        }
        #endregion
    }
}