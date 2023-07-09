using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapInside : MonoBehaviour
{
    public Transform minimapCam;
    public float minimapSize;
    Vector3 temp;
    void Start()
    {
        
    }
    void Update()
    {
        temp = transform.parent.position;
        temp.y = transform.position.y;
        transform.position = temp;
    }
    private void LateUpdate()
    {
        float posX = Mathf.Clamp(transform.position.x, minimapCam.position.x - minimapSize, minimapSize + minimapCam.transform.position.x);
        float posZ = Mathf.Clamp(transform.position.z, minimapCam.position.z - minimapSize, minimapSize + minimapCam.transform.position.z);
        transform.position = new Vector3(posX, transform.position.y, posZ);
    }
}
