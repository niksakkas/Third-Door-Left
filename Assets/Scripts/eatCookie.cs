using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eatCookie : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "cookie")
        {
            Destroy(collision.gameObject);
        }
    }
}
