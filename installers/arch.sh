#!/bin/bash
set -e

cd "$(dirname "$0")"
source ../utils/common.sh

[ -f pkgs/arch.txt ] || error "Missing pkgs/arch.txt"
[ -f pkgs/aur.txt ] || error "Missing pkgs/aur.txt"

log "Updating system..."
sudo pacman -Syu --noconfirm

log "Installing pacman packages..."
sudo pacman -S --needed --noconfirm - < pkgs/arch.txt

if ! command -v yay &>/dev/null; then
    log "Installing yay..."

    sudo pacman -S --needed --noconfirm git base-devel

    rm -rf /tmp/yay
    git clone https://aur.archlinux.org/yay.git /tmp/yay

    pushd /tmp/yay >/dev/null
    makepkg -si --noconfirm
    popd >/dev/null
fi

log "Installing AUR packages..."
yay -S --needed --noconfirm - < pkgs/aur.txt