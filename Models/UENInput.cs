using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UENCheckApp.Models
{
    public class UENInput : IValidatableObject
    {
        [RegularExpression(@"^\d{8}[A-Za-z]$",
        //[RegularExpression(@"^\d{2}$",  
            ErrorMessage = "BusinessReg must be 8 digits followed by a letter.")]
        //public string BusinessReg { get; set; } = string.Empty;
        public string BusinessReg { get; set; } = "";

        [RegularExpression(@"^(18\d{2}|19\d{2}|20(0\d|1\d|2[0-8]))\d{5}[A-Za-z]$", 
            ErrorMessage = "LocalCompany must follow yyyyNNNNNX format (1800–2028).")]
        //public string LocalCompany { get; set; } = string.Empty;
        public string LocalCompany { get; set; } = "";

        //[RegularExpression(@"^[TSR]\d{2}(LP|LL|FC|PF|RF|MQ|MM|NB|CC|CS|MB|FM|GS|DP|CP|NR|CM|CD|MD|HS|VH|CH|MH|CL|XL|CX|HC|RPTU|TC|FB|FN|PA|PB|SS|MC|SM|GA|GB)\d{4}[A-Za-z]$",
        //[RegularExpression(@"(?i)^[TSR]\d{2}(LP|LL|FC|PF|RF|MQ|MM|NB|CC|CS|MB|FM|GS|DP|CP|NR|CM|CD|MD|HS|VH|CH|MH|CL|XL|CX|HC|RPTU|TC|FB|FN|PA|PB|SS|MC|SM|GA|GB)\d{4}[A-Z]$",
        [RegularExpression(@"(?i)^(T(0[0-9]|1[0-9]|2[0-8])|[SR]\d{2})(LP|LL|FC|PF|RF|MQ|MM|NB|CC|CS|MB|FM|GS|DP|CP|NR|CM|CD|MD|HS|VH|CH|MH|CL|XL|CX|HC|RPTU|TC|FB|FN|PA|PB|SS|MC|SM|GA|GB)\d{4}[A-Z]$", 
            ErrorMessage = "OtherEntity must follow T/S/RyyXXNNNNX format.")]
        //public string OtherEntity { get; set; } = string.Empty;
        public string OtherEntity { get; set; } = "";

        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(BusinessReg) &&
                string.IsNullOrWhiteSpace(LocalCompany) &&
                string.IsNullOrWhiteSpace(OtherEntity))
            {
                yield return new ValidationResult(
                    "At least one field (BusinessReg, LocalCompany, or OtherEntity) must be provided.",
                    new[] { nameof(BusinessReg), nameof(LocalCompany), nameof(OtherEntity) });
            }
        }
        
    }
}
