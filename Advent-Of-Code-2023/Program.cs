using System.Diagnostics;
using System.Net;
using System.Reflection;

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
            bool firstStart = true;
            if (!Directory.Exists("Settings")) Directory.CreateDirectory("Settings");
            if (!File.Exists(Path.Combine("Settings", "firstStart"))) File.Create(Path.Combine("Settings", "firstStart"));
            else firstStart = false;
            Console.WriteLine("Advent of Code 2023!");
            while (!result.Equals("Q", StringComparison.CurrentCultureIgnoreCase))
            {
                if (firstStart)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("You can select the challenge using days number [1-25]");
                    Console.WriteLine("You can also select to run all available challenges by using letter \"A\"");
                    Console.WriteLine("You can delete your cached inputs by writing \"D\"");
                    Console.WriteLine("To quit write letter \"Q\"");
                    Console.WriteLine("To show this info, write letter \"I\"");
                    Console.WriteLine();
                    Console.WriteLine("This program is able to automatically download your inputs for Advent of Code");
                    Console.WriteLine("To do that you need to provide \"cookie.txt\" file in \"Settings\" folder of program.");
                    Console.WriteLine("To get detailed info about how to get \"cookie.txt\", use [C] option.");
                    Console.WriteLine("Your inputs are cached in \"Inputs\" folder.");
                    Console.WriteLine("You can also put them in manually in format \"inputData_{year}_{day}.txt\".");
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.White; 
                    Console.Write("What is your choice? ");
                    firstStart = false;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Select the challenge [1-25] or all challenges [A], write [I] for more info or [Q] to quit: ");
                }
                result = Console.ReadLine() ?? "";

                if (int.TryParse(result, out int parsed) && parsed >= 1 && parsed <= 25)
                {
                    if (CheckPossible(2023, parsed))
                    {
                        DoChallenge(2023, parsed);
                        Console.WriteLine();
                    }
                    else
                    {
                        DrawATree();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Challenge for Day {parsed} is not finished yet.");
                        Console.WriteLine();
                    }
                }
                else if (result.Equals("A", StringComparison.CurrentCultureIgnoreCase))
                {
                    DoAllChallenges();
                }
                else if (result.Equals("I", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    firstStart = true;
                }
                else if (result.Equals("C", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("To get your Advent of Code login cookie:");
                    Console.WriteLine("1) Open input in your browser when logged in.");
                    Console.WriteLine("2) Use \"F12\" to open developer tools, and select \"Network\" tab");
                    Console.WriteLine("3) Refresh the page");
                    Console.WriteLine("4) Select \"input\"");
                    Console.WriteLine("5) On right side, in  Headers tab, find \"Cookie\"");
                    Console.WriteLine("6) Copy the text of a Cookie");
                    Console.WriteLine("7) Paste the text into \"cookie.txt\" file in \"Settings\" folder of program");
                    Console.WriteLine();
                }
                else if (result.Equals("D", StringComparison.CurrentCultureIgnoreCase))
                {
                    DrawATree();
                    try
                    {
                        Directory.Delete("Inputs", true);
                    }
                    catch
                    {

                    }
                }
                else if (!result.Equals("Q", StringComparison.CurrentCultureIgnoreCase)) 
                {
                    DrawATree();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong command.");
                    Console.WriteLine();
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

        static ulong DoChallenge(int year, int day, bool nice = true)
        {
            if (nice)
            {
                DrawATree();
            }
            string? inputData = null;
            string filePath = Path.Combine("Inputs", $"inputData_{year}_{day}.txt");
            if (File.Exists(filePath))
            {
                inputData = File.ReadAllText(filePath);
            }
            else
            {
                Task<DownloadState> downloadStateTask = DownloadInput(year, day);
                downloadStateTask.Wait();
                if (downloadStateTask.Result == DownloadState.Downloaded || downloadStateTask.Result == DownloadState.FileExists)
                {
                    inputData = File.ReadAllText(filePath);
                }
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
            Stopwatch watch;
            if (challenge1method is null || inputData is null)
            {
                watch = Stopwatch.StartNew();
                result = "ERROR";
                watch.Stop();
                return (watch, result);
            }

            string[] data = [inputData];
            watch = Stopwatch.StartNew();
            result = challenge1method.Invoke(null, data)?.ToString() ?? "ERROR";
            watch.Stop();
            return (watch, result);
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
                    totalTime += DoChallenge(2023, i, false);
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

        enum DownloadState { Downloaded, FileExists, FailedDownload, FailedFileWrite, NoCookie }
        static async Task<DownloadState> DownloadInput(int year, int day)
        {
            if (!File.Exists(Path.Combine("Inputs", $"inputData_{year}_{day}.txt")))
            {
                if (!File.Exists("Settings/cookie.txt"))
                {
                    return DownloadState.NoCookie;
                }
                string cookie = File.ReadAllText("Settings/cookie.txt");

                try
                {
                    using HttpClient client = new();
                    client.DefaultRequestHeaders.Add("Cookie", cookie);
                    HttpResponseMessage response = await client.GetAsync($"https://adventofcode.com/{year}/day/{day}/input");
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        byte[] data = await response.Content.ReadAsByteArrayAsync();
                        try
                        {
                            if (!Directory.Exists("Inputs"))
                            {
                                Directory.CreateDirectory("Inputs");
                            }
                            File.WriteAllBytes(Path.Combine("Inputs",$"inputData_{year}_{day}.txt"), data);
                        }
                        catch
                        {
                            return DownloadState.FailedFileWrite;
                        }
                        return DownloadState.Downloaded;
                    }
                    else
                    {
                        return DownloadState.FailedDownload;
                    }
                }
                catch
                {
                    return DownloadState.FailedDownload;
                }
            }
            else
            {
                return DownloadState.FileExists;
            }
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