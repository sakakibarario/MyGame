using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomObj : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;

    //����
    public enum effect{
        DEBUFF,
        BUFF,
        RECOVERY,
        ATTACK,
        NORMAL
    }

    public int Effects = 0;


    public UnityEvent OnDestroyed = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        int rnd = Random.Range(1, 4);// �����_���Ȑ����𐶐�

        if (rnd == 1 || rnd == 2)
        {
            //�z���C�g�m�[�}��
            SpriteRenderer.color = new Color32(255, 255, 255, 255);
            Effects = (int)effect.NORMAL;
        }
        if(rnd >= 3 && rnd <= 6)
        {
            //���b�h�U��
            SpriteRenderer.color = new Color32(255, 0, 0, 255);
            Effects = (int)effect.ATTACK;
        }
        if (rnd == 7)
        {
            //�C�G���[�o�t
            SpriteRenderer.color = new Color32(255, 255, 0, 255);
            Effects = (int)effect.BUFF;
        }
        if (rnd == 8 || rnd == 9)
        {
            //�O���[����
            SpriteRenderer.color = new Color32(0, 255, 90, 255);
            Effects = (int)effect.RECOVERY;
        }
        if (rnd == 10)
        {
            //�p�[�v���f�o�t
            SpriteRenderer.color = new Color32(160, 0, 255, 255);
            Effects = (int)effect.DEBUFF;
        }

          
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (GameManager.GState == "Playing" || GameManager.GState == "PvE")
        {
            switch (Effects)
            {
                case (int)effect.DEBUFF:
                    FindObjectOfType<CheckObj>().DebuffEffect();
                    break;
                case (int)effect.BUFF:
                    FindObjectOfType<CheckObj>().BuffEffect();
                    break;
                case (int)effect.RECOVERY:
                    FindObjectOfType<CheckObj>().RecoveryEffect();
                    break;
                case (int)effect.ATTACK:
                    FindObjectOfType<CheckObj>().AttackEffect();
                    break;
                case (int)effect.NORMAL:
                    FindObjectOfType<CheckObj>().NormalEffect();
                    break;
                default:
                    Debug.Log("�͈͊O");
                    break;
            }
        }
      
    }
}
