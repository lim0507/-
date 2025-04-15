using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    private bool isInvincible = false;
    private float invincibleDuration = 5f;

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Respawn"))
        {
            if (!isInvincible)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if(collision.CompareTag("Finish"))
        {
            collision.GetComponent<LevelObject>().MoveToNextLevel();
        }
        if(collision.CompareTag("Enemy"))
        {
            if (!isInvincible)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if (collision.CompareTag("InvincibleItem"))  
        {
            StartCoroutine(ActivateInvincibility());
            Destroy(collision.gameObject); 
        }
        if (collision.CompareTag("SpeedItem"))
        {
            moveSpeed += 3f; 
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("JumpItem"))
        {
            jumpForce += 3f;
            Destroy(collision.gameObject);
        }
    }
    private System.Collections.IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
       
        GetComponent<SpriteRenderer>().color = Color.yellow;

        yield return new WaitForSeconds(invincibleDuration);

        isInvincible = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
