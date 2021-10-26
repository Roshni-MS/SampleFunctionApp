using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using SampleSumHttpTrigger;

/// <summary>
/// 
/// </summary>
namespace SampleOperateHttpTrigger
{
    public static class SumHttpTrigger
    {
        public enum Operations
        {
            ADD,
            SUB,
            MUL,
            DIV
        }
        /// <summary>Runs the specified req.</summary>
        /// <param name="req">The req.</param>
        /// <param name="log">The log.</param>
        /// <returns>
        ///  The sum of two whole numbers.
        /// </returns>
        [FunctionName("Operate")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string num1 = req.Query["num1"];
            //string num2 = req.Query["num2"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            Model data = JsonConvert.DeserializeObject<Model>(requestBody);
            //number1 = number1 ?? data?.number1;
            //number2 = number2 ?? data?.number2;
            string num1 = data.num1;
            string num2 = data.num2;
            string operation = data.operation;

            string responseMessage = "";

            if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2))
            {
                responseMessage = "Empty input. Enter two whole numbers.";
                log.LogError(responseMessage);
                return new OkObjectResult(responseMessage);
            }

            if(!num1.All(char.IsDigit) || !num2.All(char.IsDigit))
            {
                responseMessage = "Enter two valid whole numbers.";
                log.LogError(responseMessage);
                return new OkObjectResult(responseMessage);
            }

            int a = Convert.ToInt32(num1);
            int b = Convert.ToInt32(num2);

            switch (operation)
            {
                case nameof(Operations.ADD):
                    responseMessage = $"The result of {operation} is: {(a + b)}.";
                    break;
                case nameof(Operations.SUB):
                    responseMessage = $"The result of {operation} is: {(a - b)}.";
                    break;
                case nameof(Operations.MUL):
                    responseMessage = $"The result of {operation} is: {(a * b)}.";
                    break;
                case nameof(Operations.DIV):
                    if(b == 0)
                    {
                        responseMessage = "DIV by zero not possible.";
                        log.LogError(responseMessage);
                        return new OkObjectResult(responseMessage);
                    }
                    responseMessage = $"The result of {operation} is: {(a / b)}.";
                    break;
                default:
                    responseMessage = "Invalid Operation Input. Enter a valid operation ADD, MUL, SUB or DIV";
                    log.LogError(responseMessage);
                    return new OkObjectResult(responseMessage);
            }           
            log.LogInformation($"Request successfully processed for {num1} and {num2}.");
            return new OkObjectResult(responseMessage);
        }
    }
}
