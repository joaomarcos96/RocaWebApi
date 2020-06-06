#!/bin/sh

# Executes the tests, collecting Coverage,
# outputting them in the TestResults folder with OpenCover format
# and excluding the Migrations folder
dotnet test \
    /p:CollectCoverage=true \
    /p:CoverletOutput=TestResults/ \
    /p:CoverletOutputFormat=opencover \
    /p:ExcludeByFile='**/Migrations/'

# Generates the Coverage report in HTML using the ReportGenerator tool
reportgenerator \
    -reports:TestResults/coverage.opencover.xml \
    -targetdir:TestResults/CoverageReport \
    -reporttypes:"Html;HtmlSummary"