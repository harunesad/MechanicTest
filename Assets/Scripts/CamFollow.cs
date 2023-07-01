using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CamFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    void Start()
    {
        //VignetteAndChromaticAberration chromatic_Vignette = Camera.main.GetComponent<VignetteAndChromaticAberration>();
        //chromatic_Vignette.chromaticAberration = Mathf.PingPong(Time.time, 2);
    }
    private void Update()
    {
        //ChromaticAberration chromaticAberration = Camera.main.GetComponent<ChromaticAberration>();
        //chromaticAberration.intensity.value = Mathf.PingPong(Time.time, 2);
    }
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * 7);
    }
}
