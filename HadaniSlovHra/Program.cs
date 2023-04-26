using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace GameLib
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ResetColor();
            Console.Clear();
            bool Zapnuto = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("░██████╗░░█████╗░███╗░░░███╗███████╗██╗░░░░░██╗██████╗░");
                Console.WriteLine("██╔════╝░██╔══██╗████╗░████║██╔════╝██║░░░░░██║██╔══██╗");
                Console.WriteLine("██║░░██╗░███████║██╔████╔██║█████╗░░██║░░░░░██║██████╦╝");
                Console.WriteLine("██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░██║░░░░░██║██╔══██╗");
                Console.WriteLine("╚██████╔╝██║░░██║██║░╚═╝░██║███████╗███████╗██║██████╦╝");
                Console.WriteLine("░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝╚══════╝╚═╝╚═════╝░");
                Console.WriteLine("V.1.0.");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("0 = Hádání slov    1 = Kámen, Nůžky, Papír    2 = Hoď si kostkou!");
                Console.WriteLine("9 = Přeji si ukončit aplikaci");
                Console.WriteLine("");
                Console.WriteLine("Vítejte v GameLib! Napište číslo dle menu, abyste si zvolili hru, kterou si chcete zahrát!");
                Console.WriteLine("");
                int a = 0;
                bool Vstup = false;
                do
                {
                    try
                    {
                        a = int.Parse(Console.ReadLine());
                        Vstup = true;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Nezadali jste číslo! Zkuste to znovu.");
                    }
                } while (Vstup == false);

                switch (a)
                {
                    case 0:
                        HadaniSlov();
                        break;
                    case 1:
                        KNP();
                        break;
                    case 2:
                        Kostky();
                        break;
                    case 9:
                        Console.Clear();
                        Console.WriteLine("Uvidíme se příště, ahoj!");
                        Zapnuto = true;
                        break;
                    default:
                        Console.WriteLine("Nezadali jste číslo žádné hry!");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            } while (Zapnuto == false);
        }
        //
        //HADANI SLOV
        //
        static void HadaniSlov()
        {
            Console.ResetColor();
            Console.Clear();
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
        //
        // HADANI SLOV KONEC
        //

        //
        // KÁMEN NŮŽKY PAPÍR
        //
        static void KNP()
        {
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Zvolte si kámen, nůžky nebo papír!");
            Console.WriteLine("0 = Kámen, 1 = Nůžky, 2 = Papír");
            Console.ResetColor();
            int a = 0;
            bool Vstup = false;
            do
            {
                try
                {
                    a = int.Parse(Console.ReadLine());
                    Vstup = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Nezadali jste číslo! Zkuste to znovu.");
                }
            } while (Vstup == false);

            string[] pole = { "Kámen", "Nůžky", "Papír" };
            Random rand = new Random();
            int cislo = rand.Next(pole.Length);
            //pole[cislo] - Výběr random
            //pole[a] - Výběr hráče

            switch (a)
            {
                case 0: //Kámen - Hráč
                    if (cislo == 0) // Kámen - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Kámen");
                        Console.WriteLine("Výběr bota: Kámen");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Remíza! Vybrál si kámen stejně jako bot!");
                        Console.ResetColor();
                    }
                    else if (cislo == 1) // Nůžky - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Kámen");
                        Console.WriteLine("Výběr bota: Nůžky");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vyhrál jsi, gratuluji!");
                        Console.ResetColor();
                    }
                    else if (cislo == 2) // Papír - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Kámen");
                        Console.WriteLine("Výběr bota: Papír");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bohužel jsi prohrál, třeba to vyjde příště.");
                        Console.ResetColor();
                    }
                    break;
                case 1: //Nůžky
                    if (cislo == 0) // Kámen - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Nůžky");
                        Console.WriteLine("Výběr bota: Kámen");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bohužel jsi prohrál, třeba to vyjde příště.");
                        Console.ResetColor();
                    }
                    else if (cislo == 1) // Nůžky - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Nůžky");
                        Console.WriteLine("Výběr bota: Nůžky");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Remíza! Vybrál si nůžky stejně jako bot!");
                        Console.ResetColor();
                    }
                    else if (cislo == 2) // Papír - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Nůžky");
                        Console.WriteLine("Výběr bota: Papír");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vyhrál jsi, gratuluji!");
                        Console.ResetColor();
                    }
                    break;
                case 2: //Papír
                    if (cislo == 0) // Kámen - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Papír");
                        Console.WriteLine("Výběr bota: Kámen");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Vyhrál jsi, gratuluji!");
                        Console.ResetColor();
                    }
                    else if (cislo == 1) // Nůžky - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Papír");
                        Console.WriteLine("Výběr bota: Nůžky");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Bohužel jsi prohrál, třeba to vyjde příště.");
                        Console.ResetColor();
                    }
                    else if (cislo == 2) // Papír - Bot
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Tvůj výběr: Papír");
                        Console.WriteLine("Výběr bota: Papír");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Remíza! Vybrál si papír stejně jako bot!");
                        Console.ResetColor();
                    }
                    break;
                default:
                    Console.WriteLine("Zadali jste neplatné číslo pro hru!");
                    break;
            }
        }
        //
        // KÁMEN NŮŽKY PAPÍR KONEC
        //

        //
        // KOSTKY
        //
        static void Kostky()
        {
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   .+----------+");
            Console.WriteLine(" .'    * *   .'|");
            Console.WriteLine("+----+----+.'  |");
            Console.WriteLine("|         |  * |");
            Console.WriteLine("|    *    |    +");
            Console.WriteLine("|  *  *   |  .' ");
            Console.WriteLine("|         |.'   ");
            Console.WriteLine("+--------+'     ");
            Console.WriteLine("Zmáčkni libovolné tlačítko pro hození si kostkou!");
            Console.ReadKey();

            string[] pole = { "1", "2", "3", "4", "5" , "6", "7", "8", "9"};
            Random rand = new Random();
            int cislo = rand.Next(pole.Length);

            Console.WriteLine("Hodil si číslo {0}! Gratuluji.", pole[cislo]);
        }
        //
        // KOSTKY KONEC
        //
    }
}
