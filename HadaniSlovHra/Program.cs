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

            string obtiznost = "easyhard";
            Obtiznost(obtiznost);

            string HadaneSlovo;
            int pokusy;
            if (obtiznost == "easy")
            {
                Random rand = new Random();
                int cislo = rand.Next(easy.Length);
                HadaneSlovo = easy[cislo];
                pokusy = 6;
            }
            else
            {
                Random rand = new Random();
                int cislo = rand.Next(hard.Length);
                HadaneSlovo = hard[cislo];
                pokusy = 4;
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
            for (int i = 0; i < uhodnutePismena.Length; i++)
            {
                Console.Write(uhodnutePismena[i] + " ");
            }
            Console.WriteLine();
            Console.ResetColor();
            
            while (pokusy > 0 && spravne < hadaneSlovo.Length)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Zadej písmeno:");
                Console.ResetColor();

                char pismeno = 'n';
                bool Vstup = false;
                do
                {
                    try
                    {
                        pismeno = Console.ReadLine().ToLower()[0];
                        Vstup = true;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Nezadal jste malé písmeno z české abecedy!");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Zadej písmeno:");
                        Console.ResetColor();
                    }
                } while (Vstup == false);


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
                    for (int i = 0; i < uhodnutePismena.Length; i++)
                    {
                        Console.Write(uhodnutePismena[i] + " ");
                    }
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
        static string Obtiznost(string obtiznost)
        {
            bool Vstup = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Zvol si obtížnost. (Easy nebo Hard)");
                Console.ResetColor();
                obtiznost = Console.ReadLine();
                obtiznost = obtiznost.ToLower();
                if (obtiznost == "easy" || obtiznost == "hard")
                {
                    Vstup = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadal jsi špatný vstup, zkus to znovu!");
                    Console.ResetColor();
                }
            } while (Vstup == false);

            return obtiznost;
        }
    }
}
