using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialNext : MonoBehaviour
{
    //中途リアル画面
    public GameObject rule;
    public GameObject operat;

    private bool CheckActiveFlag = false;

    //オーディオ関連
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
        //オーディオ再生
        AudioSource.PlayOneShot(Sound1, 1.0f);

        // Titleシーンにとばす
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);

    }
    public void OnClickNext()
    {
        //オーディオ再生
        AudioSource.PlayOneShot(Sound1, 1.0f);

        if (CheckActiveFlag)
        {
            CheckActiveFlag = false;
            //反転
            rule.SetActive(false);
            operat.SetActive(true);
        }
        else
        {
            CheckActiveFlag = true;
            //反転
            rule.SetActive(true);
            operat.SetActive(false);
        }
    }
}
