using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Getrubbish : MonoBehaviour
{
    GameObject[] rubbish;
    Camera ARCamera;
    int ind;
    GameObject rub;
    Swipes swipes;
    // Start is called before the first frame update
    void Start()
    {
        ARCamera = FindObjectOfType<Camera>();
        rubbish = GameObject.FindGameObjectsWithTag("rub");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getRubbish()
    {
        ind = UnityEngine.Random.Range(0, 5);
        rub = rubbish[ind];
        rub.transform.SetParent(ARCamera.transform);
        rub.transform.localPosition = new Vector3(0.1183378f, -0.779406f, 3.700643f);
        rub.GetComponent<Rigidbody>().useGravity = false;
        rub.GetComponent<Rigidbody>().isKinematic = true;
        rub.SetActive(true);
        gameObject.SetActive(false);
    }
}
