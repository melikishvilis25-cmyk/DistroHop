using System;
using System.IO;
using System.Diagnostics;
using System.Text.Json;
using System.Collections;

namespace DistroHop;

class Downloader
{
    public void Download(List<string> pkgs, string manager)
    {
        if (pkgs == null || pkgs.Count == 0)
        {
            Console.WriteLine("No packages to install.");
            return;
        }

        string pkglist = string.Join(" ", pkgs);

        switch (manager)
        {
            case "pacman":
                DownloadPkgs("sudo", $"pacman -Syu --noconfirm {pkglist}");
                break;

            case "apt":
                DownloadPkgs("sudo", $"apt install -y {pkglist}");
                break;

            case "dnf":
                DownloadPkgs("sudo", $"dnf install -y {pkglist}");
                break;

            case "zypper":
                DownloadPkgs("sudo", $"zypper install -y {pkglist}");
                break;

            case "apk":
                DownloadPkgs("sudo", $"apk add {pkglist}");
                break;

            default:
                Console.WriteLine("Unsupported package manager.");
                return;
        }
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
        catch (Exception ex)
        {
            Console.WriteLine($"Program encountered a problem: {ex.Message}");
        }
    }
}