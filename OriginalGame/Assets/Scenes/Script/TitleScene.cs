using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    //�I�[�f�B�I�֘A
    public AudioClip Sound1;
    AudioSource AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        Mino.HoldFlag = true;
        SpawnMino.P2HoldFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick1()
    {
        //�I�[�f�B�I�Đ�
        AudioSource.PlayOneShot(Sound1, 1.0f);

        //�����_���ŏ��Ԍ���
        int rnd = Random.Range(0, 2);     
        switch (rnd)
        {
            case 0:
                Mino.P1_Turn = true;
                Debug.Log("p1");
                break;
            case 1:
                Mino.P2_Turn = true;
                Debug.Log("p2");
                break;
            default:
                Debug.Log("�͈͊O");
                break;
        }
        // �Q�[���X�^�[�g�������Ă�
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.PVP);
    }
    public void OnClick2()
    {
        //�I�[�f�B�I�Đ�
        AudioSource.PlayOneShot(Sound1, 1.0f);

        Mino.PvE = true;
   
        // �Q�[���X�^�[�g�������Ă�
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.PVE);
    }
    public void OnTutorial()
    {
        // Tutorial�ֈړ�
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Demo);
    }
}
