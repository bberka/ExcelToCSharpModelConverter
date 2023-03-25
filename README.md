# XSharp
A simple tool to extract CSharp models out of Excel files using EPPlus and read Excel files with those models.


XSharp library projects aims to make it easy to read and write data to excel with common C# models.

## How to use ?
- First you must create a simple console application referencing Core and Shared library projects.
- Put your excel files into single directory and use exporter to export C# Models out of Excel files. (This may take some time)
- XSharp is highly customizable you can create custom validator inherits from XValidator. You can make it skip some files, rows, header values and more.
- Once extraction is done it will export .cs files to working directory inside "Output" folder.
- You can now add those models to your own Excel editor project and use those models to read Excel files easily.
