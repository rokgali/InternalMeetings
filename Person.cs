using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalMeetings
{
    public class Person
    {
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }
    }
    public class Admin : Person
    {
        public bool Rights { get; set; }
        public string Password { get; set; }

        public Admin(string passwd, string name) : base(name)
        {
            Rights = true;
            Password = passwd;
        }
    }
    public class User : Person
    {
        public bool Rights { get; set; }
        public DateTime WhenAdded { get; set; }

        public User(string name, DateTime whenAdded) : base(name)
        {
            WhenAdded = whenAdded;
            Rights = false;
        }
    }
}
