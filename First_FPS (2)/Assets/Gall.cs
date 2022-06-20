using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gall : MonoBehaviour
{
    MeshRenderer meshRenderer;

    EventParam eventParam = new EventParam();
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("BALL"))
        {
            collision.gameObject.SetActive(false);
            GameMg.Instance().DisplayScore(500);
            eventParam.intParam = 50;
            EventManager.TriggerEvent("MINUSHP", eventParam);
            meshRenderer.material.color = Color.yellow;
            StartCoroutine(GallIn());
        }
    }
    IEnumerator GallIn()
    {
        yield return new WaitForSeconds(1f);
            meshRenderer.material.color = Color.white;
    }
}
