using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTutorial : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BULLET"))
        {
            TutorialGoalin.instance.BulletTriger();
        }
        if(collision.collider.CompareTag("GOALIN"))
        {
                TutorialGoalin.instance.IsGameOver();
        }
    }
}
