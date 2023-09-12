using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;

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
    public IntVariable MonsterFullCount;
    public FloatVariable SpawnCooldownTime;
    public Transform spawnHolder;

    public List<GameObject> monsters1 = new List<GameObject>();
    public List<GameObject> monsters2 = new List<GameObject>();
    public List<GameObject> monsters3 = new List<GameObject>();
    public List<GameObject> monsters4 = new List<GameObject>();

    [SerializeField] private List<GameObject> spawnableMonsters = new List<GameObject>();

    [Header("Variables")]
    public FloatVariable GameEndTime;

    private float _inTime = 0f;

    void Awake()
    {
        I = this;

        Time.timeScale = 1.0f;

        foreach (GameObject mon in monsters1)
        {
            spawnableMonsters.Add(mon);
        }

        StartCoroutine(Spawn(SpawnCooldownTime.f));
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
        _bestScoreText.text = _TimeText.text;
        _thisScoreText.text = _TimeText.text;
        _endPanel.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("StartScene");
    }

    private Vector2 RandomSpawnPosition()
    {
        float x = player.transform.position.x + Random.Range(-8.0f, 8.0f);
        float y = player.transform.position.y + Random.Range(-5.0f, 5.0f);

        x = Mathf.Max(Mathf.Min(x, 54), -54);
        y = Mathf.Max(Mathf.Min(y, 43), -43);

        return new Vector2(x, y);
    }

    private void AddMonster()
    {

    }

    private IEnumerator Spawn(float spawnCooldownTime)
    {
        while (true)
        {
            Debug.Log(_inTime);

            Instantiate(spawnableMonsters[Random.Range(0, spawnableMonsters.Count)], RandomSpawnPosition(), Quaternion.identity, spawnHolder);

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
