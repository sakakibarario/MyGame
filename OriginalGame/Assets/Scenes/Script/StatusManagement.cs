using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //�o�t�O���t�B�b�N
    public GameObject PlayerBuffCircle;
    GameObject Player1;
    GameObject Player2;

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
    private float isDebuff �@  = 0.2f; //Mino�����X�s�[�h
    private float MainFallTime = 0.6f; //Mino�ʏ헎���X�s�[�h
    private int   isRecovery   = 10;   //�񕜗�
    private int   EffectCount  = 2;     //���ʂ̌p������
    private static bool OnBuffFlag   = false;//�o�t�t���O
    private bool CircleFlag = false;      //�o�t�T�[�N���t���O

    public static int P1HissatsuCount = 0;//�K�E�J�E���g
    public static int P2HissatsuCount = 0;//�K�E�J�E���g
    private int HissatuDamage = 60;

    public static int DebuffTime = 0;       //���ʎ���
    public static int BuffTime = 0;         //���ʎ���

   

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

        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");
    }

    private void Update()
    {
        //HP�ǂ�����O�ɂȂ�����
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
        //�o�t�̃��Z�b�g
        if (BuffTime == 0 && CircleFlag)
        {
            FindObjectOfType<BuffCircle>().Invoke("CircleDelete", 0.4f);
            OnBuffFlag = false;
            CircleFlag = false;
        }
        //�f�o�t�̃��Z�b�g
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
        
        yield return new WaitForSeconds(0.7f);//�x��
        if (Mino.P1_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player2DamageAnime();
            //Hp�����炷
            yield return new WaitForSeconds(0.5f);//�x��
            current2Hp -= HissatuDamage;
            Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
        }
        if (Mino.P2_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player1DamageAnime();
            //Hp�����炷
            yield return new WaitForSeconds(0.5f);//�x��
            current1Hp -= HissatuDamage;
            Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
        }

        yield break;
    }

    //�f�o�t�̌p������
    public void DebuffCount()
    {
        DebuffTime--;
    }
    //�o�t�̌p������
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
            yield return new WaitForSeconds(0.7f);//�x��
            if (Mino.P1_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player2DamageAnime();
                //Hp�����炷
                yield return new WaitForSeconds(0.5f);//�x��
                current2Hp -= isDamage;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
            }
            if (Mino.P2_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player1DamageAnime();
                //Hp�����炷
                yield return new WaitForSeconds(0.5f);//�x��
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
        //�o�t�R���[�`��
        StartCoroutine(BuffStart());
    }

    IEnumerator BuffStart()
    {
        //�o�t�̌p������
        BuffTime = EffectCount;
        if (!CircleFlag)//Circle�𐶐����Ă��Ȃ��Ȃ�
        {
            //P1
            if (Mino.P1_Turn)
            {
                CircleFlag = true;
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player1BuffAnime();
                yield return new WaitForSeconds(1.5f);//�x��
                //�o�t�w�i
                Instantiate(PlayerBuffCircle, new Vector2(Player1.transform.position.x - 0.3f, Player1.transform.position.y), Quaternion.identity);
            }
            //P2
            if (Mino.P2_Turn)
            {
                CircleFlag = true;
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player2BuffAnime();
                yield return new WaitForSeconds(1.5f);//�x��
             �@ //�o�t�w�i
                Instantiate(PlayerBuffCircle, Player2.transform.position, Quaternion.identity);
            }
            OnBuffFlag = true;
        }
        yield break;
    }

    public void RecoveryHandle()
    {
        //�񕜃R���[�`��
        StartCoroutine(RecoveryStart());
    }

    IEnumerator RecoveryStart()
    {
        //HP���}�b�N�X�łȂ��Ȃ�
        if (current1Hp < Player1MaxHP)
        {
            //P1
            if (Mino.P1_Turn)
            {
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player1RecoveryAnime();
                yield return new WaitForSeconds(2.3f);//�x��
                //Hp����
                current1Hp += isRecovery;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;//HP�o�[�ɔ���
            }
        }
        //HP���}�b�N�X�łȂ��Ȃ�
        if (current2Hp < Player2MaxHP)
        {
            //P2
            if (Mino.P2_Turn)
            {
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player2RecoveryAnime();
                yield return new WaitForSeconds(2.3f);//�x��
                //Hp����
                current2Hp += isRecovery;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;//HP�o�[�ɔ���
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
