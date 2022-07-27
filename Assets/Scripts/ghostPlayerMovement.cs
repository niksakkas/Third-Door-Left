using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostPlayerMovement : MonoBehaviour
{
    public Rigidbody2D m_Rigidbody2D;
    public Animator animator;
    private float speed = 4.5f;
    private float horizontalMove = 0f;
    private float verticalMove = 0f;
    bool facing_right = false;

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
    }

    private void FixedUpdate()
    {
        Move(horizontalMove, verticalMove);
    }

    public void Move(float horizontalMove, float verticalMove)
    {
        Vector2 currentSpeed = m_Rigidbody2D.velocity;
        
        //if the ghost is moving on y, it cant move on x aswell
        if (currentSpeed.y != 0)
        {
            horizontalMove = 0;

            animator.SetFloat("speed", Mathf.Abs(verticalMove));
        }

        //if the ghost is moving on x, it cant move on y aswell
        if (currentSpeed.x != 0)
        {
            verticalMove = 0;

            animator.SetFloat("speed", Mathf.Abs(horizontalMove));

        }

        if (verticalMove > 0)
        {
            animator.SetInteger("direction", 1);
        }
        if (verticalMove < 0)
        {
            animator.SetInteger("direction", -1);
        }
        if (horizontalMove != 0)
        {
            animator.SetInteger("direction", 0);
            if(horizontalMove > 0 && !facing_right)
            {
                Flip();
            }
            if (horizontalMove < 0 && facing_right)
            {
                Flip();
            }

        }
        Vector3 targetVelocity = new Vector2(horizontalMove, verticalMove);
        m_Rigidbody2D.velocity = targetVelocity;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facing_right = !facing_right;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
