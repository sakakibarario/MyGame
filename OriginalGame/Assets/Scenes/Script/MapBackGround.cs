using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBackGround : MonoBehaviour
{
    int MapX = 10;
    int MapY = 20;
    public GameObject Cell;
    // Start is called before the first frame update
    void Start()
    {
        for(int y = 0; y < MapY;y++)
        {
            for(int x = 0; x< MapX;x++)
            {
                Instantiate(Cell, new Vector2(transform.position.x + x, transform.position.y + y), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
