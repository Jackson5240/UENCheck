using System;
using Microsoft.AspNetCore.Mvc;
using UENCheckApp.Models;

namespace UENCheckApp.Controllers
{
    public class UENInputController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new UENInput());
        }

        [HttpPost]
        public IActionResult Index(UENInput input)
        {
            
            Console.WriteLine("=== [UENInputController] POST received ===");
            Console.WriteLine($"BusinessReg: {input.BusinessReg}");
            Console.WriteLine($"LocalCompany: {input.LocalCompany}");
            Console.WriteLine($"OtherEntity: {input.OtherEntity}");
        Console.WriteLine("==========================================");        
            if (!ModelState.IsValid)
            {
                Console.WriteLine("=== ============== [UENInputController]   validation fail");
                return View(input);
            }
            

            ViewBag.Message = "Validation passed!";
            return View(input);
        }
    }
}
