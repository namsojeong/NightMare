using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    [SerializeField, Header("MAX HP")]
    float maxDark;

    [SerializeField, Header("HP SLIDER")]
    Image darkImage;

    float dark=50f;
    private void Update()
    {
        UpdateDark();
    }

    private void Start()
    {
        InvokeRepeating("UpDark", 1f, 1f);
        EventManager.StartListening("PLUSHP", PlusHP);
        EventManager.StartListening("MINUSHP", MinusHP);
    }
    private void OnDestroy()
    {
        EventManager.StopListening("PLUSHP", PlusHP);
        EventManager.StopListening("MINUSHP", MinusHP);
    }
    void UpDark()
    {
        dark += 1;
        if (dark >= 100)
        {
            SceneMg.Instance().ChangeScene("GameOver");
        }
    }
    void UpdateDark()
    {
        darkImage.fillAmount = Mathf.Lerp(darkImage.fillAmount, dark / maxDark, Time.deltaTime);
    }

    void MinusHP(EventParam eventParam)
    {
        dark -= eventParam.intParam;
        if (dark <= 0)
            dark = 0;
    }
    void PlusHP(EventParam eventParam)
    {
        dark += eventParam.intParam;
        if(dark >= 100)
        {
            SceneMg.Instance().ChangeScene("GameOver");
        }
    }
}
