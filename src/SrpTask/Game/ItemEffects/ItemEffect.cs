using System;

namespace SrpTask.Game.ItemEffects
{
    public interface ItemEffect
    {
        void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback);
    }
}