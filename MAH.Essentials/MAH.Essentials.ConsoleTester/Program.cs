using System;

namespace MAH.Essentials.ConsoleTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUtils.WriteLine("Starting the application");

            var username = ConsoleUtils.GetUserInput("Enter a username");

            if (username.IsNullOrWhiteSpace())
            {
                ConsoleUtils.WriteErrorLine("The username, you have entered is empty :/");
            }
            else
            {
                ConsoleUtils.WriteSuccessLine($"Top. You have entered '{username}' (y)");
            }

            var password = ConsoleUtils.GetPasswordInput("Enter a password");

            if (password.IsNullOrWhiteSpace())
            {
                ConsoleUtils.WriteErrorLine("The username, you have entered is empty :/");
            }
            else
            {
                ConsoleUtils.WriteSuccessLine($"Top. You have entered '{password}' (y)");
            }


            var firstConfirmationTest = ConsoleUtils.Confirm("Confirm anything");
            ConsoleUtils.WriteLine($"Your answer is: {firstConfirmationTest}");

            var secondConfirmationTest = ConsoleUtils.Confirm("Second confirmation");
            ConsoleUtils.WriteLine($"Your answer is: {secondConfirmationTest}");


            try
            {
                throw new Exception("Testing writing the exceptions");
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteException(ex);
            }

            try
            {
                throw new IndexOutOfRangeException();
            }
            catch (Exception ex)
            {
                ConsoleUtils.WriteException(ex);
            }

            ConsoleUtils.WriteCleanLine("It seems to be working");

            ConsoleUtils.PressAnyKeyToContinue();
        }
    }
}