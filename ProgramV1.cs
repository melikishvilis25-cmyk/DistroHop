using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
// WARNING THIS IS A LEGACY VERSION OF THE CODE

class DistroHop
{
    static void Main(string[] args)
    {
        new DistroHop().Run();
    }

    public void Run()
    {
        while (true)
        {
            MainMenu();
        }
    }

    public void MainMenu()
    {
        C.Clear();

        C.WriteL("\n<=== DistroHop ===>");
        C.WriteL("1. Start");
        C.WriteL("0. Exit");

        C.Write("Input: ");

        string choice = C.Read();

        switch (choice)
        {
            case "1":
                Menu();
                break;

            case "0":
                C.WriteL("Bye");
                Environment.Exit(0);
                break;

            default:
                C.WriteL("Wrong input!");
                C.Pause();
                break;
        }
    }


    public void Menu()
    {
        C.Clear();

        C.WriteL("<=== Checking For Your Distro ===>");

        string distro = DistroCheck();

        C.WriteL($"Detected: {distro}");

        C.Pause();

        DistroMenu(distro);
    }


    public void DistroMenu(string distro)
    {
        while (true)
        {
            C.Clear();

            C.WriteL($"\n=== {distro.ToUpper()} ===");

            string choice = CoreMenu();

            switch (choice)
            {
                case "1":
                    Pkgs(distro);
                    C.Pause();
                    break;


                case "2":
                    InstallPackages(distro);
                    C.Pause();
                    break;


                case "0":
                    return;


                default:
                    C.WriteL("Invalid input");
                    C.Pause();
                    break;
            }
        }
    }


    public void InstallPackages(string distro)
    {
        C.Write("Are you sure you want to proceed [y/n]: ");

        string sure = C.Read().ToLowerInvariant();


        if (sure != "y")
        {
            C.WriteL("Cancelled.");
            return;
        }


        var packages = LoadPkgs();


        if (!packages.ContainsKey(distro))
        {
            C.WriteL($"No packages found for {distro}");
            return;
        }


        string pkgList = string.Join(" ", packages[distro]);

        Downloader downloader = new Downloader();


        switch (distro)
        {
            case "arch":
            case "cachyos":
            case "manjaro":
            case "endeavouros":

                downloader.Download(
                    "sudo",
                    $"pacman -S --noconfirm {pkgList}"
                );

                break;


            case "fedora":

                downloader.Download(
                    "sudo",
                    $"dnf install -y {pkgList}"
                );

                break;


            case "debian":
            case "ubuntu":
            case "linuxmint":

                downloader.Download(
                    "sudo",
                    $"apt install -y {pkgList}"
                );

                break;


            default:

                C.WriteL($"Unsupported distro: {distro}");
                break;
        }
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


    public string CoreMenu()
    {
        C.WriteL("1. See packages");
        C.WriteL("2. Start installation");
        C.WriteL("0. Back");


        return C.Read();
    }


    public void Pkgs(string distro)
    {
        var packages = LoadPkgs();


        if (!packages.ContainsKey(distro))
        {
            C.WriteL($"No packages found for {distro}");
            return;
        }


        C.WriteL($"\nPackages for {distro}:");


        foreach (string pkg in packages[distro])
        {
            C.WriteL($"- {pkg}");
        }
    }



    public Dictionary<string, List<string>> LoadPkgs()
    {
        string path = "pkgs.json";


        if (!File.Exists(path))
        {
            C.WriteL("pkgs.json not found!");

            return new Dictionary<string, List<string>>();
        }


        string json = File.ReadAllText(path);


        return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json)
            ?? new Dictionary<string, List<string>>();
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



class Downloader
{
    public void Download(string command, string args)
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


            if (process.ExitCode != 0)
            {
                Console.WriteLine($"Process failed: {process.ExitCode}");
            }
        }

        catch (Exception ex)
        {
            Console.WriteLine($"Downloader error: {ex.Message}");
        }
    }
}
