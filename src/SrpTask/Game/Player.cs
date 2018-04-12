using System.Collections.Generic;
using System.Linq;

namespace SrpTask.Game
{
    public class Player
    {
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int Armour { get; set; }
        public int CarryingCapacityInKilograms { get; }
        public const int MaximumCarryingCapacityInKilograms = 1000;

        public List<Item> Inventory { get;  }
        public IGameEngine GameEngine { get; }

        public Player(IGameEngine gameEngine)
        {
            GameEngine = gameEngine;
            Inventory = new List<Item>();
            CarryingCapacityInKilograms = MaximumCarryingCapacityInKilograms;
        }

        public void UseItem(Item item)
        {
            if (item.Name == "Stink Bomb")
            {
                var enemies = GameEngine.GetEnemiesNear(this);

                foreach (var enemy in enemies)
                    enemy.TakeDamage(100);
            }
        }

        public void PickUpItem(Item item)
        {
            item.ActionForPlayer(this);
            CalculateStats();
        }

        public void TakeDamage(int damage)
        {
            if (damage < Armour)
            {
                GameEngine.PlaySpecialEffect("parry");
                return;
            }

            var damageToDeal = damage - Armour;
            Health -= damageToDeal;

            GameEngine.PlaySpecialEffect("lots_of_gore");
        }

        public bool CheckIfItemExistsInInventory(Item item) => Inventory.Any(x => x.Id == item.Id);

        public int CalculateInventoryWeight() => Inventory.Sum(x => x.Weight);

        private void CalculateStats() => Armour = Inventory.Sum(x => x.Armour);
    }
}