using System;

namespace SrpTask.Game
{
    public class RegularItemEffect
    {
        public static void Effect(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> inventoryCallback)
        {
            inventoryCallback(item, rpgPlayer);
        }
    }
}