using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sds : MonoBehaviour
{
    public Animator myAnimator;
    public int speed = 1;
    private bool isJumping = false;
    private Rigidbody2D rb;
    public float jumpForce = 3f;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator.SetBool("move", false);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        float direction = Input.GetAxis("Horizontal");
        if (direction > 0)
        {
            transform.localScale = new Vector3(2, 2, 2);
            myAnimator.SetBool("move", true);
        }
        else if (direction < 0)
        {
            transform.localScale = new Vector3(-2, 2, 2);
            myAnimator.SetBool("move", true);
        }
        else
            myAnimator.SetBool("move", false);

        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
    }
    void Jump()
    {
        isJumping = true; // 점프 중 상태로 변경
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // 위로 힘을 줌
        myAnimator.SetTrigger("Jump"); // 점프 애니메이션 트리거
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅에 닿으면 점프가 끝난 것으로 설정
        isJumping = false;
    }
}
