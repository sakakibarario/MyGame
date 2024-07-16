using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMino : MonoBehaviour
{
    public GameObject[] Minos;

    public static GameObject clonedObject;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("NewMino", 1.0f);
    }

    public void NewMino()
    {
        //MinoÇÃê∂ê¨
        clonedObject = Instantiate(Minos[Random.Range(0, Minos.Length)], new Vector2(transform.position.x, transform.position.y + 2.0f), Quaternion.identity);
        
    }

    public void HoldMino()
    {
        Mino clonedMino = clonedObject.GetComponent<Mino>();
        clonedMino.enabled = false;
        clonedObject.transform.position = new Vector2(transform.position.x, transform.position.y + 2.0f);
    }
}
