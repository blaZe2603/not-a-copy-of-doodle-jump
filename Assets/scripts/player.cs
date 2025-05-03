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
    pos position_script;
    public bool movepossiblity = true;
    public Queue<float> movesave = new Queue<float>();
    public bool p2move = false;
    public bool p2camera = false;
    public Transform epw1;
    public TextMeshProUGUI dist;
     public LayerMask obstacleMask;     
    Vector2 a;
    public float moveStep = 1f; 
   
    private float lastMoveTime;
    void Start()
    {
        gridm.parent = null;
        rb = gameObject.GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        col = gameObject.GetComponent<BoxCollider2D>();
        movesave.Enqueue(0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        position_script = GameObject.Find("pos_p1").GetComponent<pos>();
       
    }

    void Update()
    {
        dist.text = "x : " + ((int)(epw1.transform.position.x - transform.position.x)).ToString() + " y : " + ((int)(epw1.transform.position.y - transform.position.y + 0.5f)).ToString();
        a.x = UnityEngine.Input.GetAxisRaw("Horizontal");
        a.y = UnityEngine.Input.GetAxisRaw("Vertical");
        Vector2 moveDir = Vector2.zero;
            if (Mathf.Abs(UnityEngine.Input.GetAxisRaw("Horizontal")) == 1f && Mathf.Abs(UnityEngine.Input.GetAxisRaw("Vertical")) == 0f && movepossiblity)
            {
                StartCoroutine(stop());
                TryEnqueueMove(new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), 0f), UnityEngine.Input.GetAxisRaw("Horizontal"),1f);
                moveDir = new Vector2(UnityEngine.Input.GetAxisRaw("Horizontal"), 0f);                
            }

            else if (Mathf.Abs(UnityEngine.Input.GetAxisRaw("Vertical")) == 1f && Mathf.Abs(UnityEngine.Input.GetAxisRaw("Horizontal")) == 0f && movepossiblity)
            {
                StartCoroutine(stop());
                TryEnqueueMove(new Vector2(UnityEngine.Input.GetAxisRaw("Vertical"), 0f), UnityEngine.Input.GetAxisRaw("Vertical"),2f);
                moveDir += new Vector2(0f, UnityEngine.Input.GetAxisRaw("Vertical"));
            }
        if (moveDir != Vector2.zero)
        {
            Vector2 newPosition = Vector2.Lerp(rb.position, rb.position + moveDir * moveStep,walk * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }

        void TryEnqueueMove(Vector2 direction, float moveValue,float value)
        {
            if (CanMove(direction))
            {
                movesave.Enqueue(moveValue*value);
            }
        }

        bool CanMove(Vector2 direction)
        {
            Vector2 origin = transform.position;
            Vector2 targetPos = origin + direction * (Mathf.Abs(direction.y) > 0 ? 0.5f : 1f); 
            Vector2 boxSize = new Vector2(0.9f, 0.9f);

            Collider2D hit = Physics2D.OverlapBox(targetPos, boxSize, 0f, obstacleMask);
            return hit == null;
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
