using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        
        int rnd = Random.Range(0, 2);
        //ランダムで順番決め
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
                Debug.Log("範囲外");
                break;
        }
        // ゲームスタート処理を呼ぶ
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Playing);
    }
}
