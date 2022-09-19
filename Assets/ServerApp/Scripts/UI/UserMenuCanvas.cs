using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMenuCanvas : MonoBehaviour
{
  
  public Canvas canvas;
  public static UserMenuCanvas Instance;
  public GameObject canvasKeyboard;
  public bool active = true;

  
  void Awake(){
      
      if(Instance == null){
          Instance = this;
      }else{
          GameObject.DestroyImmediate(gameObject); //singleton monobehavior
      }
  }  
  
  
  public void Toggle(){
        if(!active)return;
        if (!canvas.enabled){
            Show();
        }else{
            Hide();
        }
    }

    public void Show(){
        canvas.enabled = true;
    }

    public void Hide(){
        canvas.enabled = false;

    }

    
    public void Activate(){
        active = true;
    }   
    
    public void Deactivate(){
        Hide();
        active = false;
    }
}
