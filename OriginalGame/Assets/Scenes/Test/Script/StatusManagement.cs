using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //HPバーイメージ用
    //[SerializeField] public GameObject Player1hpBar;
    //[SerializeField] public GameObject Player2hpBar;
    public Slider slider;

    //ステータス
    //最大HP
    private int Player1MaxHP = 100;
    private int Player2MaxHP = 100;
    //private int EnemyMaxHP = 100;
    //現在のHP
    private int current1Hp;
    private int current2Hp;
    //private int currentEnemyHp;

    //効果
    private int   isDamage 　  = 10;   //与えるダメージ
    private float isDebuff 　  = 0.4f; //Mino落下スピード
    private float MainFallTime = 1.0f; //Mino通常落下スピード
    private bool  isBuff       = false;//バフフラグ
    private int   isRecovery   = 10;   //回復量
    public bool   OnDebuffFlag = false;//デバフフラグ
    public bool   OnBuffFlag   = false;//バフフラグ

    //効果ステート
    public enum EffectState
    {
        DeBuffEffect,
        BuffEffect,
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;
        //Hpの記憶
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
                //Hpを減らす
                current2Hp -= isDamage;
                slider.value = (float)current2Hp / (float)Player2MaxHP;
            }
            if (Mino.P2_Turn)
            {
                //Hpを減らす
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
                //Hpを回復
                current1Hp += isRecovery;
                slider.value = (float)current1Hp / (float)Player1MaxHP;
            }
        }
        if (current2Hp <= Player2MaxHP)
        {
            if (Mino.P2_Turn)
            {
                //Hpを回復
                current2Hp += isRecovery;
                slider.value = (float)current2Hp / (float)Player2MaxHP;
            }
        }
    }

    public void NormalHandle()
    {

    }
}
