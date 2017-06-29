namespace SrpTask.Game
{
    public class ItemTooHeavyToPickupRule
    {
        private Item _item;

        public ItemTooHeavyToPickupRule(Item item)
        {
            _item = item;
        }

        public bool ItemIsTooHeavyToPickup(RpgPlayer rpgPlayer)
        {
            var weight = rpgPlayer.CalculateInventoryWeight();
            var itemWeightIsOverPlayerCarryingCapacity = weight + _item.Weight > rpgPlayer.CarryingCapacity;
            return itemWeightIsOverPlayerCarryingCapacity;
        }
    }
}