using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4A1Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        //Mermi olu�tu�u anda sa� tarafa do�ru random bir g��le f�rlat�l�r.
        float randomForce = Random.Range(0.5f, 1.5f);
        rb.AddForce(randomForce * transform.right, ForceMode.Impulse);
        Destroy(this.gameObject, 5f);
    }
}
