using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class player : MonoBehaviour
{
    public Animator animator;
    GameManager gameManager;
    BoxCollider2D col;
    Rigidbody2D rb;
    Camera _camera;

    public GameObject targetObject;
    public float walk ;
    player2 p_2;
    
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
        p_2 = targetObject.GetComponent<player2>();  
        rb = gameObject.GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        col = gameObject.GetComponent<BoxCollider2D>();
        movesave.Enqueue(0);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
       
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
                TryEnqueueMove(new Vector2(0f, UnityEngine.Input.GetAxisRaw("Vertical")), UnityEngine.Input.GetAxisRaw("Vertical"),2f);
                moveDir = new Vector2(0f, UnityEngine.Input.GetAxisRaw("Vertical"));
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

        //checking if the movement is possible or not
        bool CanMove(Vector2 direction)
        {
            Vector2 origin = transform.position;
            Vector2 targetPos = origin + direction ; 
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
    
    //to have time between movements to make it look discrete
    IEnumerator stop()
    {
        movepossiblity = false;
        yield return new WaitForSeconds(0.3f);
        movepossiblity = true;
    }

    //the collisions
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
            p_2.lose = true;
            Debug.Log("U lose");
        }

    }

}
