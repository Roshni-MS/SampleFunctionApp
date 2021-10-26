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

/// <summary>
/// 
/// </summary>
namespace SampleSumHttpTrigger
{
    public static class SumHttpTrigger
    {
        /// <summary>Runs the specified req.</summary>
        /// <param name="req">The req.</param>
        /// <param name="log">The log.</param>
        /// <returns>
        ///  The sum of two whole numbers.
        /// </returns>
        [FunctionName("Sum")]
        public static IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string num1 = req.Query["num1"];
            //string num2 = req.Query["num2"];

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            Class1 data = JsonConvert.DeserializeObject<Class1>(requestBody);
            //number1 = number1 ?? data?.Num1;
            //number2 = number2 ?? data?.Num2;
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

            if (!num1.All(char.IsDigit) || !num2.All(char.IsDigit))
            {
                responseMessage = "Enter two valid whole numbers.";
                log.LogError(responseMessage);
                return new OkObjectResult(responseMessage);
            }

            int a = Convert.ToInt32(num1);
            int b = Convert.ToInt32(num2);

            //if (operation.Equals("ADD")) {

            //    responseMessage = $"The sum is: {(a + b)}.";
            //    log.LogInformation($"Request successfully processed for {num1} and {num2} using operation {operation}.");


            //}

            switch (operation)
            {

                case "ADD":

                    responseMessage = $"The {operation} of {num1} and {num2} is : {(a + b)}.";
                    log.LogInformation($"Request successfully processed for {num1} and {num2} using operation {operation}.");
                    break;

                case "SUB":
                    responseMessage = $"The {operation} of {num1} and {num2} is : {(a - b)}.";
                    log.LogInformation($"Request successfully processed for {num1} and {num2} using operation {operation}.");
                    break;
                case "MUL":
                    responseMessage = $"The {operation} of {num1} and {num2} is : {(a * b)}.";
                    log.LogInformation($"Request successfully processed for {num1} and {num2} using operation {operation}.");
                    break;
                case "DIV":
                    if (b == 0)
                    {
                        responseMessage = "DIV by zero not possible.";
                        log.LogError(responseMessage);
                        return new OkObjectResult(responseMessage);
                    }
                    responseMessage = $"The {operation} of {num1} and {num2} is : {(a / b)}.";
                    log.LogInformation($"Request successfully processed for {num1} and {num2} using operation {operation}.");
                    break;

                default:
                    responseMessage = "Invalid Operation Input. Enter a valid operation ADD, MUL, SUB or DIV";
                    log.LogError(responseMessage);
                    return new OkObjectResult(responseMessage);



            }
            return new OkObjectResult(responseMessage);
        }
    }
}
