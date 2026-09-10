using System;

public static class GameEvents
{
    public static event Action OnFrappeCommencee;
    public static event Action OnBalleArretee;

    public static void TriggerFrappeCommencee() => OnFrappeCommencee?.Invoke();
    public static void TriggerBalleArretee() => OnBalleArretee?.Invoke();
}