using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float Speed = 5.0f;
    
    
    // Update is called once per frame
    [SerializeField]
    Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello World");
        
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 oldPosition = transform.position;

        rigidbody.MovePosition(oldPosition + new Vector3(x, y, 0) * (Speed * Time.deltaTime));
        // transform.position = oldPosition + new Vector3(xInput, yInput, 0) * MoveSpeed * Time.deltaTime;
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
