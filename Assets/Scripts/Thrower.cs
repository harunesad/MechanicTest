using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public static Thrower thrower;
    [SerializeField] GameObject throwObj;
    [SerializeField] float throwForce = 10;
    [SerializeField] float maxForce = 20;
    [SerializeField] Transform throwPos;
    [SerializeField] Vector3 throwDir = new Vector3(0, 1, 0);
    [SerializeField] LineRenderer trajectoryLine;
    public bool isCharging = false;
    float chargeTime = 0;
    public Camera cam;
    private void Awake()
    {
        thrower = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    StartThrowing();
        //}
        //if (isCharging)
        //{
        //    ChargeThrow();
        //}
        //if (Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    ReleaseThrow();
        //}
    }
    public void StartThrowing()
    {
        isCharging = true;
        chargeTime = 0;
        trajectoryLine.enabled = true;
    }
    public void ChargeThrow()
    {
        chargeTime += Time.deltaTime;
        Vector3 velocity = (cam.transform.forward + throwDir).normalized * math.min(chargeTime * throwForce, maxForce);
        ShowTrajectory(throwPos.position + throwPos.forward, velocity);
    }
    public void ReleaseThrow()
    {
        Throw(Mathf.Min(chargeTime * throwForce, maxForce));
        isCharging = false;
        trajectoryLine.enabled = false;
    }
    void Throw(float force)
    {
        Vector3 spawnPos = throwPos.position + cam.transform.forward;
        GameObject obj = Instantiate(throwObj, spawnPos, cam.transform.rotation);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        Vector3 finalThrowDir = (cam.transform.forward + throwDir).normalized;
        rb.AddForce(finalThrowDir * force, ForceMode.VelocityChange);
    }
    void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[100];
        trajectoryLine.positionCount = points.Length;
        for (int i = 0; i < points.Length; i++)
        {
            float time = i * .1f;
            points[i] = origin + speed * time + .5f * Physics.gravity * time * time;
        }
        trajectoryLine.SetPositions(points);
    }
}
