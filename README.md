# DistroHop

A lightweight Linux package setup assistant written in C#.

DistroHop helps automate the process of installing your commonly used packages after switching or reinstalling Linux distributions. It detects your distribution, loads a predefined package list, and runs the required installation commands.

## Features

- Automatic Linux distribution detection
- JSON-based package configuration
- Preview packages before installation
- Installation confirmation prompt
- Automated package installation
- Self-contained Linux x64 executable

## How It Works

1. DistroHop reads your system information from `/etc/os-release`
2. It identifies your Linux distribution
3. It loads the matching package list from `pkgs.json`
4. You can review the packages
5. DistroHop installs them using your distribution's package manager

## Supported Distributions

Currently supported:

- Arch-based distributions
- Fedora-based distributions
- Debian-based distributions

## Installation

Download the latest release from the GitHub Releases page.

Extract the archive:

```bash
tar -xzf DistroHop-linux-x64.tar.gz
```
Then go to de publish/ dir and run
```
chmod +x DistroHop
./DistroHop

```
