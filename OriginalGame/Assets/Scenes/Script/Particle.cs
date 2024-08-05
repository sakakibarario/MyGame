using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Particle : MonoBehaviour
{
    public GameObject[] effectsList;
    [System.NonSerialized] public GameObject currentEffect;

    private bool ClearFlag = false;
    private bool GOverFlag = false;

    AudioSource AudioSource;
    public AudioClip HnabiSound;

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
        AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        //勝利フラグ
        if(ClearFlag)
        {
            //X座標を指定の範囲からランダムで取得
            int Posx = Random.Range(0, 10);
            //花火生成
            Instantiate(effectsList[(int)Effect.Clear], new Vector2(Posx,this.transform.position.y), Quaternion.identity);
            AudioSource.PlayOneShot(HnabiSound, 0.6f);
        }
        //GameOverフラグ
        if(GOverFlag)
        {
            //髑髏生成
            Instantiate(effectsList[(int)Effect.GOver], new Vector2(4.3f, 8), Quaternion.identity);
        }
    }

    public void EffectImpulse(float x, float y)
    {
        //ダメージエフェクト
        Instantiate(effectsList[(int)Effect.Impulse], new Vector2(x, y), Quaternion.identity);
    }

    public void EffectClear()
    {
        //花火エフェクトコルーチン
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
        //髑髏エフェクトコルーチン
        StartCoroutine(GOverEffect());
    }
    IEnumerator GOverEffect()
    {
        GOverFlag = true;

        yield return new WaitForSeconds(0.05f);
        GOverFlag = false;

        yield return new WaitForSeconds(0.7f);
        yield return StartCoroutine(GOverEffect());

        yield break;
    }

    public void EffectDebuff(float x,float y)
    {
        //デバフエフェクト
        Instantiate(effectsList[(int)Effect.Debuff], new Vector2(x, y), Quaternion.identity);
    }

}

