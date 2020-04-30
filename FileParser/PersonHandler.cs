using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using ObjectLibrary;


namespace FileParser {
    
    //public class Person { }  // temp class delete this when Person is referenced from dll
    
    public class PersonHandler {
        public List<Person> People;

        /// <summary>
        /// Converts List of list of strings into Person objects for People attribute.
        /// </summary>
        /// <param name="people"></param>
        public PersonHandler(List<List<string>> people) {
            People = new List<Person>();
            for (int i = 1;i < people.Count; i++)
            {
                People.Add(new Person(int.Parse(people[i][0]), people[i][1], people[i][2], new DateTime(Int64.Parse(people[i][3]))));
            }
        }

        /// <summary>
        /// Gets oldest people
        /// </summary>
        /// <returns></returns>
        public List<Person> GetOldest() {
            List<Person> oldPeople = People;
            int length = oldPeople.Count;
            for (int i = 0 ; i > 0; i++)
            {
               
            }
            return oldPeople; //-- return result here
        }

        /// <summary>
        /// Gets string (from ToString) of Person from given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetString(int id) {

            return "result";  //-- return result here
        }

        public List<Person> GetOrderBySurname() {
            List<Person> surnameOrder = People;
            int length = surnameOrder.Count;

            //surnameOrder.Sort();

            for (int gap = length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < length; i++)
                {
                    Person comp = surnameOrder[i];
                    int d;
                    for (d = i; d >= gap && comp.Surname.CompareTo(surnameOrder[d - gap].Surname) < 0; d -= gap)
                    {
                        surnameOrder[d] = surnameOrder[d - gap];
                    }
                    surnameOrder[d] = comp;
                }
            }

            return surnameOrder;  //-- return result here
        }

        /// <summary>
        /// Returns number of people with surname starting with a given string.  Allows case-sensitive and case-insensitive searches
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public int GetNumSurnameBegins(string searchTerm, bool caseSensitive) {
            int result = 0;
            if (caseSensitive)
            {
                for (int i = 0; i < People.Count; i++)
                {
                    if (People[i].Surname.StartsWith(searchTerm))
                    {
                        result += 1;
                    }
                }
            }
            else
            {
                for (int i = 0; i < People.Count; i++)
                {
                    if (People[i].Surname.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                    {
                        result += 1;
                    }
                }
            }

            return result;  //-- return result here
        }
        
        /// <summary>
        /// Returns a string with date and number of people with that date of birth.  Two values seperated by a tab.  Results ordered by date.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAmountBornOnEachDate() {
            List<string> result = new List<string>();
            int length = People.Count;
            for (int i = 0; i < length; i++)
            {
                result.Add(People[i].Dob.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture));
            }

            return result;  //-- return result here
        }
    }
}