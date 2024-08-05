using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Bgm : MonoBehaviour
{
    //サウンド関連
    AudioSource AudioSource;
    public AudioClip MaingameBgm;
    public AudioClip gameOverBgm;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        //ループを有効化
        AudioSource.loop = true;
        AudioSource.PlayOneShot(MaingameBgm,0.1f);//再生
    }

    public void GOverBgm()
    {
        //ループを無効化
        AudioSource.loop = false;
        AudioSource.Stop();//現在のBGMを止める
        AudioSource.PlayOneShot(gameOverBgm, 0.1f);//再生
    }
}
