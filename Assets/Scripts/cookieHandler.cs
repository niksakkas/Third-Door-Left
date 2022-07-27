using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cookieHandler : MonoBehaviour
{
    //adjust this to change speed
    float speed = 3f;
    public CircleCollider2D Cookiecollider;
    float maxDistance;
    float startPosY;

    private void Start()
    {
        // the max distance from the original position will be one sixth of the objects height
        maxDistance = Cookiecollider.bounds.size.y / 6;
        startPosY = transform.position.y;
    }

    void Update()
    {
        //calculate what the new Y position will be
        float change = Mathf.Sin(Time.time * speed) * maxDistance;
        //set the object's Y to the new calculated Y
        transform.position = new Vector2(transform.position.x, startPosY + change) ;
    }
}
