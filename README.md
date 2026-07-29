# DistroHop 🐧

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Platform](https://img.shields.io/badge/Platform-Linux%20x64-blue)](https://github.com)
[![Language](https://img.shields.io/badge/Language-C%23-purple)](https://dotnet.microsoft.com/)

**DistroHop** is a lightweight, cross-distro package setup assistant written in C#. 

DistroHop automates the post-installation tediousness of setting up a fresh Linux machine. Whether you're constantly distro-hopping or setting up a clean install, DistroHop detects your distribution, matches it against your central package configuration, and executes the installation commands for you.

---

## 🌟 Features

- 🔍 **Automatic OS Detection:** Reads `/etc/os-release` to seamlessly identify your active distribution.
- ⚙️ **Centralized JSON Config:** Keep all your package lists for different distributions in a single `pkgs.json` file.
- 👁️ **Preview & Confirmation:** Review all packages scheduled for installation before running root-level commands.
- 🚀 **Automated Setup:** Seamlessly invokes native package managers without manual copy-pasting.
- 📦 **Standalone Executable:** Compiled as a self-contained C# Linux x64 binary—no .NET runtime required.

---

## 🐧 Supported Distributions

DistroHop out-of-the-box supports major distribution families:

- **Arch-based** (`pacman`)
- **Debian / Ubuntu-based** (`apt`)
- **Fedora-based** (`dnf`)

---

## ⚙️ Configuration (`pkgs.json`)

DistroHop relies on a `pkgs.json` file located in the same directory as the executable. You can define target packages per distribution family:

```json
{
  "arch": ["git", "neovim", "htop", "curl"],
  "debian": ["git", "neovim", "htop", "curl"],
  "fedora": ["git", "neovim", "htop", "curl"]
}
