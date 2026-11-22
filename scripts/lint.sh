#! /bin/bash
set -o pipefail

dotnet fantomas check ./
dotnet fsharplint lint ./Taw.slnx --verbosity quite --nologo