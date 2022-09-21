# Server for ViveTracker based Avatar Synchronization using Mirror

The repository contains a submodule shared with the [client](https://github.com/CarouselDancing/vive-sync-client) and needs to be cloned recursively:

```

git clone git@github.com:CarouselDancing/vive-sync-server.git --recursive

```

The project requires Unity version 2021.3.5f1.

Before opening the project you need to download the data using the PowerShell scripts /Assets/Resources/download_data.ps1 and /Assets/StreamingAssets/download_assets.ps1

## Starting the Server in the Editor

1. Open the scene Assets\ServerApp\Scenes\Start.unity and press on play
3. The application will start in a menu scene with the options Start a Server, Join as Observer, Open Settings or Exit.
   1. Settings: Change the protocol (KCP or Telepathy). 
   2. Server: Start a server using the selected protocol. Other players can see it now in the server list.
   3. Observe: Display the server list. Double click a server to join as observer. The protocol is automatically adjusted based on the server
4. In the main scene the you can control the mouse via right click (translation) and left click (rotation).





