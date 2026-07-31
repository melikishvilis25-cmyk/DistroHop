using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DistroHop;

class Loader
{
    EnvironmentChecker load = new();

    public (string distro, string pm) Initialize()
    {
        var (success, Distro, PM) = load.Check();

        if (!success)
        {
            Console.WriteLine("Environment check failed");
            return ("unknown", "unknown");
        }

        return (Distro, PM);
    }

    public void ManagerCheck(string profile, string manager)
    {
        var pkgs = Load();

        if (!pkgs.ContainsKey(profile) || !pkgs[profile].ContainsKey(manager))
        {
            Console.WriteLine($"The right type of {manager} was not found for profile {profile}");
            return;
        }

        foreach (string pkg in pkgs[profile][manager])
        {
            Console.WriteLine($"- {pkg}");
        }
    }

    public List<string> LoadProfile(string profile, string pm)
    {
        var pkgs = Load();

        if (!pkgs.ContainsKey(profile))
        {
            Console.WriteLine($"The profile '{profile}' was not found");
            return new List<string>();
        }
        
        if (!pkgs[profile].ContainsKey(pm))
        {
            Console.WriteLine($"Packages for '{pm}' were not found in profile '{profile}'");
            return new List<string>();
        }

        return pkgs[profile][pm];
    }

    public Dictionary<string, Dictionary<string, List<string>>> Load()
    {
        try
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                "pkgs.json"
            );

            if (!File.Exists(path))
            {
                Console.WriteLine("pkgs.json was not found in the same dir as the program");
                return new Dictionary<string, Dictionary<string, List<string>>>();
            }

            string json = File.ReadAllText(path);

            var data = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, List<string>>>>(json)
                ?? new Dictionary<string, Dictionary<string, List<string>>>();

            JsonIntegrity checker = new();

            if (!checker.Validate(data))
            {
                Console.WriteLine("JSON integrity check failed");
                return new Dictionary<string, Dictionary<string, List<string>>>();
            }

            return data;
        }
        catch (JsonException)
        {
            Console.WriteLine("pkgs.json has invalid JSON format");
            return new Dictionary<string, Dictionary<string, List<string>>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Loader error: {ex.Message}");
            return new Dictionary<string, Dictionary<string, List<string>>>();
        }
    }
}