using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallKillAll : MonoBehaviour
{
    [SerializeField] Health[] enemyes;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            bool allDead = true;
            foreach (Health health in enemyes)
            {

                if (!health.dead)
                {
                    allDead = false;
                }
            }

            if (allDead)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                anim.SetTrigger("disable");
            }
        }
    }
}
