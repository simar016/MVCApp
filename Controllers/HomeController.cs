using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(MVCApp.Models.RegClass registerDetails)
        {
            //We check if the model state is valid or not. We have used DataAnnotation attributes.
            //If any form value fails the DataAnnotation validation the model state becomes invalid.
            if (ModelState.IsValid)
            {
                try
                {
                    string filename = "Input.json";
                    string path = Server.MapPath("~/Files/" + filename);
                    List<MVCApp.Models.RegClass> listInput = new List<Models.RegClass>();
                    listInput.Add(registerDetails);
                    JsonFileUtils.PrettyWrite(listInput, path);
                    ViewBag.Message = "User Details Saved !";
                    return View("Index");
                }
                catch (Exception ex) {
                    throw ex;
                }
                
            }
            else
            {
                //If the validation fails, we are returning the model object with errors to the view, which will display the error messages.
                return View("Index", registerDetails);
            }
        }


        public static class JsonFileUtils
        {
            public static void PrettyWrite(List<MVCApp.Models.RegClass> obj, string fileName)
            {
                // Read existing json data
                var jsonData = System.IO.File.ReadAllText(fileName);
                // De-serialize to object or create new list
                var employeeList = JsonConvert.DeserializeObject<List<MVCApp.Models.RegClass>>(jsonData)
                                      ?? new List<MVCApp.Models.RegClass>();

                // Add any new object in the list
                employeeList.Add(new Models.RegClass()
                {
                    FirstName = obj[0].FirstName,
                    LastName = obj[0].LastName,
                    Email = obj[0].Email
                });

                // Update json data string
                jsonData = JsonConvert.SerializeObject(employeeList, Formatting.Indented);
                System.IO.File.WriteAllText(fileName, jsonData);
            }
        }
    }
}