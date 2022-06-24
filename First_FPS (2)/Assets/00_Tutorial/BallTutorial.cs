using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTutorial : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BULLET"))
        {
            TutorialGallin.instance.BulletTriger();
        }
        if(collision.collider.CompareTag("GALLIN"))
        {
                TutorialGallin.instance.IsGameOver();
        }
    }
}
