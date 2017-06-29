using System;

namespace SrpTask.Game
{
    public interface ItemEffect
    {
        void Effect(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> inventoryCallback);
    }
}