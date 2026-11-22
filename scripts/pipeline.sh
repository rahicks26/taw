#! /bin/bash
set -o pipefail

./scripts/restore.sh && ./scripts/lint.sh && ./scripts/build.sh && ./scripts/test.sh