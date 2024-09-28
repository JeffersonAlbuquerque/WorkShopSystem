using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public GameObject Door;
    public Vector3 rotation;
    public bool isActive;


    // Update is called once per frame
    void Update()
    {
        if (isActive == true)
        {
            Door.transform.localRotation = Quaternion.Lerp(Door.transform.localRotation, Quaternion.Euler(rotation), Time.deltaTime);
        }
        else
        {
            Door.transform.localRotation = Quaternion.Lerp(Door.transform.localRotation, Quaternion.Euler(0f, 0f, 0f), Time.deltaTime);
        }
    }
    public void leftDoor()
    {
        isActive = !isActive;
    }
}
