# SampleFunctionApp
Created for Aspire Training to introduce freshers to Azure functions (HTTP and Timer trigger).

The solution contains two projects -
i. Timer trigger function app: Logs the time this function is called based on the interval set.
ii. Http trigger function app: Sample app to add two whole numbers which are given by the user in the query parameters.



Feature requirement: 

Basic calculator application

HTTP Method: POST

Request Body:

{
	"Num1": 10,
	"Num2": 20,
	"Operation": "ADD"
}

Num1 - int <br />
Num2 - int <br />
Operation - enum with members [ADD, SUB, MUL, DIV] <br />


Response:
The {operation} of {num1} and {num2} is: {result}


Note: Add validations to avoid incorrect operations like division by zero or an invalid operation input. <br />

Eg of valid operation input: ADD <br />

Eg of invalid operation input: add, addition, sum <br />


Branch syntax for raising a PR: calc/{alias} 
