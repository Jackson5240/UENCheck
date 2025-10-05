using Microsoft.AspNetCore.Mvc;
using UENValidateProj.Controllers;
using UENValidateProj.Models;
using Xunit;

namespace UENValidateProj.Tests.Controllers
{
    // ------------------------------------------------------------
    // UENInputControllerTests
    // ------------------------------------------------------------
    // Purpose:
    // Unit tests for the UENInputController class.
    //
    // These tests validate that the controller:
    //   1. Returns the correct view and model for GET requests
    //   2. Correctly handles invalid model states during POST
    //
    // Note: [Fact] attributes are commented out to prevent execution
    //       if you are focusing on model-level tests first.
    //       You can uncomment them to re-enable controller tests.
    // ------------------------------------------------------------
    public class UENInputControllerTests
    {
        // ------------------------------------------------------------
        // Test 1: Index_Get_ReturnsViewWithModel
        // ------------------------------------------------------------
        // Purpose:
        // Ensures that when a GET request is made to /UENInput/Index,
        // the controller:
        //   - Returns a non-null ViewResult
        //   - Passes a UENInput model to the view
        //
        // Behavior being tested:
        // The controller's GET Index() action creates a new UENInput
        // model and passes it to the view.
        // ------------------------------------------------------------
        //[Fact]
        public void Index_Get_ReturnsViewWithModel()
        {
            // Arrange: create an instance of the controller
            var controller = new UENInputController();

            // Act: call the GET Index() action
            var result = controller.Index() as ViewResult;

            // Assert: ensure a valid ViewResult and correct model type
            Assert.NotNull(result);
            Assert.IsType<UENInput>(result.Model);
        }

        // ------------------------------------------------------------
        // Test 2: Index_Post_InvalidModel_ReturnsViewWithErrors
        // ------------------------------------------------------------
        // Purpose:
        // Ensures that when the POST action receives invalid input,
        // the controller:
        //   - Returns the same view instead of redirecting
        //   - Keeps the ModelState invalid (with error messages)
        //
        // Behavior being tested:
        // The controller’s POST Index(UENInput input) checks ModelState.
        // If invalid, it should return the same view to show validation errors.
        // ------------------------------------------------------------
        //[Fact]
        public void Index_Post_InvalidModel_ReturnsViewWithErrors()
        {
            // Arrange: create a controller and simulate an invalid input
            var controller = new UENInputController();
            controller.ModelState.AddModelError("BusinessReg", "Invalid format");

            // Act: call POST Index() with an invalid UENInput model
            var result = controller.Index(new UENInput()) as ViewResult;

            // Assert:
            // - The result is a view (not a redirect)
            // - The ModelState remains invalid
            Assert.NotNull(result);
            Assert.False(controller.ModelState.IsValid);
        }
    }
}
