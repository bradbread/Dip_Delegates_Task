using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

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
            //string builder is apparently more performant than a regular string when constantly modifying
            //will build a large string with the data formatted then write all at once to a file at the end
            var csv = new StringBuilder();
            int max = rows.Count;
            for (int i = 0; i < max; i++)
            {
                int innerMax = rows[i].Count;
                //ignore the last element we will handle it outside of the loop
                for (int a = 0; a < innerMax - 1; a++)
                {
                    csv.Append(rows[i][a] + delimeter);
                }
                //probably better just to do this one check here other idea was a check inside the inner loop
                //so we don't add the delimeter to the end of the last element and to finish the line
                if (innerMax > 0)
                {
                    csv.AppendLine(rows[i][innerMax - 1]);
                }
                else
                {
                    csv.AppendLine();
                }

            }

            File.WriteAllText(filePath, csv.ToString());
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