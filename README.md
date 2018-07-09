<p align="center"> 
<img src="https://github.com/Shadsterwolf/BotWUnpacker/blob/master/BotWUnpacker/images/ZeldaUnpackerLogo.png"/>
</p>

# UI Features:
-General Unpack, Build, Decode, and Encode <br />
-Auto Decode Unpacked Files Decode <br />
-Mass Unpack all SARC Files in one go to seperate folders or compile all to one <br />
-Smart extension handling <b>(.sbactorpack <-> .bactorpack)</b> <br />
-Build and Compare tool (To better align special size and padding rules) <br />
-Padding Editor tool (for special rare cases) <br />

# Drag and Drop
-Supports multiple/single files and folders, from the same source location! <br />
-Automatically decodes, upon detecting Yaz0 file(s) (will overwrite existing) <br />
-Automatically encodes, upon detecting the decoded folder(s) already exists <b>AND</b> is a SARC package (will overwrite existing) <br />
-Automatically unpacks, upon detecting SARC file(s) (will <b>NOT</B> overwrite existing) <br />
-Automatically builds, upon detecting folder(s) (will overwrite existing, adds ".pack") <br />

# Console
-Decode <br />
  /d <Input File> [Output File] <br />
-Encode <br />
  /e <Input File> [Output File] <br />
-Unpack <br />
  /u <Input File> [Output Folder] <br />
-Build
  /b <Input Folder> [Output File] <br />
-For more details/examples <br />
  /?

# Credits
Made by Shadsterwolf <br />
Uwizard code SARC.cs heavily modified (and mostly commented!) <br />
Decode based off of thakis's and shevious's code, recoded in C# <br />
Encode was re-researched and programmed by myself!

