using System;

namespace SrpTask.Game.ItemEffects
{
    public class RareItemEffect : ItemEffect
    {
        public void Effect(Item item, Player player, IGameEngine gameEngine, Action<Item, Player> inventoryCallback)
        {
            if (item.Rare)
            {
                gameEngine.PlaySpecialEffect("cool_swirly_particles");
                inventoryCallback(item, player);
            }
        }
    }
}