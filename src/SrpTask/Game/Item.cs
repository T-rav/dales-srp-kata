namespace SrpTask.Game
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Heal { get; set; }
        public int Armour { get; set; }
        public int Weight { get; set; }
        public bool Unique { get; set; }
        public readonly bool Rare;

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

        public static bool ItemIsTooHeavyToPickupRule(Item item, RpgPlayer rpgPlayer)
        {
            var weight = rpgPlayer.CalculateInventoryWeight();
            var itemWeightIsOverPlayerCarryingCapacity = weight + item.Weight > rpgPlayer.CarryingCapacity;
            return itemWeightIsOverPlayerCarryingCapacity;
        }

        public static bool UniqueItemPickupRule(Item item, RpgPlayer rpgPlayer)
        {
            var itemIsUniqueAndPlayerAlreadyHasIt = item.Unique && rpgPlayer.CheckIfItemExistsInInventory(item);
            return itemIsUniqueAndPlayerAlreadyHasIt;
        }

        public void RareItemEffectAction(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine)
        {
            if (Rare) gameEngine.PlaySpecialEffect("cool_swirly_particles");
        }

        public bool HealthItemEffectAction(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine)
        {
            if (Heal <= 0) return false;
            rpgPlayer.Health += Heal;

            if (rpgPlayer.Health > rpgPlayer.MaxHealth)
                rpgPlayer.Health = rpgPlayer.MaxHealth;

            if (Heal > 500)
            {
                gameEngine.PlaySpecialEffect("green_swirly");
            }

            return true;
        }

        public void ActionForPlayer(RpgPlayer rpgPlayer)
        {
            if (ItemIsTooHeavyToPickupRule(this, rpgPlayer)) return;

            if (UniqueItemPickupRule(this, rpgPlayer)) return;

            if (HealthItemEffectAction(this, rpgPlayer, rpgPlayer.GameEngine)) return;

            RareItemEffectAction(this, rpgPlayer, rpgPlayer.GameEngine);

            rpgPlayer.Inventory.Add(this);
        }
    }
}