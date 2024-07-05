using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CircleDelete()
    {
        Debug.Log("デストロイ");
        Destroy(this.gameObject);
    }
}
