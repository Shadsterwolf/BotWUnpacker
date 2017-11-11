<p align="center"> 
<img src="https://github.com/Shadsterwolf/BotWUnpacker/blob/master/BotWUnpacker/images/ZeldaUnpackerLogo.png"/>
</p>

# Change Log
<b>Version 1.1:</b> <br />
-Added support to adjust files that need a fixed data offset (add padding) <br />
-Various tweaks for user experience.


# BotW Unpacker
<b>Purpose:</b> <br />
-Extract and Build PACK/SARC files in a convenient enviorment

<b>Operation:</b> <br />
-Set a default path of your workspace to make your life easier <br />
-Extract PACK files (BotW origin, SARC big endian) <br />
-ReBuild PACK files (BotW origin, SARC big endian)

<b>Features:</b> <br />
-Build with a fixed data start offset (Add padding) <br />
-Detects if extract file is Yaz0 encoded, it lets the user know to extract it first <br />
-Detects if extract file header is unsupported, it shows the user what it actually is

<b>Helpful Tips:</b> <br />
-To ensure perfect stability, please make sure the file you modify was encoded/decoded before repacking!

<b>Future plans:</b> <br />
-Add file comparison which will detail the differences of the two files if there are any. <br />
-Add Yaz0 decoding and encoding support
