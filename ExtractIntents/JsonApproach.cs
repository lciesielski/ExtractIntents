using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using static ExtractIntents.Enums;

namespace ExtractIntents
{
    class JsonApproach
    {
        public static List<string> ExtractUtterancesFromFile(string file)
        {
            List<string> result = new List<string>();
#if DEBUG
            Console.WriteLine("[+] " + file);
#endif
            List<Utterance> utterances = JsonSerializer.Deserialize<List<Utterance>>(File.ReadAllText(file));

            if (utterances != null)
            {
                foreach (Utterance utterance in utterances)
                {
                    if (utterance != null && utterance.data != null)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (UtteranceData utteranceData in utterance.data)
                        {
                            sb.Append(utteranceData.text);
                        }

                        string utteranceText = sb.ToString().Trim();
#if DEBUG
                        Console.WriteLine("[+] " + utteranceText);
#endif
                        if (!string.IsNullOrEmpty(utteranceText)) 
                        {
                            result.Add(utteranceText);
                        }
                    }
                }
            }

            return result;
        }

        public static void CreateCsv(List<string> utterances, string intentName)
        {
#if RELEASE
            Console.WriteLine("[+] " + intentName);
#endif
            StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Enum.GetName(typeof(FolderNames), 1) + "\\" + intentName + ".csv");
            sw.WriteLine(Enum.GetName(typeof(CsvHeaders), 0) + "," + Enum.GetName(typeof(CsvHeaders), 1) + "," + Enum.GetName(typeof(CsvHeaders), 2));

            foreach (string utterance in utterances)
            {
#if DEBUG
                Console.WriteLine("[+] " + utterance);
#endif
                sw.WriteLine(Enum.GetName(typeof(CsvRowValues), 0) + "," + intentName + ".Intent,\"" + utterance + "\"");
            }

            sw.Close();
        }

        public class UtteranceData
        {
            public string text { get; set; }
            public string meta { get; set; }
            public string alias { get; set; }
            public bool userDefined { get; set; }
        }

        public class Utterance
        {
            public string id { get; set; }
            public List<UtteranceData> data { get; set; }
            public bool isTemplate { get; set; }
            public int count { get; set; }
            public string lang { get; set; }
            public int updated { get; set; }
        }
    }
}
