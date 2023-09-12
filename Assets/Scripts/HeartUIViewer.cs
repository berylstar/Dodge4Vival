using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartUIViewer : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private Transform hearts;

    public void Start()
    {
        for (int i = 0; i < GameManager.I.HP.i; i++)
        {
            AddHeart();
        }
    }

    private void AddHeart()
    {
        Instantiate(_image, hearts);
    }

    private void RemoveHeart()
    {
        Destroy(hearts.GetChild(0).gameObject);
    }

    private void Heal()
    {
        GameManager.I.HP.i += 1;
    }
}
