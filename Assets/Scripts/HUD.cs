using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    
#if UNITY_EDITOR
    private bool Editor = true;
#else
    private bool Editor = false;
#endif
    

    public void LoadOverworld()
    {
        SceneManager.LoadScene("OverWorld_Scene");

      
        
    }

    public void Quit()
    {
        if(Editor) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }


   
}
