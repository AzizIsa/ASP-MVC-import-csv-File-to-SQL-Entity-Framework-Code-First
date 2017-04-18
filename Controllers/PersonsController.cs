using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace Persons_csv.Controllers
{
    public class PersonsController : Controller
    {
        DBContext db = new DBContext();
        // GET: Persons
        public ActionResult Index()
        {
            return View(db.Persons.ToList());
        }

        // GET: Persons/Edit/5
        public ActionResult Edit(int? id)
        {
            //Check that person id is not nulll
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            //Find person with Id
            var person = db.Persons.Where(s => s.PersonId == id).FirstOrDefault();

            //Check if person is found
            if (person == null)
                return HttpNotFound();
            
            return View(person);
        }

        // POST: Persons/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult Edit(Persons CurrentPerson)
        {
            try
            {
                // Find the Person by his Id
                var person = db.Persons.Where(s => s.PersonId == CurrentPerson.PersonId).FirstOrDefault();

                if (ModelState.IsValid)
                {
                    // update the person items with the new data
                    person.FirstName = CurrentPerson.FirstName;
                    person.Surname = CurrentPerson.Surname;
                    person.Age = CurrentPerson.Age;
                    person.Sex = CurrentPerson.Sex;
                    person.Mobile = CurrentPerson.Mobile;
                    person.Active = CurrentPerson.Active;

                    // Save The updates
                    db.Entry(person).State = EntityState.Modified;
                    db.SaveChanges();
                }                
                // if success go to the main page
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                // if Error display a message and return to Edit current person page
                TempData["MessageError"] = "Error Happened :"+ex.Message;
                return View(CurrentPerson);
            }
        }        

        // Persons/Delete/5
        [ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            try
            {                
                //Check that person id is not nulll
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //Find person with Id
                var person = db.Persons.Where(s => s.PersonId == id).FirstOrDefault();

                //Check if person is found
                if (person == null)
                    return HttpNotFound();

                // Delete Selected person
                db.Persons.Remove(db.Persons.Find(id));
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
