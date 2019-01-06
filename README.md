<p align="center"> 
<img src="https://github.com/Shadsterwolf/BotWUnpacker/blob/master/BotWUnpacker/images/ZeldaUnpackerLogo.png"/>
</p>

# Intro
- <b>This is a tool for both NEW and PRO modders for Zelda: Breath of the Wild</b>
- <b>SARC</b> is a collection of files(nodes), usually in the form of ".pack" files for this game.
- <b>Yaz0</b> is a compression algotithem, usually containing an ".s" at the beginning of the extension.
- You can easily read these file's headers using HxD, which is a free hex editor.
- This modding tool can help you take apart those files and put them back together to play and test!
- As for what to do with those files is up to you! Do research, look up a guide, join a community, and have fun with it!

# Features
- Unpack & Build SARC files! <br />
- Decode & Encode Yaz0 files! <br />
- Smart extension handling <b>(.sbactorpack <-> .bactorpack)</b> when decoding/encoding <br />
- Mass Unpack all SARC files <br />
- Auto Decode Unpacked Yaz0 Files <br />
- <b>Compare Tool</b> to check your SARC nodes from original to your custom ones!<br />
- <b>Padding Tool</b> to sync your SARC nodes for those very special files!<br />

# Drag and Drop
- Drag a SARC or Yaz0 file over the .exe and watch the magic happen!
- Supports multiple files and folders, from the same source location where you dropped it from ! <br />
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
- Fody & Costura (This is to compile any .DLL files into the EXE, sometimes causes build errors at random, just retry)
- Microsoft.WindowsAPICodePack-Core  (Common .DLL library, used make folder select operations similar to file select)
- Microsoft.WindowsAPICodePack-Shell

# Credits
Made by Shadsterwolf <br />
Uwizard code SARC.cs heavily modified (and mostly commented!) <br />
Decode based off of thakis's and shevious's python code, recoded in C# <br />
Encode was re-researched and programmed by myself!

