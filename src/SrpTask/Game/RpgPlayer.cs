﻿using System.Collections.Generic;
using System.Linq;

namespace SrpTask.Game
{
    public class RpgPlayer
    {
        public const int MaximumCarryingCapacity = 1000;

        private readonly IGameEngine _gameEngine;

        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public int Armour { get; private set; }

        public List<Item> Inventory;

        /// <summary>
        /// How much the player can carry in kilograms
        /// </summary>
        public int CarryingCapacity { get; private set; }

        public RpgPlayer(IGameEngine gameEngine)
        {
            _gameEngine = gameEngine;
            Inventory = new List<Item>();
            CarryingCapacity = MaximumCarryingCapacity;
        }

        public void UseItem(Item item)
        {
            if (item.Name == "Stink Bomb")
            {
                var enemies = _gameEngine.GetEnemiesNear(this);

                foreach (var enemy in enemies)
                {
                    enemy.TakeDamage(100);
                }
            }
        }

        public bool PickUpItem(Item item)
        {
            if (!item.ItemWeightCheckAction(this)) return false;

            if (!item.UniqueItemAction(this)) return false;

            // Don't pick up items that give health, just consume them.
            if (HealthItemAction(item)) return true;

            RareItemAction(item);

            Inventory.Add(item);

            CalculateStats();

            return true;
        }

        private void RareItemAction(Item item)
        {
            if (item.Rare)
                _gameEngine.PlaySpecialEffect("cool_swirly_particles");
        }

        private bool HealthItemAction(Item item)
        {
            if (item.Heal > 0)
            {
                Health += item.Heal;

                if (Health > MaxHealth)
                    Health = MaxHealth;

                if (item.Heal > 500)
                {
                    _gameEngine.PlaySpecialEffect("green_swirly");
                }

                return true;
            }
            return false;
        }

        private void CalculateStats()
        {
            Armour = Inventory.Sum(x => x.Armour);
        }

        public bool CheckIfItemExistsInInventory(Item item)
        {
            return Inventory.Any(x => x.Id == item.Id);
        }

        public int CalculateInventoryWeight()
        {
            return Inventory.Sum(x => x.Weight);
        }

        public void TakeDamage(int damage)
        {
            if (damage < Armour)
            {
                _gameEngine.PlaySpecialEffect("parry");
                return;
            }

            var damageToDeal = damage - Armour;
            Health -= damageToDeal;
            
            _gameEngine.PlaySpecialEffect("lots_of_gore");
        }
    }
}