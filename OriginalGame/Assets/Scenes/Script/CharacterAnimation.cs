using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimation : MonoBehaviour
{
    GameObject Player1;
    GameObject Player2;
    private  SpriteRenderer Sr1;
    private SpriteRenderer Sr2;

    private int m_iAnimationIndex1 = 0;
    private int m_iAnimationIndex2 = 0;

    //アニメーション名
    private int Attck  = 0;  //攻撃
    private int Win    = 1;  //勝利
    private int Drink  = 2;  //回復
    private int Magic  = 3;  //デバフ
    private int Damage = 4;  //ダメージ
    private int Orb    = 5;  //バフ

    //勝利フラグ
    private bool WinFlag = false;

    private readonly string[] AnimationName = new string[]
    {
            "attack",
            "win",
            "drink",
            "magic",
            "damage",
            "orb",
    };

    [SerializeField] private Animator m_animatorChara1;
    [SerializeField] private Animator m_animatorChara2;

    private void Start()
    {
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");
        Sr1 = Player1.GetComponent<SpriteRenderer>();
        Sr2 = Player2.GetComponent<SpriteRenderer>(); 
    }

    private void Update()
    {
        if (GameManager.GState == "Playing")
        {
            if (Mino.P1_Turn)
            {
                Sr1.color = new Color32(255, 255, 255, 140);
            }
            else
                Sr1.color = new Color32(255, 255, 255, 255);
            if (Mino.P2_Turn)
            {
                Sr2.color = new Color32(255, 255, 255, 140);
            }
            else
                Sr2.color = new Color32(255, 255, 255, 255);
        }
    }

    //ダメージアニメ
    public void Player1DamageAnime()
    {
        m_iAnimationIndex1 = Damage;
        PlayAnime1();
    }
    public void Player2DamageAnime()
    {
        m_iAnimationIndex2 = Damage;
        PlayAnime2();
    }

    //攻撃アニメ
    public void Player1AttackAnime()
    {
        m_iAnimationIndex1 = Attck;
        PlayAnime1();
    }
    public void Player2AttackAnime()
    {
        m_iAnimationIndex2 = Attck;
        PlayAnime2();
    }

    //回復アニメ
    public void Player1RecoveryAnime()
    {
        m_iAnimationIndex1 = Drink;
        PlayAnime1();
    }
    public void Player2RecoveryAnime()
    {
        m_iAnimationIndex2 = Drink;
        PlayAnime2();
    }

    //バフアニメ
    public void Player1BuffAnime()
    {
        m_iAnimationIndex1 = Orb;
        PlayAnime1();
    }
    public void Player2BuffAnime()
    {
        m_iAnimationIndex2 = Orb;
        PlayAnime2();
    }

    //デバフアニメ
    public void Player1DebuffAnime()
    {
        m_iAnimationIndex1 = Magic;
        PlayAnime1();
    }
    public void Player2DebuffAnime()
    {
        m_iAnimationIndex1 = Magic;
        PlayAnime2();
    }

    //勝利アニメ
    public void Player1WinAnime()
    {
        WinFlag = true;
        m_iAnimationIndex1 = Win;
        PlayAnime1();
    }
    public void Player2WinAnime()
    {
        WinFlag = true;
        m_iAnimationIndex1 = Win;
        PlayAnime2();
    }

    //アニメーションの再生
    public void PlayAnime1()
    {
        m_animatorChara1.SetTrigger(AnimationName[m_iAnimationIndex1]);
        if (WinFlag)
        {
            Sr1.color = new Color32(255, 255, 255, 255);
            Sr2.color = new Color32(255, 255, 255, 140);
            Invoke("TitleScene", 5.0f);
        }
    }
    public void PlayAnime2()
    {
        m_animatorChara2.SetTrigger(AnimationName[m_iAnimationIndex2]);
        if (WinFlag)
        {
            Sr2.color = new Color32(255, 255, 255, 255);
            Sr1.color = new Color32(255, 255, 255, 140);
            Invoke("TitleScene", 5.0f);
        }
    }

    private void TitleScene()
    {
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }
}
