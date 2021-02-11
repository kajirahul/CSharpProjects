using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace MMarketing.Models
{



    public class MarketingViewModel
    {
        [Key]

        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool Subscribe { get; set; } = true;
        public string CommentID { get; set; }


    }
    public class Access_Token
    {
        [Key]
        public int ID { get; set; }
        [StringLength(Int32.MaxValue)]
        public string ACcess_TOken { get; set; }
    }
    public class CommentID
    {
        [Key]
        public string Comment_IDs { get; set; }
    }


}