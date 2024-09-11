using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float duration = 5f;

    public float radius = 10f;

    private float explosionTimer;

    public float explosionDuration = 0.25f;

    public GameObject explosionModel;

    public bool exploded;



    // Start is called before the first frame update
    void Start()
    {
        explosionTimer = duration;
        explosionModel.transform.localScale = Vector3.one * radius;
        explosionModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        explosionTimer -= Time.deltaTime;

        if (explosionTimer <= 0f && exploded == false)
        {
            exploded = true;

            Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);


            foreach(Collider myCollider in hitObjects)
            {
                if(myCollider.GetComponent<Enemy>() != null)
                {
                    myCollider.GetComponent<Enemy>().Hit();
                }
            }

            StartCoroutine(Explode());  

        }

      
    }

    private IEnumerator  Explode()
    {
       

        Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider col in hitObjects)
        {
            Debug.Log(col.name + " was hit");
        }

        explosionModel.SetActive(true);

        yield return new WaitForSeconds(explosionDuration);

        Destroy(gameObject);

       
    }
}