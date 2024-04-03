using GildedRose.ConsoleApp.Constants;
using GildedRose.ConsoleApp.Contracts;
using GildedRose.ConsoleApp.Models;
using GildedRose.ConsoleApp.Resources;

namespace GildedRose.ConsoleApp.Services;

public class GildedRoseService : IGildedRoseService
{
    private readonly IList<Item> _items;
    private readonly Dictionary<string, Action<Item>> _updateStrategies;

    public GildedRoseService(IList<Item> items)
    {
        _items = items;
        _updateStrategies = new Dictionary<string, Action<Item>>
        {
            { ProductNames.AgedBrie, UpdateAgedBrieQuality },
            { ProductNames.BackstagePasses, UpdateBackstagePassesQuality },
            { ProductNames.SulfurasRagnarosHand, _ => { } },
            { ProductNames.Conjured, UpdateConjuringQuality }
        };
    }

    // new feature with strategy
    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            if (item.Name != null && _updateStrategies.TryGetValue(item.Name, out var updateStrategy))
            {
                updateStrategy(item);
            }
            else
            {
                UpdateNormalItemQuality(item);
            }

            item.SellIn--;
        }
    }
    
    private void UpdateAgedBrieQuality(Item item)
    {
        if (item.Quality < ServiceConstants.MaximumQuality)
        {
            item.Quality++;
        }

        if (item.SellIn <= ServiceConstants.SellInZeroDay
            && item.Quality < ServiceConstants.MaximumQuality)
        {
            item.Quality++;
        }
    }

    private void UpdateBackstagePassesQuality(Item item)
    {
        if (item.Quality < ServiceConstants.MaximumQuality)
        {
            item.Quality++;
            if (item.SellIn < ServiceConstants.BackstagePassesThreshold)
            {
                item.Quality++;
                if (item.SellIn < ServiceConstants.SellInSixDays)
                {
                    item.Quality++;
                }
            }
        }

        if (item.SellIn <= ServiceConstants.SellInZeroDay)
        {
            item.Quality = 0;
        }
    }

    /// <summary>
    ///     New functional
    /// </summary>
    /// <param name="item"></param>
    private void UpdateConjuringQuality(Item item)
    {
        if (item.Quality > ServiceConstants.ZeroQuality)
        {
            item.Quality -= ServiceConstants.ConjuredItemsQuality;
        }

        if (item.SellIn <= ServiceConstants.SellInZeroDay
            && item.Quality > ServiceConstants.ZeroQuality)
        {
            item.Quality -= ServiceConstants.ConjuredItemsQuality;
        }
    }

    private void UpdateNormalItemQuality(Item item)
    {
        if (item.Quality > ServiceConstants.ZeroQuality)
        {
            item.Quality--;
            if (item.SellIn <= ServiceConstants.SellInZeroDay
                && item.Quality > ServiceConstants.ZeroQuality)
            {
                item.Quality--;
            }
        }
    }
}