using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public Animator animator;
    bool gamedone = true;
    public Transform gridm;
    player player;
    float currentmove;
    public Queue<float> something = new Queue<float>();
    bool win = false;
    public Transform epw2;
    public TextMeshProUGUI dist;
    float a, b;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<player>();
        gridm.parent = null;
       
    }

    // Update is called once per frame
    async void Update()
    {
        dist.text = "x : " + ((int)(epw2.transform.position.x - transform.position.x)).ToString() + " y : " + ((int)(epw2.transform.position.y - transform.position.y + 0.5f)).ToString();
        transform.position = Vector3.MoveTowards(transform.position, gridm.position, 20 * Time.deltaTime);
        if (player.p2move)
        {
            player.p2move = false;
            while (player.movesave.Count > 0)
            {
                currentmove = player.movesave.Dequeue();

                await Task.Delay(300);
                if (gameObject.activeSelf)
                {
                    if (Mathf.Abs(currentmove) == 1f)
                    {
                        gridm.position += new Vector3(currentmove, 0f, 0f);
                        a = currentmove;

                    }
                    else if (Mathf.Abs(currentmove) == 2f)
                    {
                        gridm.position += new Vector3(0f, currentmove / 2, 0f);
                        b = currentmove/2;
                    }
                }
            }

            animator.SetFloat("horizontal", a);
            animator.SetFloat("vertical", b);
            animator.SetFloat("speed", a*a + b*b);

        }
        if (player.movesave.Count <= 0&& gameObject != null && gamedone && !win)
        {
            gameObject.SetActive(false);
            Debug.Log("U lose");
            gamedone = false ;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "endw2")
        {
            Debug.Log("U win");
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
            win = true;
        }
        if (collision.gameObject.tag == "spike")
        {
            gameObject.SetActive(false);
            Debug.Log("U lose");
            win = true;
        }

    }
}
