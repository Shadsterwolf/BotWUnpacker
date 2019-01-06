<p align="center"> 
<img src="https://github.com/Shadsterwolf/BotWUnpacker/blob/master/BotWUnpacker/images/ZeldaUnpackerLogo.png"/>
</p>

# Features
- General Unpack, Build, Decode, and Encode <br />
- Auto Decode Unpacked Yaz0 Files <br />
- Mass Unpack all SARC Files in one go to seperate folders or compile all to one <br />
- Smart extension handling <b>(.sbactorpack <-> .bactorpack)</b> when decoding/encoding <br />
- Build and Compare tool (To better align special size and padding rules) <br />
- Padding Editor tool (for special rare cases) <br />

# Drag and Drop
- Supports multiple/single files and folders, from the same source location! <br />
- Automatically decodes, upon detecting Yaz0 file(s) (will overwrite existing) <br />
- Automatically encodes, upon detecting SARC files(s) <b>AND</b> detect existing decoded folder(s) (will overwrite existing) <br />
- Automatically unpacks, upon detecting SARC file(s) (will <b>NOT</B> overwrite existing) <br />
- Automatically builds, upon detecting folder(s) (will overwrite existing, adds ".pack") <br />

# Console
- Decode <br />
  ```
  /d <Input File> [Output File]
  ```
- Encode <br />
  ```
  /e <Input File> [Output File]
  ```
- Unpack <br />
  ```
  /u <Input File> [Output Folder]
  ``` 
- Build
  ```
  /b <Input Folder> [Output File]
  ``` 
  ```
  Examples:
  BotwUnpacker.exe /d "C:\OrignalFiles\Model.sbactorpack" "C:\CustomFiles\LinkModel\Model.bactorpack"
  BotwUnpacker.exe /u "C:\CustomFiles\LinkModel\Model.bactorpack"
  BotwUnpacker.exe /b "C:\CustomFiles\LinkModel\Model" "C:\CustomFiles\Model.bactorpack"
  ```
# Solution Build
1. Open the .sln project in Visual Studio
2. Open Tools > NuGet Package Manager > Manage NuGet Packages for Solution...
3. Search and Install the following:
- Fody & Costura (This is to compile any .DLL files into the EXE)
- Microsoft.WindowsAPICodePack-Core  (Common .DLL library, used make folder select operations similar to file select)
- Microsoft.WindowsAPICodePack-Shell

# Credits
Made by Shadsterwolf <br />
Uwizard code SARC.cs heavily modified (and mostly commented!) <br />
Decode based off of thakis's and shevious's code, recoded in C# <br />
Encode was re-researched and programmed by myself!

