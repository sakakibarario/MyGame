using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    // ゲーム開始前の状態に戻す
    private void Initialize()
    {
      

    }
    public void TimeManagement()
    {
      

    }

    // スコアの追加
    public void AddScore()
    {
     

    }

    // GameOverしたときの処理
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // GameClearした時の処理
    public void GameClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GamePause()
    {


    }

   
}
