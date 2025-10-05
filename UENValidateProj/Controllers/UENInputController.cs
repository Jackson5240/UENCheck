using System;
using Microsoft.AspNetCore.Mvc;
using UENValidateProj.Models;

namespace UENValidateProj.Controllers
{
    // ------------------------------------------------------------
    // UENInputController
    // ------------------------------------------------------------
    // Purpose:
    // Handles requests for the UEN Validation feature.
    // This controller processes both GET (page load)
    // and POST (form submission) requests for the UEN input form.
    //
    // The controller:
    //   - Renders the input form (GET)
    //   - Validates user input using model validation (POST)
    //   - Returns validation results to the same view
    // ------------------------------------------------------------
    public class UENInputController : Controller
    {
        // ------------------------------------------------------------
        // GET: /UENInput/Index
        // ------------------------------------------------------------
        // Called when the user first opens the UEN Validation page.
        // Returns the view with a fresh (empty) UENInput model
        // so the form fields are rendered empty.
        // ------------------------------------------------------------
        [HttpGet]
        public IActionResult Index()
        {
            // Create a new, empty UENInput model and render the page.
            return View(new UENInput());
        }

        // ------------------------------------------------------------
        // POST: /UENInput/Index
        // ------------------------------------------------------------
        // Called when the user submits the UEN Validation form.
        // ASP.NET Core automatically binds form data to the 'input' model.
        //
        // Steps:
        //   1️⃣ Validate the model using DataAnnotations + custom logic
        //   2️⃣ If validation fails → redisplay the same view with errors
        //   3️⃣ If validation passes → display a success message
        // ------------------------------------------------------------
        [HttpPost]
        public IActionResult Index(UENInput input)
        {
            // Check if the model validation failed
            if (!ModelState.IsValid)
            {
                ViewBag.Message = null;  // clear out any old success message
                // Return the same view with the user's inputs and error messages.
                // ModelState (and its errors) are automatically passed back to the view.
                return View(input);
            }
            // If validation succeeded, set a success message.
            ViewBag.Message = "Validation passed!";

            // Redisplay the view with the success message.
            // This ensures the user sees their entered values and result together.
            return View(input);
        }
    }
}
