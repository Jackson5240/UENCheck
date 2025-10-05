using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UENValidateProj.Tests.TestHelpers
{
    // ------------------------------------------------------------
    // ValidationTestHelper
    // ------------------------------------------------------------
    // Purpose:
    // A reusable utility class to help with validating model objects
    // in unit tests. Instead of duplicating validation logic across
    // multiple test classes, this helper provides a single static
    // method that performs validation using DataAnnotations.
    //
    // Typical usage example in a test:
    //     var results = ValidationTestHelper.Validate(myModel);
    //     Assert.True(results.Count == 0); // passes if model is valid
    //
    // Benefits:
    //   - Keeps tests clean and focused on intent
    //   - Avoids repeating ValidationContext setup in each test
    //   - Ensures consistent validation logic across all model tests
    // ------------------------------------------------------------
    public static class ValidationTestHelper
    {
            // ------------------------------------------------------------
        // Method: Validate
        // ------------------------------------------------------------
        // Purpose:
        // Validates any model object that uses DataAnnotation attributes
        // (e.g., [Required], [RegularExpression], [Range], etc.).
        //
        // Parameters:
        //   model → The object instance to be validated.
        //
        // Behavior:
        //   - Creates a ValidationContext for the given object.
        //   - Runs all DataAnnotation validations defined on its properties.
        //   - Returns a list of ValidationResult objects representing
        //     any failed validations.
        //
        // Return:
        //   List<ValidationResult> — a collection of all validation errors.
        //   If the list is empty, the model passed all validation rules.
        // ------------------------------------------------------------
        public static List<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();  // Holds validation results
            var context = new ValidationContext(model, null, null);  // Defines the validation context
            // Perform validation (true = validate all properties recursively)
            Validator.TryValidateObject(model, context, results, true);
            return results; // Return all collected validation errors
        }
    }
}
