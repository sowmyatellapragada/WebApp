using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Html;
namespace WebApp.Components
{
    public class CitySummary : ViewComponent
    {
        private CitiesData data;
        public CitySummary(CitiesData cdata)
        {
            data = cdata;
        }


        //public string Invoke()
        //{
        //    //return $"{data.Cities.Count()} cities, " + $"{data.Cities.Sum(c => c.Population)} people";

        //        if (RouteData.Values["controller"] != null)
        //        {
        //            return "Controller Request";
        //        }
        //        else
        //        {
        //            return "Razor Page Request";
        //        }

        //}
        public IViewComponentResult Invoke(string themeName)
        {
            ViewBag.Theme = themeName;
            return View(new CityViewModel
            {
                Cities = data.Cities.Count(),
                Population = data.Cities.Sum(c => c.Population)
            });
            // return Content("This is a <h3><i>string</i></h3>");
            //return new HtmlContentViewComponentResult(new HtmlString("This is a <h3><i>string</i></h3>"));


        }
    }
}