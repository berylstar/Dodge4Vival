using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public Player player;

    [Header("UI")]
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private TMP_Text _TimeText;
    [SerializeField] private TMP_Text _bestScoreText;
    [SerializeField] private TMP_Text _thisScoreText;

    [Header("MonsterSpawn")]
    public FloatVariable SpawnCooldownTime;
    public Transform spawnHolder;

    public List<GameObject> monsters1 = new List<GameObject>();
    public List<GameObject> monsters2 = new List<GameObject>();
    public List<GameObject> monsters3 = new List<GameObject>();
    public List<GameObject> monsters4 = new List<GameObject>();

    private UnityEvent _spawnEvent = new();
    private int spawnCounter = 0;

    private float _inTime = 0f;

    void Awake()
    {
        I = this;

        Time.timeScale = 1.0f;

        StartCoroutine(Spawn(SpawnCooldownTime.f));

        _spawnEvent.AddListener(AddMonster1);
    }

    void Update()
    {
        _inTime += Time.deltaTime;
        _TimeText.text = _inTime.ToString("N2");
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }

    private void AddMonster1() => Instantiate(monsters1[Random.Range(0, monsters1.Count)], RandomSpawnPosition(), Quaternion.identity, spawnHolder);
    private void AddMonster2() => Instantiate(monsters2[Random.Range(0, monsters2.Count)], RandomSpawnPosition(), Quaternion.identity, spawnHolder);
    private void AddMonster3() => Instantiate(monsters3[Random.Range(0, monsters3.Count - 1)], RandomSpawnPosition(), Quaternion.identity, spawnHolder);
    private void AddMonster3B() => Instantiate(monsters3[monsters3.Count - 1], RandomSpawnPosition(), Quaternion.identity, spawnHolder);
    private void AddMonster4() => Instantiate(monsters4[Random.Range(0, monsters4.Count - 1)], RandomSpawnPosition(), Quaternion.identity, spawnHolder);
    private void AddMonster4B() => Instantiate(monsters4[monsters4.Count - 1], RandomSpawnPosition(), Quaternion.identity, spawnHolder);

    private void ApplySpawnLevel()
    {
        spawnCounter += 1;

        switch (spawnCounter)
        {
            case 30:
            case 90:
            case 180:
            case 390:
                _spawnEvent.AddListener(AddMonster1);
                break;

            case 60:
            case 150:
            case 330:
                _spawnEvent.AddListener(AddMonster2);
                break;

            case 120:
            case 240:
            case 450:
                _spawnEvent.AddListener(AddMonster3);
                break;

            case 210:
            case 360:
                AddMonster3B();
                break;

            case 300:
            case 420:
                _spawnEvent.AddListener(AddMonster4);
                break;

            case 480:
                AddMonster4B();
                break;

            default:
                if (spawnCounter > 480)
                {
                    if (spawnCounter % 30 == 0) AddMonster4B();
                    else if (spawnCounter % 50 == 0) AddMonster3B();
                }
                break;
        }
    }

    private Vector2 RandomSpawnPosition()
    {
        Vector2 RandomVector = (Vector2)player.transform.position + Random.insideUnitCircle.normalized * 6;
        
        float x = Mathf.Max(Mathf.Min(RandomVector.x, 39), -39);
        float y = Mathf.Max(Mathf.Min(RandomVector.y, 27), -28);

        return new Vector2(x, y);
    }

    private IEnumerator Spawn(float spawnCooldownTime)
    {
        while (true)
        {
            ApplySpawnLevel();

            _spawnEvent.Invoke();

            yield return new WaitForSeconds(spawnCooldownTime);
        }        
    }

    public void OnDestroyAllMonster()
    {
        foreach (Transform child in spawnHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
