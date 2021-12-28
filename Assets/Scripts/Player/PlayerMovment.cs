using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    private int playerSize = 4;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;

    [SerializeField] private AudioClip jump;

    private void Awake()
    {
        //Pega referências de objetos
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        //troca onde o jogador está olhando
        if (horizontalInput > 0.01f)
            transform.localScale = new Vector3(playerSize, playerSize, playerSize);
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-playerSize, playerSize, playerSize);

        //seta parâmetros de movimentação
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        if(!onWall())
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        }
           

        if (onWall() && !isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y);
        }

        if (Input.GetKey(KeyCode.UpArrow) && isGrounded())
            Jump();
        }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
