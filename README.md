# DistroHop 🐧

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Platform](https://img.shields.io/badge/Platform-Linux%20x64-blue)](https://github.com)
[![Language](https://img.shields.io/badge/Language-C%23-purple)](https://dotnet.microsoft.com/)

> **A lightweight, multi-distro CLI package installer for Linux**

DistroHop simplifies fresh Linux setups by detecting your system’s package manager and installing your predefined packages from a single configuration file.

Instead of manually installing tools after every reinstall or distro switch, DistroHop handles it in one consistent workflow.

---

## 🌟 Features

* 🔍 **Automatic Detection**
  Reads `/etc/os-release` to determine the correct package manager.

* 📦 **Manager-Based Architecture**
  Organize packages by package manager (`apt`, `pacman`, `dnf`) instead of distro names.

* 👁️ **Dry-Run Preview**
  Review packages and commands before executing them.

* ⚡ **Self-Contained Binary**
  No .NET runtime or external dependencies required.

* 🚀 **Minimal & Fast**
  Simple CLI workflow with no unnecessary complexity.

---

## ⚙️ Configuration (`pkgs.json`)

DistroHop requires a `pkgs.json` file in the same directory as the executable.

Example:

```json id="cfg1"
{
  "pacman": ["git", "neovim", "htop", "curl"],
  "apt": ["git", "neovim", "htop", "curl"],
  "dnf": ["git", "neovim", "htop", "curl"]
}
```

---

## 🛠 Installation

Download and extract the latest release:

```bash id="inst1"
tar -xzf DistroHop-v1.5-linux-x64.tar.gz
cd DistroHop
chmod +x DistroHop
./DistroHop
```

---

## ⚠️ Notes

* `pkgs.json` must be present in the same directory as the executable.
* Installation may require root privileges depending on your system.
* Always review the package list before confirming execution.

---

## 🚀 Roadmap

Planned improvements:

* Interactive CLI menu (gaming / work / essentials)
* Custom user-defined package groups
* Improved safety and validation
* Better output and user experience

---

## 📄 License

This project is licensed under the Apache 2.0 License.
