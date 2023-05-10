using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMgr : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject optionObj;

    

    public void ClickOption()
    {
        optionObj.SetActive(true);
    }

    public void ClickClose()
    {
        optionObj.SetActive(false);
    }

    public void ClickStartBtn()
    {
        SceneManager.LoadScene("2_LobbyScene");
    }
    
    

    
}
