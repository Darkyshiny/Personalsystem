using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Personalsystem.Models.VM
{

    public class TransferVM
    {

        public string userID { get; set; }
        public int? groupID { get; set; }
        public IEnumerable<SelectListItem> dbgroupList { get; set; }

    }
}