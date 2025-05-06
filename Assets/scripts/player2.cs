using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public Animator animator;
    bool gamedone = true;
    public Transform gridm;
    player player;
    Rigidbody2D rb;
    float currentmove;
    public Queue<float> something = new Queue<float>();
    bool win = false;
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
        moveDir = Vector2.zero;
        dist.text = "x : " + ((int)(epw2.transform.position.x - transform.position.x)).ToString() +
                    " y : " + ((int)(epw2.transform.position.y - transform.position.y + 0.5f)).ToString();

        transform.position = Vector3.MoveTowards(transform.position, gridm.position, 20 * Time.deltaTime);

        if (player.p2move && !isMoving)
        {
            player.p2move = false;
            StartCoroutine(HandleMovement());
        }

        if (player.movesave.Count <= 0 && gameObject != null && gamedone && !win)
        {
            gameObject.SetActive(false);
            Debug.Log("U lose");
            gamedone = false;
        }
                if (moveDir != Vector2.zero)
        {
        Debug.Log(moveDir);

            Vector2 newPosition = Vector2.Lerp(rb.position, rb.position + moveDir * player.moveStep,player.walk * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }

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
                    gridm.position += new Vector3(currentmove, 0f, 0f);
                    a = currentmove;
                }
                else if (Mathf.Abs(currentmove) == 2f)
                {
                    moveDir = new Vector2(0, currentmove / 2);
                    gridm.position += new Vector3(0f, currentmove / 2, 0f);
                    b = currentmove / 2;
                }
            }
        }


        animator.SetFloat("horizontal", a);
        animator.SetFloat("vertical", b);
        animator.SetFloat("speed", moveDir.sqrMagnitude);

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
            win = true;
        }
    }
}