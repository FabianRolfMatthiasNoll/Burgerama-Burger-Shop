using Burgerama_Burger_Shop_App.products;
using Burgerama_Burger_Shop_App.src;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

            newUser.email = GetEmail();

            newUser.password = GetPassword();

            newUser.street = GetStreet();

            newUser.postal = GetZip();

            newUser.city = GetCity();

            Console.WriteLine("Thank you for registering with Burgerama Burger :)");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();

            RegisterUser(newUser);

            Program.Main();
        }

        static string GetEmail()
        {
            Console.Write("Please enter your Email: ");
            var emailCheck = new EmailAddressAttribute();
            string emailUnfiltered = Console.ReadLine();
            string email = emailUnfiltered.ToLower();

            while (IsEmailTaken(email) || !emailCheck.IsValid(email))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("The Email is either taken or not valid. Please try again: ");
                email = Console.ReadLine();
            }
            return email;
        }

        static string GetPassword()
        {
            string password;
            Console.Write("Please choose a Password: ");
            password = Login.HashString(Login.GetPassword());
            Program.ClearCurrentConsoleLine();
            return password;
        }

        static string GetCity()
        {
            City city1 = new City();
            string json = File.ReadAllText("src/data/german_cities.json");
            var germanCities = JsonConvert.DeserializeObject<List<City>>(json);
            Console.Write("Please enter your City: ");
            string userCity = Console.ReadLine();
            bool cityIsInvalid = true;
            while (cityIsInvalid)
            {
                foreach (var city in germanCities)
                {
                    if (city.city == userCity)
                    {
                        cityIsInvalid = false;
                        break;
                    }
                }
                if (cityIsInvalid == false)
                {
                    break;
                }
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your city is not in our delivery range. [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your City: ");
                userCity = Console.ReadLine();
            }
            return userCity;
        }

        static string GetStreet()
        {
            Console.Write("Please enter your Street and Housenumber: ");
            string street = Console.ReadLine();
            while (string.IsNullOrEmpty(street))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your Street and Housenumber: ");
                street = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();
            return street;
        }

        static string GetZip()
        {
            Console.Write("Please enter your ZIP-Code: ");
            string postal = Console.ReadLine();
            while (string.IsNullOrEmpty(postal))
            {
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Your Input can`t be empty! [Enter]");
                Console.ReadKey();
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter your ZIP-Code: ");
                postal = Console.ReadLine();
            }
            Program.ClearCurrentConsoleLine();
            return postal;
        }

        static bool IsEmailTaken(string email)
        {
            //return true if taken return false if not
            var usersXML = XElement.Load("src/data/user_data.xml");
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
            var usersXML = XElement.Load("src/data/user_data.xml");
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
            usersXML.Save("src/data/user_data.xml");
        }
    }
}
