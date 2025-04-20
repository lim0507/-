using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTraceController : MonoBehaviour
{
    public float moveSpeed = .5f;
    public float raycastDistance = .2f;
    public float traceDistance = 2f;

    private Transform player;
    private Animator animator;
    private Vector3 lastPosition;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    // Update is called once per frame
   private void Update()
    {
        Vector2 direction = player.position - transform.position;

        if (direction.magnitude > traceDistance)
        {
            SetIdle();
            return;
        }
        Vector2 directionNormalized = direction.normalized;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, directionNormalized, raycastDistance);
        Debug.DrawRay(transform.position, directionNormalized * raycastDistance, Color.red);

        foreach (RaycastHit2D rHit in hits)
        {
            if (rHit.collider != null && rHit.collider.CompareTag("Obstacle"))
            {
                Vector3 alternativeDirection = Quaternion.Euler(0f, 0f, -90f) * direction;
                transform.Translate(alternativeDirection * moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.Translate(direction * moveSpeed * Time.deltaTime);
            }
        }
        UpdateAnimation();
    }
    private void UpdateAnimation()
    {
        float movedDistance = (transform.position - lastPosition).magnitude;

        // 이동 여부에 따라 애니메이션 변경
        if (movedDistance > 0.001f)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        lastPosition = transform.position;
    }
    private void SetIdle()
    {
        animator.SetBool("isMoving", false);
        lastPosition = transform.position;
    }
}
