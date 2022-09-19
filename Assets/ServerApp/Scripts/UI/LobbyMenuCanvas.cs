using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.Events;


public enum AvatarModels{
    FEMALE = 0,
    MALE = 1
}
public enum MirrorProtocolOptions{
    KCP = 0,
    TELEPATHY = 1
}

[Serializable]
public struct SettingsElements{

    public Dropdown avatarDropdown;
    public Dropdown protocolDropdown;
    public Toggle hipTrackerToggle;
    public Toggle footTrackersToggle;
    public InputField hipPosOffset;
    public InputField hipRotOffset;
    public InputField leftFootPosOffset;
    public InputField leftFootRotOffset;
    public InputField rightFootPosOffset;
    public InputField rightFootRotOffset;

    public void Save(ref ClientConfig config){
        if(hipTrackerToggle != null)config.activateHipTracker = hipTrackerToggle.isOn;
        if(footTrackersToggle != null) config.activateFootTrackers = footTrackersToggle.isOn;
        if(hipPosOffset != null)StringToArray(hipPosOffset.text, ref config.hipTracker.posOffset, 3);
        if(hipRotOffset != null)StringToArray(hipRotOffset.text, ref config.hipTracker.rotOffset, 3);

        if(leftFootPosOffset != null)StringToArray(leftFootPosOffset.text, ref config.leftFootTracker.posOffset, 3);
        if(leftFootRotOffset != null)StringToArray(leftFootRotOffset.text, ref config.leftFootTracker.rotOffset, 3);

        if(rightFootPosOffset != null)StringToArray(rightFootPosOffset.text, ref config.rightFootTracker.posOffset, 3);
        if(rightFootRotOffset != null)StringToArray(rightFootRotOffset.text, ref config.rightFootTracker.rotOffset, 3);
        if(protocolDropdown != null){
            if (protocolDropdown.value == (int)MirrorProtocolOptions.KCP){
                config.protocol = "kcp";
            }else{
                config.protocol = "telepathy";
            }
        }
        if(avatarDropdown != null){
            if (avatarDropdown.value == (int)AvatarModels.FEMALE){
                config.userAvatar = avatarDropdown.value;
            }else{
                config.userAvatar = avatarDropdown.value;
            }
        }
    }

    public void Show(ref ClientConfig config){
        
        if(hipTrackerToggle != null)hipTrackerToggle.isOn = config.activateHipTracker;
        if(footTrackersToggle != null)footTrackersToggle.isOn = config.activateFootTrackers;
        if(hipPosOffset != null)hipPosOffset.text = ArrayToString(config.hipTracker.posOffset, 3);
        if(hipRotOffset != null)hipRotOffset.text = ArrayToString(config.hipTracker.rotOffset, 3);
        if(leftFootPosOffset != null)leftFootPosOffset.text = ArrayToString(config.leftFootTracker.posOffset, 3);
        if(leftFootRotOffset != null)leftFootRotOffset.text = ArrayToString(config.leftFootTracker.rotOffset, 3);
        if(rightFootPosOffset != null)rightFootPosOffset.text = ArrayToString(config.rightFootTracker.posOffset, 3);
        if(rightFootRotOffset != null)rightFootRotOffset.text = ArrayToString(config.rightFootTracker.rotOffset, 3);
        if(protocolDropdown != null){
            if (config.protocol  == "kcp"){
                protocolDropdown.value = (int)MirrorProtocolOptions.KCP;
            }else{
                protocolDropdown.value = (int)MirrorProtocolOptions.TELEPATHY;
            }
        }
            if(avatarDropdown != null){
            if (config.userAvatar  == 0){
                avatarDropdown.value = (int)AvatarModels.FEMALE;
            }else{
                avatarDropdown.value = (int)AvatarModels.MALE;
            }
        }

        

    }

    public string ArrayToString(float[] array, int size){
        string str = "";
        for(int i =0; i < size; i++){
            str += array[i].ToString();
            if (i < size-1) str+=", ";
        }
        return str;
    }    
    public bool StringToArray(string str, ref float[] array, int size){
        var splitStr = str.Split(",");
        List<float> floatList = new List<float>();
        if (splitStr.Length != size)return false;

        for(int i =0; i < splitStr.Length; i++){
            try{
                float f = Single.Parse(splitStr[i]);
                if (!Single.IsNaN(f)) floatList.Add(f);
            }catch{
                continue;
            }
        }
        if (floatList.Count != size)return false;
        array = floatList.ToArray();
        return true;
    }
}

public class LobbyMenuCanvas : MonoBehaviour
{
    public enum MenuState{
        MAIN,
        SERVER_LIST,
        SETTINGS
    }
    public Canvas canvas;
    public GameObject mainPanel;
    public GameObject serverListPanel; 
    public GameObject settingsPanel; 
    public GameObject contentObject;
    public MenuState state;
    public GameObject entryPrefab;

    MirrorGameManager manager;
    MenuController menuController;

    public List<GameObject> serverList;
    public SettingsElements settingsElements;

    public void Start(){
        serverList = new List<GameObject>();
        manager = MirrorGameManager.Instance;
        menuController = MenuController.Instance;
    }

    public void StartServer(){
        menuController.StartServer();
    }   

    public void Host(){
        menuController.Host();
    }   
    
     public void ExitGame(){
        menuController.ExitGame();
    }

    public void ShowServerList(){
        state = MenuState.SERVER_LIST;
        ClearServerList();
        mainPanel.SetActive(false);
        serverListPanel.SetActive(true);
        settingsPanel.SetActive(false);
        FillServerList();

    }

    public void ShowMain(){
        if (manager != null && state == MenuState.SETTINGS){
            settingsElements.Save(ref manager.config);
        }
        state = MenuState.MAIN;
        mainPanel.SetActive(true);
        serverListPanel.SetActive(false);
        settingsPanel.SetActive(false);
    }

    public void ShowSettings(){
        state = MenuState.SETTINGS;
        if(manager != null)settingsElements.Show(ref manager.config);
        mainPanel.SetActive(false);
        serverListPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void FillServerList(){
        Debug.Log("fill server list");
        manager.serverRegistry.FillServerList(HandleServerList);
    }



    public void HandleServerList(string responseText){
        if(manager.serverRegistry.defaultServerEntry != null) AddServerEntry(manager.serverRegistry.defaultServerEntry);

        JsonSerializer serializer = new JsonSerializer();
        Debug.Log(responseText);
        JsonReader reader = new JsonTextReader(new StringReader(responseText));
        var newServerEntries = serializer.Deserialize<List<ServerEntry>>(reader);
        foreach (var s in newServerEntries) {
           AddServerEntry(s);
        }
     
    }

    void AddServerEntry(ServerEntry s){
        var so = GameObject.Instantiate(entryPrefab, contentObject.transform);
        var t = so.GetComponentInChildren<Text>();
        t.text = s.protocol+":"+s.address+":"+s.port.ToString();
        var b = so.GetComponent<Button>();
        b.onClick.AddListener(() => {
            menuController.JoinServer(s.address, s.protocol, s.port);
        });
        serverList.Add(so);
    }


    void ClearServerList(){
        foreach (var s in serverList) {
            GameObject.Destroy(s);
        }
        serverList = new List<GameObject>();
    }

    public void Show(){
        canvas.enabled = true;
    }

    public void Hide(){
        canvas.enabled = false;

    }

}
