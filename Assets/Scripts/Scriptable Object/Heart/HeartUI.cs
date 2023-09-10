using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Heart UI")]
public class HeartUI : ScriptableObject
{
    [Header("UI")]
    public string Name;
    public Sprite Sprite;
}
