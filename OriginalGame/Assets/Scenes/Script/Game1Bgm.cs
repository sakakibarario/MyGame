using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game1Bgm : MonoBehaviour
{
    //�T�E���h�֘A
    AudioSource AudioSource;
    public AudioClip MaingameBgm;
    public AudioClip gameOverBgm;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        //���[�v��L����
        AudioSource.loop = true;
        AudioSource.PlayOneShot(MaingameBgm,0.1f);//�Đ�
    }

    public void GOverBgm()
    {
        //���[�v�𖳌���
        AudioSource.loop = false;
        AudioSource.Stop();//���݂�BGM���~�߂�
        AudioSource.PlayOneShot(gameOverBgm, 0.1f);//�Đ�
    }
}
