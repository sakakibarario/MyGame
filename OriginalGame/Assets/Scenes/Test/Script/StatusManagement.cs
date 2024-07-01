using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //HPバーイメージ用
    [SerializeField] public Image Player1hpBar;
    [SerializeField] public Image Player2hpBar;
    //public Slider slider;

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
    private int   isRecovery   = 10;   //回復量
    private int   EffectCount  = 1;     //効果の継続時間
    private static bool OnDebuffFlag = false;//デバフフラグ
    private static bool OnBuffFlag   = false;//バフフラグ
    private static bool DeleteBuffMinoFlag = false;//デリートバフみのフラグ
   
    public static int DebuffTime = 0;       //効果時間
    public static int BuffTime = 0;         //効果時間
   

    //効果ステート
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
        //Hpの記憶
        current1Hp = Player1MaxHP;
        current2Hp = Player2MaxHP;
        //currentEnemyHp = EnemyMaxHP;
    }

    private void Update()
    {
        Debug.Log(OnBuffFlag);

        if (BuffTime == 0)      
            OnBuffFlag = false;   
        if (DebuffTime == 0)
            OnDebuffFlag = false;
    }

    //デバフの継続時間
    public void DebuffCount()
    {
        DebuffTime--;
    }
    //バフの継続時間
    public void BuffCount()
    {
        BuffTime--;
    }

    public void AttackHandle()
    {
        if (!OnBuffFlag)
        {
            if (Mino.P1_Turn)
            {
                //Hpを減らす
                current2Hp -= isDamage;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
                Debug.Log("2減らす");
            }
            if (Mino.P2_Turn)
            {
                //Hpを減らす
                current1Hp -= isDamage;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
                Debug.Log("1減らす");
            }
        }
    }

    public void DebuffHandle()
    {
        if(!OnDebuffFlag)
        {
            Mino.fallTime = isDebuff;
            DebuffTime = EffectCount;
            OnDebuffFlag = true;
        }  
    }

    public void BuffHandle()
    {
        OnBuffFlag = true;
        Debug.Log("バフスタート");
        BuffTime = EffectCount;
        Debug.Log(OnBuffFlag);
    }

    public void RecoveryHandle()
    {
        if(current1Hp <= Player1MaxHP)
        {
            if (Mino.P1_Turn)
            {
                //Hpを回復
                current1Hp += isRecovery;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
        }
        if (current2Hp <= Player2MaxHP)
        {
            if (Mino.P2_Turn)
            {
                //Hpを回復
                current2Hp += isRecovery;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
            }
        }
    }

    public void NormalHandle()
    {

    }
}
