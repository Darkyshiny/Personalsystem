using Personalsystem.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models
{
    public class Company
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        PersonalSystemContext db = new PersonalSystemContext();

        public void SetWorkTimeToGroup(Group group, int starttime, int endtime)
        {
            var sresult = new TimeSpan(starttime, 0, 0);
            var eresult = new TimeSpan(endtime, 0, 0);


            List<ApplicationUser> list = db.user.Where(u => u.gId == group.Id).ToList();
            foreach (var user in list)
            {
                db.user.Find(user.Id).start = sresult;
                db.user.Find(user.Id).end = eresult;
            }
            db.SaveChanges();


        }


    }
}