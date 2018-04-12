using System;

namespace SrpTask.Game.ItemEffects
{
    public class AddHealthEffect : IEffect
    {
        public void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback)
        {
            player.Health += item.Heal;

            if (player.Health > player.MaxHealth)
                player.Health = player.MaxHealth;

            inventoryCallback(item, player);
        }
    }
}