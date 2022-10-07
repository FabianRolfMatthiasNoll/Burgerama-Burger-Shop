using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Burgerama_Burger_Shop_App
{
    [Serializable()]
    [XmlRoot(ElementName = "Users")]
    public class User
    {
        [XmlElement(ElementName = "Email")]
        public string email;

        [XmlElement(ElementName = "Password")]
        public string password;

        [XmlElement(ElementName = "Postal")]
        public string postal;

        [XmlElement(ElementName = "Street")]
        public string street;

        [XmlElement(ElementName = "City")]
        public string city;
    }
}
