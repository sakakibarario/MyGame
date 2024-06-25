using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //HP�o�[�C���[�W�p
    //[SerializeField] public GameObject Player1hpBar;
    //[SerializeField] public GameObject Player2hpBar;
    public Slider slider;

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
    private bool  isBuff       = false;//�o�t�t���O
    private int   isRecovery   = 10;   //�񕜗�
    public bool   OnDebuffFlag = false;//�f�o�t�t���O
    public bool   OnBuffFlag   = false;//�o�t�t���O

    //���ʃX�e�[�g
    public enum EffectState
    {
        DeBuffEffect,
        BuffEffect,
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        //Hp�̋L��
        current1Hp = Player1MaxHP;
        current2Hp = Player2MaxHP;
        //currentEnemyHp = EnemyMaxHP;
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
                OnBuffFlag = false;
                break;
        }
    }

    public void AttackHandle()
    {
        if (!OnBuffFlag)
        {
            if (Mino.P1_Turn)
            {
                //Hp�����炷
                current2Hp -= isDamage;
                slider.value = (float)current2Hp / (float)Player2MaxHP;
            }
            if (Mino.P2_Turn)
            {
                //Hp�����炷
                current1Hp -= isDamage;
                slider.value = (float)current1Hp / (float)Player1MaxHP;
            }
        }
    }

    public void DebuffHandle()
    {
        if(!OnDebuffFlag)
        {
            Mino.fallTime = isDebuff;
            OnDebuffFlag = true;
        }  
    }

   

    public void BuffHandle()
    {
        OnBuffFlag = true;
        Debug.Log("Buff");
    }

    public void RecoveryHandle()
    {
        if(current1Hp <= Player1MaxHP)
        {
            if (Mino.P1_Turn)
            {
                //Hp����
                current1Hp += isRecovery;
                slider.value = (float)current1Hp / (float)Player1MaxHP;
            }
        }
        if (current2Hp <= Player2MaxHP)
        {
            if (Mino.P2_Turn)
            {
                //Hp����
                current2Hp += isRecovery;
                slider.value = (float)current2Hp / (float)Player2MaxHP;
            }
        }
    }

    public void NormalHandle()
    {

    }
}
