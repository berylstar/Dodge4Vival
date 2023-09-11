using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Int Variable")]
public class IntVariable : ScriptableObject
{
    public int i;

    public void Set(int v)
    {
        i = v;
    }

    public void Change(int v)
    {
        i += v;
    }
}
