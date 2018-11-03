using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _3_LifeSim_GameOfLife_
{
    class Program
    {
        static int cursorx = 31, cursory = 12;   // position of cursor
        static ConsoleKeyInfo cki;               // required for readkey

        static int step = 0, livenumber = 0, random, randomata, sekildondurmaW = 1, sekildondurmaR = 1, counter = 0;
        static int konum = 0, konumust = 0, konumalt = 0, konumsag = 0, konumsol = 0, konumsagalt = 0, konumsagust = 0, konumsolalt = 0, konumsolust = 0;
        static Random rnd = new Random();
        static int[] randomtut = new int[10];
        static int[] randomtutyedek = new int[10];
        static char[] sekilR = new char[9];
        static char[] sekilRyedek = new char[9];
        static bool randomtekrar = true, sekildondurmaQ = true;

        static char[] cell = new char[512];
        static char[] savecell = new char[512];



        static void print()
        {/////////////////////// --- Static screen parts#2
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("+-----------------------------------------------------------------+");
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(3, 3 + i + 1);
                Console.WriteLine("|                                                                 |");
            }
            for (int i = 0; i < cell.Length; i++)                         //Tüm elemanlar dolduruldu.
            {
                if (i % 32 == 0) Console.SetCursorPosition(4, 3 + (i / 32) + 1);
                Console.Write(" " + cell[i]);
            }
            Console.SetCursorPosition(3, 20);
            Console.WriteLine("+-----------------------------------------------------------------+");
            for (int i = 0; i < cell.Length; i++) if (cell[i] == 'o') livenumber++;   //Live Number
            Console.SetCursorPosition(75, 8);
            Console.WriteLine("Live: " + livenumber);
            livenumber = 0;
            Console.SetCursorPosition(75, 6);             //Step Number
            Console.WriteLine("Step: " + step);
            Console.SetCursorPosition(75, 11); Console.WriteLine("Q");  //Tüm Sekiller Ekrana Yazılması
            Console.SetCursorPosition(79, 11);
            if (sekildondurmaQ == false) { Console.WriteLine("o o o"); Console.SetCursorPosition(79, 10); Console.WriteLine(". . ."); Console.SetCursorPosition(79, 12); Console.WriteLine(". . ."); }
            else { Console.WriteLine(". o ."); Console.SetCursorPosition(79, 10); Console.WriteLine(". o ."); Console.SetCursorPosition(79, 12); Console.WriteLine(". o ."); }
            Console.SetCursorPosition(75, 15); Console.WriteLine("w");
            Console.SetCursorPosition(79, 15);
            if (sekildondurmaW == 1) { Console.WriteLine("o . o"); Console.SetCursorPosition(79, 16); Console.WriteLine(". o o"); Console.SetCursorPosition(79, 14); Console.WriteLine(". . o"); }
            else if (sekildondurmaW == 2) { Console.WriteLine("o . ."); Console.SetCursorPosition(79, 16); Console.WriteLine("o o o"); Console.SetCursorPosition(79, 14); Console.WriteLine(". o ."); }
            else if (sekildondurmaW == 3) { Console.WriteLine("o . o"); Console.SetCursorPosition(79, 16); Console.WriteLine("o . ."); Console.SetCursorPosition(79, 14); Console.WriteLine("o o ."); }
            else if (sekildondurmaW == 4) { Console.WriteLine(". . o"); Console.SetCursorPosition(79, 16); Console.WriteLine(". o ."); Console.SetCursorPosition(79, 14); Console.WriteLine("o o o"); }
            Console.SetCursorPosition(75, 19); Console.WriteLine("R");
            Console.SetCursorPosition(79, 18);
            for (int i = 0; i < sekilR.Length; i++)
            {
                if (sekilR[i] == 'o') Console.Write("o ");
                else Console.Write(". ");
                if (i == 2) Console.SetCursorPosition(79, 19);
                if (i == 5) Console.SetCursorPosition(79, 20);
            }
            ///////////////////////////////////////
        }

        static void location()
        {
            konum = (cursory - 4) * 32 + Convert.ToInt16((cursorx - 3) / 2) - 1; //X'in olduğu konumu dizide bulma işlemi.
            konumust = (cursory - 5) * 32 + Convert.ToInt16((cursorx - 3) / 2) - 1;
            konumalt = (cursory - 3) * 32 + Convert.ToInt16((cursorx - 3) / 2) - 1;
            konumsag = (cursory - 4) * 32 + Convert.ToInt16((cursorx - 3) / 2);
            konumsol = (cursory - 4) * 32 + Convert.ToInt16((cursorx - 3) / 2) - 2;
            konumsagust = (cursory - 5) * 32 + Convert.ToInt16((cursorx - 3) / 2);
            konumsagalt = (cursory - 3) * 32 + Convert.ToInt16((cursorx - 3) / 2);
            konumsolust = (cursory - 5) * 32 + Convert.ToInt16((cursorx - 3) / 2) - 2;
            konumsolalt = (cursory - 3) * 32 + Convert.ToInt16((cursorx - 3) / 2) - 2;
        }

        static int menu()
        {
            Console.SetCursorPosition(3, 3);
            Console.WriteLine("+-----------------------------------------------------------------+");
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(3, 3 + i + 1);
                if (i == 6) Console.WriteLine("|-------------------> 1) Start The Game <-------------------------|");
                else if (i == 7) Console.WriteLine("|-------------------> 2) How To Play    <-------------------------|");
                else if (i == 8) Console.WriteLine("|-------------------> 3) Exit       >> <<-------------------------|");
                else Console.WriteLine("|-----------------------------------------------------------------|");
            }
            Console.SetCursorPosition(3, 20);
            Console.WriteLine("+-----------------------------------------------------------------+");
            Console.SetCursorPosition(41, 12);
            int choice = Convert.ToInt16(Console.ReadLine());
            return choice;
        }



        static void Main(string[] args)
        {
            int choice = menu();

            if (choice == 1)
            {

                Console.Clear();

                for (int i = 0; i < cell.Length; i++) { cell[i] = '.'; savecell[i] = '.'; }

                // --- Static screen parts
                Console.SetCursorPosition(3, 3);
                Console.WriteLine("+-----------------------------------------------------------------+");
                for (int i = 0; i < 16; i++)
                {
                    Console.SetCursorPosition(3, 3 + i + 1);
                    Console.WriteLine("|                                                                 |");
                }
                for (int i = 0; i < cell.Length; i++)                         //Tüm elemanlar dolduruldu.
                {
                    if (i % 32 == 0) Console.SetCursorPosition(4, 3 + (i / 32) + 1);
                    Console.Write(" " + cell[i]);
                }
                Console.SetCursorPosition(3, 20);
                Console.WriteLine("+-----------------------------------------------------------------+");
                Console.SetCursorPosition(75, 6);
                Console.WriteLine("Step: " + step); //adım göstergesi(space tusunda step artır.)
                for (int i = 0; i < randomtut.Length; i++) randomtut[i] = 0;//tekrar random için değerler sıfırlandı.
                for (int i = 0; i < sekilR.Length; i++) sekilR[i] = '.';
                random = rnd.Next(4, 7); //kac tane o olucagı randomlandı.
                for (int i = 1; i <= random; i++) //rastgele R oluşturuldu.
                {
                    do
                    {
                        randomtekrar = true;
                        randomata = rnd.Next(1, 10);
                        for (int j = 0; j < randomtut.Length; j++) { if (randomtut[j] == randomata) randomtekrar = false; }
                    } while (randomtekrar == false);
                    randomtut[i] = randomata;
                    sekilR[randomata - 1] = 'o';
                }
                for (int i = 0; i < sekilRyedek.Length; i++) sekilRyedek[i] = sekilR[i];//R döndürme işlemi için ilk akılda tutma işlemi yapıldı.
                //////////////////////////////////////////////////////////////////////////////////////////////

                // --- Main game loop
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        location(); //Fonksiyonla konum işlemi yapıldı.

                        // true: there is a key in keyboard buffer
                        cki = Console.ReadKey(true);       // true: do not write character 

                        if (cki.Key == ConsoleKey.RightArrow && cursorx < 66)
                        {   // key and boundary control
                            Console.SetCursorPosition(cursorx, cursory);           // delete X (old position)
                            Console.WriteLine(cell[konum]);
                            cursorx += 2;
                        }
                        if (cki.Key == ConsoleKey.LeftArrow && cursorx > 5)
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(cell[konum]);
                            cursorx -= 2;
                        }
                        if (cki.Key == ConsoleKey.UpArrow && cursory > 4)
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(cell[konum]);
                            cursory--;
                        }
                        if (cki.Key == ConsoleKey.DownArrow && cursory < 19)
                        {
                            Console.SetCursorPosition(cursorx, cursory);
                            Console.WriteLine(cell[konum]);
                            cursory++;
                        }
                        Console.Clear(); //
                        if (cki.KeyChar >= 32 && cki.KeyChar <= 122)                 //ascii kodlarını ekle(KULLANICI GİRİŞ)
                        {       // keys: 
                            Console.SetCursorPosition(75, 4);
                            Console.WriteLine("Pressed Key: " + cki.KeyChar);

                            if (cki.KeyChar == 48) for (int i = 0; i < cell.Length; i++) cell[i] = '.'; //Key 0----Oyun Alanı Temizleme

                            if (cki.KeyChar == 69 || cki.KeyChar == 101) cell[konum] = 'o';//Key e----tek nokta yerleştirme.

                            if (cki.KeyChar == 51) cell[konum] = '.';//Key 3----o yu noktaya çevirme.

                            if ((cki.KeyChar == 81 || cki.KeyChar == 113) && konum > 31 && konum < 480 && konum % 32 != 0 && konum % 32 != 31) //Key Q----1.özel sekil.
                            {
                                if (sekildondurmaQ == true) { cell[konum] = 'o'; cell[konumust] = 'o'; cell[konumalt] = 'o'; }
                                else { cell[konum] = 'o'; cell[konumsag] = 'o'; cell[konumsol] = 'o'; }
                            }
                            else if ((cki.KeyChar == 87 || cki.KeyChar == 119) && konum > 31 && konum < 480 && konum % 32 != 0 && konum % 32 != 31) //Key W----2.özel sekil.
                            {
                                if (sekildondurmaW == 1) { cell[konumsol] = 'o'; cell[konumalt] = 'o'; cell[konumsag] = 'o'; cell[konumsagust] = 'o'; cell[konumsagalt] = 'o'; }
                                else if (sekildondurmaW == 2) { cell[konumsol] = 'o'; cell[konumalt] = 'o'; cell[konumsolalt] = 'o'; cell[konumust] = 'o'; cell[konumsagalt] = 'o'; }
                                else if (sekildondurmaW == 3) { cell[konumsol] = 'o'; cell[konumsolust] = 'o'; cell[konumsolalt] = 'o'; cell[konumust] = 'o'; cell[konumsag] = 'o'; }
                                else if (sekildondurmaW == 4) { cell[konumust] = 'o'; cell[konumalt] = 'o'; cell[konumsag] = 'o'; cell[konumsagust] = 'o'; cell[konumsolust] = 'o'; }
                            }

                            else if ((cki.KeyChar == 82 || cki.KeyChar == 114) && konum > 31 && konum < 480 && konum % 32 != 0 && konum % 32 != 31) //Key R----3.özel sekil.
                            {
                                if (sekilR[0] == 'o') cell[konumsolust] = 'o';
                                if (sekilR[1] == 'o') cell[konumust] = 'o';
                                if (sekilR[2] == 'o') cell[konumsagust] = 'o';
                                if (sekilR[3] == 'o') cell[konumsol] = 'o';
                                if (sekilR[4] == 'o') cell[konum] = 'o';
                                if (sekilR[5] == 'o') cell[konumsag] = 'o';
                                if (sekilR[6] == 'o') cell[konumsolalt] = 'o';
                                if (sekilR[7] == 'o') cell[konumalt] = 'o';
                                if (sekilR[8] == 'o') cell[konumsagalt] = 'o';

                            }

                            else if (cki.KeyChar == 49)//Key 1---
                            {
                                if (sekildondurmaQ == true) sekildondurmaQ = false;
                                else sekildondurmaQ = true;
                            }

                            else if (cki.KeyChar == 50)//Key 2---
                            {
                                sekildondurmaW++;
                                if (sekildondurmaW > 4) sekildondurmaW = sekildondurmaW % 4;
                            }


                            else if (cki.KeyChar == 84 || cki.KeyChar == 116) //Key T---
                            {
                                for (int i = 0; i < randomtut.Length; i++) randomtut[i] = 0;//tekrar random için değerler sıfırlandı.
                                for (int i = 0; i < sekilR.Length; i++) sekilR[i] = '.';
                                random = rnd.Next(4, 7); //kac tane o olucagı randomlandı.
                                for (int i = 1; i <= random; i++) //rastgele R oluşturuldu.
                                {
                                    do
                                    {
                                        randomtekrar = true;
                                        randomata = rnd.Next(1, 10);
                                        for (int j = 0; j < randomtut.Length; j++) { if (randomtut[j] == randomata) randomtekrar = false; }
                                    } while (randomtekrar == false);
                                    randomtut[i] = randomata;
                                    sekilR[randomata - 1] = 'o';
                                }
                                for (int i = 0; i < sekilRyedek.Length; i++) sekilRyedek[i] = sekilR[i];
                                sekildondurmaR = 1;
                            }

                            else if (cki.KeyChar == 89 || cki.KeyChar == 121) //Key Y---
                            {
                                for (int i = 0; i < randomtut.Length; i++) randomtut[i] = 0;//tekrar random için değerler sıfırlandı.
                                for (int i = 0; i < sekilR.Length; i++) sekilR[i] = '.';
                                random = rnd.Next(4, 7); //kac tane o olucagı randomlandı.
                                for (int i = 1; i <= random; i++) //rastgele R oluşturuldu.
                                {
                                    do
                                    {
                                        randomtekrar = true;
                                        randomata = rnd.Next(1, 10);
                                        for (int j = 0; j < randomtut.Length; j++) { if (randomtut[j] == randomata) randomtekrar = false; }
                                    } while (randomtekrar == false);
                                    randomtut[i] = randomata;
                                    sekilR[randomata - 1] = 'o';
                                }
                                int randomRyer = 0;
                                do
                                {
                                    randomRyer = rnd.Next(33, 479);
                                } while (randomRyer == 31 || randomRyer == 32 || randomRyer == 63 || randomRyer == 64 || randomRyer == 95 || randomRyer == 96 || randomRyer == 127 || randomRyer == 128 || randomRyer == 159 || randomRyer == 160 || randomRyer == 191 || randomRyer == 192 || randomRyer == 223 || randomRyer == 224 || randomRyer == 255 || randomRyer == 256 || randomRyer == 287 || randomRyer == 288 || randomRyer == 319 || randomRyer == 320 || randomRyer == 351 || randomRyer == 352 || randomRyer == 383 || randomRyer == 384 || randomRyer == 415 || randomRyer == 416 || randomRyer == 447 || randomRyer == 448);
                                for (int i = 0; i < randomtut.Length; i++)
                                {
                                    if (randomtut[i] == 1) cell[randomRyer - 33] = 'o';
                                    else if (randomtut[i] == 2) cell[randomRyer - 32] = 'o';
                                    else if (randomtut[i] == 3) cell[randomRyer - 31] = 'o';
                                    else if (randomtut[i] == 4) cell[randomRyer - 1] = 'o';
                                    else if (randomtut[i] == 5) cell[randomRyer] = 'o';
                                    else if (randomtut[i] == 6) cell[randomRyer + 1] = 'o';
                                    else if (randomtut[i] == 7) cell[randomRyer + 31] = 'o';
                                    else if (randomtut[i] == 8) cell[randomRyer + 32] = 'o';
                                    else if (randomtut[i] == 9) cell[randomRyer + 33] = 'o';
                                }
                                for (int i = 0; i < sekilRyedek.Length; i++) sekilRyedek[i] = sekilR[i];
                                sekildondurmaR = 1;
                            }
                            else if (cki.KeyChar == 52) //Key 4---
                            {

                                sekildondurmaR++;
                                if (sekildondurmaR > 4) sekildondurmaR = sekildondurmaR % 4;

                                if (sekildondurmaR == 1) { sekilR[0] = sekilRyedek[0]; sekilR[1] = sekilRyedek[1]; sekilR[2] = sekilRyedek[2]; sekilR[3] = sekilRyedek[3]; sekilR[4] = sekilRyedek[4]; sekilR[5] = sekilRyedek[5]; sekilR[6] = sekilRyedek[6]; sekilR[7] = sekilRyedek[7]; sekilR[8] = sekilRyedek[8]; }
                                else if (sekildondurmaR == 2) { sekilR[0] = sekilRyedek[6]; sekilR[1] = sekilRyedek[3]; sekilR[2] = sekilRyedek[0]; sekilR[3] = sekilRyedek[7]; sekilR[4] = sekilRyedek[4]; sekilR[5] = sekilRyedek[1]; sekilR[6] = sekilRyedek[8]; sekilR[7] = sekilRyedek[5]; sekilR[8] = sekilRyedek[2]; }
                                else if (sekildondurmaR == 3) { sekilR[0] = sekilRyedek[8]; sekilR[1] = sekilRyedek[7]; sekilR[2] = sekilRyedek[6]; sekilR[3] = sekilRyedek[5]; sekilR[4] = sekilRyedek[4]; sekilR[5] = sekilRyedek[3]; sekilR[6] = sekilRyedek[2]; sekilR[7] = sekilRyedek[1]; sekilR[8] = sekilRyedek[0]; }
                                else if (sekildondurmaR == 4) { sekilR[0] = sekilRyedek[2]; sekilR[1] = sekilRyedek[5]; sekilR[2] = sekilRyedek[8]; sekilR[3] = sekilRyedek[1]; sekilR[4] = sekilRyedek[4]; sekilR[5] = sekilRyedek[7]; sekilR[6] = sekilRyedek[0]; sekilR[7] = sekilRyedek[3]; sekilR[8] = sekilRyedek[6]; }
                            }


                            else if (cki.KeyChar == 32) //Key Space---
                            {
                                for (int i = 0; i < cell.Length; i++)
                                {
                                    if (cell[i] == '.' && i < 32 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i + 33] == 'o') counter++;
                                        if (cell[i + 31] == 'o') counter++;

                                        if (counter == 3) savecell[i] = 'o';
                                    }
                                    else if (cell[i] == '.' && i > 479 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i - 33] == 'o') counter++;
                                        if (cell[i - 31] == 'o') counter++;

                                        if (counter == 3) savecell[i] = 'o';
                                    }

                                    else if (cell[i] == '.' && i % 32 != 0 && i % 32 != 31)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i - 31] == 'o') counter++;
                                        if (cell[i + 33] == 'o') counter++;
                                        if (cell[i - 33] == 'o') counter++;
                                        if (cell[i + 31] == 'o') counter++;

                                        if (counter == 3) savecell[i] = 'o';
                                    }
                                    else if (cell[i] == '.' && i % 32 == 0 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 31] == 'o') counter++;
                                        if (cell[i + 33] == 'o') counter++;

                                        if (counter == 3) savecell[i] = 'o';
                                    }
                                    else if (cell[i] == '.' && i % 32 != 31 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i - 33] == 'o') counter++;
                                        if (cell[i + 31] == 'o') counter++;

                                        if (counter == 3) savecell[i] = 'o';
                                    }
                                    else if (i == 0 && cell[i] == '.') { if (cell[i + 1] == 'o') counter++; if (cell[i + 32] == 'o') counter++; if (cell[i + 33] == 'o') counter++; if (counter == 3) savecell[i] = 'o'; }
                                    else if (i == 480 && cell[i] == '.') { if (cell[i + 1] == 'o') counter++; if (cell[i - 32] == 'o') counter++; if (cell[i - 31] == 'o') counter++; if (counter == 3) savecell[i] = 'o'; }
                                    else if (i == 31 && cell[i] == '.') { if (cell[i - 1] == 'o') counter++; if (cell[i + 32] == 'o') counter++; if (cell[i + 31] == 'o') counter++; if (counter == 3) savecell[i] = 'o'; }
                                    else if (i == 511 && cell[i] == '.') { if (cell[i - 1] == 'o') counter++; if (cell[i - 32] == 'o') counter++; if (cell[i - 33] == 'o') counter++; if (counter == 3) savecell[i] = 'o'; }
                                    ////////////////////////////////////////////////////////////////////////////////////
                                    else if (cell[i] == 'o' && i < 32 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i + 33] == 'o') counter++;
                                        if (cell[i + 31] == 'o') counter++;

                                        if (counter == 2 || counter == 3) savecell[i] = 'o';
                                        else if (counter < 2 || counter > 3) savecell[i] = '.';
                                    }
                                    else if (cell[i] == 'o' && i > 479 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i - 33] == 'o') counter++;
                                        if (cell[i - 31] == 'o') counter++;

                                        if (counter == 2 || counter == 3) savecell[i] = 'o';
                                        else if (counter < 2 || counter > 3) savecell[i] = '.';
                                    }
                                    else if (cell[i] == 'o' && i % 32 != 0 && i % 32 != 31)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i - 31] == 'o') counter++;
                                        if (cell[i + 33] == 'o') counter++;
                                        if (cell[i - 33] == 'o') counter++;
                                        if (cell[i + 31] == 'o') counter++;

                                        if (counter == 2 || counter == 3) savecell[i] = 'o';
                                        else if (counter < 2 || counter > 3) savecell[i] = '.';
                                    }
                                    else if (cell[i] == 'o' && i % 32 == 0 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i + 1] == 'o') counter++;
                                        if (cell[i - 31] == 'o') counter++;
                                        if (cell[i + 33] == 'o') counter++;

                                        if (counter == 2 || counter == 3) savecell[i] = 'o';
                                        else if (counter < 2 || counter > 3) savecell[i] = '.';
                                    }
                                    else if (cell[i] == 'o' && i % 32 == 31 && i != 0 && i != 31 && i != 480 && i != 511)
                                    {
                                        if (cell[i - 32] == 'o') counter++;
                                        if (cell[i + 32] == 'o') counter++;
                                        if (cell[i - 1] == 'o') counter++;
                                        if (cell[i - 33] == 'o') counter++;
                                        if (cell[i + 31] == 'o') counter++;

                                        if (counter == 2 || counter == 3) savecell[i] = 'o';
                                        else if (counter < 2 || counter > 3) savecell[i] = '.';
                                    }
                                    else if (i == 0 && cell[i] == 'o') { if (cell[i + 1] == 'o') counter++; if (cell[i + 32] == 'o') counter++; if (cell[i + 33] == 'o') counter++; if (counter == 2 || counter == 3) savecell[i] = 'o'; else if (counter < 2 || counter > 3) savecell[i] = '.'; }
                                    else if (i == 480 && cell[i] == 'o') { if (cell[i + 1] == 'o') counter++; if (cell[i - 32] == 'o') counter++; if (cell[i - 31] == 'o') counter++; if (counter == 2 || counter == 3) savecell[i] = 'o'; else if (counter < 2 || counter > 3) savecell[i] = '.'; }
                                    else if (i == 31 && cell[i] == 'o') { if (cell[i - 1] == 'o') counter++; if (cell[i + 32] == 'o') counter++; if (cell[i + 31] == 'o') counter++; if (counter == 2 || counter == 3) savecell[i] = 'o'; else if (counter < 2 || counter > 3) savecell[i] = '.'; }
                                    else if (i == 511 && cell[i] == 'o') { if (cell[i - 1] == 'o') counter++; if (cell[i - 32] == 'o') counter++; if (cell[i - 33] == 'o') counter++; if (counter == 2 || counter == 3) savecell[i] = 'o'; else if (counter < 2 || counter > 3) savecell[i] = '.'; }
                                    ////////////////////////////////////////////////////////
                                    counter = 0;
                                }
                                step++;
                                for (int i = 0; i < cell.Length; i++) { cell[i] = savecell[i]; savecell[i] = '.'; }

                            }





                        }

                        print(); //Fonksiyonla şekil çizdirildi.


                        if (cki.Key == ConsoleKey.Escape) break;
                    }
                    Console.SetCursorPosition(cursorx, cursory);    // refresh X (current position)
                    Console.WriteLine("");


                }
            }
            else if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine("sdfsyugodsodsog");
            }
            else { }

            Console.ReadLine();
        }
    }
}


