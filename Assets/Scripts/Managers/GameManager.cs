using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [SerializeField] private MonsterSpawnController _monsterSpawnController;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _TimeText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _thisScoreText;

    [SerializeField] private float _endTime;

    [Header("Variables")]
    public IntVariable HP;
    public IntVariable LowHP;
    public IntVariable PlayerSpeed;
    public FloatVariable PlayerAttackCooltime;

    void Awake()
    {
        I = this;

        _endTime = 100f;
        InitialVariables();
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        _monsterSpawnController.SetSpawnPosition(new Vector2(_player.transform.position.x, _player.transform.position.y));
    }

    void Update()
    {
        if (Single.Parse(_TimeText.text) > _endTime)
        {
            GameOver();
        }
    }

    private void InitialVariables()
    {
        HP.i = 5;
        PlayerSpeed.i = 5;
        PlayerAttackCooltime.f = 0.5f;
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
