using System;
using System.Collections.Generic;
using System.IO;
using static ExtractIntents.Enums;

namespace ExtractIntents
{
    class TextApproach
    {
        const string textPrefix = "\"text\": ";

        public static List<string> ExtractUtterancesFromFile(string file)
        {
#if DEBUG
            Console.WriteLine("[+] " + file);
#endif
            StreamReader sr = new StreamReader(file);
            string line = sr.ReadLine();
            List<string> utterances = new List<string>();
            while (line != null)
            {
#if DEBUG
                Console.WriteLine("[+] " + line);
#endif
                line = line.Trim();

                if (line.StartsWith(textPrefix))
                {
                    line = line.Substring(textPrefix.Length);
                    if (line.EndsWith(","))
                    {
                        line = line.Remove(line.Length - 1);
                    }
                    utterances.Add(line);
                }
                line = sr.ReadLine();
            }
            sr.Close();
            return utterances;
        }

        public static void CreateCsv(List<string> utterances, string intentName)
        {
#if DEBUG
            Console.WriteLine("[+] " + intentName);
#endif
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Enum.GetName(typeof(FolderNames), 1) + "\\" + intentName + ".csv");
            sw.WriteLine(Enum.GetName(typeof(CsvHeaders), 0) + "," + Enum.GetName(typeof(CsvHeaders), 1) + "," + Enum.GetName(typeof(CsvHeaders), 2));

            foreach (string utterance in utterances)
            {
#if DEBUG
                Console.WriteLine("[+] " + utterance);
#endif
                sw.WriteLine(Enum.GetName(typeof(CsvRowValues), 0) + "," + intentName + ".Intent," + utterance);
            }

            sw.Close();
        }
    }
}
