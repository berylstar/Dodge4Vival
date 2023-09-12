using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartUIViewer : MonoBehaviour
{
    [Header("Variable")]
    public IntVariable HP;
    public IntVariable MaxHP;

    [SerializeField] private Image _image;
    [SerializeField] private Transform hearts;

    public void Start()
    {
        for (int i = 0; i < HP.i; i++)
        {
            Instantiate(_image, hearts);
        }
    }

    private void AddHeart()
    {
        if(hearts.childCount < MaxHP.i)
            Instantiate(_image, hearts);
    }

    private void RemoveHeart()
    {
        Destroy(hearts.GetChild(0).gameObject);
    }
}
