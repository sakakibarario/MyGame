using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Particle : MonoBehaviour
{
    public GameObject[] effectsList;
    [System.NonSerialized] public GameObject currentEffect;

    private bool ClearFlag = false;

    public enum Effect
    {
        Impulse,
        Debuff,
        Clear,
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            EffectClear();
        }
        if(ClearFlag)
        {
            //XÀ•W‚ğw’è‚Ì”ÍˆÍ‚©‚çƒ‰ƒ“ƒ_ƒ€‚Åæ“¾
            int Posx = Random.Range(0, 10);
            //‰Ô‰Î¶¬
            Instantiate(effectsList[(int)Effect.Clear], new Vector2(Posx,this.transform.position.y), Quaternion.identity);
        }
    }

    public void EffectImpulse(float x, float y)
    {
        Instantiate(effectsList[(int)Effect.Impulse], new Vector2(x, y), Quaternion.identity);
    }

    public void EffectClear()
    {
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

    public void EffectDebuff(float x,float y)
    {
        Instantiate(effectsList[(int)Effect.Debuff], new Vector2(x, y), Quaternion.identity);
    }

}

