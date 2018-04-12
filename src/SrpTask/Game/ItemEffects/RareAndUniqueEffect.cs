using System;

namespace SrpTask.Game.ItemEffects
{
    public class RareAndUniqueEffect : IEffect
    {
        public void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback)
        {
            gameEngine.PlaySpecialEffect("blue_swirly");
        }
    }
}