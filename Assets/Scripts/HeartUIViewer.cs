using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUIViewer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Image _gameObject;

    [SerializeField] private HeartUI _heartUI;
    [SerializeField] private IntVariable _hp;

    private List<Image> _clones = new List<Image>();
    private Vector2[] _uiPositions;
    private int _positionLength = 7;

    public void Awake()
    {
        _uiPositions = new Vector2[_positionLength];
        for(int i = 0; i < _uiPositions.Length; i++)
        {
            _uiPositions[i].x = 53f + (i * 25f);
            _uiPositions[i].y = 65f;
        }
    }
    public void Start()
    {
        DisplayHeart();
    }

    public void DisplayHeart()
    {
        for(int i = 0; i < _hp.i; i++)
        {
            _gameObject.sprite = _heartUI.Sprite;
            Vector2 newPosition = new Vector2();
            newPosition.x = _uiPositions[i % _positionLength].x;
            newPosition.y = _uiPositions[i % _positionLength].y + (i / _positionLength * 25);
            _clones.Add(Instantiate(_gameObject, newPosition, Quaternion.identity, transform));
        }
    }

    public void AddHeart()
    {
        int addIndex = _clones.Count;
        Vector2 newPosition = new Vector2();
        newPosition.x = _uiPositions[addIndex % _positionLength].x;
        newPosition.y = _uiPositions[addIndex % _positionLength].y + (addIndex / _positionLength * 25);
        _clones.Add(Instantiate(_gameObject, newPosition, Quaternion.identity, transform));
    }

    public void RemoveHeart()
    {
        int lastIndex = _clones.Count - 1;
        if(lastIndex >= 0 )
        {
            Image clone = _clones[lastIndex];
            _clones.RemoveAt(lastIndex);
            Destroy(clone.gameObject);
        }
        
    }
}
