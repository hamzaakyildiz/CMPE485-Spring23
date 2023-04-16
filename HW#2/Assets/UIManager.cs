using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject winPanel, losePanel;


    public static UIManager instance; 
    
    
    void Awake()
    {
        Time.timeScale = 1;
        
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win()
    {
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void Lose()
    {
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }
}
