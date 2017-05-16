using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StepCount.Models;

namespace StepCount.Controllers
{

   
    public class HomeController : Controller
    {

        //private StepCountDBEntities _db = new StepCountDBEntities(); 
        private STEPATHALONEntities _db = new STEPATHALONEntities(); 
        // GET: Home

#region team
        public ActionResult Index()
        {
            return View(_db.Teams.ToList());
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        #region create
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Exclude = "Id")] Team teamToCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                _db.Teams.Add(teamToCreate);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region edit
        public ActionResult Edit(int id)
        {
            var teamToEdit = (from team in _db.Teams
                              where team.Id == id
                              select team).First();

            return View(teamToEdit);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(Team teamToEdit)
        {
            try
            {
                var teamToEditOriginal = (from team in _db.Teams
                                          where team.Id == teamToEdit.Id
                                          select team).First();

                if (!ModelState.IsValid)
                    return View(teamToEditOriginal);

                _db.Teams.AddOrUpdate(teamToEdit);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion
 
        public ActionResult Delete(int id)
        {
            var teamToDelete = (from team in _db.Teams
                                where team.Id == id
                                select team).First();
            _db.LogEntries.RemoveRange(teamToDelete.Participants.SelectMany(x => x.LogEntries));
            _db.Participants.RemoveRange(teamToDelete.Participants);
            _db.Teams.Remove(teamToDelete);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
#endregion

#region participants

        public ActionResult ListParticipants(int id)
        {
            var parentTeam = (from team in _db.Teams
                              where team.Id == id
                              select team).First();            
            return View(parentTeam);
        }

        #region create
        public ActionResult CreateParticipant(int id)
        {
            Participant p = new Participant();
            p.Team = (from team in _db.Teams
                          where team.Id == id
                          select team).First();
            p.TeamId = id;    
            
            return View(p);
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateParticipant([Bind(Exclude = "Id")] Participant participants)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();
                _db.Participants.Add(participants);
                _db.SaveChanges();
                
                return RedirectToAction("ListParticipants", new{Id = participants.TeamId});
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region edit
        public ActionResult EditParticipant(int id)
        {
            var participants = (from p in _db.Participants
                              where p.Id == id
                              select p).First();

            return View(participants);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditParticipant(Participant participants)
        {
            try
            {
                var participantsOriginal = (from p in _db.Participants
                                            where p.Id == participants.Id
                                          select p).First();

                if (!ModelState.IsValid)
                    return View(participantsOriginal);

                _db.Participants.AddOrUpdate(participants);
                _db.SaveChanges();
                return RedirectToAction("ListParticipants", new { Id = participants.TeamId });
            }
            catch
            {
                return View();
            }
        }

        #endregion

        public ActionResult DeleteParticipant(int id)
        {           
            var pToDelete = (from p in _db.Participants
                                where p.Id == id
                                select p).First();
            var team = pToDelete.TeamId;
            _db.LogEntries.RemoveRange(pToDelete.LogEntries);
            _db.Participants.Remove(pToDelete);
            _db.SaveChanges();

            return RedirectToAction("ListParticipants", new { Id = team });
        }
        
#endregion

#region logeffort
        #region create
        public ActionResult CreateLogEntry()
        {
            var allParticipants = _db.Participants.ToList();
            ViewBag.Participants = allParticipants;
            return View();            
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CreateLogEntry([Bind(Exclude = "Id")] LogEntry logEntry)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var allParticipants = _db.Participants.ToList();
                    ViewBag.Participants = allParticipants;
                    return View(logEntry);
                }
                _db.LogEntries.Add(logEntry);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region list
        public ActionResult ListLogEntries(int id)
        {
            var participant = (from p in _db.Participants
                              where p.Id == id
                              select p).First();
            return View(participant);
        }

        #endregion

        #region edit
        public ActionResult EditLogEntry(int id)
        {
            var entry = (from p in _db.LogEntries
                                where p.Id == id
                                select p).First();

            return View(entry);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditLogEntry(LogEntry entry)
        {
            try
            {
                var participantsOriginal = (from p in _db.LogEntries
                                            where p.Id == entry.Id
                                            select p).First();

                if (!ModelState.IsValid)
                    return View(participantsOriginal);

                _db.LogEntries.AddOrUpdate(entry);
                _db.SaveChanges();
                return RedirectToAction("ListLogEntries", new { Id = participantsOriginal.ParticipantId });
            }
            catch
            {
                return View();
            }
        }
        #endregion

        public ActionResult DeleteLogEntry(int id)
        {
            var pToDelete = (from p in _db.LogEntries
                             where p.Id == id
                             select p).First();
            var pId = pToDelete.ParticipantId;
            _db.LogEntries.Remove(pToDelete);
            _db.SaveChanges();

            return RedirectToAction("ListLogEntries", new { Id = pId });
        }
#endregion
    }
}