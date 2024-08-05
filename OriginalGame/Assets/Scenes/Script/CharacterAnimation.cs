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

    //�A�j���[�V������
    private int Attck  = 0;  //�U��
    private int Win    = 1;  //����
    private int Drink  = 2;  //��
    private int Magic  = 3;  //�f�o�t
    private int Damage = 4;  //�_���[�W
    private int Orb    = 5;  //�o�t
    private int Sleep  = 6;  //�s�k��

    //�����t���O
    private bool WinFlag = false;
    //�s�k�t���O
    private bool LoseFlag = false;

    //�J���[�ύX�t���O
    private bool ChangeFlag1 = false;
    private bool ChangeFlag2 = false;

    //���W�擾�p
    GameObject P1;
    GameObject P2;

    //�A�j���V���[���֘A
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
    }

    private void Update()
    {
        //�^�[���؂�ւ����̕s�����x�ύX�v���O����
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
    //�^�[���؂�ւ����̕s�����x�ύX�v���O����
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



    //�_���[�W�A�j��
    public void Player1DamageAnime()
    {
        //Damege�A�j���V�������Z�b�g
        m_iAnimationIndex1 = Damage;
        //�A�j���[�V�����Đ��֐����Ă�
        PlayAnime1();
    }
    public void Player2DamageAnime()
    {
        //Damege�A�j���V�������Z�b�g
        m_iAnimationIndex2 = Damage;
        //�A�j���[�V�����Đ��֐����Ă�
        PlayAnime2();
    }

    //�U���A�j��
    public void Player1AttackAnime()
    {
        //Attack�A�j���[�V�������Z�b�g
        m_iAnimationIndex1 = Attck;
        //�A�j���[�V�����֐����Ă�
        PlayAnime1();
    }
    public void Player2AttackAnime()
    {
        //Attack�A�j���[�V�������Z�b�g
        m_iAnimationIndex2 = Attck;
        //�A�j���[�V�����֐����Ă�
        PlayAnime2();
    }

    //�񕜃A�j��
    public void Player1RecoveryAnime()
    {
        //Drink�A�j���[�V�������Z�b�g
        m_iAnimationIndex1 = Drink;
        //�A�j���[�V�����֐����Ă�
        PlayAnime1();
    }
    public void Player2RecoveryAnime()
    {
        //Drink�A�j���[�V�������Z�b�g
        m_iAnimationIndex2 = Drink;
        //�A�j���[�V�����֐����Ă�
        PlayAnime2();
    }

    //�o�t�A�j��
    public void Player1BuffAnime()
    {
        //Orb�A�j���V�������Z�b�g
        m_iAnimationIndex1 = Orb;
        //�A�j���[�V�����֐����Ă�
        PlayAnime1();
    }
    public void Player2BuffAnime()
    {
        //Orb�A�j���V�������Z�b�g
        m_iAnimationIndex2 = Orb;
        //�A�j���[�V�����֐����Ă�
        PlayAnime2();
    }

    //�f�o�t�A�j��
    public void Player1DebuffAnime()
    {
        //Magic�A�j���V�������Z�b�g
        m_iAnimationIndex1 = Magic;
        //�A�j���[�V�����֐����Ă�
        PlayAnime1();
    }
    public void Player2DebuffAnime()
    {
        //Magic�A�j���V�������Z�b�g
        m_iAnimationIndex2 = Magic;
        //�A�j���[�V�����֐����Ă�
        PlayAnime2();
    }

    //�����A�j��
    public void Player1WinAnime()
    {
        //�����t���O��������
        WinFlag = true;
        //Win�A�j���V�������Z�b�g
        m_iAnimationIndex1 = Win;
        //�A�j���V�����֐����Ă�
        PlayAnime1();
    }
    public void Player2WinAnime()
    {
        //�����t���O��������
        WinFlag = true;
        //Win�A�j���V�������Z�b�g
        m_iAnimationIndex2 = Win;
        //�A�j���V�����֐����Ă�
        PlayAnime2();
    }

    //�s�k���̃A�j���[�V����
    public void PlayerloseAnime()
    {
        //�����t���O��������
        LoseFlag = true;
        //1�x�_���[�W�A�j���[�V�������s��
        Player1DamageAnime();
        //sleep�A�j���[�V�������Z�b�g
        m_iAnimationIndex1 = Sleep;
        //�A�j���V�����Đ��֐����Ă�
        PlayAnime1();
    }

    //�A�j���[�V�����̍Đ�
    public void PlayAnime1()
    {
        //�A�j���V���[���Đ�
        m_animatorChara1.SetTrigger(AnimationName[m_iAnimationIndex1]);
        if (WinFlag)
        {
            //cell�̍폜
            FindObjectOfType<DestroyObj>().DestroyObject();
            //�o�b�N�O���E���h�̔w�i��O�Ɏ����Ă���
            Renderer.sortingOrder = 0;
            FindObjectOfType<Particle>().EffectClear();//�G�t�F�N�g�N���A
            //character�̌����ύX
            FindObjectOfType<anogamelib.CharaController>().CharacterDirection(0, -1);
            //��������ʂ̒����Ɉړ�
            transform.position = new Vector2(8.0f, 5.0f);
            //�s�����x��߂�
            Sr1.color = new Color32(255, 255, 255, 255);
            Sr2.color = new Color32(255, 255, 255, 140);
            //�V�[���؂�ւ��֐��Ăяo��
            Invoke("TitleScene", 5.0f);
        }
        if(LoseFlag)
        {
            //cell�̍폜
            FindObjectOfType<DestroyObj>().DestroyObject();
            //�o�b�N�O���E���h�̔w�i��O�Ɏ����Ă���
            Renderer.sortingOrder = 0;
            //character�̌����ύX
            FindObjectOfType<anogamelib.CharaController>().CharacterDirection(0, -1);
            //��������ʂ̒����Ɉړ�
            transform.position = new Vector2(8.0f, 5.0f);
            //�s�����x��߂�
            Sr1.color = new Color32(255, 255, 255, 255);
            Sr2.color = new Color32(255, 255, 255, 140);
            //�V�[���؂�ւ��֐��Ăяo��
            Invoke("TitleScene", 5.0f);
        }
    }
    public void PlayAnime2()
    {
        //�A�j���V���[���Đ�
        m_animatorChara2.SetTrigger(AnimationName[m_iAnimationIndex2]);
        if (WinFlag)
        {
            //cell�̍폜
            FindObjectOfType<DestroyObj>().DestroyObject();
            //�o�b�N�O���E���h�̔w�i��O�Ɏ����Ă���
            Renderer.sortingOrder = 0;
            FindObjectOfType<Particle>().EffectClear();//�G�t�F�N�g�N���A
            //character�̌����ύX
            FindObjectOfType<anogamelib.CharaController>().CharacterDirection(0, -1);
            //��������ʂ̒����Ɉړ�
            transform.position = new Vector2(-9.0f, 5.0f);
            //�s�����x��߂�
            Sr2.color = new Color32(255, 255, 255, 255);
            Sr1.color = new Color32(255, 255, 255, 140);
            //�V�[���؂�ւ��֐��Ăяo��
            Invoke("TitleScene", 5.0f);
        }
    }

    private void TitleScene()
    {
        //�V�[�̐؂肩��
        FindObjectOfType<GameManager>().dispatch(GameManager.GameState.Title);
    }
}
