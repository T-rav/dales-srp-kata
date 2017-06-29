using System;

namespace SrpTask.Game.ItemEffects
{
    public class RegularItemEffect : ItemEffect
    {
        public void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback)
        {
            inventoryCallback(item, player);
        }
    }
}