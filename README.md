# BeastBoards - a Steam Friends List Leader Board for I Am Your Beast 🐻 🛹

![The end screen from a level of I Am Your Beast, with the leaderboard of times in the bottom left](https://raw.githubusercontent.com/hnegus1/beastboards/refs/heads/master/img/Screenshot1.png)

A leader board mod for I Am Your Beast. Currently Steam Friends only. Requires your friends to be using the mod.

## Installation

BeastBoard requires a Unity Mod Loader to be installed first. Both [MelonLoader 5 Mono](https://melonwiki.xyz) and [BepInEx 5 Mono](https://docs.bepinex.dev/articles/user_guide/installation/index.html) are supported. Many mods for I Am Your Beast use BepInEx so you may want to use that to ensure compatability. Make sure you are using a legally obtained Steam copy of I Am Your Beast. Other versions of the game are not supported and won't work. 

1. Download the mod from the releases section for the loader that you have used and place the contents of the download into your I Am Your Beast folder

2. Launch the game via Steam. The mod will retroactively add your best times on completion of a level. 

## Building / Project Information

Repo contains both the BeastBoards mod and the server code. The mod is a simple MelonLoader mod while the server is ASP. Requires .NET 9 to build and run.

The UI code is not here. Let me know if you want to have a look.

I’m not much of a modder, so suggestions and contributions are welcome.  

## Data

The mod has to interact with a server in order to store and display your times. This server stores your Steam Id alongside the best time for the level. This is required in order for the mod to function. It does not store any other information such as information on your Steam Profile. Any other information shown by the mod (Steam Avatar, profile name, etc) is done on the mod side only. Please reach out if you would like me to remove your times. 

## Contact

Feel free to leave an issue here if you run into any issues or have any suggestions. You can also contact me on [BlueSky](https://bsky.app/profile/videogamesarebad.co.uk).
