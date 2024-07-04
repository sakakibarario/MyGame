using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //�o�t�O���t�B�b�N
    public GameObject PlayerBuffCircle;

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
    private int   EffectCount  = 1;     //���ʂ̌p������
    private static bool OnDebuffFlag = false;//�f�o�t�t���O
    private static bool OnBuffFlag   = false;//�o�t�t���O
   
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
    }

    private void Update()
    {
        if (BuffTime == 0)      
            OnBuffFlag = false;   
        if (DebuffTime == 0)
        {
            Mino.fallTime = MainFallTime;
            OnDebuffFlag = false;
        } 

        //�o�t�w�i�폜
        

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
    }

    public void AttackHandle()
    {
        if (Mino.P1_Turn)
            FindObjectOfType<CharacterAnimation>().Player1AttackAnime();
        if (Mino.P2_Turn)
            FindObjectOfType<CharacterAnimation>().Player2AttackAnime();
        if (!OnBuffFlag)
        {
            if (Mino.P1_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Invoke("Player2DamageAnime", 0.5f);
                //Hp�����炷
                current2Hp -= isDamage;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;               
            }
            if (Mino.P2_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Invoke("Player1DamageAnime", 0.5f);
                //Hp�����炷
                current1Hp -= isDamage;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
        }
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
        OnDebuffFlag = true;

    }

    public void BuffHandle()
    {
        if (Mino.P1_Turn)
        {
            //�o�t�w�i
            //Instantiate(PlayerBuffCircle, transform.position, Quaternion.identity);
            FindObjectOfType<CharacterAnimation>().Player1BuffAnime();
        }
        if (Mino.P2_Turn)
        {
            //�o�t�w�i
            //Instantiate(PlayerBuffCircle, transform.position, Quaternion.identity);
            FindObjectOfType<CharacterAnimation>().Player2BuffAnime();
        }
        OnBuffFlag = true;
        Debug.Log("�o�t�X�^�[�g");
        BuffTime = EffectCount;
        Debug.Log(OnBuffFlag);
    }

    public void RecoveryHandle()
    {
        if(current1Hp <= Player1MaxHP)
        {
            if (Mino.P1_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player1RecoveryAnime();
                //Hp����
                current1Hp += isRecovery;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
        }
        if (current2Hp <= Player2MaxHP)
        {
            if (Mino.P2_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player2RecoveryAnime();
                //Hp����
                current2Hp += isRecovery;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
            }
        }
    }

    public void NormalHandle()
    {

    }
}
