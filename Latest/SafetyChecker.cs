using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace DistroHop;

class EnvironmentChecker
{
    public (bool checker, string Distro, string PM) Check()
    {
        string distro = DistroCheck();
        string pm = pkgmanager();
        if (pm == "unknown" || distro == "unknown")
        {
            return (false, distro, pm);
        }
        return (true, distro, pm);
    }

    private string DistroCheck()
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

    private string pkgmanager()
    {
        string[] managers = { "apt", "pacman", "dnf", "zypper", "apk", "eopkg" };

        foreach (var manager in managers)
        {
            if (File.Exists($"/usr/bin/{manager}") || File.Exists($"/bin/{manager}"))
            {
                return manager;
            }
        }
        return "unknown";
    }
}

class JsonIntegrity
{
    public bool Validate(Dictionary<string, Dictionary<string, List<string>>> data)
    {
        if (data == null || data.Count == 0)
        {
            Console.WriteLine("JSON is empty or null");
            return false;
        }

        foreach (var profileEntry in data)
        {
            if (!IsValidKey(profileEntry.Key))
            {
                Console.WriteLine($"Invalid profile key: {profileEntry.Key}");
                return false;
            }

            if (profileEntry.Value == null || profileEntry.Value.Count == 0)
            {
                Console.WriteLine($"Empty package managers list for profile: {profileEntry.Key}");
                return false;
            }

            foreach (var pmEntry in profileEntry.Value)
            {
                if (!IsValidKey(pmEntry.Key))
                {
                    Console.WriteLine($"Invalid package manager key: {pmEntry.Key}");
                    return false;
                }

                if (pmEntry.Value == null || pmEntry.Value.Count == 0)
                {
                    Console.WriteLine($"Empty package list for manager: {pmEntry.Key}");
                    return false;
                }

                foreach (var pkg in pmEntry.Value)
                {
                    if (!IsSafePackage(pkg))
                    {
                        Console.WriteLine($"Unsafe package detected: {pkg}");
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private bool IsValidKey(string key)
    {
        return !string.IsNullOrWhiteSpace(key) &&
               key.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_');
    }

    private bool IsSafePackage(string pkg)
    {
        return !string.IsNullOrWhiteSpace(pkg) &&
               pkg.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_');
    }
}

class PackageManagerIntegrity
{
    public bool Validate(string pm, Dictionary<string, Dictionary<string, List<string>>> data)
    {
        foreach (var profile in data)
        {
            if (profile.Value.ContainsKey(pm))
            {
                return true; 
            }
        }
        
        Console.WriteLine($"Package manager {pm} is not supported in the config");
        return false;
    }
}