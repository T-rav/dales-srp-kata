using System.Collections.Generic;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SrpTask.Game;

namespace SrpTask.GameTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void PickUpItem_ThatCanBePickedUp_ItIsAddedToTheInventory()
        {
            // Arrange
            var item = new ItemBuilder().Build();
            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine);

            // Act
            player.PickUpItem(item);

            // Assert
            player.Inventory.Should().Contain(item);
        }

        [Test]
        public void PickUpItem_ThatGivesHealth_ItemIsNotAddedToInventory()
        {
            // Arrange
            var maxHealth = 100;
            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine) {MaxHealth = maxHealth, Health = 10};
            var healthPotion = new ItemBuilder()
                                    .WithHeal(maxHealth)
                                    .Build();
            // Act
            player.PickUpItem(healthPotion);

            // Assert
            player.Inventory.Should().BeEmpty();
        }

        [Test]
        public void PickUpItem_ThatGivesHealth_HealthDoesNotExceedMaxHealth()
        {
            // Arrange
            var maxHealth = 50;
            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine) {MaxHealth = maxHealth, Health = 10};

            var healthPotion =
                new ItemBuilder()
                .WithHeal(maxHealth)
                .Build();

            // Act
            player.PickUpItem(healthPotion);

            // Assert
            player.Health.Should().Be(maxHealth);
        }

        [Test]
        public void PickUpItem_ThatIsRare_ASpecialEffectShouldBePlayed()
        {
            // Arrange
            var effect = "cool_swirly_particles";
            var rareItem = new ItemBuilder()
                                .WithRareAttribute(true)
                                .Build();
            var gameEngine = Substitute.For<IGameEngine>();
            gameEngine.PlaySpecialEffect(effect);
            var player = new Player(gameEngine);

            // Act
            player.PickUpItem(rareItem);

            // Assert
            gameEngine.Received().PlaySpecialEffect(effect);
        }

        [Test]
        public void PickUpItem_ThatTwoItemsContainSameId_SecondItemShouldNotBePickedUpIfThePlayerAlreadyHasItInTheirInventory()
        {
            // Arrange
            var item = new ItemBuilder()
                            .WithId(100)
                            .Build();
            
            var uniqueItem = new ItemBuilder()
                .WithId(100)
                .WithUniqueAttribute(true)
                .Build();

            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine);

            // Act
            player.PickUpItem(item);
            player.PickUpItem(uniqueItem);

            // Assert
            player.Inventory.Should().Contain(item);
            player.Inventory.Should().NotContain(uniqueItem);
        }

        [Test]
        public void PickUpItem_ThatIsBothRareAndUnique_BlueSwirlyEffectOccurs()
        {
            // Arrange
            var rareAndUniqueItem = new ItemBuilder()
                .WithId(100)
                .WithUniqueAttribute(true)
                .WithRareAttribute(true)
                .Build();

            var effect = "blue_swirly";
            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine);

            // Act
            player.PickUpItem(rareAndUniqueItem);

            // Assert
            gameEngine.Received(1).PlaySpecialEffect(effect);
        }

        [Test]
        public void PickUpItem_ThatDoesMoreThan500Healing_GreenSwirlyEffectOccurs()
        {
            // Arrange
            var xPotion = new ItemBuilder()
                                .WithHeal(501)
                                .Build();

            var effect = "green_swirly";
            var gameEngine = Substitute.For<IGameEngine>();
            gameEngine.PlaySpecialEffect(effect);
            var player = new Player(gameEngine);

            // Act
            player.PickUpItem(xPotion);

            // Assert
            gameEngine.Received().PlaySpecialEffect(effect);
        }

        [Test]
        public void PickUpItem_ThatGivesArmour_ThePlayersArmourValueShouldBeIncreased()
        {
            // Arrange
            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine){Armour = 0};

            var itemArmourAmount = 100;
            var armour = new ItemBuilder()
                            .WithArmour(itemArmourAmount)
                            .Build();

            // Act
            player.PickUpItem(armour);

            // Assert
            var expectedArmour = 100;
            player.Armour.Should().Be(expectedArmour);
        }

        [Test]
        public void PickUpItem_ThatIsTooHeavy_TheItemIsNotPickedUpByPlayer()
        {
            // Arrange
            var gameEngine = Substitute.For<IGameEngine>();
            var player = new Player(gameEngine);
            var itemWeight = player.CarryingCapacityInKilograms + 1;
            var heavyItem = new ItemBuilder()
                                .WithWeight(itemWeight)
                                .Build();

            // Act
            player.PickUpItem(heavyItem);

            // Assert
            player.Inventory.Should().NotContain(heavyItem);
        }

        [Test]
        public void TakeDamage_WithNoArmour_HealthIsReducedAndParticleEffectIsShown()
        {
            // Arrange
            var gameEngine = Substitute.For<IGameEngine>();
            var effect = "lots_of_gore";
            var player = new Player(gameEngine) { Health = 200};
            var damage = 100;

            // Act
            player.TakeDamage(damage);

            // Assert
            var expectedHealth = 100;
            player.Health.Should().Be(expectedHealth);
            gameEngine.Received(1).PlaySpecialEffect(effect);
        }

        [Test]
        public void TakeDamage_With50Armour_DamageIsReducedBy50AndParticleEffectIsShown()
        {
            // Arrange
            var gameEngine = Substitute.For<IGameEngine>();
            var item = new ItemBuilder().WithArmour(50).Build();
            var player = new Player(gameEngine) { Health = 200};
            var effect = "lots_of_gore";

            // Act
            player.PickUpItem(item);
            var damage = 100;
            player.TakeDamage(damage);

            // Assert
            var expectedHealth = 150;
            player.Health.Should().Be(expectedHealth);
            gameEngine.Received(1).PlaySpecialEffect(effect);
        }

        [Test]
        public void TakeDamage_WithMoreArmourThanDamage_NoDamageIsDealtAndParryEffectIsPlayed()
        {
            // Arrange
            var effect = "parry";
            var gameEngine = Substitute.For<IGameEngine>();
            var item = new ItemBuilder().WithArmour(2000).Build();
            var player = new Player(gameEngine) { Health = 200};

            // Act
            player.PickUpItem(item);
            player.TakeDamage(100);

            // Assert
            var expectedHealth = 200;
            player.Health.Should().Be(expectedHealth);
            gameEngine.Received(1).PlaySpecialEffect(effect);
        }

        [Test]
        public void UseItem_StinkBomb_AllEnemiesNearThePlayerAreDamaged()
        {
            // Arrange
            var enemy = Substitute.For<IEnemy>();
            var gameEngine = Substitute.For<IGameEngine>();
            gameEngine.GetEnemiesNear(Arg.Any<Player>()).Returns(new List<IEnemy>{ enemy});
            var player = new Player(gameEngine);

            var item = new ItemBuilder().WithName("Stink Bomb").Build();

            // Act
            player.UseItem(item);

            // Assert
            var expectedDamage = 100;
            enemy.Received(1).TakeDamage(expectedDamage);
        }
    }
}