using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public static MenuController Instance;
    MirrorGameManager manager;
    public UserMenuCanvas ingameMenu;
    public LobbyMenuCanvas lobbyMenu;


    void Awake(){
        
        if(Instance == null){
            Instance = this;
        }else{
            GameObject.DestroyImmediate(gameObject); //singleton monobehavior
        }
    }

    public void Start(){
        ingameMenu = UserMenuCanvas.Instance;
        manager = MirrorGameManager.Instance;
        manager.onStop.AddListener(OnStopMirror);
    }

 
    public void JoinServer(string url, string protocol="", int port=-1)
    {
        lobbyMenu.Hide();
        ingameMenu.Activate();
        manager.JoinServer(url, protocol, port);
    }

    public void StartServer(){
        lobbyMenu.Hide();
        ingameMenu.Activate();
        manager.StartServer();
    }   
   public void Host(){
        lobbyMenu.Hide();
        ingameMenu.Activate();
        manager.HostServer();
    }   
    
     public void ExitGame(){
        manager.ExitGame();
    }

     public void OnStopMirror(){
        lobbyMenu.Show();
        ingameMenu.Deactivate();
    }
}
