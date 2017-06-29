using System;

namespace SrpTask.Game
{
    public class LargeHealthQuantitySpecialEffect : ItemEffect
    {
        public void Effect(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> inventoryCallback)
        {
            if (item.Heal > 500)
            {
                gameEngine.PlaySpecialEffect("green_swirly");
            }
        }
    }
}