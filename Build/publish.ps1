New-Item -ItemType Directory -Force -Path zips;

Set-Location ..\BeastBoards\;
dotnet publish -c Mod_BepInEx_RELEASE -o ../Build/publish/BepInEx;
dotnet publish -c Mod_MelonLoader_RELEASE -o ../Build/publish/MelonLoader;
$csproj = [Xml] (Get-Content .\BeastBoards.csproj)
$version = [String] $csproj.Project.PropertyGroup.AssemblyVersion
$version = $version.Trim()
Set-Location ..\Build\;
New-Item -ItemType Directory -Force -Path out/$version;

# BepInEx
New-Item -ItemType Directory -Force -Path out/$version/BepInEx;

New-Item -ItemType Directory -Force -Path "out/$version/BepInEx/I Am Your Beast_Data/StreamingAssets";
New-Item -ItemType Directory -Force -Path out/$version/BepInEx/BepInEx/plugins/BeastBoards;

Copy-Item -Path publish/BepInEx/BeastBoards.BepInEx.dll out/$version/BepInEx/BepInEx/plugins/BeastBoards -Force
Copy-Item -Path publish/BepInEx/Newtonsoft.Json.dll out/$version/BepInEx/BepInEx/plugins/BeastBoards -Force
Copy-Item -Path .\beastboardsui "out/$version/BepInEx/I Am Your Beast_Data/StreamingAssets/beastboardsui" -Force
Copy-Item -Path .\README_BEPINEX.txt out/$version/BepInEx\README.txt -Force

# MelonLoader

New-Item -ItemType Directory -Force -Path out/$version/MelonLoader;

New-Item -ItemType Directory -Force -Path "out/$version/MelonLoader/I Am Your Beast_Data/StreamingAssets";
New-Item -ItemType Directory -Force -Path out/$version/MelonLoader/Mods;
New-Item -ItemType Directory -Force -Path out/$version/MelonLoader/UserLibs;

Copy-Item -Path publish/MelonLoader/BeastBoards.MelonLoader.dll out/$version/MelonLoader/Mods -Force
Copy-Item -Path publish/MelonLoader/Newtonsoft.Json.dll out/$version/MelonLoader/UserLibs -Force
Copy-Item -Path .\beastboardsui "out/$version/MelonLoader/I Am Your Beast_Data/StreamingAssets/beastboardsui" -Force
Copy-Item -Path .\README_MELONLOADER.txt out/$version/MelonLoader\README.txt -Force


Compress-Archive -Path out\$version\BepInEx\* -DestinationPath zips\BeastBoards-$version-BepInEx.zip -Force
Compress-Archive -Path out\$version\MelonLoader\* -DestinationPath zips\BeastBoards-$version-MelonLoader.zip -Force