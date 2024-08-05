using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimation : MonoBehaviour
{
    public GameObject BackGround;
    Renderer Renderer;

    GameObject Player1;
    GameObject Player2;
    private  SpriteRenderer Sr1;
    private  SpriteRenderer Sr2;

    private int m_iAnimationIndex1 = 0;
    private int m_iAnimationIndex2 = 0;

    //アニメーション名
    private int Attck  = 0;  //攻撃
    private int Win    = 1;  //勝利
    private int Drink  = 2;  //回復
    private int Magic  = 3;  //デバフ
    private int Damage = 4;  //ダメージ
    private int Orb    = 5;  //バフ
    private int Sleep  = 6;  //敗北時

    //サウンド
    public AudioClip AttackSound;  //攻撃サウンド
    public AudioClip DamageSound; //ダメージサウンド
    public AudioClip HeelSound;   //回復サウンド
    public AudioClip DebuffSound; //デバフサウンド
    public AudioClip BuffSound;   //バフサウンド

    private AudioClip NowSound1;//現在のサウンド
    private AudioClip NowSound2;//現在のサウンド

    AudioSource AudioSource;

    //勝利フラグ
    private bool WinFlag = false;
    //敗北フラグ
    private bool LoseFlag = false;

    //カラー変更フラグ
    private bool ChangeFlag1 = false;
    private bool ChangeFlag2 = false;

    //座標取得用
    GameObject P1;
    GameObject P2;

    //アニメショーン関連
    private readonly string[] AnimationName = new string[]
    {
            "attack",
            "win",
            "drink",
            "magic",
            "damage",
            "orb",
            "sleep",
    };

    [SerializeField] private Animator m_animatorChara1;
    [SerializeField] private Animator m_animatorChara2;

    private void Start()
    {
        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");
        Sr1 = Player1.GetComponent<SpriteRenderer>();
        Sr2 = Player2.GetComponent<SpriteRenderer>();
        Renderer = BackGround.GetComponent<SpriteRenderer>();
        AudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        //ターン切り替え時の不透明度変更プログラム
        if (GameManager.GState == "Playing")
        {
            if (Mino.P1_Turn)
            {
                ChangeFlag1 = false;
                Invoke("Transparent1", 1.0f);
               
            }
            else
                Invoke("ChangeColor1", 1.0f);
        
            if (Mino.P2_Turn)
            {
                ChangeFlag2 = false;
                Invoke("Transparent2", 1.0f);
               
            }
            else
                Invoke("ChangeColor2", 1.0f);
            

            if (ChangeFlag1)
                Sr1.color = new Color32(255, 255, 255, 255);
            if(ChangeFlag2)
                Sr2.color = new Color32(255, 255, 255, 255);
        }
    }
    //ターン切り替え時の不透明度変更プログラム
    private void Transparent1()
    {
        if (GameManager.GState == "Playing")
            Sr1.color = new Color32(255, 255, 255, 140);
    }
    private void Transparent2()
    {
        if (GameManager.GState == "Playing")
            Sr2.color = new Color32(255, 255, 255, 140);
    }
    private void ChangeColor1()
    {
        ChangeFlag1 = true;
    }
    private void ChangeColor2()
    {
        ChangeFlag2 = true;
    }



    //ダメージアニメ
    public void Player1DamageAnime()
    {
        //Damegeアニメションをセット
        m_iAnimationIndex1 = Damage;
        //Damgeサウンドをセット
        NowSound1 = DamageSound;
        //アニメーション再生関数を呼ぶ
        PlayAnime1();
    }
    public void Player2DamageAnime()
    {
        //Damegeアニメションをセット
        m_iAnimationIndex2 = Damage;
        //Damgeサウンドをセット
        NowSound2 = DamageSound;
        //アニメーション再生関数を呼ぶ
        PlayAnime2();
    }

    //攻撃アニメ
    public void Player1AttackAnime()
    {
        //Attackアニメーションをセット
        m_iAnimationIndex1 = Attck;
        //Attackサウンドをセット
        NowSound1 = AttackSound;
        //アニメーション関数を呼ぶ
        PlayAnime1();
    }
    public void Player2AttackAnime()
    {
        //Attackアニメーションをセット
        m_iAnimationIndex2 = Attck;
        //Attackサウンドをセット
        NowSound2 = AttackSound;
        //アニメーション関数を呼ぶ
        PlayAnime2();
    }

    //回復アニメ
    public void Player1RecoveryAnime()
    {
        //Drinkアニメーションをセット
        m_iAnimationIndex1 = Drink;
        //Heelサウンドをセット
        NowSound1 = HeelSound;
        //アニメーション関数を呼ぶ
        PlayAnime1();
    }
    public void Player2RecoveryAnime()
    {
        //Drinkアニメーションをセット
        m_iAnimationIndex2 = Drink;
        //Heelサウンドをセット
        NowSound2 = HeelSound;
        //アニメーション関数を呼ぶ
        PlayAnime2();
    }

    //バフアニメ
    public void Player1BuffAnime()
    {
        //Orbアニメションをセット
        m_iAnimationIndex1 = Orb;
        //Buffサウンドをセット
        NowSound1 = BuffSound;
        //アニメーション関数を呼ぶ
        PlayAnime1();
    }
    public void Player2BuffAnime()
    {
        //Orbアニメションをセット
        m_iAnimationIndex2 = Orb;
        //Buffサウンドをセット
        NowSound2 = BuffSound;
        //アニメーション関数を呼ぶ
        PlayAnime2();
    }

    //デバフアニメ
    public void Player1DebuffAnime()
    {
        //Magicアニメションをセット
        m_iAnimationIndex1 = Magic;
        //Debuffサウンドをセット
        NowSound1 = DebuffSound;
        //アニメーション関数を呼ぶ
        PlayAnime1();
    }
    public void Player2DebuffAnime()
    {
        //Magicアニメションをセット
        m_iAnimationIndex2 = Magic;
        //Debuffサウンドをセット
        NowSound2 = DebuffSound;
        //アニメーション関数を呼ぶ
        PlayAnime2();
    }

    //勝利アニメ
    public void Player1WinAnime()
    {
        //勝利フラグをあげる
        WinFlag = true;
        //Winアニメションをセット
        m_iAnimationIndex1 = Win;
        //アニメション関数を呼ぶ
        PlayAnime1();
    }
    public void Player2WinAnime()
    {
        //勝利フラグをあげる
        WinFlag = true;
        //Winアニメションをセット
        m_iAnimationIndex2 = Win;
        //アニメション関数を呼ぶ
        PlayAnime2();
    }

    //敗北時のアニメーション
    public void PlayerloseAnime()
    {
        //負けフラグをあげる
        LoseFlag = true;
        //1度ダメージアニメーションを行う
        Player1DamageAnime();
        //sleepアニメーションをセット
        m_iAnimationIndex1 = Sleep;
        //アニメション再生関数を呼ぶ
        PlayAnime1();
    }

    //アニメーションの再生
    public void PlayAnime1()
    {
        //アニメショーン再生
        m_animatorChara1.SetTrigger(AnimationName[m_iAnimationIndex1]);

        if (WinFlag)
        {
            //cellの削除
            FindObjectOfType<DestroyObj>().DestroyObject();
            //バックグラウンドの背景を前に持ってくる
            Renderer.sortingOrder = 0;
            FindObjectOfType<Particle>().EffectClear();//エフェクトクリア
            //characterの向き変更
            FindObjectOfType<anogamelib.CharaController>().CharacterDirection(0, -1);
            //勝利時画面の中央に移動
            transform.position = new Vector2(8.0f, 5.0f);
            //不透明度を戻す
            Sr1.color = new Color32(255, 255, 255, 255);
            Sr2.color = new Color32(255, 255, 255, 140);
            //シーン切り替え関数呼び出し
            Invoke("TitleScene", 5.0f);
        }
        if(LoseFlag)
        {
            //GOverBgmに変更
            FindObjectOfType<Game1Bgm>().GOverBgm();
            //cellの削除
            FindObjectOfType<DestroyObj>().DestroyObject();
            //バックグラウンドの背景を前に持ってくる
            Renderer.sortingOrder = 0;
            FindObjectOfType<Particle>().EffectGOver();//エフェクトGオーバー
            //characterの向き変更
            FindObjectOfType<anogamelib.CharaController>().CharacterDirection(0, -1);
            //勝利時画面の中央に移動
            transform.position = new Vector2(8.0f, 5.0f);
            //不透明度を戻す
            Sr1.color = new Color32(255, 255, 255, 255);
            Sr2.color = new Color32(255, 255, 255, 140);
            //シーン切り替え関数呼び出し
            Invoke("TitleScene", 5.0f);
        }
        if(!LoseFlag && !WinFlag)
        {
            //サウンドの再生
            AudioSource.PlayOneShot(NowSound1, 0.5f);
        }
    }
    public void PlayAnime2()
    {
        //アニメショーン再生
        m_animatorChara2.SetTrigger(AnimationName[m_iAnimationIndex2]);
        

        if (WinFlag)
        {
            //cellの削除
            FindObjectOfType<DestroyObj>().DestroyObject();
            //バックグラウンドの背景を前に持ってくる
            Renderer.sortingOrder = 0;
            FindObjectOfType<Particle>().EffectClear();//エフェクトクリア
            //characterの向き変更
            FindObjectOfType<anogamelib.CharaController>().CharacterDirection(0, -1);
            //勝利時画面の中央に移動
            transform.position = new Vector2(-9.0f, 5.0f);
            //不透明度を戻す
            Sr2.color = new Color32(255, 255, 255, 255);
            Sr1.color = new Color32(255, 255, 255, 140);
            //シーン切り替え関数呼び出し
            Invoke("TitleScene", 5.0f);
        }
        else
        {
            //サウンドの再生
            AudioSource.PlayOneShot(NowSound2, 0.5f);
        }
    }

    private void TitleScene()
    {
        //シーの切りかえ
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }
}
