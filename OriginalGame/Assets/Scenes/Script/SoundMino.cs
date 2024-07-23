using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMino : MonoBehaviour
{
    //オーディオ関連
    public AudioClip Sound1;
    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void MinoSound()
    {
        //サウンド再生
        AudioSource.PlayOneShot(Sound1,1.0f);
    }
}
