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
    // �Q�[���J�n�O�̏�Ԃɖ߂�
    private void Initialize()
    {
      

    }
    public void TimeManagement()
    {
      

    }

    // �X�R�A�̒ǉ�
    public void AddScore()
    {
     

    }

    // GameOver�����Ƃ��̏���
    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // GameClear�������̏���
    public void GameClear()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GamePause()
    {


    }

   
}
