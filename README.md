# Server for ViveTracker based Avatar Synchronization using Mirror

The repository contains a submodule shared with the [client](https://github.com/CarouselDancing/vive-sync-client) and needs to be cloned recursively:

```

git clone git@github.com:CarouselDancing/vive-sync-server.git --recursive

```

The project requires Unity version 2021.3.5f1.

Before opening the project you need to download the data using the PowerShell script /Assets/Resources/download_data.ps1

## Starting the Server in the Editor

1. Open the scene Assets/Scenes/Start.unity
2. Press Play
3. Press on Server


## Starting a test client in the Editor
CAUTION : this is the procedure for starting a test client from the server module.

For spawning an avatar in the client, please refer to the procedure described in the vive-sync-client readme. 

The test client will spawn an avatar in the default pose in the scene.

1. Open the scene Assets/BaselineAgent/Scenes/main.unity
2. Set the target IP in the NetworkManager
3. Press Play
4. Press on Join


