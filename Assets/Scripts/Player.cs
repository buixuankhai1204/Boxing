using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public LayerMask groundMask;
    public Rigidbody rigidbodyPlayer;
    private Animator animatorPlayer;
    private bool isGround = true;
    [SerializeField]
    private float speed = 3f;
    private float runSpeed = 5f;

    private void Start()
    {
        rigidbodyPlayer = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
    }

    public void Update()
    {
        Movement();
    }

    public void Movement()
    {
        CheckCollision();
        Jumb();
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
           animatorPlayer.SetBool(Tags.canRun, true);
           animatorPlayer.SetBool(Tags.canWalk, false);
           rigidbodyPlayer.transform.Translate(new Vector3(horizontal, 0, vertical) * runSpeed * Time.deltaTime, Space.Self);
        }
        else
        {
            animatorPlayer.SetBool(Tags.canRun, false);
            animatorPlayer.SetBool(Tags.canWalk, true);
            rigidbodyPlayer.transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime, Space.Self);
        }

        if (horizontal == 0 && vertical == 0)
        {
            animatorPlayer.SetBool(Tags.canRun, false);
            animatorPlayer.SetBool(Tags.canWalk, false);
        }
    }

    public void Jumb()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("asd");
                rigidbodyPlayer.AddForce(Vector3.up * 2,ForceMode.Force );
                isGround = false;
            }
        }
    }

    public void CheckCollision()
    {
        if(Physics.Raycast(rigidbodyPlayer.transform.position, Vector3.down, 2f, groundMask))
        {
            isGround = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("asdad");
    }
}
