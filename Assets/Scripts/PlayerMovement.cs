using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Visuals")]
    public GameObject model;
    public float rotatingSpeed = 2f;
    public float throwingSpeed = 5f;
    public int bombAmount = 3;
    public int arrowAmount = 15;

    [Header("Health")]
    public float health = 5;

    [Header("Movement")]
    public float jumpingForce = 50f;
    private bool canJump = false;
    public int jumpingVelocity;
    public float movingVelocity;
    private Quaternion targetModelRotation;
    public float knockBackForce = 5;
    public float knockBackTimer;

    [Header("Equipment")]
    public Sword sword;
    public GameObject bombPrefab;
    public Bow bow;


    private Rigidbody playerRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        targetModelRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        model.transform.rotation = Quaternion.Lerp(model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);

        if(knockBackTimer > 0)
        {
            knockBackTimer -= Time.deltaTime;

        }
        else
        {
            processInput();
        }
    }

    public void processInput()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0 ,  GetComponent<Rigidbody>().velocity.y , 0);

        if(Input.GetKey("right"))
        {

            playerRigidBody.velocity = new Vector3(-movingVelocity , 
                                        playerRigidBody.velocity.y,
                                        playerRigidBody.velocity.z);

            targetModelRotation = Quaternion.Euler(0, 270 , 0);

        }

        if (Input.GetKey("left"))
        {
            playerRigidBody.velocity = new Vector3(movingVelocity , 
                                        playerRigidBody.velocity.y,
                                        playerRigidBody.velocity.z);

            targetModelRotation = Quaternion.Euler(0, 90 , 0);
        }

        if (Input.GetKey("up"))
        {
            playerRigidBody.velocity = new Vector3(
                                        playerRigidBody.velocity.x ,

                                        playerRigidBody.velocity.y ,
                                        
                                        -movingVelocity

                                        );

            targetModelRotation = Quaternion.Euler(0, 180 , 0);
        }

        if (Input.GetKey("down"))
        {
             playerRigidBody.velocity = new Vector3(
                                        playerRigidBody.velocity.x ,

                                        playerRigidBody.velocity.y ,
                                        
                                        movingVelocity

                                        );
            targetModelRotation = Quaternion.Euler(0, 0 , 0);
        }

        if (Input.GetKey("space") && canJump)
        {
            canJump = false;
            playerRigidBody.AddForce(0 , jumpingForce , 0);
            
            playerRigidBody.velocity = new Vector3(

                playerRigidBody.velocity.x ,
                 jumpingVelocity, 
                 playerRigidBody.velocity.z
            );
        }

        //sword attack 
        if(Input.GetKeyDown("q"))
        {
            sword.gameObject.SetActive(true);
            bow.gameObject.SetActive(false);
            sword.Attack();
        }

        if(Input.GetKeyDown("x"))
        {
            ThrowBomb();
        }

        if(Input.GetKeyDown("b"))
        {
            if(arrowAmount > 0)
            {
                sword.gameObject.SetActive(false);
                bow.gameObject.SetActive(true);
                bow.Attack();
            }
            else
            {
                return;
            }
            arrowAmount--;
           
        }
    }

    private void ThrowBomb()
    {
       

        if(bombAmount <= 0)
        {
            return;
        }

        GameObject bombObj = Instantiate(bombPrefab);

        bombObj.transform.position = transform.position + model.transform.forward;

        Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;

        bombObj.GetComponent<Rigidbody>().AddForce(throwingDirection * throwingSpeed);

        bombAmount--;
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.name == "Ground")
        {
            Debug.Log("Hit the floor..");
            canJump = true;
        }

        if(col.gameObject.GetComponent<Enemy>() != null)
        {
            Hit((transform.position - col.transform.position).normalized);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyBullet>() != null)
        {
            Hit((transform.position - other.transform.position).normalized);
        }
        Debug.Log(other.name);    
    }

    private void Hit(Vector3 direction)
    {
        Vector3 knockBackDirection = (direction + Vector3.up).normalized;

        playerRigidBody.AddForce(knockBackDirection * knockBackForce);

        knockBackTimer = 1f;

        health--;

        if(health <= 0)
        {
            Destroy(gameObject);
        }

    }
}
