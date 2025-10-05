using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [Header("BGM")]
    public AudioClip bgmClip;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = bgmClip;
        audioSource.loop = true;
        audioSource.volume = 0.2f;//‰¹—Ê
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
