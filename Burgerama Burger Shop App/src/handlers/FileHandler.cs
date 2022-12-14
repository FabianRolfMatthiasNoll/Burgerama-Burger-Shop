using Burgerama_Burger_Shop_App.products;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Burgerama_Burger_Shop_App.src.moods;

namespace Burgerama_Burger_Shop_App.src.handlers
{
    public class FileHandler
    {
        public string filePath;

        public FileHandler(string path)
        {
            filePath = path;
        }

        public List<User> LoadUserData(string fileName)
        {
            List<User> users = new List<User>();
            using (var reader = new StreamReader($"{filePath}{fileName}"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<User>),
                    new XmlRootAttribute("Users"));
                users = (List<User>)deserializer.Deserialize(reader);
            }
            return users;
        }

        public void WriteUserData(User user, string fileName)
        {
            var usersXml = XElement.Load($"{filePath}{fileName}");
            usersXml.Add(new XElement("User",
                                //new XAttribute("ID", user),
                                new XElement("Email", user.email),
                                new XElement("Postal", user.postal),
                                new XElement("City", user.city),
                                new XElement("Street", user.street),
                                new XElement("Password", user.password)
                                )
                            );
            //saves the document after adding the new user
            usersXml.Save($"{filePath}{fileName}");
        }

        public bool IsDataAvailable(string fileName)
        {
            if (File.Exists($"{filePath}{fileName}"))
            {
                return true;
            }
            return false;
        }

        [ExcludeFromCodeCoverage]
        public List<Driver> ReadDriverList(string fileName)
        {
            string json = File.ReadAllText($"{filePath}{fileName}");
            List<Driver> list = JsonConvert.DeserializeObject<List<Driver>>(json);

            foreach (var driver in list)
            {
                switch (driver.mood.MoodName)
                {
                    case "Happy":
                        driver.mood = new HappyMood(20, driver.capacity, driver.openOrders);
                        break;
                    case "Bored":
                        driver.mood = new BoredMood(20, driver.capacity, driver.openOrders);
                        break;
                    case "Balanced":
                        driver.mood = new BalancedMood(20, driver.capacity, driver.openOrders);
                        break;
                    case "Stressed":
                        driver.mood = new StressedMood(20, driver.capacity, driver.openOrders);
                        break;
                    case "Exhausted":
                        driver.mood = new ExhaustedMood(20, driver.capacity, driver.openOrders);
                        break;
                }
            }

            return list;
        }

        public List<T> ReadJson<T>(string fileName)
        {
            string json = File.ReadAllText($"{filePath}{fileName}");
            List<T> list = JsonConvert.DeserializeObject<List<T>>(json);

            return list;
        }

        public void WriteJson<T>(List<T> list, string fileName)
        {
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText($"{filePath}{fileName}", json);
        }
    }
}
