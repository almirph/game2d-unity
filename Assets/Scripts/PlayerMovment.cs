using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private int playerSize = 4;
    [SerializeField] private float speed;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;


    private void Awake()
    {
        // Referencias pelo Rigidbody para Objeto
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //Troca onde o player está virado
        if (horizontalInput > 0.001f)
            transform.localScale = transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        else if (horizontalInput < -0.001f)
            transform.localScale = new Vector3(-playerSize, playerSize, playerSize);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        //Set animator parâmetros
        anim.SetBool("run", horizontalInput != 0);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        grounded = false;
        anim.SetTrigger("jump");
        anim.SetBool("grounded", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            anim.SetBool("grounded", true);
        }   
    }
}
