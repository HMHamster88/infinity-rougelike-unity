using Unity.Properties;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float Value = 1;
    public float BaseMaxValue = 10;

    [CreateProperty]
    public float MaxValue
    {
        get { return BaseMaxValue; }
    }
    

    void Start()
    {
        Value = BaseMaxValue;
    }

    public void ApplyDamage(float damage)
    {
        Value -= Mathf.Round(damage);
        if (Value <= 0)
        {
            Value = 0;
        }
    }
}
