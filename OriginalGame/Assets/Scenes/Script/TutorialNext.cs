using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNext : MonoBehaviour
{
    //���r���A�����
    public GameObject rule;
    public GameObject operat;

    private bool CheckActiveFlag = false;

    //�I�[�f�B�I�֘A
    public AudioClip Sound1;
    AudioSource AudioSource;

    // Start is called before the first frame update
    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        rule.SetActive(false);
        operat.SetActive(true);
    }

    public void OnClickTitle()
    {
        //�I�[�f�B�I�Đ�
        AudioSource.PlayOneShot(Sound1, 1.0f);

        // Title�V�[���ɂƂ΂�
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);

    }
    public void OnClickNext()
    {
        //�I�[�f�B�I�Đ�
        AudioSource.PlayOneShot(Sound1, 1.0f);

        if (CheckActiveFlag)
        {
            CheckActiveFlag = false;
            //���]
            rule.SetActive(false);
            operat.SetActive(true);
        }
        else
        {
            CheckActiveFlag = true;
            //���]
            rule.SetActive(true);
            operat.SetActive(false);
        }
    }
}
