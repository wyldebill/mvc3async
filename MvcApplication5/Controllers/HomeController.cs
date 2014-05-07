using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication5.Controllers
{
    public class HomeController : AsyncController
    {
        // the route still specifies Index as the default action...some magic here somewhere
        public void IndexAsync()      // must return void due to the asynchron now
        {

            AsyncManager.OutstandingOperations.Increment();

            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (sender, evt) =>
                {
                    //var pageData = evt.Result;
                    AsyncManager.Parameters["data"] = evt.Result;
                    AsyncManager.OutstandingOperations.Decrement();
                };
            webClient.DownloadStringAsync(new Uri("http://www.google.com"));

            // doesn't make sense anymore right??? return View();
        }



        // called by the os when IndexAsync is considered done. when is done??
        public ActionResult IndexCompleted(string data)  // model binding occurs here from the Parameter[] above
        {
            ViewBag.Data = data;
            return View();

        }

        public ActionResult About()
        {
            return View();
        }
    }
}
