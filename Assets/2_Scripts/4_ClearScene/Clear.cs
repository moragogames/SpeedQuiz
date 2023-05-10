using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    [SerializeField] GameObject checkBtn;
    [SerializeField] GameObject BonusBtn;
    void Start()
    {
        checkBtn.SetActive(false);
        BonusBtn.GetComponent<Image>().color = Color.white;
    }
    bool isBonus = false;
    public void ClickBonus()
    {
        if (isBonus)
        {
            return;
        }
        else
        {
            isBonus = true;
            User.Instance.myCoin += 15;
            checkBtn.SetActive(true);
            BonusBtn.GetComponent<Image>().color = new Color32(154, 154, 154, 255);
        }
    }
}
