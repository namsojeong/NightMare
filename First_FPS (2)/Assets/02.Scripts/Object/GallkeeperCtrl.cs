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
    void MoveGallkeeper()
    {
        int ran = Random.Range(0,100);
        if(ran<=30)
        {
        targetPos = points[0];
        }
        else if(ran<=50)
        {
        targetPos = points[1];
        }
        else if(ran<=80)
        {
        targetPos = points[1];
        }
        else
        {
            targetPos = transform;
        }
        transform.DOMove(targetPos.position, 0.5f*Vector3.Distance(transform.position, targetPos.position)).OnComplete(()=>MoveGallkeeper());
    }


}
