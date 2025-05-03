using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movableblocks : MonoBehaviour
{
    BoxCollider2D col;
    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "spike")
        {
            col.enabled = false;
        }
    }
}
