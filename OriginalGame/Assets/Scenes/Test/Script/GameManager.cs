using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //ゲームステート
    public enum GameState
    {
        Playing,
        Result,
        Home,
        Pose,
        Title,
        Demo
    }
    //フェード用
    public string sceneNameR;
    public string sceneNameG;
    public string sceneNameH;
    public string sceneNameT;
    public string sceneNameD;

    public Color loadToColor = Color.white;
    public float fadeSpeed;

    // 現在のゲーム進行状態
    GameState currentState = GameState.Home;

    public static string GState = "title";//ゲームの状態

    // Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //    dispatch(GameState.Playing);
    }

    // 状態による振り分け処理
    public void dispatch(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.Playing:
                GameStart();
                break;
            case GameState.Result:
                GameResult();
                break;
            case GameState.Home:
                GameHome();
                break;
            case GameState.Pose:
                GamePose();
                break;
            case GameState.Title:
                GameTitle();
                break;
            case GameState.Demo:
                GameDemo();
                break;
        }

    }

    void GameDemo()
    {
        GState = "Demo";
        Debug.Log("Demo");
     
    }

    void GameTitle()
    {
        Debug.Log("Title");
      
    }
    // オープニング処理
    void GameHome()
    {
        Debug.Log("home");
    }

    //ポーズ処理
    void GamePose()
    {
        GState = "Pose";
    }

    // ゲームスタート処理
    void GameStart()
    {
        GState = "Playing";
     
        Debug.Log("playing");
    }

    //リザルト処理
    void GameResult()
    {
        GState = "GameResult";

        Debug.Log("Result");
    }

}
