

using System;

public static class StaticEvent
{
    public static event Action<Player> OnPlayerDie;

    public static void CallPlayerDieEvent(Player player)
    {
        OnPlayerDie?.Invoke(player);
    }
}

