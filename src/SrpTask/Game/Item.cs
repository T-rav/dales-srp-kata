namespace SrpTask.Game
{
    public class Item
    {
        /// <summary>
        /// Items unique Id;
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Items name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// How much the item heals by.
        /// </summary>
        public int Heal { get; set; }

        /// <summary>
        /// How much armour the player gets when it is equipped.
        /// </summary>
        public int Armour { get; set; }

        /// <summary>
        /// How much this item weighs in kilograms.
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// A unique item can only be picked up once.
        /// </summary>
        public bool Unique { get; set; }

        /// <summary>
        /// Rare items are posh and shiny
        /// </summary>
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

        public bool IsLightEnoughToPickup(RpgPlayer rpgPlayer)
        {
            var weight = rpgPlayer.CalculateInventoryWeight();
            var itemWeightIsOverPlayerCarryingCapacity = weight + Weight > rpgPlayer.CarryingCapacity;
            return !itemWeightIsOverPlayerCarryingCapacity;
        }

        public bool UniqueItemAction(RpgPlayer rpgPlayer)
        {
            if (Unique && rpgPlayer.CheckIfItemExistsInInventory(this))
                return false;
            return true;
        }

        public void RareItemAction(RpgPlayer rpgPlayer)
        {
            if (Rare)
                rpgPlayer.GameEngine.PlaySpecialEffect("cool_swirly_particles");
        }

        public bool HealthItemAction(RpgPlayer rpgPlayer)
        {
            if (Heal <= 0) return false;
            rpgPlayer.Health += Heal;

            if (rpgPlayer.Health > rpgPlayer.MaxHealth)
                rpgPlayer.Health = rpgPlayer.MaxHealth;

            if (this.Heal > 500)
            {
                rpgPlayer.GameEngine1.PlaySpecialEffect("green_swirly");
            }

            return true;
        }

        public void ActionForPlayer(RpgPlayer rpgPlayer)
        {
            if (!IsLightEnoughToPickup(rpgPlayer)) return;

            if (!UniqueItemAction(rpgPlayer)) return;

            // Don't pick up items that give health, just consume them.
            if (HealthItemAction(rpgPlayer)) return;

            RareItemAction(rpgPlayer);

            rpgPlayer.Inventory.Add(this);
        }
    }
}