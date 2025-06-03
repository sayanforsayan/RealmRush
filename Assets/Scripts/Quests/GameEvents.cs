using System;
public static class GameEvents
{
    public static event Action OnEnemyKilled = delegate { };
    public static event Action OnItemCollected = delegate { };
    public static event Action OnAreaReached = delegate { };
    public static event Action OnResetCall = delegate { };
    public static void EnemyKilled()
    {
        OnEnemyKilled?.Invoke();
    }

    public static void ItemCollected()
    {
        OnItemCollected?.Invoke();
    }
    public static void AreaReached()
    {
        OnAreaReached?.Invoke();
    }

    public static void ResetData()
    {
        OnResetCall?.Invoke();
    }
}
