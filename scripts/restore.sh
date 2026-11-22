#! /bin/bash
set -o pipefail

dotnet tool restore
dotnet restore Taw.slnx --nologo --artifacts-path ./release 