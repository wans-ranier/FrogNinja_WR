using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime;

    private TargetJoint2D target; //manipula o movimento da plataforma
    private BoxCollider2D boxColl; //manipula o contato fisico dele

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") //se tocar na tag "Player" -> função de cair
        {
            Invoke("Falling", fallingTime);
        }
    }

    void Falling()
    {
        target.enabled = false; // cansela seu movimento
        boxColl.isTrigger = true; // fica intangível
    }
}
