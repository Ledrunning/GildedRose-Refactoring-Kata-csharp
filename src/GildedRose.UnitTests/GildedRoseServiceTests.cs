using GildedRose.ConsoleApp.Constants;
using GildedRose.ConsoleApp.Models;
using GildedRose.ConsoleApp.Services;

namespace GildedRose.UnitTests;

[TestFixture]
public class Tests
{
    // Just for tests setup...
    [Test]
    public void UpdateQuality_DecreasesQuality_ForNormalItem()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Lower Item", 15, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(24));
    }

    // Just for tests setup...
    [Test]
    public void UpdateQuality_SellInAndQualityLower()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Lower Item", 15, 25);

        // Fluent API Assert
        Assert.That(items.SellIn, Is.EqualTo(14));
        Assert.That(items.Quality, Is.EqualTo(24));
    }

    // Once the sell by date has passed, Quality degrades twice as fast
    [Test]
    public void UpdateQuality_PastSellInAndQuality_DegradesTwiceAsFast()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Divided Quality Item", 0, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(23));

        // Assert
        Assert.AreEqual(23, items.Quality);
    }

    // The Quality of an item is never negative
    [Test]
    public void UpdateQuality_ItemQuality_IsNeverNegative()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Negative Quality Item", 15, 0);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(0));

        // Assert
        Assert.AreEqual(0, items.Quality);
    }

    // "Aged Brie" actually increases in Quality the older it gets

    [Test]
    public void UpdateQuality_AgedBrie_QualityIncreases()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Aged Brie", 15, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(26));

        // Assert
        Assert.AreEqual(26, items.Quality);
    }

    // The Quality of an item is never more than 50
    [Test]
    public void UpdateQuality_ItemQuality_IsNeverMoreThan_50()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Aged Brie", 15, 50);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(50));

        // Assert
        Assert.AreEqual(50, items.Quality);
    }

    // Sulfuras", being a legendary item, never has to be sold or decreases in Quality
    [Test]
    public void UpdateQuality_SulfurasNeverSold_Or_QualityDecrease()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Sulfuras, Hand of Ragnaros", 15, ServiceConstants.SulfurasLegendaryQuality);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(ServiceConstants.SulfurasLegendaryQuality));

        // Assert
        Assert.AreEqual(ServiceConstants.SulfurasLegendaryQuality, items.Quality);
    }

    //"Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
    // Quality increases by 2 when there are 10 days or less and by 3 when there are 5 days or less but
    // Quality drops to 0 after the concert
    [Test]
    public void UpdateQuality_BackstagePasses_QualityIncrease()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Backstage passes to a TAFKAL80ETC concert", 15, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(26));

        // Assert
        Assert.AreEqual(26, items.Quality);
    }

    [Test]
    public void UpdateQuality_BackstagePasses10Days_QualityIncreaseOn2()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Backstage passes to a TAFKAL80ETC concert", 10, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(27));

        // Assert
        Assert.AreEqual(27, items.Quality);
    }

    [Test]
    public void UpdateQuality_BackstagePasses5Days_QualityIncreaseOn3()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Backstage passes to a TAFKAL80ETC concert", 5, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(28));

        // Assert
        Assert.AreEqual(28, items.Quality);
    }

    [Test]
    public void UpdateQuality_ConjuredItems_QualityDecreaseTwiceAsFast()
    {
        // Arrange
        // Act
        var items = CreateAndUpdateConcreteItem("Conjured", 15, 25);

        // Fluent API Assert
        Assert.That(items.Quality, Is.EqualTo(23));

        // Assert
        Assert.AreEqual(23, items.Quality);
    }


    private static Item CreateAndUpdateConcreteItem(string name, int sellIn, int quality)
    {
        var items = new List<Item> { new() { Name = name, SellIn = sellIn, Quality = quality } };
        var service = new GildedRoseService(items);

        service.UpdateQuality();
        return items[0];
    }


}