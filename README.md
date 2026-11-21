# Taw

Taw (Tav) is a library designed to parse and analyze Biblical Hebrew using functional programming concepts. It is named after the last Hebrew letter because naming things is hard ;), but the `w` is pronounced as a `v`.

This is largely a personal project to improve my understanding of Hebrew, by combining it with my love for functional programming. That said don't expect perfection or timely response as I am currently working full time will attending graduate school and raising teenagers with my amazing wife.

## Build or Test

Most of what you will need can be found in `./scripts`, but be sure to read through the prerequisites first. These will not be installed by any of these scripts. If you have worked with F# before then you will feel right at home!

### Prerequisites

Taw is built on the .NET platform with some standard F# defaults. Please ensure you have the .NET 10 SDK installed before running this locally.

### Build

The build script is written in plain bash to keep things simple. This will build the solution for production and put it in the provided output directory.

``` bash
./scripts/build output_dir
```

If `output_dir` is missing then it will default to `./output`.

### Lint

The lint script ensures that the code is correctly formatted and meets all static code standards. Before the solution is built for production it must pass linting.

``` bash
./scripts/lint auto_fix
```

Passing `auto_fix` will fix any errors that can be fixed automatically. Without it the lint script will simple fail with a list of errors.

### Test

The test script builds and runs all tests for the solution and outputs a simple code coverage report.

``` bash
./scripts/test output_dir
```

If no `output_dir` is set the coverage will just echo to the console.

### Package & Tool Restore

A few tools are required to build the solution these are all captured in the tool-manifest or mentioned as prerequisites.

``` bash
./scripts/restore only_packages
```

If you include `only_packages` then local tools will not be installed by default.

### Pipeline

The full pipeline can be run locally with:

``` bash
./scripts/pipeline output_dir
```

This will by default fail on any linting error and restore all local tools.

## Contributions

At this time I am not taking any contributions to the project. The activity is largely to grapple with the beauty and complexity of Biblical Hebrew for my own growth in both the F# and Hebrew.

## License and Usage

I have not decided on licensing yet, but if you choose to use any of this software it is at your own risk. No promises or grantees are made at this time around correctness or completeness. However, it is my express desire that if any of this solution is used for in a commercial product a Pull Request be opened with a request for usage added. Once the Pull Request is approved it may be used.

All request for usage should be included in `./request_for_usage` and follow the template `./request_for_usage/template.md`. If you have any questions about this please open an issue.

This section (License and Usage) will be superseded once a license is selected.
