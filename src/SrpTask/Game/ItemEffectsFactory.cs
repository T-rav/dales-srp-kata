using System.Collections.Generic;

namespace SrpTask.Game
{
    public class ItemEffectsFactory
    {
        public static IEnumerable<ItemEffect> EffectsForItem(Item item)
        {
            return new List<ItemEffect>
            {
                new HealthItemEffect(),
                new RareItemEffect(),
                new RegularItemEffect()
            };
        }
    }
}