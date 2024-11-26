Probability Calculator

Overview

This is a simple probability calculator designed for investment consultants. It allows users to input two probabilities between 0 and 1 and perform basic calculations:

CombinedWith: Calculates the probability of both events occurring.
Either: Calculates the probability of either event occurring.
Technologies Used

Backend: C# ASP.NET Core Web API
Frontend: React
Installation and Setup

Clone the Repository:
Bash
git clone https://github.com/francis04j/probability-calculator.git


Backend Setup:
Navigate to the backend directory:
Bash
cd ProbabilityApi


Restore NuGet packages:
Bash
dotnet restore


Run the application:
Bash
dotnet run

Navigate to http://localhost:5137/health

Frontend Setup:
Navigate to the frontend directory:
Bash
cd frontend


Install dependencies:
Bash
npm install


Start the development server:
Bash
npm start   


  
Usage

Access the Frontend:
Open your web browser and navigate to http://localhost:5173.
Input Probabilities:
Enter two probabilities between 0 and 1 in the designated input fields.
Select a Function:
Choose either "CombinedWith" or "Either" from the dropdown menu.
Calculate Result:
Click the "Calculate" button to display the result.
Backend API

The backend API exposes two endpoints:

/calculate:
Accepts two probabilities and a function name as query parameters.
Returns the calculated result.

/health
Health check to validate API running smoothly

Testing

Unit tests are included to ensure the correctness of the calculations and validation logic. To run the tests:



Future Improvements


Error Handling: Enhance error handling and provide more informative error messages.
User Interface: Improve the user interface for better usability and accessibility.

