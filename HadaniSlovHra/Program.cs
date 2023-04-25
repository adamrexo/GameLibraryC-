using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace HadaniSlovHra
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] easy = File.ReadAllLines("easy.txt");
            string[] hard = File.ReadAllLines("hard.txt");

            Console.WriteLine("Zvol si obtížnost. (Easy nebo Hard)");
            string obtiznost = Console.ReadLine();
            if (obtiznost == "Easy" || obtiznost == "easy" || obtiznost == "Hard" || obtiznost == "hard")
            {
            }
            else
            {
                Console.WriteLine("Zadal jsi špatný vstup!");
                Console.ReadKey();
                Environment.Exit(0);
            }

            string HadaneSlovo;
            int pokusy;
            if (obtiznost == "easy" || obtiznost == "Easy")
            {
                Random rand = new Random();
                int cislo = rand.Next(easy.Length);
                HadaneSlovo = easy[cislo];
                pokusy = 5;
            }
            else
            {
                Random rand = new Random();
                int cislo = rand.Next(hard.Length);
                HadaneSlovo = hard[cislo];
                pokusy = 3;
            }

            Console.Clear();

            string hadaneSlovo = HadaneSlovo.ToLower();
            char[] uhodnutePismena = new char[hadaneSlovo.Length];
            int spravne = 0;

            for (int i = 0; i < hadaneSlovo.Length; i++)
            {
                uhodnutePismena[i] = '_';
            }


            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Pravidla:");
            Console.WriteLine("Hádáš bez diakritiky a malé písmena!");
            Console.WriteLine("");
            Console.WriteLine("Hádej slovo:");
            Console.WriteLine(string.Join(" ", uhodnutePismena));
            Console.ResetColor();
            
            while (pokusy > 0 && spravne < hadaneSlovo.Length)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Zadej písmeno:");
                Console.ResetColor();
                char pismeno = Console.ReadLine().ToLower()[0];

                bool uhodnuto = false;
                for (int i = 0; i < hadaneSlovo.Length; i++)
                {
                    if (hadaneSlovo[i] == pismeno)
                    {
                        uhodnutePismena[i] = pismeno;
                        spravne++;
                        uhodnuto = true;
                    }
                }

                if (uhodnuto == false)
                {
                    pokusy = pokusy - 1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Špatně! Zbývá ti tento počet pokusů: " + pokusy);
                    Console.WriteLine("");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Správně! Zbyva ti uhodnout tento počet písmen: " + (hadaneSlovo.Length - spravne));
                    //Console.WriteLine(string.Join(" ", uhodnutePismena));
                    Console.WriteLine("");
                }
            }

            if (spravne == hadaneSlovo.Length)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Gratuluji! Uhodl jsi slovo " + hadaneSlovo + "!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bohužel, prohrál si. Slovo, které si měl hádat bylo: " + hadaneSlovo);
            }

            Console.ReadKey();
        }
    }
}
