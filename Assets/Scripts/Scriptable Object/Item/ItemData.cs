using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public string Name;
    public string Discription;
    public Sprite Sprite;
    public GameEvent ItemEvent;
}
