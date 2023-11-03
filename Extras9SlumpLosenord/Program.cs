using System.ComponentModel;
using System.Net.Http.Headers;

namespace Extras9SlumpLosenord
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Introduction and getting information on requirements

            Console.Write("I can help you create a strong password.\nHow many characters should it have (between 8 and 20)? ");
            int length = int.Parse(Console.ReadLine()); 
            while (!(length >= 8 && length <= 20 ))
            {
                Validate();
                length = int.Parse(Console.ReadLine());
            }
            
            Console.Write("Should it include capital (C) or small (S) letters, or both (B) ? ");
            string bigsmall=Console.ReadLine().ToUpper();
            while (bigsmall != "C" && bigsmall != "S" && bigsmall != "B")
            {
                Validate();
                bigsmall = Console.ReadLine().ToUpper();
            }

            Console.Write("Number? (Y/N) ");
            string numbers = Console.ReadLine().ToUpper();
            while (numbers != "Y" && numbers != "N")
            {
                Validate();
                numbers = Console.ReadLine().ToUpper();
            }

            Console.Write("Special characters, e.g. #-?! ? (Y/N) ");
            string special = Console.ReadLine().ToUpper();
            while (special != "Y" && special != "N") 
            {
                Validate();
                special = Console.ReadLine().ToUpper(); ;
            }

           
            // list with possible characters
            // 26 small letters: a-z abcdefghijklmnopqrstuvwxyz
            // 26 big letters: A-Z ABCDEFGHIJKLMNOPQRSTUVWXYZ
            // 10 numbers: 0-9
            // 4 specials: #-?!

            int charactersCount = 0;
            string characters = "";
            bool charSmall = true;  // bools & ints to control if at least one character of each group is present
            bool charCapital = true;
            bool charNumbers = true;
            bool charSpecial = true;
            int smallStart=0, smallEnd=0, capitalStart=0, capitalEnd = 0, numbersStart = 0, numbersEnd = 0, specialStart = 0, specialEnd = 0;

            if (bigsmall == "S" || bigsmall == "B") {
                characters += "abcdefghijklmnopqrstuvwxyz";
                smallStart = charactersCount + 1;
                smallEnd = charactersCount + 26;
                charactersCount += 26;
            }
            if (bigsmall == "C" || bigsmall == "B") {
                characters += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                capitalStart = charactersCount + 1;
                capitalEnd = charactersCount + 26;
                charactersCount += 26;
            }
            if (numbers == "Y") {
                characters += "0123456789";
                numbersStart = charactersCount + 1;
                numbersEnd = charactersCount + 10;
                charactersCount += 10;
            }
            if (special == "Y") {
                characters += "#-?!";
                specialStart = charactersCount + 1;
                specialEnd = charactersCount + 26;
                charactersCount += 4; 
            }

            // generate the password
            // random character from all chosen categories until the length is achieved
            // three passwords are generated to choose among
            // if a password doesnt consist of at least character of each chosen category, it makes another try

            int nr;
            string password = "";
            string choice = "Three alternatives for your password: ";

            // loop three times to generate three possible passwords
            for (int i=0; i < 3; i++) 
            {
                // reset of which categories are needed
                if (bigsmall == "S" || bigsmall == "B") charSmall = false;
                if (bigsmall == "C" || bigsmall == "B") charCapital = false;
                if (numbers == "Y") charNumbers = false;
                if (special == "Y") charSpecial = false;

                // loop until charSmall, charCapital, charNumbers and charSpecial are true (all required types of characters are represented)
                while (!(charSmall && charCapital && charNumbers && charSpecial)) 
                {
                    password = "";

                    // inner loop to create the password itself
                    for (int j = 0; j < length; j++)
                    {
                        // random character from the list
                        Random random = new Random();
                        nr = random.Next(0, charactersCount);
                        password += characters[nr];

                        // control if all required types of characters are represented in the password
                        if (charSmall == false && nr >= smallStart - 1 && nr <= smallEnd - 1)
                            charSmall = true;
                        if (charCapital == false && nr >= capitalStart - 1 && nr <= capitalEnd - 1)
                            charCapital = true;
                        if (charNumbers == false && nr >= numbersStart - 1 && nr <= numbersEnd - 1)
                            charNumbers = true;
                        if (charSpecial == false && nr >= specialStart - 1 && nr <= specialEnd - 1)
                            charSpecial= true;

                    }
                }
                choice += $"\n{i+1}: {password}";
            }


            // Information
            Console.WriteLine($"Your password will consist of {length} characters. Each of them is randomized among {charactersCount} possible characters.");

            // Presentation of the passwords
            Console.WriteLine($"{choice}");

            // if I work more on that some day:
            // create bools to mark with categories are used (instead of checking the conditions each time)
            // use these bools for separate bools in the loops, these would only be used to control if passwords consists of them
            // fix control so that the first input from a user is an integer
            // make more sense of Validate-method

        }
        static void Validate()
        {   
            Console.Write("Incorrect input. Try again: ");

        }
    }
}