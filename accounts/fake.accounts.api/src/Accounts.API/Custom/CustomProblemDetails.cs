using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Accounts.API.Custom
{
    public class CustomProblemDetails : ProblemDetails
    {
        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }

        public CustomProblemDetails(ActionContext context)
        {
            Title = "Invalid arguments to the API";
            Status = 400;
            Type = context.HttpContext.TraceIdentifier;
            ConstructErrorMessages(context);
            Detail = SetDatail();
            
        }

        private void ConstructErrorMessages(ActionContext context)
        {
            Errors = new List<string>();
            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    for (var i = 0; i < errors.Count; i++)
                    {
                        var error = GetErrorMessage(errors[i]);
                        Errors.Add($"{error}");
                    }
                }
            }
            
            
        }

        private string SetDatail()
        {
            if(Errors.Count == 1)
                return  Errors.First();

            return "The inputs supplied to the API are invalid";
        }

        string GetErrorMessage(ModelError error)
        {
            return string.IsNullOrEmpty(error.ErrorMessage) ?
                "The input was not valid." :
                error.ErrorMessage;
        }

    }
}