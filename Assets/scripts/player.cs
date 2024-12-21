using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;


public class player : MonoBehaviour
{
    GameManager gameManager;
    public Transform gridm; 
    BoxCollider2D col;
    //Rigidbody2D _rb;
    Camera _camera;
    float inputh;
    float inputv;
    [SerializeField]float walk ;
    [SerializeField] float jump;
    Vector2 jump_sprint;
    Vector2 move;
    bool jumppossible;
    bool movepossiblity = true;
    public Queue<float> movesave = new Queue<float>();
    public Queue<float> movesave1 = new Queue<float>();
    
    float currentmove;
    void Start()
    {
        gridm.parent = null;
        //_rb = gameObject.GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        col = gameObject.GetComponent<BoxCollider2D>();
        movesave.Enqueue(0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, gridm.position, walk*Time.deltaTime);
        if (Vector3.Distance(transform.position, gridm.position) < 0.05f)
        {
            if (Mathf.Abs(UnityEngine.Input.GetAxisRaw("Horizontal")) == 1f && Mathf.Abs(UnityEngine.Input.GetAxisRaw("Vertical")) == 0f && movepossiblity)
            {
                StartCoroutine(stop());
                
                gridm.position += new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0f, 0f);
                movesave.Enqueue(UnityEngine.Input.GetAxisRaw("Horizontal"));

            }

            else if (Mathf.Abs(UnityEngine.Input.GetAxisRaw("Vertical")) == 1f && Mathf.Abs(UnityEngine.Input.GetAxisRaw("Horizontal")) == 0f && movepossiblity)
            {
                StartCoroutine(stop());
                gridm.position += new Vector3(0f, UnityEngine.Input.GetAxisRaw("Vertical"), 0f);
                movesave.Enqueue(UnityEngine.Input.GetAxisRaw("Vertical")*2);
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            gameManager.gameover();
         
        }
    }

    IEnumerator stop()
    {
        movepossiblity = false;
        yield return new WaitForSeconds(0.3f);
        movepossiblity = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if( collision.gameObject.tag == "ground")
        {
            jumppossible = true;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            jumppossible = false;
        }
    }
}
