#! /bin/bash
set -o pipefail

dotnet test Taw.slnx --nologo --no-restore --no-build  -c Release --artifacts-path ./release