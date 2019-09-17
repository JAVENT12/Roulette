using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Roulette
{
    public class Gambler
    {
        // roulette table method, timer
        private int  pick { get; set;} // single choice
        private string name { get; set;}
        private int [] picks; // orginal array
        private int[] landings { get; set; } // transfered array, hopeful landings
        private int [] choosing { get; set; } // initial rando, array
        private int [] randLand { get; set; } // holds random array, random landings (For future bets)
        private string columnChoice { get; set; } // column choice
        private int money { get; set; }
        private int wallet { get; set; }

        //public int this[int i] // For use in a different class
        //{
        //    get { return picks[i]; }
        //    set { picks[i] = value; }
        //}

        public void table()
        {
            Console.WriteLine("********************************|0|"+"|00|");
            Console.WriteLine("*******************************|1|"+"|2|"+"|3|");
            Console.WriteLine("*******************************|4|"+"|5|"+"|6|");
            Console.WriteLine("*******************************|7|"+"|8|"+"|9|");
            Console.WriteLine("******************************|10|"+"|11|"+"|12|"); 
            Console.WriteLine("******************************|13|"+"|14|"+"|15|"); 
            Console.WriteLine("******************************|16|"+"|17|"+"|18|"); 
            Console.WriteLine("******************************|19|"+"|20|"+"|21|"); 
            Console.WriteLine("******************************|22|"+"|23|"+"|24|"); 
            Console.WriteLine("******************************|25|"+"|26|"+"|27|"); 
            Console.WriteLine("******************************|28|"+"|29|"+"|30|"); 
            Console.WriteLine("******************************|31|"+"|32|"+"|33|"); 
            Console.WriteLine("******************************|34|"+"|35|"+"|36|"); 
        }
        public static int RedColor(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)                             //landed in red
        {
            Console.WriteLine($"You landed in {rouletteCol[0]}!");
            if (choice == "straight up") return StraightUpWin(rouletteInt, numberLuck, rouletteCol, dude, choice);
            if (choice == "split") return SplitBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
            return 1;
        }
        public static int BlackColor(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)                         //landed in black
        {
            Console.WriteLine($"You landed in {rouletteCol[1]}!");
            if (choice == "straight up") return StraightUpWin(rouletteInt, numberLuck, rouletteCol, dude, choice);
            if (choice == "split") return SplitBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
            return 1;
        }
        public static int HighLuck(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice) // landing above 19
        {
            Console.WriteLine("You landed on a number above 18");
            if (numberLuck % 2 != 0) return HighOddLuck(rouletteInt, numberLuck, rouletteCol, dude, choice);
            return HighEvenLuck(rouletteInt, numberLuck, rouletteCol, dude, choice); // change later
        }
        public static int HighOddLuck(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice) //landing on odd above 19
        {
            Console.WriteLine("It's an odd number");
            if (choice == "0" || choice == "00") return ZeroBet(rouletteInt, rouletteCol, numberLuck, dude);
            if (numberLuck > 18 && numberLuck < 37) return RedColor(rouletteInt, numberLuck, rouletteCol, dude, choice);  //--3-1
            return 1; // Color is last fact
        }
        public static int HighEvenLuck(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice) //landing on even above 19
        {
            Console.WriteLine("It's an even number");
            if (choice == "0" || choice == "00") return ZeroBet(rouletteInt, rouletteCol, numberLuck, dude);
            if (numberLuck > 18 && numberLuck < 29) return BlackColor(rouletteInt, numberLuck, rouletteCol, dude, choice);  //--3-1
            if (numberLuck > 18 && numberLuck < 37) return RedColor(rouletteInt, numberLuck, rouletteCol, dude, choice);
            return 1; // Nothing for now
        }
        public static int NumLuck(int[] rouletteInt, string[] rouletteCol, int numberLuck, Gambler dude, string choice) //what number you landed 
        {
            Console.WriteLine($"You landed on {numberLuck}");
            if (numberLuck == 0 || numberLuck == 00) return LowLuck(rouletteInt, numberLuck, rouletteCol, dude, choice);
            else if (numberLuck > 1 && numberLuck < 19) return LowLuck(rouletteInt, numberLuck, rouletteCol, dude, choice);
            else if (numberLuck > 18 && numberLuck < 37) return HighLuck(rouletteInt, numberLuck, rouletteCol, dude, choice);
            return 1; //Nothing for now
        }
        public static int ZeroBet(int[] rouletteInt, string[] rouletteCol, int numberLuck, Gambler dude) // Win 35 times amount          
        {
            if (numberLuck != 0 || numberLuck != 00)
            {
                Console.WriteLine("You lost!");
                if (dude.wallet < 1) throw new Exception("You is broke!");

            }
            else
            {
                int win = dude.money * 35;
                Console.WriteLine($"You landed on {rouletteCol[2]}!"); // numbers are 0 or 00 
                Console.WriteLine($"Congrats on winning ${win}!");
                dude.wallet = dude.wallet - dude.money;
                return Bet(rouletteInt, rouletteCol, numberLuck, dude);
            }
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int LowLuck(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice) // landing below 18
        {
            Console.WriteLine("You landed on a number below 19");
            if (numberLuck % 2 != 0) return LowOddLuck(rouletteInt, numberLuck, rouletteCol, dude, choice);
            return LowEvenLuck(rouletteInt, numberLuck, rouletteCol, dude, choice); // change later
        }
        public static int LowOddLuck(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)//landing on odd below 18
        {
            Console.WriteLine("It's an odd number");
            if (choice == "0" || choice == "00") return ZeroBet(rouletteInt, rouletteCol, numberLuck, dude);
            if (numberLuck < 10) return RedColor(rouletteInt, numberLuck, rouletteCol, dude, choice);   
            if (numberLuck > 10 && numberLuck < 18) return BlackColor(rouletteInt, numberLuck, rouletteCol, dude, choice);                                     
            return 1; // Nothing for now
        }
        public static int LowEvenLuck(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice) //landing on even below 18
        {
            Console.WriteLine("It's an even number");
            if (choice == "0" || choice == "00") return ZeroBet(rouletteInt, rouletteCol, numberLuck, dude);
            if (numberLuck < 11) return BlackColor(rouletteInt, numberLuck, rouletteCol, dude, choice);                                      
            if (numberLuck > 11 && numberLuck < 19) return RedColor(rouletteInt, numberLuck, rouletteCol, dude, choice);                            
            return 1; 
        }
        public static int Luck()
        {          
            try
            {
                int[] rouletteInt = {0, 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,
            17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,00};
                string[] rouletteCol = { "Red", "Black", "Green" };                                                                   
                Console.WriteLine("Roulette!");
                Random r = new Random();
                int numberLuck = r.Next(rouletteInt.Length); // for betting on 1 spot
                Gambler dude = new Gambler { name = "Dan", pick = 0, columnChoice = " ", money = 0, wallet = 0 };
                Console.WriteLine("How much money do you have?");
                int wallet = int.Parse(Console.ReadLine());
                dude.wallet = wallet;
                return Bet(rouletteInt, rouletteCol, numberLuck, dude);
            }
            catch (FormatException)
            {
                return Luck();
            }
        }
        public static int Bet(int[] rouletteInt, string[] rouletteCol, int numberLuck, Gambler dude)    //What bet will you place?
        {
            
                Console.WriteLine($"You have {dude.wallet} to play with! \nHow much will you bet?");          
                int money = int.Parse(Console.ReadLine()); 
                dude.money = money;
            if (dude.money == 0) throw new Exception("You broke!");
            if (money > dude.wallet) return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                Console.WriteLine("What bet will you be placing?");
                string[] bet = { "0", "00", "Straight Up", "Row", "Split", "Street", "Corner", "Top Line", "Six Line", "Column Bet" };
                foreach (var b in bet)
                    Console.WriteLine($"Bet: {b}");
                dude.table();
                string choose = Console.ReadLine().ToLower();
                string choice = choose; // Temp variable to pass 
                switch (choice)
                {
                    case "0":
                        NumLuck(rouletteInt, rouletteCol, numberLuck, dude, choice);   // Recursive        
                        break;
                    case "00":
                        NumLuck(rouletteInt, rouletteCol, numberLuck, dude, choice); //Recursive
                        break;
                    case "straight up":
                        StraightUpBet(rouletteInt, numberLuck, rouletteCol, dude, choice); //Recursive
                        break;
                    case "corner":
                        CornerBets(rouletteInt, numberLuck, rouletteCol, dude, choice);  //Recursive
                        break;
                    case "split":
                        SplitBets(rouletteInt, numberLuck, rouletteCol, dude, choice); //Recursive 
                        break;
                    case "row":
                        RowBets(rouletteInt, numberLuck, rouletteCol, dude, choice); //Recursive
                        break;
                    case "street":
                        StreetBets(rouletteInt, numberLuck, rouletteCol, dude, choice);   //Recursive
                        break;
                    case "top line":
                        TopLineBets(rouletteInt, numberLuck, rouletteCol, dude, choice); //Recursive
                        break;
                    case "six line":
                        SixBetChoice(rouletteInt, numberLuck, rouletteCol, dude, choice); // Recursive
                        break;
                    case "column bet":
                        ColumnBetChoice(rouletteInt, numberLuck, rouletteCol, dude, choice); //Recursive!
                        break;
                    default:
                        Bet(rouletteInt, rouletteCol, numberLuck, dude);
                        break;
                }

            return 1;// Nothing for now
        }
        public static int ColumnBetChoice(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Console.WriteLine("What column do you bet on?\nA) 1,4,7,10,13,16,19,22,25,28,31,34\n" +
                "B) 2,5,8,11,14,17,20,23,26,29,32,35\n" +
                "C) 3,6,9,12,15,18,21,24,27,30,33,36");
            dude.landings = new int[12];
            dude.columnChoice = Console.ReadLine().ToLower();
            if (dude.columnChoice == "a")
            {
                dude.picks = new int[12] { 1, 4, 7, 10, 13, 16, 19, 22, 25, 28, 31, 34 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 12);
                return ColumnBets(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "b")
            {
                dude.picks = new int[12] { 2, 5, 8, 11, 14, 17, 20, 23, 26, 29, 32, 35 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 12);
                return ColumnBets(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "c")
            {
                dude.picks = new int[12] { 3, 6, 9, 12, 15, 18, 21, 24, 27, 30, 33, 36 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 12);
                return ColumnBets(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            return ColumnBetChoice(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int ColumnBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            if (dude.columnChoice == "a")
            {
                Console.WriteLine($"Let's hope it lands on 1,4,7,10,13,16,19,22,25,28,31,34");
            }
            else if (dude.columnChoice == "b")
            {
                Console.WriteLine($"Let's hope it lands on 2,5,8,11,14,17,20,23,26,29,32,35");
            }
            else if (dude.columnChoice == "b")
            {
                Console.WriteLine($"Let's hope it lands on 3,6,9,12,15,18,21,24,27,30,33,36");
            }
            Random rand = new Random();
            dude.choosing = new int[12];
            dude.randLand = new int[12];
            for (int i = 0; i < 12; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 12);
            Console.WriteLine($"You landed on {numberLuck}");
            return ColumnBetResult(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int ColumnBetResult(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in {n}!");
                }
                else
                {
                    int win = dude.money * 2;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    dude.wallet = win + dude.money;
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int SixBetChoice(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Console.WriteLine("Which horizontal row will you bet on?\nA) 1,2,3,4,5,6\n" +
                "B) 7,8,9,10,11,12\nC) 13,14,15,16,17,18\nD) 19,20,21,22,23,24\nE) 25,26,27,28,29,30" +
                "\nF) 31,32,33,34,35,36");
            dude.landings = new int[6];
            dude.columnChoice = Console.ReadLine().ToLower();
            if (dude.columnChoice == "a")
            {
                dude.picks = new int[6] { 1, 2, 3, 4, 5, 6 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 6);
                return SixLineGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "b")
            {
                dude.picks = new int[6] { 7, 8, 9, 10, 11, 12 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 6);
                return SixLineGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "c")
            {
                dude.picks = new int[6] { 13, 14, 15, 16, 17, 18 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 6);
                return SixLineGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "d")
            {
                dude.picks = new int[6] { 19, 20, 21, 22, 23, 24 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 6);
                return SixLineGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "e")
            {
                dude.picks = new int[6] { 25, 26, 27, 28, 29, 30 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 6);
                return SixLineGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            if (dude.columnChoice == "f")
            {
                dude.picks = new int[6] { 31, 32, 33, 34, 35, 36 };
                Array.Copy(dude.picks, 0, dude.landings, 0, 6);
                return SixLineGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            return SixBetChoice(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int SixLineGame(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Random rand = new Random();
            dude.choosing = new int[6];
            dude.randLand = new int[6];
            for (int i = 0; i < 6; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 6);
            return SixBets(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int SixBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            if (dude.columnChoice == "a")
            {
                Console.WriteLine($"Let's hope they land on 1,2,3,4,5,6");
            }
            else if (dude.columnChoice == "b")
            {
                Console.WriteLine($"Let's hope they land on 7,8,9,10,11,12");
            }
            else if (dude.columnChoice == "c")
            {
                Console.WriteLine($"Let's hope they land on 13,14,15,16,17,18");
            }
            if (dude.columnChoice == "d")
            {
                Console.WriteLine($"Let's hope they land on 19,20,21,22,23,24");
            }
            else if (dude.columnChoice == "e")
            {
                Console.WriteLine($"Let's hope they land on 25,26,27,28,29,30");
            }
            else if (dude.columnChoice == "f")
            {
                Console.WriteLine($"Let's hope they land on 31,32,33,34,35,36");
            }
            Random rand = new Random();
            dude.choosing = new int[6];
            dude.randLand = new int[6];
            for (int i = 0; i < 6; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 6);
            Console.WriteLine($"You landed on {numberLuck}");
            return SixBetResult(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int SixBetResult(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in {n}!");
                }
                else
                {
                    int win = dude.money * 5;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    dude.wallet = win + dude.money;
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int TopLineBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Console.WriteLine($"Let's hope they land on {rouletteInt[0]}, {rouletteInt[36]}, {rouletteInt[1]}, {rouletteInt[2]}, and {rouletteInt[3]}");
            Random rand = new Random();
            dude.choosing = new int[5];
            dude.randLand = new int[5];
            for (int i = 0; i < 5; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 5);
            Console.WriteLine($"You landed on {numberLuck}");
            return TopLineBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int TopLineBet(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            dude.landings = new int[5] { 0, 00, 1, 2, 3 };
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in {n}!");
                }
                else
                {
                    int win = dude.money * 6;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    dude.wallet = dude.wallet + dude.money;
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int StreetBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            try
            {
                dude.picks = new int[3];                                                         //determine size of array 
                dude.landings = new int[3];
                Console.WriteLine("What 3 numbers will you bet on?\nA) 1,2,3\nB) 4,5,6\nC) 7,8,9\nD) 10,11,12" +
                    "\nE) 13,14,15\nF) 16,17,18\nG) 19,20,21 \nH) 22,23,24\nI) 25,26,27\nJ) 28,29,30," +
                    "\nK) 31,32,33\nL) 34,35,36\n\t Enter a group of these three numbers ");
                for (int i = 0; i < 3; i++)
                {
                    dude.picks[i] = int.Parse(Console.ReadLine());                                 // determine what numbers to bet on
                }
                Array.Copy(dude.picks, 0, dude.landings, 0, 3);
                return StreetGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            catch (Exception)
            { return StreetGame(rouletteInt, numberLuck, rouletteCol, dude, choice); }
        }
        public static int StreetGame(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Random rand = new Random();
            dude.choosing = new int[3];
            dude.randLand = new int[3];
            for (int i = 0; i < 3; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 3);
            Console.WriteLine($"You landed on {numberLuck}!"); //random landing
            return StreetBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int StreetBet(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in {n}!");
                }
                else
                {
                    int win = dude.money * 11;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int RowBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Console.WriteLine($"Let's hope it lands on 0 or 00");
            Random rand = new Random();
            dude.choosing = new int[2];
            dude.randLand = new int[2];
            for (int i = 0; i < 2; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 2);
            Console.WriteLine($"You landed on {numberLuck}!");
            return RowBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int RowBet(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            dude.landings = new int[2] { 0, 00 };
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in 0 or 00!");
                }
                else
                {
                    int win = dude.money * 17;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    dude.wallet = win + dude.money;
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }

        public static int CornerBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            try
            {
                dude.picks = new int[4];                                                         //determine size of array 
                dude.landings = new int[4];
                Console.WriteLine("What 4 numbers will you bet on?\nThe numbers must adjoin in a block" +
                    "\nExample: 1,2,4,5, or 22,23,25,26");
                for (int i = 0; i < 4; i++)
                {
                    dude.picks[i] = int.Parse(Console.ReadLine());                                 // determine what numbers to bet on
                }
                Array.Copy(dude.picks, 0, dude.landings, 0, 4);
                return CornerGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            catch (Exception)
            { return CornerGame(rouletteInt, numberLuck, rouletteCol, dude, choice); }
        }
        public static int CornerGame(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            Random rand = new Random();
            dude.choosing = new int[4];
            dude.randLand = new int[4];
            for (int i = 0; i < 4; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 4);
            Console.WriteLine($"You landed on {numberLuck} !");
            return CornerBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int CornerBet(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in {n}!");
                }
                else
                {
                    int win = dude.money * 8;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    dude.wallet = win + dude.money;
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int SplitBets(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            try
            {
                dude.picks = new int[2];                                                         //determine size of array 
                dude.landings = new int[2];
                Console.WriteLine("What 2 numbers will you bet on?\nNumbers must be adjoin" +
                    "vertical or horizontal\n\tExample: 23/26 or 26,27");
                for (int i = 0; i < 2; i++)
                {
                    dude.picks[i] = int.Parse(Console.ReadLine());                                 // determine what numbers to bet on
                }
                Array.Copy(dude.picks, 0, dude.landings, 0, 2);
                return SplitGame(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            catch (Exception)
            { return SplitBets(rouletteInt, numberLuck, rouletteCol, dude, choice); }
        }
        public static int SplitGame(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)        // calculate random landings
        {
            Random rand = new Random();
            dude.choosing = new int[2];
            dude.randLand = new int[2];
            for (int i = 0; i < 2; i++)
            {
                dude.choosing[i] = rand.Next(rouletteInt.Length);
            }
            Array.Copy(dude.choosing, 0, dude.randLand, 0, 2);
            Console.WriteLine($"You landed on {numberLuck}!");
            return SplitBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
        }
        public static int SplitBet(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice) // calculate winner or losser      *** 
        {
            foreach (int n in dude.landings)
            {
                if (n != numberLuck)
                {
                    Console.WriteLine($"Looks like it didn't land in {n}!");
                }
                else
                {
                    int win = dude.money * 17;
                    Console.WriteLine($"Looks like you did land in one!\nCongrats on winning {win}!");
                    dude.wallet = win + dude.money;
                    return Bet(rouletteInt, rouletteCol, numberLuck, dude);
                }
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
        public static int StraightUpBet(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)
        {
            try
            {
                Console.WriteLine("What will you bet on?");
                dude.pick = int.Parse(Console.ReadLine());
                if (dude.pick > 0 && dude.pick < 37) return NumLuck(rouletteInt, rouletteCol, numberLuck, dude, choice);
                return StraightUpBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
            catch (Exception)
            {
                return StraightUpBet(rouletteInt, numberLuck, rouletteCol, dude, choice);
            }
        }
        public static int StraightUpWin(int[] rouletteInt, int numberLuck, string[] rouletteCol, Gambler dude, string choice)  // Win 35 times amount  Bet 2
        {
            if (dude.pick == numberLuck)
            {
                int win = dude.money * 35;
                Console.WriteLine($"You won {win}!");
                dude.wallet = win + dude.money;
                return Bet(rouletteInt, rouletteCol, numberLuck, dude);
            }
            else if (dude.pick != numberLuck)
            {
                Console.WriteLine($"Looks like you didn't land in {dude.pick}!");
            }
            Console.WriteLine("You lost!");
            dude.wallet = dude.wallet - dude.money;
            return Bet(rouletteInt, rouletteCol, numberLuck, dude);
        }
    }
}
