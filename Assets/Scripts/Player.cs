using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField] float speed = 1f ;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private Camera _camera;


    private Coroutine powerUPCoroutine;
    private Rigidbody rb;

    public void PickPowerUp()
    {
        if(powerUPCoroutine != null)
        {
            StopCoroutine(powerUPCoroutine);
        }
        powerUPCoroutine = StartCoroutine(StartPowerUP());
    }

    private IEnumerator StartPowerUP()
    {
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }
        yield return new WaitForSeconds(powerUpDuration);
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }


    }

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
    void Update()
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
