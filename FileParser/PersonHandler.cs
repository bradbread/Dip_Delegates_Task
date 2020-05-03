using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using ObjectLibrary;
using System.Collections;


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

            for (int i = 0;i < people.Count; i++)
            {
                int id;
                long bDate;
                bool iDSuccess = int.TryParse(people[i][0], out id);
                bool bDateSuccess = Int64.TryParse(people[i][3], out bDate);
                //if both the id and date are useable add the person
                if (iDSuccess && bDateSuccess)
                {
                    People.Add(new Person(id, people[i][1], people[i][2], new DateTime(bDate)));
                }
                //otherwise handle it
                else
                {
                    //not adding the person atm but should probably log/display the error in some capacity
                    //Log.("Error in people input string oucured at line " + i + " id " + id + " birthdate " + bDate)
                    //or something or other depending on how errors are handeld within the program
                }
            }
        }

        /// <summary>
        /// Gets oldest people
        /// </summary>
        /// <returns></returns>
        public List<Person> GetOldest() {
            List<Person> oldPeople = People;
            int length = oldPeople.Count;
            //order list
            for (int gap = length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < length; i++)
                {
                    Person comp = oldPeople[i];
                    int d;
                    for (d = i; d >= gap && comp.Dob < oldPeople[d - gap].Dob; d -= gap)
                    {
                        oldPeople[d] = oldPeople[d - gap];
                    }
                    oldPeople[d] = comp;
                }
            }

            //add ordered people to the result until birth dates do not align
            List<Person> result = new List<Person>();
            string target = oldPeople[0].Dob.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; oldPeople[i].Dob.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) == target; i++)
            {
                result.Add(oldPeople[i]);
            }

            return result; //-- return result here
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
            List<DateTime> sortBirthday = new List<DateTime>();
            //was going to use a hash table but it seemed a dictonary was more perfomant
            Dictionary<string, int> dictDOB = new Dictionary<string, int>();
            int length = People.Count;

            //populate a list just of birthdays
            for (int i = 0; i < length; i++)
            {
                sortBirthday.Add(People[i].Dob);
            }

            //sort probably should go in it's own method in future
            for (int gap = length / 2; gap > 0; gap /= 2)
            {
                for (int i = gap; i < length; i++)
                {
                    DateTime comp = sortBirthday[i];
                    int d;
                    for (d = i; d >= gap && comp < sortBirthday[d - gap]; d -= gap)
                    {
                        sortBirthday[d] = sortBirthday[d - gap];
                    }
                    sortBirthday[d] = comp;
                }
            }

            //turn ordered birthdates into a dictonary with the ordered dates and the number of people who had that date
            for (int i = 0; i < length; i++)
            {
                //result.Add(People[i].Dob.ToString("dd/mm/yyyy", CultureInfo.InvariantCulture));
                string s = sortBirthday[i].ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (!dictDOB.ContainsKey(s))
                {
                    dictDOB.Add(s, 1);
                }
                else 
                {
                    dictDOB[s] = dictDOB[s] + 1;
                }
            }

            
            //put the dates and their counts into a list of strings to return
            foreach (KeyValuePair<string, int> de in dictDOB)
            {
                result.Add(de.Key.ToString() + " " + de.Value.ToString());
            }

            return result;  //-- return result here
        }
    }
}