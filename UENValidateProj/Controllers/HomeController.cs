using Microsoft.AspNetCore.Mvc;

namespace UENValidateProj.Controllers
{
    // ------------------------------------------------------------
    // HomeController
    // ------------------------------------------------------------
    // Purpose:
    // Provides informational pages such as "About" and "Help"
    // for the UEN Validator web application.
    //
    // These pages are static (no form processing or input binding)
    // and serve general content to guide or inform users.
    // ------------------------------------------------------------
    public class HomeController : Controller
    {
            // ------------------------------------------------------------
        // GET: /Home/About
        // ------------------------------------------------------------
        // Purpose:
        // Returns the "About" page, which typically explains
        // the purpose of this application and provides background info.
        //
        // Behavior:
        // - Sets the page title via ViewData["Title"]
        // - Returns the corresponding Razor view: Views/Home/About.cshtml
        // ------------------------------------------------------------
        public IActionResult About()
        {
            ViewData["Title"] = "About";
            return View();
        }

        // ------------------------------------------------------------
        // GET: /Home/Help
        // ------------------------------------------------------------
        // Purpose:
        // Returns the "Help" page that provides UEN format examples
        // and usage guidance for users who need reference on valid inputs.
        //
        // Behavior:
        // - Sets the page title via ViewData["Title"]
        // - Returns the corresponding Razor view: Views/Home/Help.cshtml
        // ------------------------------------------------------------
        public IActionResult Help()
        {
            ViewData["Title"] = "Help - UEN Examples";
            return View();
        }
    }
}
