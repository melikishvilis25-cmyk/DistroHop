#!/bin/bash
set -e
[ -f pkgs/apt.txt ] || error "Missing pkgs/apt.txt"
echo "[*] Updating system..."
sudo apt update
sudo apt upgrade -y

echo "[*] Installing APT packages..."
PKGS=$(grep -vE '^\s*#' pkgs/apt.txt | tr '\n' ' ')
sudo apt install -y $PKGS