using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float swingingSpeed = 2f;
    public float coolDownSpeed = 2f;
    public float coolDownDuration = 2f;
    public float attackDuration = 0.25f;

    private Quaternion targertRotaion;
    private float coolDownTimer;

    private bool isAttacking;


    public bool IsAttacking
    {
        get
        {
            return isAttacking;
        }


    }

    private void Start()
    {
        targertRotaion = Quaternion.Euler(0, 0, 0);
    }

    private void Update()
    {
        if ((isAttacking))
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targertRotaion, Time.deltaTime * (isAttacking ? swingingSpeed : coolDownSpeed));
        }


        coolDownTimer -= Time.deltaTime;
    }

    public void Attack()
    {
        if (coolDownTimer > 0f)
        {
            return;
        }

        targertRotaion = Quaternion.Euler(90, 0, 0);

        coolDownTimer = coolDownDuration;

        StartCoroutine(CoolDownWait());
    }

    private IEnumerator CoolDownWait()
    {
        isAttacking = true;

        yield return new WaitForSeconds(attackDuration);

        // isAttacking = false;

        targertRotaion = Quaternion.Euler(0, 0, 0);
    }
}
