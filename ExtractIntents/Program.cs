using System;
using System.IO;
using static ExtractIntents.Enums;

namespace ExtractIntents
{
    class Program
    {
        const string fileSuffix = "_usersays_en.json";

        static void Main(string[] args)
        {
            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + Enum.GetName(typeof(FolderNames), 0), "*" + fileSuffix, SearchOption.TopDirectoryOnly))
            {
#if DEBUG
                Console.WriteLine("[+] " + Path.GetFileName(file));
#endif

                string intentName = Path.GetFileName(file);
                intentName = intentName.Substring(0, intentName.Length - fileSuffix.Length);

                //TextApproach.CreateCsv(TextApproach.ExtractIntentsFromFile(file), intentName);
                JsonApproach.CreateCsv(JsonApproach.ExtractUtterancesFromFile(file), intentName);

#if DEBUG
                break;
#endif
            }
#if DEBUG
            Console.ReadKey();
#endif
        }
    }
}
