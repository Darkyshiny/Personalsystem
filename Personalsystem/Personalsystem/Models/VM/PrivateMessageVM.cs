using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models.VM
{
    public class PrivateMessageVM
    {
        public PrivateMessageVM()
        {
            PrivateMessage PM = new PrivateMessage();
        }
        public PrivateMessage PM { get; set; }
        public string UserName { get; set; }
    }
}