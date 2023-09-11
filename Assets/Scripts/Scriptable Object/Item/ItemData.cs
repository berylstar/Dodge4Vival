using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Item")]
public class ItemData : ScriptableObject
{
    public string ItemName { get; }
    public string ItemDiscription { get; }
    public GameEvent GameEvent { get; }
}
