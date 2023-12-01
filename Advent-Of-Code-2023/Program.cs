using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows.Forms;

namespace AdventOfCode
{
    /// <summary>
    /// Main Advent of Code 2023 Class
    /// </summary>
    class Program
    {
        static void Main()
        {
            DrawATree();
            Console.ForegroundColor = ConsoleColor.White;
            string result = "";
            while (!result.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Advent of Code 2023!");
                Console.Write("Select the challenge [1-25] or all challenges [A], write [Q] to quit: ");
                result = Console.ReadLine() ?? "";

                if (int.TryParse(result, out int parsed) && parsed >= 1 && parsed <= 25)
                {
                    if (CheckPossible(2023, parsed))
                    {
                        DoChallenge(parsed);
                        Console.WriteLine();
                    }
                    else
                    {
                        DrawATree();
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($"Challenge for Day {parsed} is not finished yet.");
                        Console.WriteLine();
                    }
                }
                else if (result.Equals("A", StringComparison.CurrentCultureIgnoreCase))
                {
                    DoAllChallenges();
                }
                else if (!result.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Wrong command.");
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static private string FormatTime(ulong milliseconds)
        {
            uint milli = (uint)(milliseconds % 1000);
            uint seconds = (uint)(milliseconds / 1000);
            uint minutes = seconds / 60;
            seconds %= 60;
            uint hours = minutes / 60;
            minutes %= 60;
            string time = "";
            if (hours > 0) time = hours.ToString().PadLeft(2, '0') + ":";
            if (minutes > 0 || hours > 0) time += minutes.ToString().PadLeft(2, '0') + ":";
            if (seconds > 0 && (minutes > 0 || hours > 0)) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0');
            else if (seconds > 0) time += seconds.ToString().PadLeft(2, '0') + "." + milli.ToString().PadLeft(3, '0') + "s";
            else time += milli.ToString().PadLeft(3, '0') + "ms";
            return time;
        }

        static ulong DoChallenge(int day, bool nice = true)
        {
            if (nice)
            {
                DrawATree();
            }
            string? inputData = null;
            if (File.Exists($"inputData{day}.txt"))
            {
                inputData = File.ReadAllText($"inputData{day}.txt");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Results of Day {day} are: ");
            (Stopwatch watch1, string result1) = RunChallenge(2023, day, 1, inputData);
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (result1.Contains('\n') || result1.Length > 20)
            {
                Console.WriteLine();
                Console.WriteLine(result1);
            }
            else
            {
                Console.Write(result1);
            }
            Console.ForegroundColor = ConsoleColor.White;
            (Stopwatch watch2, string result2) = RunChallenge(2023, day, 2, inputData);
            Console.Write(" and ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (result2.Contains('\n') || result2.Length > 20)
            {
                Console.WriteLine();
                Console.WriteLine(result2);
            }
            else
            {
                Console.Write(result2);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("! ");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("It took ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FormatTime((ulong)watch1.Elapsed.TotalMilliseconds));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" and ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FormatTime((ulong)watch2.Elapsed.TotalMilliseconds));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");
            return (ulong)watch1.ElapsedMilliseconds + (ulong)watch2.ElapsedMilliseconds;
        }

        static (Stopwatch watch, string result) RunChallenge(int year, int day, int challenge, string? inputData)
        {
            MethodInfo? challenge1method = GetMethodInfo(2023, day, challenge);
            string result = "";
            if (challenge1method != null && inputData != null)
            {
                string[] data = [inputData];
                Stopwatch watch = Stopwatch.StartNew();
                result = challenge1method.Invoke(null, data)?.ToString() ?? "ERROR";
                watch.Stop();
                return (watch, result);
            }
            else
            {
                Stopwatch watch = Stopwatch.StartNew();
                result = "ERROR";
                watch.Stop();
                return (watch, result);
            }
        }

        static bool CheckPossible(int year, int day)
        {
            Type? type = Type.GetType($"AdventOfCode.Day{day.ToString().PadLeft(2, '0')}.Challenge1, Advent-Of-Code-{year}-{day.ToString().PadLeft(2, '0')}");
            if (type == null) { return false; }
            return true;
        }

        static MethodInfo? GetMethodInfo(int year, int day, int challenge)
        {
            Type? type = Type.GetType($"AdventOfCode.Day{day.ToString().PadLeft(2, '0')}.Challenge{challenge}, Advent-Of-Code-{year}-{day.ToString().PadLeft(2,'0')}");
            if (type == null) { return null; }
            return type.GetMethod("DoChallenge");
        }

        static void DoAllChallenges()
        {
            DrawATree();
            ulong totalTime = 0;
            for (int i = 1; i <= 25; i++)
            {
                if (CheckPossible(2023, i))
                {
                    totalTime += DoChallenge(i, false);
                }
                else
                {
                    break;
                }
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Total time is: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(FormatTime(totalTime));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(".");
            Console.WriteLine();
        }

        static void DrawATree()
        {
            Console.Clear();
            for (int i = 0; i < Console.WindowWidth / 40 * Console.WindowHeight; i++)
            {
                Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 16);
                int x = new Random().Next(0, Console.WindowWidth);
                int y = new Random().Next(0, Console.WindowHeight);
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write('*');

            }
            Console.CursorLeft = 0;
            Console.CursorTop = 0;
            
            DrawLine(@"()", 11, 25);
            DrawLine(@"/\", 11, 23);
            DrawLine(@"/i\\", 10, 21);
            DrawLine(@"o/\\", 10, 19);
            DrawLine(@"///\i\", 9, 17);
            DrawLine(@"/*/o\\", 9, 15);
            DrawLine(@"/i//\*\", 8, 13);
            DrawLine(@"/ o/*\\i\", 8, 11);
            DrawLine(@"//i//o\\\\", 7, 9);
            DrawLine(@"/*////\\\\i\", 6, 7);
            DrawLine(@"//o//i\\*\\\", 6, 5);
            DrawLine(@"/i///*/\\\\\o\", 5, 3);
            DrawLine(@"||", 11, 1);
        }

        static void DrawLine(string line, int left, int day)
        {
            Console.CursorLeft += left;
            foreach (char c in line)
            {
                if (c == '\\' || c == '/')
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else if (c == '(' || c == ')')
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = (ConsoleColor)new Random().Next(1, 16);
                }
                if (!(DateTime.Now.Year > 2023 || (DateTime.Now.Year == 2023 && DateTime.Now.Month == 12 && DateTime.Now.Day >= day)))
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                }
                Console.Write(c);
            }
            Console.WriteLine();
        }
    }
}