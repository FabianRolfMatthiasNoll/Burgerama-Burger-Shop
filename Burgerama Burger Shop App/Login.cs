using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            Console.Write("Password:");
            string password = Console.ReadLine();
            
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


        static bool CheckLoginCredentials(string email, string password)
        {
            var usersXML = XElement.Load("C:\\workspace\\Burgerama Burger Shop App\\Burgerama Burger Shop App\\user_data.xml");
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
            using (var reader = new StreamReader("C:\\workspace\\Burgerama Burger Shop App\\Burgerama Burger Shop App\\user_data.xml"))
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
    }
}
