using System;

namespace Loty
{
    public class Person
    {
        public Person(Gender gender, string name, int year, int month, int day)
        {
            Gender = gender;
            Name = name;
            BirthDate = new DateTime(year, month, day);
        }

        public Gender Gender { get; set; }

        public string Name { get; set; }
        public DateTime BirthDate { get; set; }


    }
}
