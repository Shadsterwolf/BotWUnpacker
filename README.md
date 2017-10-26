# BotWUnpacker
Zelda : Breath of the Wild Unpacker tool. Extract and Build pack files, heavily modified code from UWizard SARC

Purpose: 
-Extract and Build PACK/SARC files better than UWizard 

Operation: 
-Set a default path of your workspace to make your life easier
-Extract PACK files (BotW origin, SARC big endian) 
-ReBuild PACK files (BotW origin, SARC big endian)

Features:
-Detects if extract file is Yaz0 encoded, it lets the user know to extract it first
-Detects if extract file header is unsupported, it shows the user what it actually is

Helpful Tips:
-To ensure perfect stability, please make sure if the file you modify was encoded/decoded before repacking!

Future plans:
-Add file comparison which will detail the differences of the two files if there are any.
-Add auto Yaz0 decompress & recompress
