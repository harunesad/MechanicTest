using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public List<AudioClip> sounds;
    public List<float> soundsVolume;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            AudioSource.PlayClipAtPoint(sounds[int.Parse(other.gameObject.name)], transform.position, soundsVolume[int.Parse(other.gameObject.name)]);
            UIManager.uýManager.SoundaInc(soundsVolume[int.Parse(other.gameObject.name)] / 10);
        }
    }
}
