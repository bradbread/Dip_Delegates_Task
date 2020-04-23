using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FileParser {
    public class FileHandler {
       
        public FileHandler() { }

        /// <summary>
        /// Reads a file returning each line in a list.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<string> ReadFile(string filePath) {
            List<string> lines = new List<string>();
            using (var reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }
            }
                return lines; //-- return result here
        }

        
        /// <summary>
        /// Takes a list of a list of data.  Writes to file, using delimeter to seperate data.  Always overwrites.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimeter"></param>
        /// <param name="rows"></param>
        public void WriteFile(string filePath, char delimeter, List<List<string>> rows) {

            
        }
        
        /// <summary>
        /// Takes a list of strings and seperates based on delimeter.  Returns list of list of strings seperated by delimeter.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public List<List<string>> ParseData(List<string> data, char delimiter) {
            List<List<string>> result = new List<List<string>>();
            int max = data.Count;
            for (int i = 0; i < max; i++)
            {
                //create a new list to store our seperated values
                result.Add(new List<string>());

                /* first idea probably no point making it a list
                List<string> seperated = new List<string>();
                seperated.AddRange(data[i].Split(delimiter));
                */

                //turn an entry in the list into a seperate array
                string[] seperated = data[i].Split(delimiter);

                //add our seperate array into our list within a list
                foreach(string word in seperated)
                {
                    result[i].Add(word);
                }
            }

            return result; //-- return result here
        }
        
        /// <summary>
        /// Takes a list of strings and seperates on comma.  Returns list of list of strings seperated by comma.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> ParseCsv(List<string> data) {
            
            return new List<List<string>>(ParseData(data, ','));  //-- return result here
        }
    }
}