using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

public class SpawnController : MonoBehaviour
{
    private PlayerController _player;

    private Vector3 _spawnPoint;

    private string currentScene;
    // Start is called before the first frame update
    void Start()
    {
        _spawnPoint = transform.position;

       currentScene = SceneManager.GetActiveScene().name;
       
       
       //DontDestroyOnLoad(gameObject);
    }


    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SaveGame()
    {


        if (_player == null)
           _player = FindObjectOfType<PlayerController>();
        
        
        Debug.Log("Saved Game : " + currentScene + ", " + _player.gameObject.transform.position.x + ','
                  + _player.gameObject.transform.position.y + ", Location : " + Application.persistentDataPath);
        
        
        string saveFilePath = Application.persistentDataPath + "/save-game.sav";

        
        //create new file / streamwriter at file path location
        StreamWriter streamWriter = new StreamWriter(saveFilePath);
        
            streamWriter.WriteLine(currentScene + ',' + _player.gameObject.transform.position.x + ','
                                   + _player.gameObject.transform.position.y);
        
            
        

        streamWriter.Close();
    }

    public void LoadGame()
    {
        string saveFilePath = Application.persistentDataPath + "/save-game.sav";
        
        if (!File.Exists(saveFilePath))
        {
            Debug.Log("No save game found");
            return;
        }

        StreamReader streamReader = new StreamReader(saveFilePath);

        string line = "";

        while ((line = streamReader.ReadLine()) != null)
        {
            string[] stats = line.Split(',');
            
            Debug.Log("Loading...");
            SceneManager.LoadScene(stats[0]);
            
        }
        
        
        streamReader.Close();
    }
}
