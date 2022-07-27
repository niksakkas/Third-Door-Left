using UnityEngine.SceneManagement;
using UnityEngine;

public class specialCollisions : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "cookie")
        {
            Destroy(collision.gameObject);
        }
        if (collision.collider.tag == "spikes")
        {
            YOUDIED();
        }
    }
    private void YOUDIED()
    {
        Debug.Log("rip");
        Destroy(gameObject);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().path);
    }
}
