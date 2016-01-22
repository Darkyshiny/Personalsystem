using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
    public class BlogPost : Message
    {
        
    }
    public class PrivateMessage : Message
    {
        public int SenderId { get; set; }
    }
}