using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RandomObj : MonoBehaviour
{
    SpriteRenderer SpriteRenderer;

    //効果
    public enum effect{
        DEBUFF,
        BUFF,
        RECOVERY,
        ATTACK,
        NORMAL
    }

    private int Effects = 0;


    public UnityEvent OnDestroyed = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();

        int rnd = Random.Range(0, 11);// ランダムな整数を生成

        if (rnd >= 0 && rnd <= 6)
        {
            //ブルー
            SpriteRenderer.color = new Color32(40, 0, 255, 255);
            Effects = (int)effect.NORMAL;
        }
        if(rnd == 7)
        {
            //レッド
            SpriteRenderer.color = new Color32(255, 0, 0, 255);
            Effects = (int)effect.ATTACK;
        }
        if (rnd == 8)
        {
            //イエロー
            SpriteRenderer.color = new Color32(255, 255, 0, 255);
            Effects = (int)effect.BUFF;
        }
        if (rnd == 9)
        {
            //グリーン
            SpriteRenderer.color = new Color32(0, 255, 90, 255);
            Effects = (int)effect.RECOVERY;
        }
        if (rnd == 10)
        {
            //パープル
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
        Debug.Log("Destroyed");
        switch(Effects){
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
                Debug.Log("範囲外");
                break;
        }
      
    }
}
