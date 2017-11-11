# Change Log
Version 1.1: <br />
-Added support to adjust files that need a fixed data offset (add padding) <br />
-Various tweaks for user experience.


# BotW Unpacker
Zelda : Breath of the Wild Unpacker tool. Extract and Build pack files, heavily modified code from UWizard SARC

Purpose: <br />
-Extract and Build PACK/SARC files

Operation: <br />
-Set a default path of your workspace to make your life easier <br />
-Extract PACK files (BotW origin, SARC big endian) <br />
-ReBuild PACK files (BotW origin, SARC big endian)

Features: <br />
-Build with a fixed data start offset (Add padding) <br />
-Detects if extract file is Yaz0 encoded, it lets the user know to extract it first <br />
-Detects if extract file header is unsupported, it shows the user what it actually is

Helpful Tips: <br />
-To ensure perfect stability, please make sure the file you modify was encoded/decoded before repacking!

Future plans: <br />
-Add file comparison which will detail the differences of the two files if there are any. <br />
-Add Yaz0 decoding and encoding support
