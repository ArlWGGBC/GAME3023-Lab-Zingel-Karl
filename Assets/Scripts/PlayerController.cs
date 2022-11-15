using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float Speed = 5.0f;
    
    
    // Update is called once per frame
    [SerializeField]
    Rigidbody2D rigidbody;
    //Reference to animator.
    [SerializeField]
    private Animator _animator;
    
    //Animator values
    private static readonly int XSpeed = Animator.StringToHash("x_speed");
    private static readonly int YSpeed = Animator.StringToHash("y_speed");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    
    //Save/Load Spawn controller reference
    private SpawnController _sController;
    

    // Start is called before the first frame update
    void Start()
    {

        //Initialize components
        rigidbody = GetComponent<Rigidbody2D>();
        _sController = FindObjectOfType<SpawnController>();

        if (_animator == null)
            _animator = FindObjectOfType<Animator>();
        
        
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        
        Vector3 oldPosition = transform.position;

        rigidbody.MovePosition(oldPosition + new Vector3(x, y, 0) * (Speed * Time.deltaTime));
        
        SetAnimationValues(x, y);
        
        
        
        if(Input.GetKeyDown(KeyCode.X))
            Save();
            
       
    }


    private void Save()
    {
        if (_sController == null)
           _sController = FindObjectOfType<SpawnController>();
        
        
         _sController.SaveGame();
    }
    private void SetAnimationValues(float x, float y)
    {
        
        
        _animator.SetFloat(XSpeed, x);
        _animator.SetFloat(YSpeed, y);

        if (isEqual(x, y))
        {
            _animator.SetBool(IsMoving, false);
        }
        else
        {
            _animator.SetBool(IsMoving, true);
        }




    }


    private void Spawn()
    {
        
        string saveFilePath = Application.persistentDataPath + "/save-game.sav";
        
        if (!File.Exists(saveFilePath))
        {
            gameObject.transform.position = FindObjectOfType<SpawnController>().transform.position;
            Debug.Log("No save game found");
            return;
        }

        StreamReader streamReader = new StreamReader(saveFilePath);

        string line = "";

        while ((line = streamReader.ReadLine()) != null)
        {
            string[] stats = line.Split(',');

            Debug.Log(stats[1] + stats[2]);
            SetPosition(float.Parse(stats[1],
                System.Globalization.CultureInfo.InvariantCulture), float.Parse(stats[2],
                System.Globalization.CultureInfo.InvariantCulture));
        }
        
        
        streamReader.Close();
    }
    private bool isEqual(float x, float y)
    {
        float margin = 0.1f;

        return math.abs(x - y) <= margin * math.abs(x);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player Triggered with: " + collision.gameObject.name);
    }


    public void SetPosition(float x, float y)
    {
        rigidbody.MovePosition(new Vector2(x, y));
        
    }
}
