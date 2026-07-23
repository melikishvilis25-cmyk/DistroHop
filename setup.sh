#!/bin/bash
set -e

cd "$(dirname "$0")"
source utils/common.sh

log "Detecting distro..."

source /etc/os-release

case "$ID" in
    arch)
        bash installers/arch.sh
        ;;
    ubuntu|debian)
        bash installers/apt.sh
        ;;
    fedora)
        bash installers/dnf.sh
        ;;
    *)
        error "Unsupported distro: $ID"
        ;;
esac