using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System;
using System.IO;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] private GameObject PlaneMarker;
    [SerializeField] private GameObject RecycleBin;
    [SerializeField] private GameObject SpawnButton;
    [SerializeField] private Camera ARCamera;


    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private ARRaycastManager ARRaycastManager;
    private Vector2 TouchPosition;
    private GameObject SelectedObject;
    private Quaternion YRotation;
    GameObject GetrubbishButton;


    void Start()
    {
        ARRaycastManager = FindObjectOfType<ARRaycastManager>();
        GetrubbishButton = GameObject.Find("GetRubbish");

        PlaneMarker.SetActive(false);
        SpawnButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
        ShowMarker();
    }

    public void spawnRecycleBin()
    {

        ARRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        Instantiate(RecycleBin, hits[0].pose.position, RecycleBin.transform.rotation);
        SpawnButton.SetActive(false);

        GetrubbishButton.SetActive(true);

    }
    void ShowMarker()
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        ARRaycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.Planes);

        if (hits.Count > 0)
        {
            PlaneMarker.transform.position = hits[0].pose.position;

            PlaneMarker.SetActive(true);
            SpawnButton.SetActive(true);
        }
    }

    void MoveObject()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            TouchPosition = touch.position;

            if (touch.phase == TouchPhase.Stationary)
            {
                Ray ray = ARCamera.ScreenPointToRay(touch.position);
                RaycastHit hitObject;

                if (Physics.Raycast(ray, out hitObject))
                {
                    if (hitObject.collider.CompareTag("UnSelected"))
                    {
                        hitObject.collider.gameObject.tag = "Selected";
                        Handheld.Vibrate();

                    }
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                ARRaycastManager.Raycast(TouchPosition, hits, TrackableType.Planes);
                SelectedObject = GameObject.FindWithTag("Selected");
                SelectedObject.transform.position = hits[0].pose.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (SelectedObject.CompareTag("Selected"))
                {
                    SelectedObject.tag = "UnSelected";
                }
            }

            if (Input.touchCount == 2)
            {
                Touch touch1 = Input.touches[0];
                Touch touch2 = Input.touches[1];

                if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
                {
                    float DistanceBetweenTouches = Vector2.Distance(touch1.position, touch2.position);
                    float prevDistanceBetweenTouches = Vector2.Distance(touch1.position - touch1.deltaPosition, touch2.position - touch2.deltaPosition);
                    float Delta = DistanceBetweenTouches - prevDistanceBetweenTouches;

                    if (Mathf.Abs(Delta) > 0)
                    {
                        Delta *= 0.1f;
                    }
                    else
                    {
                        DistanceBetweenTouches = Delta = 0;
                    }
                    YRotation = Quaternion.Euler(0f, -touch1.deltaPosition.x * Delta, 0f);
                    SelectedObject.transform.rotation = YRotation * SelectedObject.transform.rotation;
                }

            }
        }

    }
}
