using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Strikes : MonoBehaviour
{
    // Start is called before the first frame update
    Text score;
    void Start()
    {
        score = GameObject.Find("Counter").GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        score.text = Convert.ToString(Convert.ToInt16(score.text) + 10);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        score.text = "тр попал";

    }
}
