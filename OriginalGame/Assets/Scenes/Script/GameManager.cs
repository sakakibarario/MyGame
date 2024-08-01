using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //�Q�[���X�e�[�g
    public enum GameState
    {
        PVP,
        Title,
        Demo,
        PVE
    }
    //�t�F�[�h�p
    public string sceneNameR;
    public string sceneNameG;
    public string sceneNameT;
    public string sceneNameD;
    public string sceneNameE;

    public Color loadToColor = Color.white;
    public float fadeSpeed;

    // ���݂̃Q�[���i�s���
    GameState currentState = GameState.Title;

    //public static string GState = "Title";//�Q�[���̏��
    public static string GState = "Title";//�Q�[���̏��

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

    // ��Ԃɂ��U�蕪������
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

    //�|�[�Y����
    void GamePose()
    {
        GState = "Pose";
    }

    // �Q�[���X�^�[�g����
    void GamePVP()
    {
        GState = "Playing";
        Initiate.Fade(sceneNameG, loadToColor, fadeSpeed);
        Debug.Log("playing");
    }
    // �Q�[���X�^�[�g����
    void GamePVE()
    {
        GState = "PvE";
        Initiate.Fade(sceneNameE, loadToColor, fadeSpeed);
        Debug.Log("playing");
    }

}
