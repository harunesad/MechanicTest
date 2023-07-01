using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemySound : MonoBehaviour
{
    public static BaseEnemySound baseEnemy;
    public AudioSource myAudio;
    private void Awake()
    {
        myAudio = GetComponent<AudioSource>();
        baseEnemy = this;
    }
    void Start()
    {
        InvokeRepeating("PlaySound", 0, 10);
    }
    void PlaySound()
    {
        myAudio.Play();
    }
}
