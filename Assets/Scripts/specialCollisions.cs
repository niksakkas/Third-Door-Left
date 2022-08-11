using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections;

public class specialCollisions : MonoBehaviour
{
    
    public Animator playerAnimator;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "cookie")
        {
            Destroy(collision.gameObject);
        }
        if (collision.collider.tag == "spikes")
        {
            StartCoroutine(YOUDIED());
        }
    }

    IEnumerator YOUDIED()
    {
        playerAnimator.SetBool("playerDead", true);
        Debug.Log("YOU DIED");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }

}
