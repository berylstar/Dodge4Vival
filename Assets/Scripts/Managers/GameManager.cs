using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public Player player;

    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _TimeText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _thisScoreText;

    [SerializeField] private MonsterSpawnController _monsterSpawnController;

    private float _inTime = 0f;

    [Header("Variables")]
    public FloatVariable GameEndTime;

    void Awake()
    {
        I = this;

        Time.timeScale = 1.0f;
    }

    void Start()
    {
        _monsterSpawnController.SetSpawnPosition(player.transform.position);
    }

    void Update()
    {
        if (_inTime > GameEndTime.f)
        {
            GameOver();
        }
        else
        {
            _inTime += Time.deltaTime;
            _TimeText.text = _inTime.ToString("N2");
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        _monsterSpawnController.SetCanSpawn(false);
        _bestScoreText.text = _TimeText.text;
        _thisScoreText.text = _TimeText.text;
        _endPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("StartScene");
    }
}
