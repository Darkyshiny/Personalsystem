using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Personalsystem.DataAccessLayer;
using Personalsystem.Models;
using Personalsystem.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Repositories
{
    public class PMRepo
    {
        private PersonalSystemContext db = new PersonalSystemContext();
  
        public void SaveNewMessageToDatabase(PrivateMessageVM privateMessageVM)
        {
            db.message.Add(privateMessageVM.PM);
            db.SaveChanges();
        }

        public List<PrivateMessage> GetReceivedMessages(string userid)
        {
            var message = db.message.Include("Receiver").Include("Sender").Where(p => p.receiverId == userid).ToList();
            return message;
        }

        public List<PrivateMessage> GetSentMessages(string userid)
        {
            var message = db.message.Include("Receiver").Include("Sender").Where(p => p.senderId == userid).ToList();
            return message;
        }

        public PrivateMessage FindPM(int? id)
        {
            PrivateMessage privateMessage = db.message.Find(id.Value);
            return privateMessage;
        }

        public PrivateMessageVM SetPMProperties(PrivateMessageVM privateMessageVM, string userid)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            privateMessageVM.PM.receiverId = userManager.FindByName(privateMessageVM.UserName).Id;
            privateMessageVM.PM.senderId = userManager.FindById(userid).Id;
            return privateMessageVM;
        }

        public void DeletePM(PrivateMessage privateMessage)
        {
            db.message.Remove(privateMessage);
            db.SaveChanges();
        }
    }
}