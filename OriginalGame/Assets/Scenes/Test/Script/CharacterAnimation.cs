using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimation : MonoBehaviour
{

    private int m_iAnimationIndex1 = 0;
    private int m_iAnimationIndex2 = 0;

    //�A�j���[�V������
    private int Attck  = 0;  //�U��
    private int Win    = 1;  //����
    private int Drink  = 2;  //��
    private int Magic  = 3;  //�f�o�t
    private int Damage = 4;  //�_���[�W
    private int Orb    = 5;  //�o�t

    private readonly string[] AnimationName = new string[]
    {
            "attack",
            "win",
            "drink",
            "magic",
            "damage",
            "orb",
    };

    [SerializeField]private Animator m_animatorChara1;
    [SerializeField] private Animator m_animatorChara2;

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    Debug.Log("�Q�b�g��");
        //    Player1DamageAnime();
        //}
    }

    //�_���[�W�A�j��
    public void Player1DamageAnime()
    {
        Debug.Log("�A�j���[�V�����_���[�W");
        m_iAnimationIndex1 = Damage;
        PlayAnime1();
    }
    public void Player2DamageAnime()
    {
        m_iAnimationIndex2 = Damage;
        PlayAnime2();
    }

    //�U���A�j��
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

    //�񕜃A�j��
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

    //�o�t�A�j��
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

    //�f�o�t�A�j��
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

    //�A�j���[�V�����̍Đ�
    public void PlayAnime1()
    {
        Debug.Log("�A�j���[�V�����Đ�");
        m_animatorChara1.SetTrigger(AnimationName[m_iAnimationIndex1]);
    }
    public void PlayAnime2()
    {
        m_animatorChara2.SetTrigger(AnimationName[m_iAnimationIndex2]);
    }
}
