using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingEnemy : MonoBehaviour
{
    [SerializeField]
    private float angularVelocity = 100;

    private AudioSource soundFx;

    // Start is called before the first frame update
    void Start()
    {
        soundFx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,angularVelocity*Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundFx.Play();
        }
    }
}
