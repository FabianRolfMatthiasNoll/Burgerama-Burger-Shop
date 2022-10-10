using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Burgerama_Burger_Shop_App
{
    internal class Registration
    {
        public static void RegistrationMenu()
        {
            User newUser = new User();

            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("For perfect customer satisfication we need a bit of information about you :)\n");

            Console.Write("Please enter your Email: ");
            newUser.email = Console.ReadLine();
            Program.ClearCurrentConsoleLine();

            while (IsEmailTaken(newUser.email))
            {
                //Email is taken reenter until a new email is registered
                Console.WriteLine("The Email you entered is already taken [Press any Key]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();

                Console.Write("Please enter another Email: ");
                newUser.email = Console.ReadLine();
                Program.ClearCurrentConsoleLine();
            }

            Console.Write("Please choose a Password: ");
            newUser.password = Login.HashString(Login.GetPassword());
            Program.ClearCurrentConsoleLine();

            Console.Write("Please enter your Street: ");
            newUser.street = Console.ReadLine();
            Program.ClearCurrentConsoleLine();

            Console.Write("Please enter your ZIP-Code: ");
            newUser.postal = Console.ReadLine();
            Program.ClearCurrentConsoleLine();

            Console.Write("Please enter your City: ");
            newUser.city = Console.ReadLine();
            Program.ClearCurrentConsoleLine();

            Console.WriteLine("Thank you for registering with Burgerama Burger :)");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            //give over the user data and write it into an xml file
            RegisterUser(newUser);

            //return to the main menu so you can log in
            Program.Main();
        }

        static bool IsEmailTaken(string email)
        {
            //return true if taken return false if not
            var usersXML = XElement.Load("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\user_data.xml");
            IEnumerable<XElement> users = usersXML.Elements();

            foreach (var user in users)
            {
                //compares the string of user emails and the given email
                if (String.Equals((user.Element("Email").Value), email))
                {
                    return true;
                }
            }

            return false;
        }

        static void RegisterUser(User newUser)
        {
            //loading in the current User File
            var usersXML = XElement.Load("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\user_data.xml");
            usersXML.Add(new XElement("User",
                                //new XAttribute("ID", user),
                                new XElement("Email", newUser.email),
                                new XElement("Postal", newUser.postal),
                                new XElement("City", newUser.city),
                                new XElement("Street", newUser.street),
                                new XElement("Password", newUser.password)
                                )
                            );
            //saves the document after adding the new user
            usersXML.Save("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\user_data.xml");
        }
    }
}
