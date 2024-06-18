using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //�Q�[���X�e�[�g
    public enum GameState
    {
        Playing,
        Result,
        Home,
        Pose,
        Title,
        Demo
    }
    //�t�F�[�h�p
    public string sceneNameR;
    public string sceneNameG;
    public string sceneNameH;
    public string sceneNameT;
    public string sceneNameD;

    public Color loadToColor = Color.white;
    public float fadeSpeed;

    // ���݂̃Q�[���i�s���
    GameState currentState = GameState.Home;

    public static string GState = "title";//�Q�[���̏��

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

    // ��Ԃɂ��U�蕪������
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
    // �I�[�v�j���O����
    void GameHome()
    {
        Debug.Log("home");
    }

    //�|�[�Y����
    void GamePose()
    {
        GState = "Pose";
    }

    // �Q�[���X�^�[�g����
    void GameStart()
    {
        GState = "Playing";
     
        Debug.Log("playing");
    }

    //���U���g����
    void GameResult()
    {
        GState = "GameResult";

        Debug.Log("Result");
    }

}
