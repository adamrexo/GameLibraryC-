﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;

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
                Console.WriteLine("V.1.3.");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("0 = Hádání slov    1 = Kámen, Nůžky, Papír    2 = Hoď si kostkou!");
                Console.WriteLine("3 = ATM Simulator    4 = Blackjack");
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
                    case 3:
                        ATM();
                        break;
                    case 4:
                        Blackjack();
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

            string[] pole = { "1", "2", "3", "4", "5" , "6"};
            Random rand = new Random();
            int cislo = rand.Next(pole.Length);

            Console.Clear();

            Console.WriteLine("Hodil si číslo {0}! Gratuluji.", pole[cislo]);
        }
        //
        // KOSTKY KONEC
        //

        //
        // ATM
        //

        static void ATM()
        {
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;

            ATMMenu();

            Console.Clear();

            string jmeno = "n";
            using (StreamReader sr = new StreamReader(@"jmeno.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    jmeno = s;
                }
            }
            string prijmeni = "n";
            using (StreamReader sr = new StreamReader(@"prijmeni.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    prijmeni = s;
                }
            }
            int pin = 0000;
            using (StreamReader sr = new StreamReader(@"pin.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    pin = int.Parse(s);
                }
            }

            Console.WriteLine("Vítej v bankomatu!");
            string zj = "Jmeno";
            bool Vstup1 = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Zadej prosím své jméno:");
                zj = Console.ReadLine();
                if (zj == jmeno)
                {
                    Vstup1 = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadal jsi špatné jméno! Zkus to znovu.");
                    Console.ResetColor();
                }
            } while (Vstup1 == false);

            string zp = "Prijmeni";
            bool Vstup2 = false;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Zadej prosím své příjmení:");
                zp = Console.ReadLine();
                if (zp == prijmeni)
                {
                    Vstup2 = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadal jsi špatné příjmení! Zkus to znovu.");
                    Console.ResetColor();
                }
            } while (Vstup2 == false);

            int zpok = 3;
            bool Vstup3 = false;
            int zpin = 1;
            do
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Zadej svůj pin:");
                try
                {
                    zpin = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadali jste neplatný vstup! Zkuste to znovu.");
                    Console.ResetColor();
                }
                if (zpin == pin)
                {
                    Vstup3 = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Zadali jste špatný pin!");
                    zpok--;
                    Console.WriteLine("Zbývá ti následující počet pokusu: " + zpok);
                    if (zpok == 0)
                    {
                        Console.WriteLine("Bohužel, vyčerpal jsi maximální počet pokusů.");
                        Console.WriteLine("Zkus to příště!");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
                    Console.ResetColor();
                }
            } while (Vstup3 == false);
            bool End = false;
            do
            {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Vítej {0} {1} v ATM!", jmeno, prijmeni);
            Console.WriteLine("Zadej akci, kterou chceš provést:");
            Console.WriteLine("0 = Výběr    1 = Vklad");
            Console.WriteLine("2 = Změnit PIN    3 = Stav účtu");
            Console.WriteLine("4 = Opustit bankomat");
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
                    Console.WriteLine("Nezadali jste číslo!");
                }
            } while (Vstup == false);
            double penize = 0;
            using (StreamReader sr = new StreamReader(@"penize.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    penize = double.Parse(s);
                }
            }
            switch (a)
            {
                case 0:
                    double c = 0;
                    Vstup = false;
                    do
                    {
                        Console.WriteLine("Kolik si chceš vybrat?");
                        try
                        {
                            c = int.Parse(Console.ReadLine());
                            Vstup = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Zadali jste neplatný vstup! Zkuste to znovu.");
                        }
                    } while (Vstup == false);
                    if (c > penize)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Chcete vybrat více peněz, než máte!");
                        Console.ResetColor();
                        Console.ReadKey();
                        Console.Clear();
                    }
                    else
                    {
                        penize = penize - c;
                        Console.WriteLine("Úspěšně jste vybrali {0} peněz!", c);
                        Console.WriteLine("Momentálně na Vašem účtu máte: {0}", penize);
                        Console.ReadKey();
                        Console.Clear();
                    }

                    using (StreamWriter sw = new StreamWriter(@"penize.txt"))
                    {
                        sw.WriteLine(penize);
                        sw.Flush();
                    }
                    break;
                case 1:
                    c = 0;
                    Vstup = false;
                    do
                    {
                        Console.WriteLine("Kolik chcete vložit?");
                        try
                        {
                            c = int.Parse(Console.ReadLine());
                            Vstup = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Zadali jste neplatný vstup! Zkuste to znovu.");
                        }
                    } while (Vstup == false);
                    penize = penize + c;
                    Console.WriteLine("Úspěšně jste vložili {0} peněz!", c);
                    Console.WriteLine("Momentálně na Vašem účtu máte: {0}", penize);
                    using (StreamWriter sw = new StreamWriter(@"penize.txt"))
                    {
                        sw.WriteLine(penize);
                        sw.Flush();
                    }
                        Console.ReadKey();
                        Console.Clear();
                    break;
                case 2:
                    int pinz = 0;
                    Vstup = false;
                    do
                    {
                        try
                        {
                            Console.WriteLine("Jaký pin chcete nastavit?");
                            pinz = int.Parse(Console.ReadLine());
                            Vstup = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Zadali jste neplatný vstup! Zkuste to znovu.");
                        }
                    } while (Vstup == false);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("PIN Úspěšně změněn!");
                    Console.ResetColor();
                    using (StreamWriter sw = new StreamWriter(@"pin.txt"))
                    {
                        sw.WriteLine(pinz);
                        sw.Flush();
                    }
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 3:
                        Console.WriteLine("Zůstatek na účtu činí: {0}", penize);
                        Console.WriteLine("PIN vašeho účtu je: {0}", pin);
                        Console.ReadKey();
                        Console.Clear();
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Děkujeme za použití bankomatu!");
                    Console.WriteLine("Vidíme se příště!");
                    Console.ResetColor();
                    End = true;
                    break;
                default:
                    Console.WriteLine("Zadal jsi špatnou hodnotu!");
                    break;
            }
            } while (End == false);
        }

        static void ATMMenu()
        {
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Vítej v ATM Simulátoru! Uživatelské jméno a příjmení v simulátoru je:");
            using (StreamReader sr = new StreamReader(@"jmeno.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.Write(s + " ");
                }
            }
            using (StreamReader sr = new StreamReader(@"prijmeni.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            Console.WriteLine("Stiskni libovolné tlačítko pro pokračování do simulátoru.");
            Console.ReadKey();
        }
        //
        // ATM Konec
        //
        //
        // Blackjack
        //
        static void Blackjack()
        {
            BJMenu();

            bool Vstup = false;
            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Vyber si akci, kterou chceš provést:");
                Console.WriteLine("0 = Začít hru    1 = Ukončit hru");
                Console.ResetColor();
                int a = 0;
                bool b = false;
                do
                {
                    try
                    {
                        a = int.Parse(Console.ReadLine());
                        b = true;
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Zadali jste neplatný vstup! Zkuste to znovu.");
                        Console.ResetColor();
                    }
                } while (b == false);

                switch (a)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Clear();

                        Random rand = new Random();
                        int dealer = rand.Next(16, 25 + 1);
                        int hrac = 0;

                        int cislo = rand.Next(13);
                        hrac = hrac + cislo;
                        Console.WriteLine("Tvoje karty mají hodnotu: {0}", hrac);
                        Console.WriteLine("");

                        b = false;
                        do
                        {
                            Console.WriteLine("Přeješ si další karty? (Ano/Ne)");
                            string Prih = Console.ReadLine().ToLower();
                            Console.WriteLine("");
                            if (Prih == "ano")
                            {
                                cislo = rand.Next(1, 13);
                                hrac = hrac + cislo;
                                Console.WriteLine("Tvoje karty mají hodnotu: {0}", hrac);
                                Console.WriteLine("");
                                if (hrac > 21)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Prohrál jsi, tvé karty přesáhly hodnotu 21!");
                                    Console.WriteLine("");
                                    Console.ReadKey();
                                    Console.ResetColor();
                                }
                                else
                                {
                                    Console.WriteLine("Přeješ si další karty? (Ano/Ne)");
                                    Prih = Console.ReadLine().ToLower();
                                    Console.WriteLine("");
                                    if (Prih == "ano")
                                    {
                                        cislo = rand.Next(1, 13);
                                        hrac = hrac + cislo;
                                        Console.WriteLine("Tvoje karty mají hodnotu: {0}", hrac);
                                        Console.WriteLine("");
                                        if (hrac > 21)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("Prohrál jsi, tvé karty přesáhly hodnotu 21!");
                                            Console.WriteLine("");
                                            Console.ReadKey();
                                            Console.ResetColor();
                                        }
                                        else
                                        {
                                            Console.WriteLine("Přeješ si další karty? (Ano/Ne)");
                                            Prih = Console.ReadLine().ToLower();
                                            Console.WriteLine("");
                                            if (Prih == "ano")
                                            {
                                                cislo = rand.Next(1, 13);
                                                hrac = hrac + cislo;
                                                Console.WriteLine("Tvoje karty mají hodnotu: {0}", hrac);
                                                Console.WriteLine("");
                                                if (hrac > 21)
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red;
                                                    Console.WriteLine("Prohrál jsi, tvé karty přesáhly hodnotu 21!");
                                                    Console.WriteLine("");
                                                    Console.ReadKey();
                                                    Console.ResetColor();
                                                }
                                                else
                                                {
                                                    Console.Clear();
                                                    Konec(hrac, dealer);
                                                }
                                            }
                                            else
                                            {
                                                Console.Clear();
                                                Konec(hrac, dealer);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Konec(hrac, dealer);
                                    }
                                    }
                                b = true;
                            }
                            else
                            {
                                Konec(hrac, dealer);
                            }
                        } while (b = false);

                        break;
                    case 1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Děkuji za vyzkoušení Blackjacku,");
                        Console.WriteLine("Snad se ještě uvidíme!");
                        Console.ResetColor();
                        Vstup = true;
                        break;
                    default:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Zadal jsi špatnou hodnotu!");
                        Console.ResetColor();
                        break;
                }
            } while (Vstup == false);
        }
        static void Konec(int x, int y)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tvoje současná hodnota karet činí: {0}", x);
            Console.WriteLine("Dealerova současná hodnota karet činí: {0}", y);

            if (x > y && x <= 21)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vyhrál jsi!");
                Console.ResetColor();
            }
            else if (x > y && x > 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Prohrál jsi!");
                Console.ResetColor();
            }
            else if (y > x && y <= 21)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Prohrál jsi!");
                Console.ResetColor();
            }
            else if (y > x && y > 21)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Vyhrál jsi!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Remíza, můžeš to zkusit znovu!");
                Console.ResetColor();
            }
            Console.ReadKey();
        }
        static void BJMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Vítej ve hře Blackjack!");
            Console.WriteLine("Tvým úkolem je získat co nejvyšší hodnotu karet, ale nepřesáhnout 21!");
            Console.WriteLine("Hraješ proti dealerovi, který hází karty tobě, ale i sobě.");
            Console.WriteLine("Pokud hodnota dealera bude menší, než ta tvoje, vyhrál si.");
            Console.WriteLine("Pokud bude stejná, je to remíza! A pokud bude větší, prohrál si.");
            Console.WriteLine("Přibrat si karty můžeš maximálně 3!");
            Console.WriteLine("");
            Console.WriteLine("Pokud rozumíš pravidlům, zmáčkni libovolné tlačítko.");
            Console.ResetColor();
            Console.ReadKey();
        }
        //
        // Blackjack Konec
        //
    }
}
