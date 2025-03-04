using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class VaiSaw : MonoBehaviour
{
    
    public float speed;
    public float MoveTime;
    private bool dirLeft = true;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        if (dirLeft == true)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }    else{
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            
        }
        timer = timer + Time.deltaTime;
        if (timer >= MoveTime)
        { 
            dirLeft = !dirLeft;
            timer = 0f; 
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
        Debug.Log("MORREU!");
        }
    }
}
