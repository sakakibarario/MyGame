using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//UI
using UnityEngine.UI;

public class StatusManagement : MonoBehaviour
{
    //�K�E�e�L�X�g
    public Text P1Hissatsu;
    public Text P2Hissatsu;

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
    //���݂�HP
    private int current1Hp;
    private int current2Hp;
    //private int currentEnemyHp;

    //����
    private int   isDamage �@  = 10;   //�^����_���[�W
    private int   isEnemyDamage= 20;   //�G���^����_���[�W
    private float isDebuff �@  = 0.2f; //Mino�����X�s�[�h
    private float MainFallTime = 0.6f; //Mino�ʏ헎���X�s�[�h
    private int   isRecovery   = 10;   //�񕜗�
    private int   EffectCount  = 2;     //���ʂ̌p������
    private static bool OnBuffFlag   = false;//�o�t�t���O
    private bool CircleFlag = false;      //�o�t�T�[�N���t���O

    //�K�E�֌W
    private int MAXHissatu = 10;//�ő�
    private static int P1HissatsuCount = 0;//�K�E�J�E���g
    private static int P2HissatsuCount = 0;//�K�E�J�E���g
    private int HissatuDamage = 60;//�K�E���̃_���[�W��

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
        

        Player1 = GameObject.FindWithTag("Player");
        Player2 = GameObject.FindWithTag("Player2");
        P1HissatsuCount = 0;
        P2HissatsuCount = 0;
    }

    private void Update()
    {
        //HP�ǂ�����O�ɂȂ�����
        if (current1Hp <= 0)
        {
            //�Đ����̃R���[�`�����~�߂�
            StopAllCoroutines();
            if (GameManager.GState == "PvE")
            {
                FindObjectOfType<CharacterAnimation>().PlayerloseAnime();
            }
            else
            {
                FindObjectOfType<CharacterAnimation>().Player2WinAnime();
            }
            GameManager.GState = "Title";
        }
        if (current2Hp <= 0)
        {
            //�Đ����̃R���[�`�����~�߂�
            StopAllCoroutines();
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

        //�K�E�J�E���g�`��
        P1Hissatsu.text = "Special:" + P1HissatsuCount + "/" + MAXHissatu;

        if(GameManager.GState == "PvE")
            P2Hissatsu.text = "MoveCount:" + Mino.EnemyMoveCount;//�G�̓����܂ł̎��Ԃ�`��
        else
            P2Hissatsu.text = "Special:" + P2HissatsuCount + "/" + MAXHissatu;

        //�K�E����
        if (P1HissatsuCount >= MAXHissatu)
        {
            StartCoroutine(HissattuAttack());
        }
        if (P2HissatsuCount >= MAXHissatu)
        {
            StartCoroutine(HissattuAttack());
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            current2Hp -= isDamage;
            Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            current1Hp -= isDamage;
            Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
        }

    }
    IEnumerator HissattuAttack()
    {
        if (Mino.P1_Turn || Mino.PvE)
        {
            P1HissatsuCount = 0;
            FindObjectOfType<CharacterAnimation>().Player1AttackAnime();
        }
           
        if (Mino.P2_Turn )
        {
            P2HissatsuCount = 0;
            FindObjectOfType<CharacterAnimation>().Player2AttackAnime();
        }
            yield return new WaitForSeconds(0.7f);//�x��
        if (Mino.P1_Turn || Mino.PvE)
        {
            FindObjectOfType<CharacterAnimation>().Player2DamageAnime();
            FindObjectOfType<Particle>().EffectImpulse(Player2.transform.position.x - 2.0f, Player2.transform.position.y);//�_���[�W�G�t�F�N�g
            //Hp�����炷
            yield return new WaitForSeconds(0.5f);//�x��
            current2Hp -= HissatuDamage;
            Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
        }
        if (Mino.P2_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player1DamageAnime();
            FindObjectOfType<Particle>().EffectImpulse(Player1.transform.position.x + 2.0f, Player1.transform.position.y);//�_���[�W�G�t�F�N�g
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
       // Debug.Log(DebuffTime);
    }

    public void AttackHandle()
    {
        StartCoroutine(AttackStart());
    }
    public void EnemyAttackHandle()
    {
        StartCoroutine(EAttackStart());
    }
    IEnumerator EAttackStart()
    {
        FindObjectOfType<CharacterAnimation>().Player1AttackAnime();

        yield return new WaitForSeconds(0.7f);//�x��

        FindObjectOfType<CharacterAnimation>().Player2DamageAnime();
        FindObjectOfType<Particle>().EffectImpulse(Player2.transform.position.x - 2.0f, Player2.transform.position.y);//�_���[�W�G�t�F�N�g
        //Hp�����炷
        yield return new WaitForSeconds(0.5f);//�x��
        current2Hp -= isDamage;
        Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;

        yield break;
    }
    IEnumerator AttackStart()
    {
        if (Mino.P1_Turn)
            FindObjectOfType<CharacterAnimation>().Player1AttackAnime();
        if (Mino.P2_Turn || EnemyMoveRandom.EnemyMoveFlag)
            FindObjectOfType<CharacterAnimation>().Player2AttackAnime();

        if (!OnBuffFlag)
        {
            yield return new WaitForSeconds(0.7f);//�x��
            if (Mino.P1_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player2DamageAnime();//�_���[�W�A�j���[�V����
                FindObjectOfType<Particle>().EffectImpulse(Player2.transform.position.x -2.0f, Player2.transform.position.y);//�_���[�W�G�t�F�N�g
                //Hp�����炷
                yield return new WaitForSeconds(0.5f);//�x��
                current2Hp -= isDamage;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;
            }
            if (Mino.P2_Turn)
            {
                FindObjectOfType<CharacterAnimation>().Player1DamageAnime();//�_���[�W�A�j���[�V����
                FindObjectOfType<Particle>().EffectImpulse(Player1.transform.position.x + 2.0f, Player1.transform.position.y);//�_���[�W�G�t�F�N�g
                //Hp�����炷
                yield return new WaitForSeconds(0.5f);//�x��
                current1Hp -= isDamage;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
            if (EnemyMoveRandom.EnemyMoveFlag)
            {
                FindObjectOfType<CharacterAnimation>().Player1DamageAnime();//�_���[�W�A�j���[�V����
                FindObjectOfType<Particle>().EffectImpulse(Player1.transform.position.x + 2.0f, Player1.transform.position.y);//�_���[�W�G�t�F�N�g
                //Hp�����炷
                yield return new WaitForSeconds(0.5f);//�x��
                current1Hp -= isEnemyDamage;
                Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;
            }
        }

        if(GameManager.GState == "PvE" && EnemyMoveRandom.EnemyMoveFlag)
            EnemyMoveEnd();//�G�̓������Ƃ߂�
 
        yield break;
    }

    public void DebuffHandle()
    {
        if (Mino.P1_Turn)
        {
            FindObjectOfType<CharacterAnimation>().Player1DebuffAnime();//�f�o�t�A�j���[�V����
            FindObjectOfType<Particle>().EffectDebuff(Player2.transform.position.x, Player2.transform.position.y);//�f�o�t�G�t�F�N�g
            Mino.fallTime = isDebuff;
            DebuffTime = EffectCount;
        }
        if (Mino.P2_Turn || EnemyMoveRandom.EnemyMoveFlag)
        {
            FindObjectOfType<CharacterAnimation>().Player2DebuffAnime();//�f�o�t�A�j���[�V����
            FindObjectOfType<Particle>().EffectDebuff(Player1.transform.position.x, Player1.transform.position.y);//�f�o�t�G�t�F�N�g
            Mino.fallTime = isDebuff;
            DebuffTime = EffectCount;
        }  
        if (GameManager.GState == "PvE" && EnemyMoveRandom.EnemyMoveFlag)
            EnemyMoveEnd();//�G�̓������Ƃ߂�     
    }
    public void EnemyDevuff()
    {
        FindObjectOfType<CharacterAnimation>().Player1DebuffAnime();//�f�o�t�A�j���[�V����
        FindObjectOfType<Particle>().EffectDebuff(Player2.transform.position.x, Player2.transform.position.y);//�f�o�t�G�t�F�N�g
         //�G�̍U����x�点��
        Mino.EnemyMoveCount++;
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
            if (Mino.P1_Turn || GameManager.GState == "PvE")
            {
                CircleFlag = true;
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player1BuffAnime();
                yield return new WaitForSeconds(1.5f);//�x��
                //�o�t�w�i
                Instantiate(PlayerBuffCircle, new Vector2(Player1.transform.position.x - 0.3f, Player1.transform.position.y - 0.5f), Quaternion.identity);//����Ă镪���炷
            }
            //P2
            if (Mino.P2_Turn)
            {
                CircleFlag = true;
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player2BuffAnime();
                yield return new WaitForSeconds(1.5f);//�x��
             �@ //�o�t�w�i
                Instantiate(PlayerBuffCircle, new Vector2(Player2.transform.position.x, Player2.transform.position.y - 0.5f), Quaternion.identity);//����Ă镪���炷
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
    
    public void EnemyRecovery()
    {
        //�񕜃R���[�`��
        StartCoroutine(ERecoveryStart());
    }
    IEnumerator ERecoveryStart()
    {
        //HP���}�b�N�X�łȂ��Ȃ�
        if (current1Hp < Player1MaxHP)
        {
            //�A�j���[�V�����Đ�
            FindObjectOfType<CharacterAnimation>().Player1RecoveryAnime();
            yield return new WaitForSeconds(2.3f);//�x��
                                                  //Hp����
            current1Hp += isRecovery;
            Player1hpBar.fillAmount = (float)current1Hp / (float)Player1MaxHP;//HP�o�[�ɔ���
        }
        yield break;
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
            if (Mino.P2_Turn || EnemyMoveRandom.EnemyMoveFlag)
            {
                //�A�j���[�V�����Đ�
                FindObjectOfType<CharacterAnimation>().Player2RecoveryAnime();
                yield return new WaitForSeconds(2.3f);//�x��
                //Hp����
                current2Hp += isRecovery;
                Player2hpBar.fillAmount = (float)current2Hp / (float)Player2MaxHP;//HP�o�[�ɔ���
            }
        }
        else if (EnemyMoveRandom.EnemyMoveFlag)
        {
            //Debug.Log("������x");
            FindObjectOfType<EnemyMoveRandom>().EnemyMove();
            yield break;
        }

        if (GameManager.GState == "PvE" &&  EnemyMoveRandom.EnemyMoveFlag)
            EnemyMoveEnd();//�G�̓������Ƃ߂�
        yield break;
    }

    public void NormalHandle()
    {
        if(Mino.P1_Turn || Mino.PvE)
        {
            P1HissatsuCount++;
        }
        if (Mino.P2_Turn )
        {
            P2HissatsuCount++;
        }
    }

    private void EnemyMoveEnd()
    {
        Mino.PvE = true;//�~�߂Ă����t���O��������
        EnemyMoveRandom.EnemyMoveFlag = false;//�G�̍U���I��
    }
}
