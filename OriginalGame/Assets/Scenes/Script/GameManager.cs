using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //ゲームステート
    public enum GameState
    {
        PVP,
        Title,
        Demo,
        PVE
    }
    //フェード用
    public string sceneNameR;
    public string sceneNameG;
    public string sceneNameT;
    public string sceneNameD;
    public string sceneNameE;

    public Color loadToColor = Color.white;
    public float fadeSpeed;

    // 現在のゲーム進行状態
    GameState currentState = GameState.Title;

    //public static string GState = "Title";//ゲームの状態
    public static string GState = "Title";//ゲームの状態

    // Start is called before the first frame update
    void Start()
    {

    }

    //Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        //    GState = "Palying";
    }

    // 状態による振り分け処理
    public void dispatch(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.PVP:
                GamePVP();
                break;
            case GameState.Title:
                GameTitle();
                break;
            case GameState.Demo:
                GameDemo();
                break;
            case GameState.PVE:
                GamePVE();
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
        GState = "Title";
        Debug.Log("Title");
        Initiate.Fade(sceneNameT, loadToColor, fadeSpeed);
    }

    //ポーズ処理
    void GamePose()
    {
        GState = "Pose";
    }

    // ゲームスタート処理
    void GamePVP()
    {
        GState = "Playing";
        Initiate.Fade(sceneNameG, loadToColor, fadeSpeed);
        Debug.Log("playing");
    }
    // ゲームスタート処理
    void GamePVE()
    {
        GState = "PvE";
        Initiate.Fade(sceneNameE, loadToColor, fadeSpeed);
        Debug.Log("playing");
    }

}
