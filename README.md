# XSharp
A simple tool to extract CSharp models out of Excel files using EPPlus and read Excel files with those models.


XSharp library projects aims to make it easy to read and write data to excel with common C# models.

## What this projects contain ?
There are 2 seperate Class Libraries Core and Shared.

### Core

#### Contains
- Model Exporter
- File Reader and Updater
- Extension Methods
- Custom Serializers
- Excel File Parsers

### Shared
This project has no code that will read or write any file.
It only has some files that can be shared with each project to test etc.

#### Contains
- Custom attributes
- Constants
- Custom Base Excel Models
- Validators
- XKernel

## How to use ?
- First you must create a simple console application referencing Core and Shared library projects.
- Put your excel files into single directory and use exporter to export C# Models out of Excel files. (This may take some time)
- XSharp is highly customizable you can create custom validator inherits from XValidator. You can make it skip some files, rows, header values and more.
- You can also use XOption to set namespaces of created files and more.
- Once extraction is done it will export .cs files to working directory inside "Output" folder.
- You can now add those models to your own Excel editor project and use those models to read Excel files easily.
- More details can be found in Example project

## Disclaimer
This project is not commercial. It uses EPPlus NonCommercial Licence to read Excel files. If you are building a commercial application by using this project you must purchase a licence here [link](https://www.epplussoftware.com/) otherwise creator or contributors of XSharp project can not be held responsible.
