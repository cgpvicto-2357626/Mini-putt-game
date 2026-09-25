using System;

public static class GameEvents
{
    public static event Action OnFrappeCommencee;
    public static event Action OnBalleArretee;
    public static event Action OnModeFixeActive;
    public static event Action<int> onCoupEffectue;
    public static event Action OnBalleAuTrou;

    public static void TriggerFrappeCommencee()
    {
        if(OnFrappeCommencee != null)
        {
            OnFrappeCommencee?.Invoke();
        }
    }
    public static void TriggerBalleArretee()
    {
        if (OnBalleArretee != null)
        {
            OnBalleArretee?.Invoke();
        }
    }

    public static void TriggerModeFixeActive()
    {
        if(OnModeFixeActive != null)
        {
            OnModeFixeActive.Invoke();
        }
    }

    public static void TriggerCoupEffectue(int nombreCoups)
    {
        onCoupEffectue?.Invoke(nombreCoups);
    }

    public static void TriggerTrouTombe()
    {
        OnBalleAuTrou?.Invoke();
    }

}