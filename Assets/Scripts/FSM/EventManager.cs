using UnityEngine.Events;

public static class EventManager 
{
    public static UnityEvent OnPlayerIdle = new UnityEvent();
    public static UnityEvent OnPlayerWalk = new UnityEvent();
}
