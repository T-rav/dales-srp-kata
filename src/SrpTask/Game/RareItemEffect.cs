using System;

namespace SrpTask.Game
{
    public class RareItemEffect
    {
        public void Effect(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> inventoryCallback)
        {
            if (item.Rare)
            {
                gameEngine.PlaySpecialEffect("cool_swirly_particles");
                inventoryCallback(item, rpgPlayer);
            }
        }
    }
}