using System;
using UnityEngine;

[Serializable]
public class Wrapper<T>
{
    public T Value;

    public static implicit operator Wrapper<T>(T v) { return new Wrapper<T> { Value = v }; }
    public static implicit operator T(Wrapper<T> w) { return w.Value; }

    public override string ToString() { return Value.ToString(); }
    public override int GetHashCode() { return Value.GetHashCode(); }
}
