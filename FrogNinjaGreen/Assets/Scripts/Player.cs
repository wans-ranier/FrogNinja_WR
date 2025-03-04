using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D rig;

    private Animator anim;
    public float JumpForce;

    public bool isJumping;
    public bool doubleJump;
    public bool onTrampoline;
    public Vector2 lastSafePosition; // Corrigido para lastSafePosition

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastSafePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckFalling();
    }

    void Move()
    {
        Vector3 movimentar = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movimentar * Time.deltaTime * Speed;

        if (Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if (Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }

    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                doubleJump = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if (doubleJump && !onTrampoline)
                {
                    rig.AddForce(new Vector2(0f, JumpForce * 1.5f), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
            anim.SetBool("fall", false); // Desativa a animação de queda
            lastSafePosition = transform.position; // Corrigido para lastSafePosition
        }
        if (collision.gameObject.tag == "Spike")
        {
            Destroy(gameObject);
            GameController.instance.ShowGameOver();
            Debug.Log("MORREU!");
        }
        if (collision.gameObject.tag == "Saw")
        {
            Destroy(gameObject);
            GameController.instance.ShowGameOver();
            Debug.Log("MORREU!");
        }
        if (collision.gameObject.tag == "Trampoline")
        {
            onTrampoline = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
        if (collision.gameObject.tag == "Trampoline")
        {
            onTrampoline = false;
        }
    }

    void CheckFalling()
    {
        if (rig.linearVelocity.y < 0 && isJumping)
        {
            anim.SetBool("fall", true); // Ativa a animação de queda
        }
        else
        {
            anim.SetBool("fall", false); // Desativa a animação de queda
        }
    }

    void CheckIfStuck()
    {
        if (rig.linearVelocity.magnitude < 0.1f && isJumping)
        {
            // Se o jogador estiver preso, reposicione-o na última posição segura
            transform.position = lastSafePosition; // Corrigido para lastSafePosition
        }
    }
}
