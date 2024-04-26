public class FixedObject : UnityEngine.Object
{
    public static implicit operator bool(FixedObject thing)
    {
        return !System.Object.ReferenceEquals(thing, null);
    }

    public static bool operator ==(FixedObject a, FixedObject b)
    {
        return System.Object.ReferenceEquals(a, b);
    }

    public static bool operator !=(FixedObject a, FixedObject b)
    {
        return !System.Object.ReferenceEquals(a, b);
    }

    public override bool Equals(object obj)
    {
        return System.Object.ReferenceEquals(this, obj);
    }

    public override int GetHashCode()
    {
        return this.GetInstanceID();
    }

    public override string ToString()
    {
        return name;
    }
}

