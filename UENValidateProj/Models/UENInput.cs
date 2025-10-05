using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UENValidateProj.Models
{
    // ------------------------------------------------------------
    // UENInput Model
    // ------------------------------------------------------------
    // Represents a data model used to capture and validate
    // a user's input for Singapore's Unique Entity Number (UEN).
    // 
    // It includes three possible formats:
    //   1️⃣ BusinessReg   - For businesses registered with ACRA
    //   2️⃣ LocalCompany  - For local incorporated companies
    //   3️⃣ OtherEntity   - For other types of registered entities
    //
    // The model uses Data Annotations and custom validation logic
    // to ensure the UEN entered matches the correct format.
    // ------------------------------------------------------------
    public class UENInput : IValidatableObject
    {
            // ------------------------------------------------------------
        // Business Registration Number (ACRA)
        // ------------------------------------------------------------
        // Pattern: 8 digits followed by a letter (e.g., 12345678A)
        // - ^ and $ mark the start and end of the string
        // - \d{8} means exactly 8 digits
        // - [A-Za-z] means a single uppercase or lowercase letter
        // ------------------------------------------------------------
        [RegularExpression(@"^\d{8}[A-Za-z]$",
            ErrorMessage = "BusinessReg must be 8 digits followed by a letter.")]
        public string BusinessReg { get; set; } = "";

        // ------------------------------------------------------------
        // Local Company Registration (ACRA)
        // ------------------------------------------------------------
        // Pattern: yyyyNNNNNX (e.g., 200312345A)
        // - Must start with a year between 1800 and 2028
        // - Followed by 5 digits and a letter
        // ------------------------------------------------------------
        [RegularExpression(@"^(18\d{2}|19\d{2}|20(0\d|1\d|2[0-8]))\d{5}[A-Za-z]$",
            ErrorMessage = "LocalCompany must follow yyyyNNNNNX format (1800–2028).")]
        public string LocalCompany { get; set; } = "";

        // ------------------------------------------------------------
        // Other Entities (Societies, LLPs, Foreign Companies, etc.)
        // ------------------------------------------------------------
        // Pattern: T/S/RyyXXNNNNX
        // Example: T19LL0001K
        // - Prefix: Tyy / Syy / Ryy
        // - Entity code: one of a large set (LP, FC, NB, etc.)
        // - Then 4 digits and a letter
        // The (?i) prefix makes the pattern case-insensitive.
        // ------------------------------------------------------------
        [RegularExpression(@"(?i)^(T(0[0-9]|1[0-9]|2[0-8])|[SR]\d{2})(LP|LL|FC|PF|RF|MQ|MM|NB|CC|CS|MB|FM|GS|DP|CP|NR|CM|CD|MD|HS|VH|CH|MH|CL|XL|CX|HC|RPTU|TC|FB|FN|PA|PB|SS|MC|SM|GA|GB)\d{4}[A-Z]$",
            ErrorMessage = "OtherEntity must follow T/S/RyyXXNNNNX format.")]
        public string OtherEntity { get; set; } = "";

        // ------------------------------------------------------------
        // Custom validation logic for model-level validation.
        // ------------------------------------------------------------
        // Implements IValidatableObject.Validate(), allowing us to perform
        // additional checks beyond field-level regex validations.
        //
        // Rule:
        //   ➤ At least one of the three fields (BusinessReg,
        //     LocalCompany, or OtherEntity) must be provided.
        //   ➤ If all are empty, return a ValidationResult with an error message.
        // ------------------------------------------------------------
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Check if all three fields are empty or whitespace
            if (string.IsNullOrWhiteSpace(BusinessReg) &&
                string.IsNullOrWhiteSpace(LocalCompany) &&
                string.IsNullOrWhiteSpace(OtherEntity))
            {
                yield return new ValidationResult(
                    "Please provide 1 input for the field under (BusinessReg, LocalCompany, or OtherEntity).",
                    new[] { nameof(BusinessReg), nameof(LocalCompany), nameof(OtherEntity) });
            }
        }

    }
}
