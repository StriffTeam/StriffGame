using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    Image[] Img_Healts;

    [SerializeField]
    GameObject gameMenu;

    private static UIManager instance;

    public static UIManager _Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    public void HealtControl(int healt)
    {
        Img_Healts[healt - 1].fillAmount = 0;
        if (healt == 1)
        {
            OpenGameMenu();

        }
    }
   void OpenGameMenu()
    {
        gameMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
