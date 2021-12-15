using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            addForceToObj();
        }
    }
    
    void addForceToObj()
    {
        rb.AddRelativeForce(0, 0.25f * 15 * 10000f, 0.25f * 15 * 10000f, ForceMode.Impulse);

    }
}
