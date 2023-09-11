using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Float Variable")]
public class FloatVariable : ScriptableObject
{
    public float f;

    public void Set(float v)
    {
        f = v;
    }

    public void Change(float v)
    {
        f += v;
    }
}
