using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMino : MonoBehaviour
{
    //�I�[�f�B�I�֘A
    public AudioClip Sound1;
    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void MinoSound()
    {
        //�T�E���h�Đ�
        AudioSource.PlayOneShot(Sound1,1.0f);
    }
}
