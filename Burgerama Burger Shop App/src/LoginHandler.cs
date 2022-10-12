using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Burgerama_Burger_Shop_App.src
{
    internal class LoginHandler
    {
        FileHandler userData;

        public LoginHandler()
        {
            userData = new FileHandler("src/data/");
        }

        public static string GetEmail()
        {
            Console.Write("Email:");
            var emailCheck = new EmailAddressAttribute();
            string emailUnfiltered = Console.ReadLine();
            string email = emailUnfiltered.ToLower();

            while (!emailCheck.IsValid(email))
            {
                if (email == "manager" || email == "admin")
                {
                    break;
                }

                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Program.ClearCurrentConsoleLine();
                Console.Write("Please enter a valid email adress:");
                email = Console.ReadLine();
            }
            return email;
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

        public static void IsUserManager(string email, string password)
        {
            if (email == "manager" && password == "39F968F400E6B06A5153F37683C348C94C948539B17636C0529A4E833ACE9C40")
            {
                Console.Clear();
                Managment.ManagerMenu();
            }
        }

        public bool CheckLoginCredentials(string email, string password)
        {
            List<User> userList = this.userData.LoadUserData();
            foreach (var user in userList)
            {
                //compares the string of user emails and the given email
                if (user.email == email && user.password == password)
                {
                    Console.Clear();
                    return true;
                }
            }
            return false;
        }

        public Object ReturnUser(string email, string password)
        {
            List<User> userList = this.userData.LoadUserData();

            //check which user is logged in and return it for future use
            foreach (var login in userList)
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
