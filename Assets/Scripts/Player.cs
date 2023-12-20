using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class Player : MonoBehaviour
{
    public Action OnPowerUpStart;
    public Action OnPowerUpStop;

    [SerializeField] float speed = 1f ;
    [SerializeField] private float powerUpDuration;
    [SerializeField] private Camera _camera;
    [SerializeField] private int health;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Transform respawnPoint;


    private Coroutine powerUPCoroutine;
    private Rigidbody rb;
    private bool isPowerupActive = false;

    public void dead()
    {
        health -= 1;
        if(health > 0)
        {
            transform.position = respawnPoint.position;
        }
        else
        {
            health = 0;
            Debug.Log("Lose");
        }
        UpdateUI();
    }

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
        isPowerupActive = true;
        
        if (OnPowerUpStart != null)
        {
            OnPowerUpStart();
        }

        yield return new WaitForSeconds(powerUpDuration);
        
        isPowerupActive = false;
        
        if (OnPowerUpStop != null)
        {
            OnPowerUpStop();
        }


    }

    private void Awake()
    {
        UpdateUI();
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

    private void OnCollisionEnter(Collision collision)
    {
        if (isPowerupActive)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<Enemy>().Dead();
            }
        }
    }

    private void UpdateUI()
    {
        healthText.text = "Health : " + health;
    }

}
