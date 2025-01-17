﻿using Personalsystem.DataAccessLayer;
using Personalsystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personalsystem.Models.VM
{
    public class EventVM
    {
        PersonalSystemContext db = new PersonalSystemContext();
        public ScheduleRepo e = new ScheduleRepo();

        public List<Event> events()
        {
            return db.companyEvent.ToList();
        }
        public int? week { get; set; }
        public int? year { get; set; }
        public List<Event> eventList { get; set; }

    }
}