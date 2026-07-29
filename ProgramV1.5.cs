using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Collections;
using System.Xml.Serialization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

/*
    Here are some useful information if you reading this code
    1.If i use "__" it means that im copying the comment above
    2.If you found variables that have funny names.. Sorry
    3.This script in split so
        Menu and app flow -> Class DistroHop
        Info Checking -> class pkgs
        class C is for making menus faster
        class Downloader is used to download and load pkgs from the json file
    4.If you dont understand a part of a script check the comments
    5.thanks for reading!
*/

class DistroHop
{
    public bool running = true;
    public string pkgschecked = "pkgs to be installed []";
    public bool PkgsBool = false;
    public string sinfo = "Scirpt Information []"; //s stands for script
    public bool SinfoMenu = false;
    pkgs Pkgs = new pkgs();
    Downloader downloader = new Downloader();
    static void Main(string[] args)
    {
        new DistroHop().Run();
    }
    public void Run()
    {
        while (running)
        {
            Core();
        }
    }
    public void Core()
    {
        string manager = Pkgs.pkgmanager();
        string distro = Pkgs.DistroCheck();
        Menu(distro, manager);
    }
    public void Menu(string Distro, string manager)
    {
        
        C.Clear();
        C.WriteL($"\n<==={Distro}===>");
        C.WriteL($"Manager: {manager}");
        C.WriteL($"1. {pkgschecked}");
        C.WriteL($"2. {sinfo}");
        C.WriteL("0.Exit");
        

        if (PkgsBool == true && SinfoMenu == true)
        {
            C.WriteL("3. Start the script");
            string choice = C.Read() ?? "";
            switch (choice)
            {
                case "1": 
                    PkgsMenu(manager);
                    break;
                case "2":
                    ScriptInfo(); 
                    break; 
                case "3":
                    StartMenu(Distro, manager);
                    break; 
                case "0": 
                    running = false; 
                    break;
                default:
                    C.WriteL("Wrong input");
                    return;
            }

        }
        else
        {
            C.WriteL("Check Pkgs and Script info menus to start the script");
            Console.Write("Input: ");
            string choice1 = C.Read() ?? "";
            switch (choice1)
            {
                case "1":
                    PkgsMenu(manager); 
                    break;
                case "2":
                    ScriptInfo(); 
                    break;
                case "0": 
                    running = false;
                    break;
                default: 
                    C.WriteL("wrong input");
                    return;
                
            }
        }
        

    }
    public void PkgsMenu(string manager)
    {
        pkgschecked = "pkgs to be installed [X]";
        PkgsBool = true;
        C.Clear();
        C.WriteL($"{manager}s pkgs about to be installed");
        downloader.ManagerCheck(manager);
        ConsoleKeyInfo input = Console.ReadKey(true);
        C.WriteL("Press ENTER to exit back");
        if (input.Key == ConsoleKey.Enter)
        {
            return;
        }
    }
    public void ScriptInfo()
    {
        sinfo = "Scirpt Information [X]";
        SinfoMenu = true;
        C.Clear();
        C.WriteL($"This script will install the pkgs that were shown in Pkgs Menu, It automatically detects your Distro and Pkg manager");
        ConsoleKeyInfo input = Console.ReadKey(true);
        C.WriteL("Press ENTER to exit back");
        if (input.Key == ConsoleKey.Enter)
        {
            return;
        }
    }
    public void StartMenu(string disto, string manager )
    {
        
        C.Clear();
        C.WriteL($"\n<==={disto}Hop===>");
        C.WriteL("1. Start");
        C.WriteL("0. abort the script");
        Console.Write("input");
        string choice = C.Read() ?? "";
        if (choice == "1")
        {
            C.WriteL("downloading starting...");
            Download(manager);
            
        }
        else if (choice == "0")
        {
            Console.Write("are you sure?[y/n]");
            string surebuddy = C.Read() ?? "";
            if (surebuddy.ToLowerInvariant() == "y")
            {
                C.WriteL("bye :(");
                running = false;
            }
            else if (surebuddy.ToLowerInvariant() == "n")
            {
                return;
            }
            else
            {
                C.WriteL("invalid input");
                return;
            }
            
        }
        else
        {
            C.WriteL("invalid input!");
            return;
        }

    }
    public void Download(string manager)
    {
        var pkgs = downloader.Load();
        if (!pkgs.ContainsKey(manager))
        {
            C.WriteL("Your pkg manager is not supported");
            return;
        }
        string pkgslist = string.Join(" ", pkgs[manager]);
        switch (manager)
        {
            case "pacman":
                downloader.DownloadPkgs(
                    "sudo",
                    $"pacman -S --noconfirm {pkgslist}"
                );
                break;
            case "dnf":
                downloader.DownloadPkgs(
                    "sudo",
                    $"dnf install -y {pkgslist}"
                );
                break;
            case "apt":
                downloader.DownloadPkgs(
                    "sudo",
                    $"apt install -y {pkgslist}"
                );
                break;
            case "zypper":
                downloader.DownloadPkgs(
                    "sudo",
                    $"zypper install -y {pkgslist}"
                );
                break;
            default:
                C.WriteL("unsupported pkg manager sorry will be fixed in 2.0");
                return;
            
        }
    }

}
class pkgs
{
    public string pkgmanager()
    {
        string[] managers = { "apt", "pacman", "dnf", "zypper", "apk", "eopkg" };

        foreach (var manager in managers)
        {
            if (File.Exists($"/usr/bin/{manager}"))
            {
                return manager;
            }
        }
        return "unknown";
    }
    public string DistroCheck()
    {
        string path = "/etc/os-release";


        if (!File.Exists(path))
            return "unknown";


        string? id = null;
        string? idLike = null;


        foreach (string line in File.ReadAllLines(path))
        {
            if (line.StartsWith("ID="))
            {
                id = line.Substring(3).Replace("\"", "");
            }


            if (line.StartsWith("ID_LIKE="))
            {
                idLike = line.Substring(8).Replace("\"", "");
            }
        }


        return id ?? idLike ?? "unknown";
    }
}
class Downloader
{
    public void ManagerCheck(string manager)
    {
        var pkgs = Load();

        if (!pkgs.ContainsKey(manager))
        {
            C.WriteL($"The right type of {manager} was not found");
            return;
        }
        foreach (string pkg in pkgs[manager])
        {
            C.WriteL($"- {pkg}");
        }
    }
    public Dictionary<string, List<string>> Load()
    {
        string path = Path.Combine(AppContext.BaseDirectory, "pkgs.json");
        if (!File.Exists(path))
        {
            C.WriteL("pkgs.json was not found in the same dir as the program");
            return new Dictionary<string, List<string>>();
        }
        string json = File.ReadAllText(path);


        return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json)
            ?? new Dictionary<string, List<string>>();
    }
    public void DownloadPkgs(string command, string args)
    {
        try
        {
            Process process = new Process();


            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;


            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine(e.Data);
            };


            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine(e.Data);
            };


            process.Start();


            process.BeginOutputReadLine();
            process.BeginErrorReadLine();


            process.WaitForExit();

        }
        catch(Exception problemLOl)
        {
            C.WriteL($"Program encountered a problem. The problem: {problemLOl}");
        }
    }

}
class C
{
    public static void WriteL(string text)
    {
        Console.WriteLine(text);
    }


    public static string Read()
    {
        return Console.ReadLine() ?? "";
    }


    public static void Write(string text)
    {
        Console.Write(text);
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
