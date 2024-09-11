using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health = 1;
    public virtual void Hit()
    {
        Health--;

        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Sword>() != null)
        {
            if (other.GetComponent<Sword>().IsAttacking)
            {
                Hit();
            }
        }

       

        else if (other.GetComponent<Arrow>() != null)
        {
            Hit();

            Destroy(other.gameObject);
        }


    }
}
