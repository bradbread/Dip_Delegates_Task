using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using FileParser;

public delegate List<List<string>> Parser(List<List<string>> data);

namespace Delegate_Exercise
{
    class Delegate_Exercise
    {
        static void Main(string[] args)
        {
            //read write locations change path if needed
            string readLoc =  @"C:/Users/Bradley/Desktop/swinburne/diplo/ICT418/Delegates_Task/data.csv"; ;
            string  writeLoc = @"C:/Users/Bradley/Desktop/swinburne/diplo/ICT418/Delegates_Task/processed_data.csv";

            //delegate
            Func<List<List<string>>, List<List<string>>> Process = new Func<List<List<string>>, List<List<string>>>(new DataParser().StripQuotes);
            Process += new DataParser().StripWhiteSpace;
            Process += RemoveHashes;

            //read file process data and write to a file
            CsvHandler csv = new CsvHandler();
            csv.ProcessCsv(readLoc, writeLoc, Process);

            //fluff replace with user interface perhaps
            Console.WriteLine("data processed press any key to continue");
            Console.ReadKey();
        }

        public static List<List<string>> RemoveHashes(List<List<string>> data) {
            foreach(var row in data) {
                for (var index = 0; index < row.Count; index++) {
                    if(row[index][0] == '#')
                        row[index] = row[index].Remove(0,1);
 
                }
            }
            return data;
            
        }
    }
}
