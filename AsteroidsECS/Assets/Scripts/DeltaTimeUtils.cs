using UnityEngine;

public interface IDeltaTimeSource
{
    float DeltaTime { get; }
}
    
public class DeltaTimeSource : IDeltaTimeSource
{
    public float DeltaTime => Time.deltaTime;
}