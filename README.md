# DistroHop 🐧 

DistroHop is a CLI tool that automatically downloads packages after you hop to a different distro.

## Features

* **Automatic PM Detection** -> Uses `/etc/os-release` to identify your package manager (e.g., `apt`, `dnf`).
* **Self-Contained** -> Meaning the `.NET` runtime isn't needed.
* **Declarative** -> Keep your package list in a `pkgs.json` file.
* **Preview** -> Preview the packages that are going to be installed by the script directly in the menu.

---

## Configuration

DistroHop automatically has a template for `pkgs.json`, but you can freely add your own packages to the template.

`pkgs.json` example:

```json
{
  "pacman": ["git", "neovim", "htop", "curl"],
  "apt": ["git", "neovim", "htop", "curl"],
  "dnf": ["git", "neovim", "htop", "curl"]
}
```
---

## Quick Start

Download the latest release and run:

```bash
tar -xzf DistroHop-v1.5-linux-x64.tar.gz
cd DistroHop
chmod +x DistroHop
./DistroHop
```