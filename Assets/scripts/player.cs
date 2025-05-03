using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

//using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;


public class player : MonoBehaviour
{
    public Animator animator;
    GameManager gameManager;
    public Transform gridm; 
    BoxCollider2D col;
    Rigidbody2D rb;
    Camera _camera;
    float inputh;
    float inputv;
    [SerializeField]float walk ;
    bool movepossiblity = true;
    public Queue<float> movesave = new Queue<float>();
    public bool p2move = false;
    public bool p2camera = false;
    public Transform epw1;
    public TextMeshProUGUI dist;
    Vector2 a;
    void Start()
    {
        gridm.parent = null;
        rb = gameObject.GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        col = gameObject.GetComponent<BoxCollider2D>();
        movesave.Enqueue(0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        dist.text = "x : " + ((int)(epw1.transform.position.x - transform.position.x)).ToString() + " y : " + ((int)(epw1.transform.position.y - transform.position.y + 0.5f)).ToString();
        a.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        a.y = UnityEngine.Input.GetAxisRaw("Vertical");
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
        if (Input.GetKeyDown(KeyCode.Z))
        {
            gameManager.gameover();
        }
        animator.SetFloat("horizontal", a.x);
        animator.SetFloat("vertical", a.y);
        animator.SetFloat("speed", a.sqrMagnitude);
    }
    

    IEnumerator stop()
    {
        movepossiblity = false;
        yield return new WaitForSeconds(0.3f);
        movepossiblity = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "end")
        {
            gameManager.gameover();
            p2move = true;
        }
        if (collision.gameObject.tag == "spike")
        {
            Destroy(gameObject);
            Debug.Log("U lose");
        }
        if(collision.gameObject.tag == ("Block"))
        {
            // Stop movement to prevent overlap
            rb.velocity = Vector2.zero;
        }

    }
    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "ground")
    //    {
    //        jumppossible = false;
    //    }
    //}
}
