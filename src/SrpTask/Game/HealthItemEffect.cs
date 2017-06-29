using System;

namespace SrpTask.Game
{
    public class HealthItemEffect
    {
        public static void Effect(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> inventoryCallback)
        {
            rpgPlayer.Health += item.Heal;

            if (rpgPlayer.Health > rpgPlayer.MaxHealth)
                rpgPlayer.Health = rpgPlayer.MaxHealth;

            if (item.Heal > 500)
            {
                gameEngine.PlaySpecialEffect("green_swirly");
            }

            inventoryCallback(item, rpgPlayer);
        }
    }
}