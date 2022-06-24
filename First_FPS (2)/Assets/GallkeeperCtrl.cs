using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GallkeeperCtrl : MonoBehaviour
{
    [SerializeField]
    float speed;

    public List<Transform> points = new List<Transform>();

    Transform targetPos;
    private void Awake()
    {
        Transform movepos = GameObject.Find("MovePos")?.transform;

        foreach (Transform pos in movepos)
        {
            points.Add(pos);
        }

        MoveGallkeeper();
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime*speed);
    }
    void MoveGallkeeper()
    {
        int ran = Random.Range(0,100);

        if(ran<=50)
        {
        targetPos = points[0];
        }
        else
        {
        targetPos = points[1];
        }
        transform.DOMove(targetPos.position, speed * Vector3.Distance(transform.position, targetPos.position)).OnComplete(()=>MoveGallkeeper());
    }


}
