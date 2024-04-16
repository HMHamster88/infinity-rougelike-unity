using UnityEngine;

[System.Serializable]
public class MinMaxInt
{
    public int Min;
    public int Max;

    public int GetRandomInclusive()
    { 
        return Random.Range(Min, Max + 1);
    }

    public int GetRandom()
    {
        return Random.Range(Min, Max);
    }
}
