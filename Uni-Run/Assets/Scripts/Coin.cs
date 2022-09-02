using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Vector2 poolPosition = new Vector2(0, -25);

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            gameObject.transform.position = poolPosition;
            GameManager.instance.AddScore(1);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Dead")
        {
            gameObject.transform.position = poolPosition;
        }
    }
}
