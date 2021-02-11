using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MMarketing.Models
{
    public class FacebookPlay
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Recipient
        {
            public string comment_id { get; set; }
        }

        public class Button
        {
            public string type { get; set; }
            public string url { get; set; }
            public string title { get; set; }
            public string webview_height_ratio { get; set; }
            public ShareContents share_contents { get; set; }
            public string payload { get; set; }
            public GameMetadata game_metadata { get; set; }
          
        }
            public class Attachment
        {
            public string type { get; set; }
            public Payload payload { get; set; }
        }

        public class Message
        {
            public Attachment attachment { get; set; }
        }

        public class Root
        {
            public Recipient recipient { get; set; }
            public Message message { get; set; }
        }



        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
      

        public class GameMetadata
        {
            public string player_id { get; set; }
        }

     

        public class Payload
        {
            public string template_type { get; set; }
            public string text { get; set; }
            public List<Button> buttons { get; set; }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
     

        public class DefaultAction
        {
            public string type { get; set; }
            public string url { get; set; }
        }

        public class Button2
        {
            public string type { get; set; }
            public string url { get; set; }
            public string title { get; set; }
        }

        public class Element2
        {
            public string title { get; set; }
            public string subtitle { get; set; }
            public string image_url { get; set; }
            public DefaultAction default_action { get; set; }
            public List<Button2> buttons { get; set; }
        }

        public class Payload2
        {
            public string template_type { get; set; }
            public List<Element2> elements { get; set; }
        }

        public class Attachment2
        {
            public string type { get; set; }
            public Payload2 payload { get; set; }
        }

        public class ShareContents
        {
            public Attachment2 attachment { get; set; }
        }

        
        public class Element
        {
            public string title { get; set; }
            public string subtitle { get; set; }
            public string image_url { get; set; }
            public List<Button> buttons { get; set; }
        }

        public class Value
        {
            public string post_id { get; set; }
            public string comment_id { get; set; }
            public string sender_name { get; set; }
            public string sender_id { get; set; }
            public string item { get; set; }
            public string verb { get; set; }
            public int published { get; set; }
            public int created_time { get; set; }
            public string message { get; set; }
        }
        public class Change
        {
            public string field { get; set; }
            public Value value { get; set; }
        }
        public class Entry
        {
            public List<Change> changes { get; set; }
            public string id { get; set; }
            public int time { get; set; }
        }
        public class Feed
        {
            public List<Entry> entry { get; set; }
            public string @object { get; set; }
        }
        public static class Hubb
        {
            public static string mode { get; set; }
            public static int challenge{ get; set; }
            public static string verify_token { get; set; }
        }
        public class Hub
        {
            public  string mode { get; set; }
            public  int challenge { get; set; }
            public string verify_token { get; set; }
        }
    }
}