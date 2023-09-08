using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private MonsterSpawnController _monsterSpawnController;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _TimeText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _thisScoreText;

    [SerializeField] private float _endTime = 5f;

    void Awake()
    {
        _monsterSpawnController = GetComponent<MonsterSpawnController>();
        _monsterSpawnController.SetFullCount(10);
        _monsterSpawnController.SetSpawnPosition(new Vector2(_player.transform.position.x, _player.transform.position.y));
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        if (Single.Parse(_TimeText.text) > _endTime)
            GameOver();
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        _bestScoreText.text = _TimeText.text;
        _thisScoreText.text = _TimeText.text;
        _endPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("MainScene"); // Test version
        //SceneManager.LoadScene("StartScene");
    }
}
