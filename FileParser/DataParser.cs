using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;

namespace FileParser {
    public class DataParser {
        

        /// <summary>
        /// Strips any whitespace before and after a data value.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> StripWhiteSpace(List<List<string>> data) {
            int max = data.Count;
            for (int i = 0; i < max; i++)
            {
                int innerMax = data[i].Count;
                for (int a = 0; a < innerMax; a++)
                {
                    //if trim is passed nothing it will remove all characers from the start and end of a string
                    //that return true when passed to Char.IsWhiteSpace()
                    data[i][a] = data[i][a].Trim();
                }
            }
            return data; //-- return result here
        }

        /// <summary>
        /// Strips quotes from beginning and end of each data value
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> StripQuotes(List<List<string>> data) {
            int max = data.Count;
            //chars we wanna get rid of at the start and end
            char[] charsToTrim = { '\'', '"' };
            for (int i = 0; i < max; i++)
            {
                int innerMax = data[i].Count;
                for (int a = 0; a < innerMax; a++)
                {
                    data[i][a] = data[i][a].Trim(charsToTrim);
                }
            }
            return data; //-- return result here
        }

    }
}