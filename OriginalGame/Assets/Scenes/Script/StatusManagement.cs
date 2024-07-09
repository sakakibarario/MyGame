using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //バフグラフィック
    public GameObject PlayerBuffCircle;
    GameObject Player1;
    GameObject Player2;

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
    private float isDebuff 　  = 0.2f; //Mino落下スピード
    private float MainFallTime = 0.6f; //Mino通常落下スピード
    private int   isRecovery   = 10;   //回復量
    private int   EffectCount  = 2;     //効果の継続時間
    private static bool OnBuffFlag   = false;//バフフラグ
    private bool CircleFlag = false;      //バフサークルフラグ

    public static int P1HissatsuCount = 0;//必殺カウント
    public static int P2HissatsuCount = 0;//必殺カウント
    private int HissatuDamage = 60;

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

        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");
    }

    private void Update()
    {
        //HPどちら方０になったら
        if (current1Hp <= 0)
        {
            FindObjectOfType<CharacterAnimation>().Player2WinAnime();
            GameManager.GState = "Title";
        }
        if (current2Hp <= 0)
        {
            FindObjectOfType<CharacterAnimation>().Player1WinAnime();
            GameManager.GState = "Title";
        }
        //バフのリセット
        if (BuffTime == 0 && CircleFlag)
        {
            FindObjectOfType<BuffCircle>().Invoke("CircleDelete", 0.4f);
            OnBuffFlag = false;
            CircleFlag = false;
        }
        //デバフのリセット
        if (DebuffTime == 0)
        {
            Mino.fallTime = MainFallTime;
        }
        Debug.Log(P2HissatsuCount);
        Debug.Log(P1HissatsuCount);
        if (P1HissatsuCount >= 10)
        {
            StartCoroutine(HissattuAttack());
        }
        if (P2HissatsuCount >= 10)
        {
            StartCoroutine(HissattuAttack());
        }


    }
    IEnumerator HissattuAttack()
    {
        if (Mino.P1_Turn)
        {
            P1HissatsuCount = 0;
            FindObjectOfType<CharacterAnimation>().Player1AttackAnime();
        }
           
        if (Mino.P2_Turn)
        {
            P2HissatsuCount = 0;
            FindObjectOfType<CharacterAnimation>().Player2AttackAnime();
        }
        
        yield return new WaitForSeconds(0.7f);//遅延
        if (Mino.P1_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player2DamageAnime();
            //Hpを減らす
            yield return new WaitForSeconds(0.5f);//遅延
            current2Hp -= HissatuDamage;
            Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
        }
        if (Mino.P2_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player1DamageAnime();
            //Hpを減らす
            yield return new WaitForSeconds(0.5f);//遅延
            current1Hp -= HissatuDamage;
            Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
        }

        yield break;
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
        Debug.Log(DebuffTime);
    }

    public void AttackHandle()
    {
        StartCoroutine(AttackStart());
    }
    IEnumerator AttackStart()
    {
        if (Mino.P1_Turn)
            FindObjectOfType<CharacterAnimation>().Player1AttackAnime();
        if (Mino.P2_Turn)
            FindObjectOfType<CharacterAnimation>().Player2AttackAnime();
      

        if (!OnBuffFlag)
        {
            yield return new WaitForSeconds(0.7f);//遅延
            if (Mino.P1_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player2DamageAnime();
                //Hpを減らす
                yield return new WaitForSeconds(0.5f);//遅延
                current2Hp -= isDamage;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
            }
            if (Mino.P2_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player1DamageAnime();
                //Hpを減らす
                yield return new WaitForSeconds(0.5f);//遅延
                current1Hp -= isDamage;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
        }

        yield break;
    }

    public void DebuffHandle()
    {
        if (Mino.P1_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player1DebuffAnime();
        }
        if (Mino.P2_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player2DebuffAnime();
        }
        Mino.fallTime = isDebuff;
        DebuffTime = EffectCount;
    }

    public void BuffHandle()
    {
        //バフコルーチン
        StartCoroutine(BuffStart());
    }

    IEnumerator BuffStart()
    {
        //バフの継続時間
        BuffTime = EffectCount;
        if (!CircleFlag)//Circleを生成していないなら
        {
            //P1
            if (Mino.P1_Turn)
            {
                CircleFlag = true;
                //アニメーション再生
                FindObjectOfType<CharacterAnimation>().Player1BuffAnime();
                yield return new WaitForSeconds(1.5f);//遅延
                //バフ背景
                Instantiate(PlayerBuffCircle, new Vector2(Player1.transform.position.x - 0.3f, Player1.transform.position.y), Quaternion.identity);
            }
            //P2
            if (Mino.P2_Turn)
            {
                CircleFlag = true;
                //アニメーション再生
                FindObjectOfType<CharacterAnimation>().Player2BuffAnime();
                yield return new WaitForSeconds(1.5f);//遅延
             　 //バフ背景
                Instantiate(PlayerBuffCircle, Player2.transform.position, Quaternion.identity);
            }
            OnBuffFlag = true;
        }
        yield break;
    }

    public void RecoveryHandle()
    {
        //回復コルーチン
        StartCoroutine(RecoveryStart());
    }

    IEnumerator RecoveryStart()
    {
        //HPがマックスでないなら
        if (current1Hp < Player1MaxHP)
        {
            //P1
            if (Mino.P1_Turn)
            {
                //アニメーション再生
                FindObjectOfType<CharacterAnimation>().Player1RecoveryAnime();
                yield return new WaitForSeconds(2.3f);//遅延
                //Hpを回復
                current1Hp += isRecovery;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;//HPバーに判定
            }
        }
        //HPがマックスでないなら
        if (current2Hp < Player2MaxHP)
        {
            //P2
            if (Mino.P2_Turn)
            {
                //アニメーション再生
                FindObjectOfType<CharacterAnimation>().Player2RecoveryAnime();
                yield return new WaitForSeconds(2.3f);//遅延
                //Hpを回復
                current2Hp += isRecovery;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;//HPバーに判定
            }
        }
        yield break;
    }

    public void NormalHandle()
    {
        if(Mino.P1_Turn)
        {
            P1HissatsuCount++;
        }
        if (Mino.P2_Turn)
        {
            P2HissatsuCount++;
        }        
    }
}
