using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;

namespace SampleSumHttpTrigger
{
    public static class CalculatorFunction
    {
        public enum validOperations
        {
            ADD, SUB, MUL, DIV
        }

        [FunctionName("Calculator")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);

            string num1 = data?.Num1;
            string num2 = data?.Num2;
            string operation = data?.Operation;
            string responseMessage = "";
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2))
            {
                sb.AppendLine("Empty input. Enter two whole numbers.");
                responseMessage = sb.ToString();
                log.LogError(responseMessage);
                return new BadRequestObjectResult(responseMessage);
            }
            if (!double.TryParse(num1, out _) || !double.TryParse(num2, out _))
            {
                responseMessage = "Input are not numbers . \nPlease Enter two valid numbers.";
                log.LogError(responseMessage);
                return new OkObjectResult(responseMessage);
            }

            double number1 = Convert.ToDouble(num1);
            double number2 = Convert.ToDouble(num2);
            double result;
            bool isValidOperaion = true;
            switch (operation)
            {
                case nameof(validOperations.ADD):
                    result = Calculator.AdditionOperation(number1, number2);
                    responseMessage = $"The {operation} of {number1} and {number2} is : {result}";
                    log.LogInformation(responseMessage);
                    break;

                case nameof(validOperations.SUB):
                    result = Calculator.SubtractionOperation(number1, number2);
                    responseMessage = $"The {operation} of {number1} and {number2} is : {result}";
                    log.LogInformation(responseMessage);
                    break;

                case nameof(validOperations.MUL):
                    result = Calculator.MultiplicationOperation(number1, number2);
                    responseMessage = $"The {operation} of {number1} and {number2} is : {result}";
                    log.LogInformation(responseMessage);
                    break;

                case nameof(validOperations.DIV):
                    if (number2 != 0)
                    {
                        result = Calculator.DivisionOperation(number1, number2);
                        responseMessage = $"The {operation} of {number1} and {number2} is : {result}";
                        log.LogInformation(responseMessage);
                    }
                    else
                    {
                        responseMessage = $"Invalid Division operation.Denominator cant be zero";
                        log.LogError(responseMessage);
                    }

                    break;

                default:
                    isValidOperaion = false;
                    responseMessage = $"Invalid Operation : {operation}.\nPlease enter a valid Operation to be performed";
                    break;
            }

            if (isValidOperaion)
            {
                return new OkObjectResult(responseMessage);
            }
            else
            {
                return new BadRequestObjectResult(responseMessage);
            }

        }
    }
}
