using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Persons_csv.Controllers
{
    public class ImportController : Controller
    {

        // Post: Import
        [HttpPost]        
        public ActionResult Import(HttpPostedFileBase FileUpload)
        {
            try
            {
                //Check if there is a file is sent to the controller
                if (FileUpload != null && FileUpload.ContentLength > 0)
                {
                    // Check if the file ends with csv extenstion
                    if (FileUpload.FileName.EndsWith(".csv"))
                    {
                        // Instance of EF Class;
                        DBContext db = new DBContext();
                        Validation_CVS csvValidate = new Validation_CVS(); //Instance of Validation Class

                        // Read the file as a stream
                        StreamReader streamCsv = new StreamReader(FileUpload.InputStream);

                        string csvDataLine = ""; int CurrentLine = 0;
                        string[] PersonData = null;

                        // Delete Data each time import the file
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE Persons");

                        #region while loop 
                        // Looping to read the File stream and Add Data to the database line by line
                        while ((csvDataLine = streamCsv.ReadLine()) != null)
                        {
                            // Ignore First Line of the file where columns names
                            if (CurrentLine != 0)
                            {
                                // Validate File Data and normalize it
                                csvDataLine = csvValidate.Validate(csvDataLine);

                                // Add the returned data to an array
                                PersonData = csvDataLine.Split(',');

                                // Pass PersonData Array values to the person object
                                var newPersonData = new Persons
                                {
                                    PersonId = int.Parse(PersonData[0]),
                                    FirstName = PersonData[1],
                                    Surname = PersonData[2],
                                    Age = int.Parse(PersonData[3]),
                                    Sex = PersonData[4],
                                    Mobile = PersonData[5],
                                    Active = bool.Parse(PersonData[6])
                                };

                                // Add Data to The Database
                                db.Persons.Add(newPersonData);
                                db.SaveChanges();
                            }
                            CurrentLine += 1;
                        }
                        #endregion

                    }
                    else
                        TempData["MessageError"] = "File Format is not Supported";
                }
                else
                    TempData["MessageError"] = "Please Add File";

                // Back to the first view
                return RedirectToAction("index", "Persons");
            }
            catch (Exception ex)
            {
                TempData["MessageError"] = "Error:" + ex.Message;
                return RedirectToAction("index", "Persons");
            }
        }
    }
}