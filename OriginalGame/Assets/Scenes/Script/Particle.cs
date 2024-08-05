using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Particle : MonoBehaviour
{
    public GameObject[] effectsList;
    [System.NonSerialized] public GameObject currentEffect;

    private bool ClearFlag = false;
    private bool GOverFlag = false;

    public enum Effect
    {
        Impulse,
        Debuff,
        Clear,
        GOver,
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        //�����t���O
        if(ClearFlag)
        {
            //X���W���w��͈̔͂��烉���_���Ŏ擾
            int Posx = Random.Range(0, 10);
            //�ԉΐ���
            Instantiate(effectsList[(int)Effect.Clear], new Vector2(Posx,this.transform.position.y), Quaternion.identity);
        }
        //GameOver�t���O
        if(GOverFlag)
        {
            //X���W���w��͈̔͂��烉���_���Ŏ擾
            int Posx = Random.Range(0, 10);
            //�鐐���
            Instantiate(effectsList[(int)Effect.GOver], new Vector2(Posx, this.transform.position.y), Quaternion.identity);
        }
    }

    public void EffectImpulse(float x, float y)
    {
        //�_���[�W�G�t�F�N�g
        Instantiate(effectsList[(int)Effect.Impulse], new Vector2(x, y), Quaternion.identity);
    }

    public void EffectClear()
    {
        //�ԉ΃G�t�F�N�g�R���[�`��
        StartCoroutine(ClearEffect());   
    }
    IEnumerator ClearEffect()
    {
        ClearFlag = true;

        yield return new WaitForSeconds(0.05f);
        ClearFlag = false;

        yield return new WaitForSeconds(0.7f);
        yield return StartCoroutine(ClearEffect());

        yield break;
    }

    public void EffectGOver()
    {
        //�鐃G�t�F�N�g�R���[�`��
        StartCoroutine(GOverEffect());
    }
    IEnumerator GOverEffect()
    {
        GOverFlag = true;

        yield return new WaitForSeconds(0.05f);
        GOverFlag = false;

        yield return new WaitForSeconds(0.7f);
        yield return StartCoroutine(ClearEffect());

        yield break;
    }

    public void EffectDebuff(float x,float y)
    {
        //�f�o�t�G�t�F�N�g
        Instantiate(effectsList[(int)Effect.Debuff], new Vector2(x, y), Quaternion.identity);
    }

}

