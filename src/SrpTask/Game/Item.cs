using System;

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

        public static bool HealthItemPickupRule(Item item, RpgPlayer rpgPlayer)
        {
            return item.Heal > 0;
        }

        public static void RareItemEffectAction(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> callback)
        {
            if (item.Rare)
            {
                gameEngine.PlaySpecialEffect("cool_swirly_particles");
                callback(item, rpgPlayer);
            }
        }

        public static void HealthItemEffectAction(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> callback)
        {
            rpgPlayer.Health += item.Heal;

            if (rpgPlayer.Health > rpgPlayer.MaxHealth)
                rpgPlayer.Health = rpgPlayer.MaxHealth;

            if (item.Heal > 500)
            {
                gameEngine.PlaySpecialEffect("green_swirly");
            }

            callback(item, rpgPlayer);
        }

        public static void RegularItemEffectAction(Item item, RpgPlayer rpgPlayer, IGameEngine gameEngine, Action<Item, RpgPlayer> callback)
        {
            callback(item, rpgPlayer);
        }

        public void ActionForPlayer(RpgPlayer rpgPlayer)
        {
            if (ItemIsTooHeavyToPickupRule(this, rpgPlayer)) return;
            if (UniqueItemPickupRule(this, rpgPlayer)) return;

            HealthItemEffectAction(this, rpgPlayer, rpgPlayer.GameEngine, AddItemToInventory);
            RareItemEffectAction(this, rpgPlayer, rpgPlayer.GameEngine, AddItemToInventory);
            RegularItemEffectAction(this, rpgPlayer, rpgPlayer.GameEngine, AddItemToInventory);
        }

        private void AddItemToInventory(Item item, RpgPlayer rpgPlayer)
        {
            if (rpgPlayer.Inventory.Contains(item)) return;
            if (item.Heal > 0) return;

            rpgPlayer.Inventory.Add(this);
        }
    }
}