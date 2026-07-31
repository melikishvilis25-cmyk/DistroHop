using System;
using System.Security.Cryptography.X509Certificates;


namespace DistroHop;

class Menu
{
    public string Heading = "\n<===DistroHop V2===>";
    public string[] check = {"", "x"};
    public string MainMenu()
    {
        Helper.WL(Heading);
        Helper.WL("\n==Made by DatoVarZma/Melikishvilis25-cmyk");
        Helper.WL("1.Start");
        Helper.WL("2.Script information");
        Helper.WL("0.Exit");
        Console.WriteLine("Input: ");
        return Helper.Read() ?? "";
        
    }
    public void ScriptInfo()
    {
        Helper.WL("This script automatically detects your PM,Distro and checks if pkgs.json has your pm and then downloads pks based on what profile you chose");
        Helper.Pause();
        return;
    }
    public string SetupMenu(string PM, string Distro, string state)
    {
        Helper.WL(Heading + "+" + Distro);
        Helper.WL($"Package manager: {PM}");
        Helper.WL($"1.{state}");
        Helper.WL("Select package profile to start the script");
        return Helper.Read() ?? "";
    }
    private int selected = 3; // default = essential

    public string Profiles()
    {
        while (true)
        {
            Helper.Clear();
            Helper.WL(Heading);

            Helper.WL($"1. Gaming [{(selected == 1 ? "x" : "")}]");
            Helper.WL($"2. Work   [{(selected == 2 ? "x" : "")}]");
            Helper.WL($"3. Essential [{(selected == 3 ? "x" : "")}]");
            Helper.WL("4. Submit");

            string input = Helper.Read() ?? "";

            switch (input)
            {
                case "1": selected = 1; break;
                case "2": selected = 2; break;
                case "3": selected = 3; break;
                case "4":
                    return selected switch
                    {
                        1 => "gaming",
                        2 => "work",
                        _ => "essential"
                    };
                default:
                    Helper.WL("Invalid input");
                    Helper.Pause();
                    break;
            }
        }
    }
    public string InstallerMenu(string PM)
    {
        Helper.Clear();
        Helper.WL(Heading);
        Helper.WL($"Using {PM} to download pkgs");

        Helper.WL("1.See pkgs that are going to be installed");
        Helper.WL("0.Abort the script");
        return Helper.Read() ?? "";
    }
}
class Helper
{
    public static void WL(string text) //WL is WriteLine
    {
        Console.WriteLine(text);
    }
    public static string Read()
    {
        string ReadLine = Console.ReadLine() ?? "";
        return ReadLine;
    }
    public static void Clear()
    {
        Console.Clear();
    }
    public static void Pause()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}