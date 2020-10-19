# Frends.Community.WaitForFile

frends Community Task for WaitFile

[![Actions Status](https://github.com/CommunityHiQ/Frends.Community.WaitForFile/workflows/PackAndPushAfterMerge/badge.svg)](https://github.com/CommunityHiQ/Frends.Community.WaitForFile/actions) ![MyGet](https://img.shields.io/myget/frends-community/v/Frends.Community.WaitForFile) [![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) 

- [Installing](#installing)
- [Tasks](#tasks)
     - [WaitFile](#WaitFile)
- [Building](#building)
- [Contributing](#contributing)
- [Change Log](#change-log)

# Installing

You can install the Task via frends UI Task View or you can find the NuGet package from the following NuGet feed
https://www.myget.org/F/frends-community/api/v3/index.json and in Gallery view in MyGet https://www.myget.org/feed/frends-community/package/nuget/Frends.Community.WaitForFile

# Tasks

## WaitFile

FRENDS Task that waits if file appears into specific folder.

### Parameters

| Property            |  Type   | Description								| Example                     |
|---------------------|---------|-------------------------------------------|-----------------------------|
| FolderPath		  | string	| File path | ´c:\temp\´ |
| FileMask			  | string	| File mask | ´test.*´ |
| TimeoutMS			  | int		| Timeout in milliseconds					| 2000 |
| ContinueIfExists    | bool	| Continue without delay if file exisist	| true |

### Returns

| Property        | Type     | Description                      |
|-----------------|----------|----------------------------------|
| FilePath        | string   | Path of the file with activity. Empty if no file activity.|
| FileExists      | bool     | Returns true if input files exist and ContinueIfExists is 'true'|

Usage:
To fetch result use syntax:

`#result.WaitForFileToAppear`

# Building

Clone a copy of the repository

`git clone https://github.com/CommunityHiQ/Frends.Community.WaitForFile.git`

Rebuild the project

`dotnet build`

Run tests

`dotnet test`

Create a NuGet package

`dotnet pack --configuration Release`

# Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repository on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

# Change Log


| Version | Changes |
| ------- | ------- |
| 1.0.0   | First version. |
| 1.1.0   | TimeoutMS will be now showed as int in FRENDS UI. |
| 1.1.1   | Converted to support .Net Framework 4.7.1 and .Net Standard 2.0