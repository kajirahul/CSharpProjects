using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Twitter;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using MMarketing.Models;
using Microsoft.Owin.Security.Facebook;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.Configuration;

namespace MMarketing
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864


        public class FacebookBackChannelHandler : HttpClientHandler
        {
            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                if (!request.RequestUri.AbsolutePath.Contains("/oauth"))
                {
                    request.RequestUri = new Uri(request.RequestUri.AbsoluteUri.Replace("?access_token", "&access_token"));
                }

                return await base.SendAsync(request, cancellationToken);
            }

           
      
        }
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.

            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //consumerKey: "pW5HqWhWNuTwe6q9wuknbYhni",
            //consumerSecret: "AGgwXXiMRLHGl0KpmjgjuFH3Kt2vmoWYkidc8O3GwUclY7PvRA");

            /* app.UseFacebookAuthentication(new FacebookAuthenticationOptions()
             {
                 AppId = "379050216797422",
                 AppSecret = "e0fb119bdbb7dd6d5096b525d9da115d"
             });
            */

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "88628182739-oashagp3v9t313qe9ckik782o94vqprc.apps.googleusercontent.com",
                ClientSecret = "6ft3kvXLqoa2wvhyHFVxG_16"
            });
            var twitterOptions = new Microsoft.Owin.Security.Twitter.TwitterAuthenticationOptions()
            {
                ConsumerKey = "xNKsNyv31ybiN5IsMrmr0r80L",
                ConsumerSecret = "WKZ527rn7A5bD9uzpjfVwd8mJ25RClWnBzowmHkgWf6ea5g68l",

                BackchannelCertificateValidator = new Microsoft.Owin.Security.CertificateSubjectKeyIdentifierValidator(new[]
              {
                "A5EF0B11CEC04103A34A659048B21CE0572D7D47", // VeriSign Class 3 Secure Server CA - G2
                "0D445C165344C1827E1D20AB25F40163D8BE79A5", // VeriSign Class 3 Secure Server CA - G3
                "7FD365A7C2DDECBBF03009F34339FA02AF333133", // VeriSign Class 3 Public Primary Certification Authority - G5
                "39A55D933676616E73A761DFA16A7E59CDE66FAD", // Symantec Class 3 Secure Server CA - G4
                "‎add53f6680fe66e383cbac3e60922e3b4c412bed", // Symantec Class 3 EV SSL CA - G3
                "4eb6d578499b1ccf5f581ead56be3d9b6744a5e5", // VeriSign Class 3 Primary CA - G5
                "5168FF90AF0207753CCCD9656462A212B859723B", // DigiCert SHA2 High Assurance Server C‎A 
                "B13EC36903F8BF4701D498261A0802EF63642BC3" // DigiCert High Assurance EV Root CA
             }),
            };
            app.UseTwitterAuthentication(twitterOptions);
            twitterOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;

            var facebookOptions = new FacebookAuthenticationOptions()

            {
                AppId = "379050216797422",
                AppSecret = "e0fb119bdbb7dd6d5096b525d9da115d",
                AuthorizationEndpoint = "https://www.facebook.com/v8.0/dialog/oauth",
                BackchannelHttpHandler = new FacebookBackChannelHandler(),
                TokenEndpoint = "https://graph.facebook.com/v8.0/oauth/access_token",
                UserInformationEndpoint = "https://graph.facebook.com/v2.4/me?fields=id,name,email,first_name,last_name,pages_show_list,pages_manage_post",
                //  UserPage = ""https://graph.facebook.com/v2.4/me/accounts",


                Provider = new FacebookAuthenticationProvider()
                {
                    OnAuthenticated = async context =>
                {
                    await Task.Run(() => context.Identity.AddClaim(new System.Security.Claims.Claim("FacebookAccessToken", context.AccessToken)));
                    foreach (var claim in context.User)
                    {
                        var claimType = string.Format("urn:Facebook:{0}", claim.Key);
                        string claimValue = claim.Value.ToString();
                        if (!context.Identity.HasClaim(claimType, claimValue))
                            context.Identity.AddClaim(new System.Security.Claims.Claim(claimType, claimValue, "XmlSchemaString", "Facebook"));

                    }
                }
                }
            };
            facebookOptions.Scope.Add("email");
            //facebookOptions.Scope.Add("pages_manage_posts");
            //facebookOptions.Scope.Add("pages_show_list");
            //facebookOptions.Scope.Add("pages_messaging");
            //facebookOptions.Scope.Add("pages_read_user_content");
            //facebookOptions.Scope.Add("user_posts");
            //facebookOptions.Scope.Add("pages_manage_engagement");


            facebookOptions.SignInAsAuthenticationType = DefaultAuthenticationTypes.ExternalCookie;
            app.UseFacebookAuthentication(facebookOptions);

        }
    }
    }
