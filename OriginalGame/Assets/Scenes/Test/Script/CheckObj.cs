using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CheckObj : MonoBehaviour
{
    //RandomObj RandomObj;

    RandomObj RandomObj;
   
    // Start is called before the first frame update
    void Start()
    {
        RandomObj = GetComponent<RandomObj>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void DebuffEffect()
    {
        Debug.Log("Debuff");
    }

    public void BuffEffect()
    {
        Debug.Log("buff");
    }

    public void RecoveryEffect()
    {
        Debug.Log("Recovery");
    }

    public void AttackEffect()
    {
        Debug.Log("Attack");
    }

    public void NormalEffect()
    {
        Debug.Log("Normal");
    }

}
