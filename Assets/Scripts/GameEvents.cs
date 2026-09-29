using System;

/// <summary>
/// Contient tous les évenements du jeu, ca permet aux scripts de se parler sans se connaitre
/// </summary>
public static class GameEvents
{
    public static event Action OnFrappeCommencee;
    public static event Action OnBalleArretee;
    public static event Action OnModeFixeActive;
    public static event Action<int> onCoupEffectue;
    public static event Action OnBalleAuTrou;
    public static event Action OnBalleReinitialisee;

    /// <summary>
    /// Avertit tout le monde que le joueur vient de frapper la balle
    /// </summary>
    public static void TriggerFrappeCommencee()
    {
        OnFrappeCommencee?.Invoke();
    }

    /// <summary>
    /// Avertit tout le monde que la balle a fini de rouler
    /// </summary>
    public static void TriggerBalleArretee()
    {
        OnBalleArretee?.Invoke();
    }

    /// <summary>
    /// Avertit tout le monde qu'on passe en mode camera fixe
    /// </summary>
    public static void TriggerModeFixeActive()
    {
        OnModeFixeActive?.Invoke();
    }

    /// <summary>
    /// Avertit tout le monde qu'un coup a ete jouer
    /// </summary>
    /// <param name="nombreCoups">Le nombre de coups fait par le joueur</param>
    public static void TriggerCoupEffectue(int nombreCoups)
    {
        onCoupEffectue?.Invoke(nombreCoups);
    }

    /// <summary>
    /// Avertit tout le monde que la balle est tomber dans le trou
    /// </summary>
    public static void TriggerTrouTombe()
    {
        OnBalleAuTrou?.Invoke();
    }

    /// <summary>
    /// Avertit tout le monde que la balle est remise a sa position de depart
    /// </summary>
    public static void TriggerBalleReinitialisee()
    {
        OnBalleReinitialisee?.Invoke();
    }
}