using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using PetShopTest.Models;

namespace PetShopTest.Controllers
{
    public class PetShopController : Controller
    {
        public IActionResult Index()
        {
            string baseurl = "https://petstore.swagger.io/";
            List < Pets > PetList = new List<Pets>();

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync(baseurl + "v2/pet/findByStatus?status=available");
                responseTask.Wait();

                var result = responseTask.Result;

                var readTask = result.Content.ReadAsAsync<IList<Pets>>();
                readTask.Wait();

                foreach (var eachResult in readTask.Result)
                {

                    Pets newPet = new Pets();
                    newPet.Id = eachResult.Id;
                    newPet.Name = eachResult.Name;
                    PetList.Add(newPet);
                }

            }

            return View(PetList);
        }
    }
}