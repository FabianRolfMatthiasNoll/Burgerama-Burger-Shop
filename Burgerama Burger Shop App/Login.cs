﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Burgerama_Burger_Shop_App
{
    internal class Login
    {
        public static void LoginMenu()
        {
            Console.Clear();
            Console.WriteLine("\n                            |\\ /| /|_/|\r\n                          |\\||-|\\||-/|/|\r\n                           \\\\|\\|//||///\r\n          _..----.._       |\\/\\||//||||\r\n        .'     o    '.     |||\\\\|/\\\\ ||\r\n       /   o       o  \\    | './\\_/.' |\r\n      |o        o     o|   |          |\r\n      /'-.._o     __.-'\\   |          |\r\n      \\      `````     /   |          |\r\n      |``--........--'`|    '.______.'\r\n       \\              /\r\n        `'----------'`\n");
            Console.WriteLine("");
            Console.WriteLine("Please input your Login Credentials\n");
            Console.Write("Email:");
            string email = Console.ReadLine();
            Program.ClearCurrentConsoleLine();

            
            //hides password input 
            Console.Write("Password:");
            string password = HashString(GetPassword());

            //special login for manager overview
            if (email == "Manager" && password == "king")
            {
                Console.Clear();
                Managment.ManagerMenu();
                Program.Main();
            }

            if (CheckLoginCredentials(email, password))
            {
                User LoggedUser = (User)ReturnUser(email, password);
                //i could read in the xml and filter out the user again and give it to the order menu
                //i can do this like with the json one so i dont have to rebuild the current system :D
                Ordering.OrderMenu(LoggedUser);
            }
            else
            {
                Console.WriteLine("Your login credentials were incorrect");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                LoginMenu();
            } 
        }

        public static string GetPassword()
        {
            StringBuilder input = new StringBuilder();
            while (true)
            {
                int x = Console.CursorLeft;
                int y = Console.CursorTop;
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input.Remove(input.Length - 1, 1);
                    Console.SetCursorPosition(x - 1, y);
                    Console.Write(" ");
                    Console.SetCursorPosition(x - 1, y);
                }
                else if (key.KeyChar < 32 || key.KeyChar > 126)
                {
                    Trace.WriteLine("Output suppressed: no key char"); //catch non-printable chars, e.g F1, CursorUp and so ...
                }
                else if (key.Key != ConsoleKey.Backspace)
                {
                    input.Append(key.KeyChar);
                    Console.Write("*");
                }
            }
            return input.ToString();
        }

        static bool CheckLoginCredentials(string email, string password)
        {
            var usersXML = XElement.Load("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\user_data.xml");
            IEnumerable<XElement> users = usersXML.Elements();

            foreach (var user in users)
            {
                //compares the string of user emails and the given email
                if (String.Equals((user.Element("Email").Value), email) && String.Equals((user.Element("Password").Value), password))
                {
                    Console.Clear();
                    return true;
                }
            }
            return false;
        }

        static Object ReturnUser(string email, string password)
        {
            List<User> userList;

            //read in the xml file, deserialize it and put it into a userlist
            using (var reader = new StreamReader("C:\\Users\\fanoll\\Source\\Repos\\burgerama-burger-shop\\Burgerama Burger Shop App\\user_data.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<User>),
                    new XmlRootAttribute("Users"));
                userList = (List<User>)deserializer.Deserialize(reader);
            }

            //check which user is logged in and return it for future use
            foreach(var login in userList)
            {
                if (String.Equals(login.email, email) && String.Equals(login.password, password))
                {
                    return login;
                }
            }
            return false;
        }

        public static string HashString(string text, string salt = "Burger")
        {
            if (String.IsNullOrEmpty(text))
            {
                return String.Empty;
            }

            // Uses SHA256 to create the hash
            using (var sha = SHA256.Create())
            {
                // Convert the string to a byte array first, to be processed
                byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(text + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                // Convert back to a string, removing the '-' that BitConverter adds
                string hash = BitConverter
                    .ToString(hashBytes)
                    .Replace("-", String.Empty);

                return hash;
            }
        }
    }
}
