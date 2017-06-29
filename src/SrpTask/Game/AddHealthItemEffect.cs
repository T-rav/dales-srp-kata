using System;

namespace SrpTask.Game
{
    public class AddHealthItemEffect : ItemEffect
    {
        public void Effect(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> inventoryCallback)
        {
            rpgPlayer.Health += item.Heal;

            if (rpgPlayer.Health > rpgPlayer.MaxHealth)
                rpgPlayer.Health = rpgPlayer.MaxHealth;

            inventoryCallback(item, rpgPlayer);
        }
    }
}