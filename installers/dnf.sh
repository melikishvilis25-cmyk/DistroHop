#!/bin/bash
set -e
[ -f pkgs/dnf.txt ] || error "Missing pkgs/dnf.txt"
echo "[*] Updating system..."
sudo dnf upgrade -y

echo "[*] Installing DNF packages..."
PKGS=$(grep -vE '^\s*#' pkgs/dnf.txt | tr '\n' ' ')
sudo dnf install -y $PKGS
