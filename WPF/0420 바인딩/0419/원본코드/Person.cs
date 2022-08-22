using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0419
{
    internal class Person
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public Person() { }
        public Person(string name, string phone, int age)
        {
            Name = name;
            Phone = phone;
            Age = age;
        }

        public override string ToString()
        {
            return Name + ", " + Phone + ", " + Age;
        }
    }
}
