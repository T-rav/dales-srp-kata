using System.Collections.Generic;

namespace SrpTask.Game
{
    public class ItemEffectsFactory
    {
        public static IEnumerable<ItemEffect> EffectsFor(Item item)
        {
            return new List<ItemEffect>
            {
                new AddHealthItemEffect(),
                new LargeHealthQuantitySpecialEffect(),
                new RareItemEffect(),
                new RegularItemEffect()
            };
        }
    }
}