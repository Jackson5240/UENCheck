using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UENValidateProj.Models;
using Xunit;

namespace UENValidateProj.Tests.Models
{
    // ------------------------------------------------------------
    // UENInputTests
    // ------------------------------------------------------------
    // Purpose:
    // Unit tests for the UENInput model class.
    //
    // These tests validate the behavior of the RegularExpression
    // attributes and custom validation logic inside UENInput.
    //
    // The tests are grouped into 4 sections:
    //   1. BusinessReg  → validates 8 digits + 1 letter format
    //   2. LocalCompany → validates yyyyNNNNNX format (1800–2028)
    //   3. OtherEntity  → validates T/S/RyyXXNNNNX format
    //   4. General rule → ensures at least one field is provided
    // ------------------------------------------------------------
    public class UENInputTests
    {
        // ------------------------------------------------------------
        // Helper: ValidateModel()
        // ------------------------------------------------------------
        // Purpose:
        // Performs model validation manually by using the
        // System.ComponentModel.DataAnnotations.Validator class.
        //
        // Behavior:
        // - Creates a ValidationContext for the provided model.
        // - Collects any validation errors (regex or custom rules).
        // - Returns a list of ValidationResult objects.
        // ------------------------------------------------------------
        private static IList<ValidationResult> ValidateModel(UENInput model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        // ---------- Data Sources ----------

        // ---------- BusinessReg Test Data ----------
        public static IEnumerable<object[]> BusinessRegCases =>
            new List<object[]>
            {
                new object[] { "12345678A", true },   // valid
                new object[] { "87654321Z", true },   // valid
                new object[] { "123456789", false },  // too long
                new object[] { "ABCDEFGHA", false },  // not digits
                new object[] { "12345678", false }    // missing letter
            };

        // ---------- LocalCompany Test Data ----------
        public static IEnumerable<object[]> LocalCompanyCases =>
            new List<object[]>
            {
                new object[] { "200312345A", true },   // valid
                new object[] { "189912345B", true },   // valid
                new object[] { "202812345C", true },   // valid
                new object[] { "179912345D", false },  // invalid: year < 1800
                new object[] { "202912345E", false },  // invalid: year > 2028
                new object[] { "18991234A", false }    // invalid: only 4 digits
            };

        // ---------- OtherEntity Test Data ----------
        public static IEnumerable<object[]> OtherEntityCases =>
            new List<object[]>
            {
                new object[] { "T19LL0001K", true },   // valid
                new object[] { "S85FC1234L", true },   // valid
                new object[] { "R01LP9999Z", true },   // valid
                new object[] { "T29ZZ9999Z", false },  // invalid entity code
                new object[] { "T99LL1234", false },   // missing letter
                new object[] { "X19LL0001K", false }   // wrong prefix
            };

        // ------------------------------------------------------------
        // Section 1: BusinessReg validation tests
        // ------------------------------------------------------------
        // Validates whether BusinessReg regex rule correctly identifies
        // valid and invalid formats for business registration numbers.
        // ------------------------------------------------------------
        [Theory]
        [MemberData(nameof(BusinessRegCases))]
        public void BusinessReg_Validation_Works(string input, bool expectedValid)
        {
            var model = new UENInput { BusinessReg = input };
            var results = ValidateModel(model);
            Assert.Equal(expectedValid, results.Count == 0);
        }

        // ------------------------------------------------------------
        // Section 2: LocalCompany validation tests
        // ------------------------------------------------------------
        // Ensures LocalCompany field follows the yyyyNNNNNX rule where:
        //   - yyyy = 1800–2028
        //   - NNNNN = 5 digits
        //   - X = any letter
        // ------------------------------------------------------------
        [Theory]
        [MemberData(nameof(LocalCompanyCases))]
        public void LocalCompany_Validation_Works(string input, bool expectedValid)
        {
            var model = new UENInput { LocalCompany = input };
            var results = ValidateModel(model);
            Assert.Equal(expectedValid, results.Count == 0);
        }

        // ------------------------------------------------------------
        // Section 3: OtherEntity validation tests
        // ------------------------------------------------------------
        // Validates complex patterns like T/S/RyyXXNNNNX where:
        //   - T, S, or R = prefix type
        //   - yy = 2 digits
        //   - XX = entity type code (LL, LP, FC, etc.)
        //   - NNNN = sequence number
        //   - X = check letter
        // ------------------------------------------------------------
        [Theory]
        [MemberData(nameof(OtherEntityCases))]
        public void OtherEntity_Validation_Works(string input, bool expectedValid)
        {
            var model = new UENInput { OtherEntity = input };
            var results = ValidateModel(model);
            Assert.Equal(expectedValid, results.Count == 0);
        }

        // ------------------------------------------------------------
        // Section 4: General validation rule
        // ------------------------------------------------------------
        // Purpose:
        // Ensures that the custom IValidatableObject.Validate() rule
        // triggers when all fields (BusinessReg, LocalCompany, OtherEntity)
        // are empty.
        //
        // Behavior:
        // - Should fail validation
        // - Should return an error message indicating that at least one
        //   field must be provided.
        // ------------------------------------------------------------
        [Fact]
        public void Should_Fail_When_All_Empty()
        {
            var model = new UENInput();
            var results = ValidateModel(model);

            // Ensure that the expected custom validation message is present
            Assert.Contains(results, r => r.ErrorMessage.Contains("Please provide 1 input for the field under (BusinessReg, LocalCompany, or OtherEntity)."));
        }
    }
}
