using System.Collections.Generic;

namespace SrpTask.Game.ItemEffects
{
    public class EffectsFactory
    {
        public static IEnumerable<IEffect> EffectsFor(Item item)
        {
            var effects = new List<IEffect>
            {
                new RegularEffect()
            };

            if (item.Heal > 0)
            {
                effects.Add(new AddHealthEffect());
                effects.Add(new LargeHealthQuantitySpecialEffect());
            }

            if (item.Rare)
            {
                effects.Add(new RareEffect());
            }

            if (item.Rare && item.Unique)
            {
                effects.Add(new RareAndUniqueEffect());
            }

            return effects;
        }
    }
}