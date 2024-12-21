using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class player2 : MonoBehaviour
{
    public Transform gridm;
    bool waitforsometimepls = true;
    bool plswait = true;
    player player;
    float currentmove;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<player>();
        gridm.parent = null;
    }

    // Update is called once per frame
    async void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, gridm.position, 20 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Z))
        {

            while (player.movesave.Count > 0)
            {
                currentmove = player.movesave.Dequeue();
                await Task.Delay(500);
                if (Mathf.Abs(currentmove) == 1f && waitforsometimepls)
                {
                    gridm.position += new Vector3(currentmove, 0f, 0f);

                }
                else if (Mathf.Abs(currentmove) == 2f && waitforsometimepls)
                {
                    gridm.position += new Vector3(0f, currentmove / 2, 0f);

                }
            }

        }
    }
    IEnumerator waiting()
    {
        waitforsometimepls = false;
        yield return new WaitForSeconds(0.5f);
        plswait = true;
        waitforsometimepls = true;
        
    }
}
