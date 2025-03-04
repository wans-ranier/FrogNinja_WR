using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class testeSaw  : MonoBehaviour
{
    public float JumpForce;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
        
    }
}
