using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
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
    private float minInterval = 0.04f; //if the interval is smaller than this, it's propably because of a collision and the
                                       //player's sprite shouldnt flip even though he is moving in the opposite direction
    //sound                                   
    public string robotDeathSoundPath = "event:/robot_shutting_down";
    public fmodPlayer soundScript;                                       

    List<Vector2> ghostPositions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        cookieList = FindObjectsOfType<cookieHandler>();
        StartCoroutine(handleCalculatingSound());
    }

    // Update is called once per frame
    private void Update()
    {
        
        //check if game started
        if (Input.GetKeyDown(KeyCode.Space) && ghostStarted == true)
        {
            ghostEnded = true;
            playerAnimator.SetBool("ghostEnded", true);

        }
        //check if player wants to restart
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Restarting");
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);

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
            // MovePlayer();
        }
    }

    void MovePlayer()
    {
        if (playerAnimator.GetBool("playerDead") == true)
        {
            return;
        }
        //move the player to mimic the ghosts movement
        if (ghostPositions.Count > 0)
        {
            HandlePlayerAnimation();
            player.position = ghostPositions[0];
            ghostPositions.RemoveAt(0);
        }
        else
        {
            Debug.Log("Failed, press R to try again");
        }
        //Destroy(ghostPlayer);
        if (FindObjectsOfType<cookieHandler>().Length == 0)
        {
            Debug.Log("GG EZ");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    void HandlePlayerAnimation()
    {
        Vector2 interval = ghostPositions[0] - player.position;
        if (interval.x != 0)
        {
            playerAnimator.SetInteger("direction", 0);
            playerAnimator.SetFloat("speed", 10);
            if (interval.x > minInterval && !playerFacingRight)
            {
                FlipPlayer();
            }
            if (interval.x < -0.04 && playerFacingRight)
            {
                FlipPlayer();
            }
        }
        else if (interval.y != 0)
        {
            if (interval.y > 0.04)
            {
                playerAnimator.SetInteger("direction", 1);
                playerAnimator.SetFloat("speed", 10);
            }
            if (interval.y < -0.04)
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
    IEnumerator handleCalculatingSound()
    {
        yield return new WaitUntil(()=> ghostStarted == true);
        soundScript.playSound(robotDeathSoundPath);
    }
}



