using System;
using System.Collections.Generic;

namespace DistroHop;

class App
{
    private bool _running = true;

    private readonly Menu menu = new();
    private readonly Loader loader = new();

    private readonly Downloader downloader = new();
    
    private string distro = "unknown";
    private string pm = "unknown";

    private string Profile = "";
    private List<string> ProfilePkgs = new();

    private readonly string ProfileMenu = "Package profiles []";

    public void MainLoop()
    {
        Initialize();

        while (_running)
        {
            string choice = menu.MainMenu();
            MenuLogic(choice);
        }
    }

    private void Initialize()
    {
        var result = loader.Initialize();

        distro = result.distro;
        pm = result.pm;

        if (distro == "unknown" || pm == "unknown")
        {
            Console.WriteLine($"System initialization failed.\nDistro: {distro}\nPackage manager: {pm}");
            _running = false;
        }
    }

    private void MenuLogic(string choice)
    {
        switch (choice)
        {
            case "1":
                StartSetup();
                break;

            case "2":
                menu.ScriptInfo();
                break;

            case "0":
                Stop();
                break;

            default:
                Console.WriteLine("Wrong input");
                Helper.Pause();
                break;
        }
    }

    private void StartSetup()
    {
        string setupChoice = menu.SetupMenu(
            pm,
            distro,
            ProfileMenu
        );

        switch (setupChoice)
        {
            case "1":
                SelectProfile();
                break;

            case "0":
                Stop();
                break;

            default:
                Console.WriteLine("Wrong input");
                Helper.Pause();
                break;
        }
    }

    private void SelectProfile()
    {
        Profile = menu.Profiles();

        ProfilePkgs = loader.LoadProfile(Profile, pm);

        if (ProfilePkgs.Count == 0)
        {
            Console.WriteLine("No packages loaded.");
            Helper.Pause();
            return;
        }

        ShowPackages();

        string installerChoice = menu.InstallerMenu(pm);

        switch (installerChoice)
        {
            case "1":
                Console.WriteLine("downloading pkgs");
                downloader.Download(ProfilePkgs, pm);
                break;

            case "0":
                Stop();
                break;

            default:
                Console.WriteLine("Wrong input");
                break;
        }
    }

    private void ShowPackages()
    {
        Console.WriteLine("\nPackages:");

        foreach (string pkg in ProfilePkgs)
        {
            Console.WriteLine($"- {pkg}");
        }

        Helper.Pause();
    }

    private void Stop()
    {
        _running = false;
    }
}