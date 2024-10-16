using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{

    public GameObject target;
    public Vector3 offset;
    public float focusSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, Time.deltaTime * focusSpeed);
        }
    }
}
