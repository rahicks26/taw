#! /bin/bash
set -o pipefail

dotnet build Taw.slnx --nologo --no-restore --artifacts-path ./release -c Release