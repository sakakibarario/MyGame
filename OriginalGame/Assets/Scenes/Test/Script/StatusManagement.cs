using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //HP�o�[�C���[�W�p
    [SerializeField] public Image Player1hpBar;
    [SerializeField] public Image Player2hpBar;
    //public Slider slider;

    //�X�e�[�^�X
    //�ő�HP
    private int Player1MaxHP = 100;
    private int Player2MaxHP = 100;
    //private int EnemyMaxHP = 100;
    //���݂�HP
    private int current1Hp;
    private int current2Hp;
    //private int currentEnemyHp;

    //����
    private int   isDamage �@  = 10;   //�^����_���[�W
    private float isDebuff �@  = 0.4f; //Mino�����X�s�[�h
    private float MainFallTime = 1.0f; //Mino�ʏ헎���X�s�[�h
    private int   isRecovery   = 10;   //�񕜗�
    private bool  OnDebuffFlag = false;//�f�o�t�t���O
    private bool  OnBuffFlag   = false;//�o�t�t���O
    //public  int   DebuffTime   = 1;    //���ʎ���
    //public  int   BuffTime     = 1;    //���ʎ���

    //���ʃX�e�[�g
    public enum EffectState
    {
        DeBuffEffect,
        BuffEffect,
    }

    // Start is called before the first frame update
    void Start()
    {
        Player1hpBar.fillAmount = 1;
        Player2hpBar.fillAmount = 1;
        //Hp�̋L��
        current1Hp = Player1MaxHP;
        current2Hp = Player2MaxHP;
        //currentEnemyHp = EnemyMaxHP;
    }

   

    public void AttackHandle()
    {
        //Debug.Log("�_���[�W");
        if (!OnBuffFlag)
        {
            if (Mino.P1_Turn)
            {
                //Hp�����炷
                current2Hp -= isDamage;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
                Debug.Log("2���炷");
            }
            if (Mino.P2_Turn)
            {
                //Hp�����炷
                current1Hp -= isDamage;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
                Debug.Log("1���炷");
            }
        }
    }

    public void DebuffHandle()
    {
        if(!OnDebuffFlag)
        {
            Mino.fallTime = isDebuff;
            FindObjectOfType<Mino>().EffectDebuffTime(1);
            OnDebuffFlag = true;
        }  
    }

   

    public void BuffHandle()
    {
        Debug.Log("�o�t�X�^�[�g");
       
        OnBuffFlag = true;
        Debug.Log(OnBuffFlag);
        FindObjectOfType<Mino>().EffectDebuffTime(1);
    }

    public void RecoveryHandle()
    {
        if(current1Hp <= Player1MaxHP)
        {
            if (Mino.P1_Turn)
            {
                //Hp����
                current1Hp += isRecovery;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
        }
        if (current2Hp <= Player2MaxHP)
        {
            if (Mino.P2_Turn)
            {
                //Hp����
                current2Hp += isRecovery;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
            }
        }
    }

    public void NormalHandle()
    {

    }

    public void divide(EffectState state)
    {
        switch (state)
        {
            case EffectState.DeBuffEffect:
                Mino.fallTime = MainFallTime;
                OnDebuffFlag = false;
                break;
            case EffectState.BuffEffect:
                Debug.Log("�o�t�I��");
                OnBuffFlag = false;
                break;
        }
    }
}
