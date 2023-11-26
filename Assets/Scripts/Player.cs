using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{

    [SerializeField] float speed = 1f ;

    private Rigidbody rb;

    [SerializeField] private Camera _camera; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        HideAndLockCursor();
        
    }
    private void HideAndLockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 horizontalDirection = horizontal * _camera.transform.right;
        Vector3 verticalDirection = vertical * _camera.transform.forward;
        horizontalDirection.y = 0f;
        verticalDirection.y = 0f;


        Vector3 movementDirection = horizontalDirection + verticalDirection;

        rb.velocity = movementDirection * speed * Time.fixedDeltaTime;

        
    }
}
