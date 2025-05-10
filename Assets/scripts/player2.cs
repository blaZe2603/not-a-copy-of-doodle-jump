using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public Animator animator;
    bool gamedone = false;
    public Transform gridm;
    player player;
    Rigidbody2D rb;
    float currentmove;
    public Queue<float> something = new Queue<float>();
    public bool win =false;
    public bool lose = false;
    public Transform epw2;
    public TextMeshProUGUI dist;
    float a, b;
    bool isMoving = false;
    public LayerMask obstacleMask; 
    Vector2 moveDir;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<player>();
        gridm.parent = null;
    }

    void Update()
    {
        
        dist.text = "x : " + ((int)(epw2.transform.position.x - transform.position.x)).ToString() +
                    " y : " + ((int)(epw2.transform.position.y - transform.position.y + 0.5f)).ToString();

        
        if (player.p2move && !isMoving)
        {
            player.p2move = false;
            StartCoroutine(HandleMovement());
        }
        if(player.movesave.Count == 0)
        {
            StartCoroutine(wait());
        }

        if (moveDir != Vector2.zero)
        {
            Vector2 newPosition = Vector2.Lerp(rb.position, rb.position + moveDir * player.moveStep,player.walk * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
            moveDir = Vector2.zero;
        }
        if (player.movesave.Count <= 0 && gameObject != null && gamedone && !win)
        {
            Debug.Log("U lose");
            lose = true;
        }
    }
IEnumerator wait()
{
    
    yield return new WaitForSeconds(0.5f);
    gamedone = true;
}
IEnumerator HandleMovement()
{
    isMoving = true;

    while (player.movesave.Count > 0)
    {
        currentmove = player.movesave.Dequeue();
        yield return new WaitForSeconds(0.3f);

        if (gameObject.activeSelf)
        {
            if (Mathf.Abs(currentmove) == 1f)
            {
                moveDir = new Vector2(currentmove, 0);
                a = currentmove;
                b = 0f; // reset vertical
            }
            else if (Mathf.Abs(currentmove) == 2f)
            {
                moveDir = new Vector2(0, currentmove / 2);
                b = currentmove / 2;
                a = 0f; // reset horizontal
            }

            animator.SetFloat("horizontal", (moveDir * player.moveStep).x);
            animator.SetFloat("vertical",(moveDir * player.moveStep).y);
            animator.SetFloat("speed",(moveDir * player.moveStep).sqrMagnitude);
        }
    }

    isMoving = false;
}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("endw2"))
        {
            Debug.Log("U win");
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            win = true;
        }

        if (collision.gameObject.CompareTag("spike"))
        {
            Debug.Log("U lose");
            gameObject.SetActive(false);
            lose = true;
        }
    }
}