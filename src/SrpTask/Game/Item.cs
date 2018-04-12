using SrpTask.Game.ItemEffects;

namespace SrpTask.Game
{
    public class Item
    {
        public int Id { get; }
        public string Name { get;  }
        public int Heal { get;  }
        public int Armour { get;  }
        public int Weight { get;  }
        public bool Unique { get; }
        public bool Rare { get; }

        public Item(int id, string name, int heal, int armour, int weight, bool unique, bool rare)
        {
            Rare = rare;
            Name = name;
            Heal = heal;
            Armour = armour;
            Weight = weight;
            Unique = unique;
            Id = id;
        }

        public bool ItemIsTooHeavyToPickupRule(Item item, Player player)
        {
            var weight = player.CalculateInventoryWeight();
            var itemWeightIsOverPlayerCarryingCapacity = weight + item.Weight > player.CarryingCapacityInKilograms;
            return itemWeightIsOverPlayerCarryingCapacity;
        }

        public bool UniqueItemPickupRule(Item item, Player player)
        {
            var itemIsUniqueAndPlayerAlreadyHasIt = item.Unique && player.CheckIfItemExistsInInventory(item);
            return itemIsUniqueAndPlayerAlreadyHasIt;
        }

        public void ActionForPlayer(Player player)
        {
            if (ItemIsTooHeavyToPickupRule(this, player)) return;
            if (UniqueItemPickupRule(this, player)) return;

            foreach (var effect in EffectsFactory.EffectsFor(this))
                effect.Effect(this, player, player.GameEngine, AddItemToInventory);
        }

        private void AddItemToInventory(Item item, Player player)
        {
            if (player.Inventory.Contains(item)) return;
            if (item.Heal > 0) return;

            player.Inventory.Add(this);
        }
    }
}