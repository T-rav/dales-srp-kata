using System;

namespace SrpTask.Game.ItemEffects
{
    public class LargeHealthQuantitySpecialEffect : ItemEffect
    {
        public void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback)
        {
            if (item.Heal > 500)
            {
                gameEngine.PlaySpecialEffect("green_swirly");
            }
        }
    }
}