using UnityEngine;

public interface IDeltaTimeService
{
    float DeltaTime { get; }
}
    
public class DeltaTimeService : IDeltaTimeService
{
    public float DeltaTime => Time.deltaTime;
}