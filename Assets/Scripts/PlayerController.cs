using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float Speed = 5.0f;
    
    
    // Update is called once per frame
    [SerializeField]
    Rigidbody2D rigidbody;

    [SerializeField]
    private Animator _animator;

    private static readonly int XSpeed = Animator.StringToHash("x_speed");
    private static readonly int YSpeed = Animator.StringToHash("y_speed");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        
        rigidbody = GetComponent<Rigidbody2D>();

        if (_animator == null)
            _animator = FindObjectOfType<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Debug.Log(x);
        Vector3 oldPosition = transform.position;

        rigidbody.MovePosition(oldPosition + new Vector3(x, y, 0) * (Speed * Time.deltaTime));
        
        SetAnimationValues(x, y);
       
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


       // gameObject.transform.localRotation = Quaternion.Euler(0.0f, x > 0 ? 0.0f : 180.0f, 0f);
        
        
        
      
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
}
