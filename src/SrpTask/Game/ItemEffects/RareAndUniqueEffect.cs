using System;

namespace SrpTask.Game.ItemEffects
{
    public class RareAndUniqueEffect : ItemEffect
    {
        public void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback)
        {
            gameEngine.PlaySpecialEffect("blue_swirly");
        }
    }
}