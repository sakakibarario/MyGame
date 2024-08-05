using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCircle : MonoBehaviour
{
 
    public void CircleDelete()
    {
        //Debug.Log("デストロイ");
        Destroy(this.gameObject);
    }
}
