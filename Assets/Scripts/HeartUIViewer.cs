using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        EventManager.I.PlayerHitEvent.AddListener(RemoveHeart);

        EventManager.I.PlayerHealingEvent.AddListener(AddHeart);
        EventManager.I.PlayerHealingEvent.AddListener(Heal);
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
