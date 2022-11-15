using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
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



    [SerializeField] private Transform _scrollParent;

    [SerializeField] private GameObject _scrollObject;
    
    private SpawnController _spawnController;


    private TextMeshProUGUI[] _scrollList;
    private void Start()
    {
        _spawnController = FindObjectOfType<SpawnController>();

        if (_spawnController == null)
            _spawnController = gameObject.AddComponent<SpawnController>();


       _scrollList = _scrollParent.GetComponentsInChildren<TextMeshProUGUI>();
        
        
    }


    public void ContinueGame()
    {
        _spawnController.LoadGame();
    }
    public void LoadOverworld()
    {
        SceneManager.LoadScene("OverWorld_Scene");

      
        
    }


    public void LoadGameWindow()
    {
        if(!_scrollObject.gameObject.activeInHierarchy)
        _scrollObject.gameObject.SetActive(true);
        else
        {
            _scrollObject.gameObject.SetActive(false);
        }

        string filePath = Application.persistentDataPath;
        var info = new DirectoryInfo(filePath);
        var fileInfo = info.GetFiles();
        
        int i;
        
        for (i = 0; i < _scrollList.Length; i++)
        {
            for (; i < fileInfo.Length; )
            {
                _scrollList[i].text = fileInfo[i].ToString();
                break;
            }
        }
    }

    public void Quit()
    {
        if(Editor) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }


   
}
