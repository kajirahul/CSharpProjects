using Newtonsoft.Json;
using SharpRaven;
using SharpRaven.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Facebook;
using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using System.Text;
using RestSharp;
using Microsoft.Graph;
using MMarketing.Models;
using Microsoft.Owin.Security;

namespace MMarketing.Helper
{
    public class FacebookApi
    {
        private readonly string FB_PAGE_ID;
        private readonly string FB_POST_ID;
        private readonly string FB_ACCESS_TOKEN;
        private const string FB_BASE_ADDRESS = "https://graph.facebook.com/v8.0/";
        private RavenClient ravenClient = new RavenClient("https://f8ff1d46824442df943ef272bc251fe1@sentry.io/1251860");
        public FacebookApi(string accessToken, string fb_Page_Id, string fb_Post_Id)
        {
            FB_ACCESS_TOKEN = accessToken;
            FB_PAGE_ID = fb_Page_Id;
            FB_POST_ID = fb_Post_Id;
        }

        public string PrivateReply (string commentId)
        {
            try
            {
                var client3 = new RestClient("https://graph.facebook.com/v8.0/" + FB_PAGE_ID + "/messages");
                var request = new RestRequest();
                FacebookPlay.Recipient recipientModel = new FacebookPlay.Recipient();
                List<FacebookPlay.Button> buttonModel = new List<FacebookPlay.Button>();
                FacebookPlay.Payload payloadModel = new FacebookPlay.Payload();
                FacebookPlay.Attachment attachmentModel = new FacebookPlay.Attachment();
                FacebookPlay.Message messageModel = new FacebookPlay.Message();
                FacebookPlay.Element elementModel = new FacebookPlay.Element();
                FacebookPlay.GameMetadata gameMetadataModel = new FacebookPlay.GameMetadata();
                FacebookPlay.ShareContents shareContentsModel = new FacebookPlay.ShareContents();
                FacebookPlay.Root rootModel = new FacebookPlay.Root();
                FacebookPlay.Button2 button2Model = new FacebookPlay.Button2();
                FacebookPlay.Element2 element2Model = new FacebookPlay.Element2();
                FacebookPlay.Payload2 payload2Model = new FacebookPlay.Payload2();
                FacebookPlay.DefaultAction defaultActionModel = new FacebookPlay.DefaultAction();

                buttonModel.Add(new FacebookPlay.Button
                {
                    title = "Naruto",
                    type = "web_url",
                    url = "https://localhost:44366"

                });
                buttonModel.Add(new FacebookPlay.Button
                {
                    title = "Zoro",
                    type = "web_url",
                    url = "https://localhost:44366"
                });

                payloadModel.template_type = "button";
                payloadModel.text = "Chance to win $$$$";
                payloadModel.buttons = buttonModel;
                attachmentModel.type = "template";
                attachmentModel.payload = payloadModel;
                messageModel.attachment = attachmentModel;
                rootModel.message = messageModel;
                rootModel.recipient = recipientModel;
                elementModel.buttons = buttonModel;
                elementModel.title = "Share";
                recipientModel.comment_id = commentId;
                string recipientJson = JsonConvert.SerializeObject(recipientModel);
                string messageJson = JsonConvert.SerializeObject(messageModel);
                request.AddParameter("recipient", recipientJson);
                request.AddParameter("message", messageJson);
                request.AddParameter("message_type", "RESPONSE");
                request.AddParameter("access_token", FB_ACCESS_TOKEN);
                // commented for no private replies.
                var response3 = client3.Post(request);




            }
            catch (Exception )
            {
                throw;
            }
            return null;
        }
       
        public string ReplyComment(string commentId, string message)
        {
            
            try
            {   //********************************************************************
                /// Send a private message when a person comments on a post in a page.
                //********************************************************************

                var client3 = new RestClient("https://graph.facebook.com/v8.0/" + FB_PAGE_ID + "/messages");
                var request = new RestRequest();
                FacebookPlay.Recipient recipientModel = new FacebookPlay.Recipient();
                List<FacebookPlay.Button> buttonModel = new List<FacebookPlay.Button>();
                FacebookPlay.Payload payloadModel = new FacebookPlay.Payload();
                FacebookPlay.Attachment attachmentModel = new FacebookPlay.Attachment();
                FacebookPlay.Message messageModel = new FacebookPlay.Message();
                FacebookPlay.Element elementModel = new FacebookPlay.Element();
                FacebookPlay.GameMetadata gameMetadataModel = new FacebookPlay.GameMetadata();
                FacebookPlay.ShareContents shareContentsModel = new FacebookPlay.ShareContents();
                FacebookPlay.Root rootModel = new FacebookPlay.Root();
                FacebookPlay.Button2 button2Model = new FacebookPlay.Button2();
                FacebookPlay.Element2 element2Model = new FacebookPlay.Element2();
                FacebookPlay.Payload2 payload2Model = new FacebookPlay.Payload2();
                FacebookPlay.DefaultAction defaultActionModel = new FacebookPlay.DefaultAction();

                buttonModel.Add(new FacebookPlay.Button
                {
                    title = "Naruto",
                    type = "web_url",
                    url = "https://localhost:44366"
                    
                });
                buttonModel.Add(new FacebookPlay.Button
                {
                    title = "Zoro",
                    type = "web_url",
                    url = "https://localhost:44366"
                });
              
                payloadModel.template_type = "button";
                payloadModel.text = "Chance to win $$$$";
                payloadModel.buttons = buttonModel;
                attachmentModel.type = "template";
                attachmentModel.payload = payloadModel;
                messageModel.attachment = attachmentModel;
                rootModel.message = messageModel;
                rootModel.recipient = recipientModel;
                elementModel.buttons = buttonModel;
                elementModel.title = "Share";
                recipientModel.comment_id = commentId;
                string recipientJson = JsonConvert.SerializeObject(recipientModel);
                string messageJson = JsonConvert.SerializeObject(messageModel);
                request.AddParameter("recipient", recipientJson);
                request.AddParameter("message", messageJson);
                request.AddParameter("message_type", "RESPONSE");
                request.AddParameter("access_token", FB_ACCESS_TOKEN);
                // commented for no private replies.
                var response3 = client3.Post(request);
                //var content = response3.Content; // Raw content as string

                //***************************************************************
                /// Reply to a comment in a post in a page.
                //***************************************************************
                Uri uri = new Uri(FB_BASE_ADDRESS + commentId + "/comments"); 
                string myparameters = string.Format("message={0}&access_token={1}", message, FB_ACCESS_TOKEN);
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var response = client.UploadString(uri, myparameters);
                }

                /****************************************************************
                 **************-----------LIKE THE COMMENTS----------************
                 ****************************************************************
                */
                uri = new Uri(FB_BASE_ADDRESS + commentId + "/likes");
                myparameters = string.Format("access_token={0}",FB_ACCESS_TOKEN);
                using (WebClient client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    var response = client.UploadString(uri, myparameters);
                }


            }
            catch (Exception )
            {
                throw;
            }
            return null;
        }
            public FacebookObj QueryComments()
        {
            try
            {
                var webRequest = new WebClient().DownloadString("https://graph.facebook.com/v8.0/" + FB_POST_ID + "/comments?access_token=" + FB_ACCESS_TOKEN);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return JsonConvert.DeserializeObject<FacebookObj>(webRequest, settings);
            }
            catch (Exception exception)
            {
                ravenClient.Capture(new SentryEvent(exception));
            }
            return null;
        }
        public FacebookObj QueryPost()
        {
            try
            {
                var webRequest = new WebClient().DownloadString("https://graph.facebook.com/v8.0/" + FB_PAGE_ID + "/feed?access_token=" + FB_ACCESS_TOKEN);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return JsonConvert.DeserializeObject<FacebookObj>(webRequest, settings);
            }
            catch (Exception exception)
            {
                ravenClient.Capture(new SentryEvent(exception));
            }
            return null;
        }


        public FacebookAccount QueryPageToken()
        {
            try
            {
                var webRequest = new WebClient().DownloadString("https://graph.facebook.com/v8.0/" + FB_PAGE_ID + "?fields=access_token&access_token=" + FB_ACCESS_TOKEN);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return JsonConvert.DeserializeObject<FacebookAccount>(webRequest, settings);
            }
            catch (Exception exception)
            {
                ravenClient.Capture(new SentryEvent(exception));
            }
            return null;
        }

        public FacebookObj QueryPages()
        {
            try
            {
                var webRequest = new WebClient().DownloadString("https://graph.facebook.com/v8.0/me/accounts?access_token=" + FB_ACCESS_TOKEN);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                return JsonConvert.DeserializeObject<FacebookObj>(webRequest, settings);
            }
            catch (Exception exception)
            {
                ravenClient.Capture(new SentryEvent(exception));
            }
            return null;
        }
    }

    public class AccessToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
    }
    [DataContract]
    public class FacebookObj
    {
        [DataMember]
        public List<FacebookAccount> data;
        [DataMember]
        public FacebookNext paging;
    }
    [DataContract]
    public class FacebookAccount
    {
        [DataMember]
        public string name;
        [DataMember]
        public string category;
        [DataMember]
        public string id;
        [DataMember]
        public string access_token;
        [DataMember]
        public string created_time;
        [DataMember]
        public string message;
    }
    [DataContract]
    public class FacebookNext
    {
        [DataMember]
        public string next;
    }
    public class Recipient
    {
        public string comment_id { get; set; }
    }
    public class GameMetadata
    {
        public string game { get; set; }
    }
    public class Attachment
    {
        public string attach { get; set; }
    }
    public class Type
    {
        public string template { get; set; }
    }
    public class Payload
    {
        public string template_type { get; set; }

    }
    public class Button
    {
        public string type { get; set; }
    }
    public class Message
{
    public string text { get; set; }
}
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class AuthResponse
    {
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string reauthorize_required_in { get; set; }
        public string signedRequest { get; set; }
        public string userID { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public AuthResponse authResponse { get; set; }
    }



}




