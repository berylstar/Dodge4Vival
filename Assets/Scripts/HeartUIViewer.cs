using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HeartUIViewer : MonoBehaviour
{
    [Header("Variable")]
    public IntVariable HP;
    public IntVariable MaxHP;

    [SerializeField] private List<Image> hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;

    private int index = 4;

    public void AddHeart()
    {
        if (index >= 4)
            return;

        index += 1;
        hearts[index].sprite = fullHeart;
    }

    public void RemoveHeart()
    {
        if (index < 0)
            return;

        hearts[index].sprite = emptyHeart;
        index -= 1;
    }
}
