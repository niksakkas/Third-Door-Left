using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class globalController : MonoBehaviour
{
    public cookieHandler[] cookieList;
    public Rigidbody2D ghostPlayer;
    public Rigidbody2D player;
    public Animator playerAnimator;
    private bool ghostStarted = false;
    private bool ghostEnded = false;
    private bool playerFacingRight = false;

    List<Vector2> ghostPositions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        cookieList = FindObjectsOfType<cookieHandler>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && ghostStarted == true)
        {
            ghostEnded = true;
            playerAnimator.SetBool("ghostEnded", true);

        }

    }

    void FixedUpdate()
    {
        if ((Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) && ghostStarted == false)
        {
            //game starts
            ghostStarted = true;
            playerAnimator.SetBool("ghostStarted", true);
        }
        if (ghostStarted == true && ghostEnded == false)
        {
            //record user's movement
            ghostPositions.Add(ghostPlayer.position);
        }
        if (ghostEnded == true)
        {
            MovePlayer();
        }
    }
    void MovePlayer(){

        //move the player to mimic the ghosts movement
        if (ghostPositions.Count > 1)
        {
            HandlePlayerAnimation();
            player.position = ghostPositions[0];
            ghostPositions.RemoveAt(0);
        }
        //Destroy(ghostPlayer);
        if (FindObjectsOfType<cookieHandler>().Length == 0)
        {
            Debug.Log("You won!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void HandlePlayerAnimation()
    {
        Vector2 interval = ghostPositions[0] - player.position;
        if(interval.x != 0)
        {
            playerAnimator.SetInteger("direction", 0);
            playerAnimator.SetFloat("speed", 10);
            if (interval.x > 0 && !playerFacingRight)
            {
                FlipPlayer();
            }
            if (interval.x < 0 && playerFacingRight)
            {
                FlipPlayer();
            }
        }
        else if (interval.y != 0)
        {
            if (interval.y > 0)
            {
                playerAnimator.SetInteger("direction", 1);
                playerAnimator.SetFloat("speed", 10);
            }
            if (interval.y < 0)
            {
                playerAnimator.SetInteger("direction", -1);
                playerAnimator.SetFloat("speed", 10);
            }
        }
        else
        {
            playerAnimator.SetFloat("speed", 0);
        }
    }
    private void FlipPlayer()
    {
        // Switch the way the player is labelled as facing.
        playerFacingRight = !playerFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = player.transform.localScale;
        theScale.x *= -1;
        player.transform.localScale = theScale;
    }
}
