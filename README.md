# DistroHop

DistroHop is a CLI tool that automatically downloads packages after you hop to a different distro.

## Features

* Automatic PM Detection: Uses /etc/os-release to identify your distro and package manager (e.g., apt, pacman, dnf, zypper, apk).
* Profile-Based Setup: Organize packages into profiles like Gaming, Work, or Essential.
* Self-Contained: The .NET runtime isn't required to run the pre-compiled binary.
* Declarative: Keep your package profiles structured in a single pkgs.json file.
* Safety Checks: Built-in string validation and JSON structure integrity checks before installation.
* Preview: Preview packages that will be installed directly inside the interactive menu.

---

## Configuration

DistroHop comes with a default pkgs.json template, but you can freely modify it or add new profiles and packages. V2 uses a nested structure organized by profile and package manager.

pkgs.json example:

```json
{
  "essential": {
    "apt": [
      "curl",
      "wget",
      "git",
      "fastfetch",
      "neovim"
    ],
    "pacman": [
      "curl",
      "wget",
      "git",
      "fastfetch",
      "neovim"
    ],
    "dnf": [
      "curl",
      "wget",
      "git",
      "fastfetch",
      "neovim"
    ]
  },
  "gaming": {
    "apt": [
      "steam",
      "lutris",
      "wine"
    ],
    "pacman": [
      "steam",
      "lutris",
      "wine"
    ]
  }
}
```

---

## Quick Start

Download the latest release (2.0) and run :

```bash
tar -xzf distrohop-v2-linux-x64.tar.gz
cd DistroHop
chmod +x DistroHop
./DistroHop
```

---
### Built by melikishvilis25-cmyk (DatoVarZma) as a learning project focused on C# architecture and cross-distro tooling.
---
