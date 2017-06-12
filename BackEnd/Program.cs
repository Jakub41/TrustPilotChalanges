using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DecryptAnagram
{
    class Program
    {
        const string ANAGRAM = "poultry outwits ants"; // Anagram provided 
        const string TARGET_HASH = "4624d200580677270a54ccff86b9610e"; // HASH to target
        const string RAINBOW_TABLE_FILENAME = "Rainbow.txt"; // The rainbow table for find the right words

        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\t\t\t>>> Decrypt Anagram <<<");
            var words = File.ReadAllLines("wordlist");
            if (!File.Exists(RAINBOW_TABLE_FILENAME))   // Check if File is not exist create a file for MD5 And MAgic Words
            {
                Console.WriteLine("Cannot find rainbow-table-file: {0}", RAINBOW_TABLE_FILENAME);
                Console.WriteLine("To decrypt the hash: {0} for the anagram: {1} you need to generate a rainbow-table-file: {2}.", TARGET_HASH, ANAGRAM, RAINBOW_TABLE_FILENAME);
                Console.WriteLine("This process will take abount 1-2 houer and abount 3GB of disk! ");
                Console.WriteLine("Do you want to start generateing this file (Y=Yes, N=No)?");
                var returnKey = Console.ReadKey();
                if (returnKey.KeyChar.ToString().ToUpper() == "Y")
                {
                    Console.Clear();
                    CreateRainbowTable(words);
                }
            }
            else //Else if File exist Then Go to Actual  Functions To Find The Magic Words/ Phrase
            {
                Console.WriteLine("\nAnagram: " + ANAGRAM);
                Console.WriteLine("Anagram MD5: " + TARGET_HASH);
                FindTheMagicWords(TARGET_HASH); // Here the Function Which calling for Find Pharse....
            }
            Console.Read();
        }
        private static void FindTheMagicWords(string MyActualHashAnagram)  // This Function is responsive for get all the data from the file and find if any Magic Words Find
        {

            string HoldAllData = "0";
            var start = DateTime.Now;
            using (var sr = new System.IO.StreamReader(RAINBOW_TABLE_FILENAME))
            {
                var line = String.Empty;
                while ((line = sr.ReadLine()) != null)  //  Get All the Data From the File...
                {
                    Console.CursorLeft = 0;
                    HoldAllData = line;
                    Console.Write("\n{0} - Working, Please Wait... ", DateTime.Now - start); //  Here The Time is Calculated
                }
            }

            int MagicWordsPostion = (SearchString(HoldAllData, MyActualHashAnagram)); //  Here SearchString Is Calling and after that result save into MagicWordsPostion
                                                                                      // MagicWordsPostion is a variable which hold the Actual position of Magic Words
            Console.WriteLine("\n\nMagic Word Location Found: " + MagicWordsPostion);

            if (MagicWordsPostion != -1)
            {
                MagicWordsPostion += 34; // 34 is added to HASH which has 32. That for narrow the possibilities and mark the right hashes of the Magic Words
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Magic Word are: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int i = MagicWordsPostion; i < MagicWordsPostion + 21; i++) // 21 characters - This loop is just responsive for the print out the words
                {
                    Console.Write(HoldAllData[i]);
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Gray;

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nSorry - cannot find the magic words :(");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
        public static int SearchString(string str, string pat) // This Function Is Used to find Actual Pharase Which we want!!! 
        {

            if (ANAGRAM != "poultry outwits ants")
            {
                return -1;
            }
            int M = pat.Length;

            int N = str.Length;

            for (int i = 0; i <= N - M; i++)
            {
                int j;
                for (j = 0; j < M; j++)
                {
                    if (str[i + j] != pat[j])
                        break;
                }
                if (j == M)
                {
                    return i;  // If Magic Word Found it's return indicator Which Postion in file for matching the phrase !!
                }
            }
            return -1;// Nothing Match!!
        }
        private static void CreateRainbowTable(string[] words) // This Function Genrate Rainbow Table For Phrases...
        {
            var anagramWords = ANAGRAM.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var anagramChars = new String(ANAGRAM.Replace(" ", String.Empty).Distinct().ToArray());
            var aCandidate = new Func<string, int, bool>((word, len) =>
            {
                var count = 0;
                foreach (var c in word.ToCharArray())
                {
                    if (anagramChars.Contains(c)) count++;
                    if (count == len) return true;
                }
                return (count >= len);
            });

            var getCandidates = new Func<int, List<string>>(target =>
            {
                return (from word in words
                        where word.Length == anagramWords[target].Length && aCandidate(word, anagramWords[target].Length)
                        select word).ToList();
            });
            var MD5 = new Func<string, string, string, string>((string word1, string word2, string word3) => System.BitConverter.ToString(
                System.Security.Cryptography.MD5.Create()
                    .ComputeHash(Encoding.UTF7.GetBytes(String.Format("{0} {1} {2}", word1, word2, word3))))
                .Replace("-", System.String.Empty)
                .ToLower());
            var candidates1 = getCandidates(0); // The candidates are narrow down to 3 possibilieties
            var candidates2 = getCandidates(1);
            var candidates3 = getCandidates(2);
            var start = DateTime.Now;
            using (var fs = File.CreateText(RAINBOW_TABLE_FILENAME))
            {
                fs.AutoFlush = true;
                for (var i1 = 0; i1 <= candidates1.Count() - 1; i1++)
                    for (var i2 = 0; i2 <= candidates2.Count() - 1; i2++)
                        for (var i3 = 0; i3 <= candidates3.Count() - 1; i3++)
                        {
                            if (candidates1[i1] == candidates2[i2]) continue;
                            if (candidates1[i1] == candidates3[i3]) continue;
                            if (candidates2[i2] == candidates3[i3]) continue;
                            Console.CursorLeft = 0;
                            var target = String.Format("{0} {1} {2}", candidates1[i1], candidates2[i2], candidates3[i3]);
                            var hash = MD5(candidates1[i1], candidates2[i2], candidates3[i3]);
                            Console.Write("{0}: [{1}/{2}]:{3} - [{4}/{5}]:{6} - [{7}/{8}]:{9}", DateTime.Now - start, i1, candidates1.Count(), candidates1[i1], i2, candidates2.Count(), candidates2[i2], i3, candidates3.Count(), candidates3[i3]);
                            fs.Write(String.Format("{0} | {1} ", hash, target)); // Write Data into File...
                        }
            }

        }

    }
}
