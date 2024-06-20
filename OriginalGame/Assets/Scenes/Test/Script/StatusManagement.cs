using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManagement : MonoBehaviour
{
    //ステータス
    private int Player1HP = 100;
    private int Player2HP = 100;
    //private int EnemyHP   = 100;

    //効果
    private int   isDamage 　= 10;   //与えるダメージ
    private float isDebuff 　= 0.4f; //Mino落下スピード
    private bool  isBuff     = false;//バフフラグ
    private int   isRecovery = 10;   //回復量

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void AttackHandle()
    {

    }

    public void DebuffHandle()
    {

    }

    public void BuffHandle()
    {

    }

    public void RecoveryHandle()
    {

    }

    public void NormalHandle()
    {

    }
}
