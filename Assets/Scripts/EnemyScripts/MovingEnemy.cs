using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField]
    private int horizantalSpeed = 0, verticalSpeed = 0;
    private int  speedX,speedY;

    [SerializeField]
    private float minX,maxX,minY,maxY;

    void Start()
    {
        if (horizantalSpeed < 0)
            horizantalSpeed = -horizantalSpeed;
        if (verticalSpeed < 0)
            verticalSpeed = -verticalSpeed;

        speedX = horizantalSpeed;
        speedY = verticalSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < minX)
            speedX = horizantalSpeed;
        if (transform.position.x > maxX )
            speedX = -horizantalSpeed;
        if (transform.position.y < minY)
            speedY = verticalSpeed;
        if (transform.position.y > maxY)
            speedY = -verticalSpeed;



        transform.Translate(new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime, 0f));
    }
}
