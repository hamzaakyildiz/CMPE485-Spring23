using System;
using System.Collections;
using System.Collections.Generic;
using Car;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneTemplate;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager instance;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
    }

    #endregion

    private Transform canvas;
    
    public List<GameObject> Panels;

    public TextMeshProUGUI speedText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI countDownText;

    public int expectedTime = 120;

    private float currentTime;

    public static event Action LevelStarted;
    private void Start()
    {
        if (SceneController.sceneIndex != 0)
        {
            StartCoroutine(CountdownSequence());
        }
    }

    IEnumerator CountdownSequence()
    {
        for (int i = 3; i > 0; i--)
        {
            if (i == 1)
            {
                AudioManager.instance.Play("Ignition");
            }
            countDownText.transform.localScale = Vector3.one;
            countDownText.color = Color.white;
            countDownText.text = i.ToString();
            countDownText.transform.DOScale(0.5f, 1).SetEase(Ease.OutBack);
            countDownText.DOColor(Color.clear, 1).SetEase(Ease.InOutSine);
            yield return new WaitForSeconds(1);
        }
        
        timeText.gameObject.SetActive(true);
        currentTime = 0;
        LevelStarted?.Invoke();
    }

    private void Update()
    {
        speedText.text = (Player.CurrentSpeed * 3.6f).ToString("00");
        timeText.text = (expectedTime - currentTime).ToString("00");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenPanel(2);
        }
    }

    private void FixedUpdate()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= expectedTime)
        {
            OpenPanel("LosePanel");
        }
    }

    public void OpenPanel(string panelName)
    {
        switch (panelName)
        {
            case "LosePanel":
                OpenPanel(0);
                break;
            case "WinPanel":
                OpenPanel(1);
                break;
            case "PausePanel":
                OpenPanel(2);
                break;
        }
    }

    private void OpenPanel(int index)
    {
        Panels[index].SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }
}