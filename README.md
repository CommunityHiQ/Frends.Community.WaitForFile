**[Table of Contents](http://tableofcontent.eu)**
- [Frends.Community.Files.WaitForFile](#frendscommunitywaitforfile)
  - [Contributing](#contributing)
  - [Documentation](#documentation)
    - [Parameters](#parameters)
    - [Options](#options)
    - [Result](#result)
  - [License](#license)
  - [Change Log](#change-log)


# Frends.Community.WaitForFile
FRENDS Task that waits if file appears into specific folder.

## Contributing
When contributing to this repository, please first discuss the change you wish to make via issue, email, or any other method with the owners of this repository before making a change.

1. Fork the repo on GitHub
2. Clone the project to your own machine
3. Commit changes to your own branch
4. Push your work back up to your fork
5. Submit a Pull request so that we can review your changes

NOTE: Be sure to merge the latest from "upstream" before making a pull request!

## Documentation

### Parameters

| Property            |  Type   | Description								| Example                     |
|---------------------|---------|-------------------------------------------|-----------------------------|
| FolderPath		  | string	| File path | ´c:\temp\´ |
| FileMask			  | string	| File mask | ´test.*´ |
| TimeoutMS			  | int		| Timeout in milliseconds					| 2000 |
| ContinueIfExists    | bool	| Continue without delay if file exisist	| true |

### Result

| Property        | Type     | Description                      |
|-----------------|----------|----------------------------------|
| FilePath        | string   | Path of the file with activity. Empty if no file activity.|
| FileExists      | bool     | Returns true if input files exist and ContinueIfExists is 'true'|

## License

This project is licensed under the MIT License - see the LICENSE file for details

# Change Log

| Version | Changes |
| ------- | ------- |
| 1.0.0   | First version. |
| 1.1.0   | TimeoutMS will be now showed as int in FRENDS UI. |