using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1
{
    class Person
    {
        public int age;
        public string name;

        public int id { get; }

        public override string ToString()
        {
            return string.Format($"(age={age}, name={name}, id={id})");
        }

        public override bool Equals(object? obj)
        {
            //return base.Equals(obj);
            if (obj == null) return false;
            Person other = (Person)obj;
            return age == other.age && name == other.name && id == other.id;
        }
    }
}
