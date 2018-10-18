using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhotoshopRegitryNoSplash
{
    class Program
    {
        static void Main(string[] args)
        {

            var isClean = args.Length > 0;

            {
                var p = new RegistryReplace();
                Console.WriteLine(isClean ? "Clean start..." : "'Fix Photoshop spash screen' start...");
                p.Replace(isClean);

                if (p.effecteds.Count == 0)
                    Console.WriteLine("No keys modified.");
                else
                {
                    Console.WriteLine("bellow effected keys:");
                    foreach (var item in p.effecteds)
                        Console.WriteLine($"key: {item.Item1}\n from: {item.Item2}\n to: {item.Item3}\n---------");

                    Console.WriteLine($"\n\n{p.effecteds.Count} Keys Effected.");
                }
            }

            if (!isClean)
            {
                var sh = new ShortcutsReplace();
                sh.ChangeAll();
                if (sh.repleaced.Count == 0)
                {
                    Console.WriteLine("\nNo shortcuts modified.");
                }
                else
                {
                    Console.WriteLine("\nbellow effected shortcuts files:");
                    foreach (var item in sh.repleaced)
                        Console.WriteLine(item);

                    Console.WriteLine($"\n\n{sh.repleaced.Count} Files Effected.");
                }
            }

            Console.ReadLine();
        }






    }
}
