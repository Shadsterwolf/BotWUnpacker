<p align="center"> 
<img src="https://github.com/Shadsterwolf/BotWUnpacker/blob/master/BotWUnpacker/images/ZeldaUnpackerLogo.png"/>
</p>

# Change Log
<b>Version 1.1:</b> <br />
-Added support to adjust files that need a fixed data offset (add padding) <br />
-Various tweaks for user experience.

<b>Version 1.2:</b> <br />
-Added Extract All to mass extract SARC types (does not support Yaz0 yet)<br />
-Added simple Open Root Folder button<br />
-Added placeholder button for Compare and Build Pack

<b>Version 1.3:</b> <br />
-Added Auto Yaz0 Decoding!<br />
-Cleaned up unused code and adjusted for readability

<b>Version 1.4:</b> <br />
-Added Compile All to Extract All process to combine common folders from other SARC sources

<b>Version 1.5:</b> <br />
-Bugfix Auto Yaz0 decode first pass for encoded SARC data, falsely handling inital Yaz0 encoded files while checked on <br />

<b>Version 1.6:</b> <br />
-Added Yaz0 Encoding! Which currently abides by most rules for Nintendo's algorithem.<br />

# BotW Unpacker
<b>Purpose:</b> <br />
-Extract and Build PACK/SARC files in a convenient environment

<b>Operation:</b> <br />
-Set a default path of your workspace to make your life easier <br />
-Extract PACK files (BotW origin, SARC big endian) <br />
-ReBuild PACK files (BotW origin, SARC big endian)

<b>Features:</b> <br />
-General Extract, Build, Decode, and Encode
-Auto Yaz0 Decode <br />
-Extract all Pack Files in one go to seperate folders or compile all to one <br />
-Build with a fixed data start offset (Add padding) <br />

<b>Helpful Tips:</b> <br />
-To ensure perfect stability, please make sure the file you modify was encoded/decoded before repacking!

<b>Future plans:</b> <br />
-Add file comparison which will detail the differences of the two files if there are any. <br />

# Credits
Made by Shadsterwolf <br />
Uwizard code SARC.cs heavily modified (and mostly commented!) <br />
Decode based off of thakis's and shevious's code, recoded in C# <br />
Encode was re-researched and programmed by myself!

