using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Swipes : MonoBehaviour
{

    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Rigidbody rb;
    Camera ARCamera;
    public float force = 25f;
    GameObject GetrubbishButton;
    Vector3 pos;
    Boolean swipeable;
    public void Swipe()
    {

        if (Input.touches.Length > 0)
        {
            
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                if (secondPressPos.y - firstPressPos.y > Screen.height/8)
                {
                    gameObject.transform.SetParent(null);

                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                    currentSwipe.Normalize();
                    rb.useGravity = true;
                    rb.isKinematic = false;
                    pos = gameObject.transform.localPosition;
                    
                    rb.AddRelativeForce(currentSwipe.x * 10f, currentSwipe.y/Screen.height * force*10000f*Math.Abs(ARCamera.transform.rotation.x), currentSwipe.y * 10f, ForceMode.Impulse);

                    GetrubbishButton.SetActive(true);
                }
            }
        }
    }
     void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        ARCamera = FindObjectOfType<Camera>();
        GetrubbishButton = GameObject.Find("GetRubbish");
        GetrubbishButton.SetActive(false);

    }

    void Update()
    {
        Swipe();
        gameObject.transform.rotation.Set(ARCamera.transform.rotation.x, 0f,0f, 0f);

        if (gameObject.transform.position.y < -50)
        {
            gameObject.transform.position = new Vector3(100f,100f,0f);
            rb.useGravity = false;
        }
    }
}
