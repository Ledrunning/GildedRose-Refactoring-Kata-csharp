using GildedRose.ConsoleApp.Constants;
using GildedRose.ConsoleApp.Contracts;
using GildedRose.ConsoleApp.Models;
using GildedRose.ConsoleApp.Resources;

namespace GildedRose.ConsoleApp.Services;

public class GildedRoseService : IGildedRoseService
{
    private readonly IList<Item> _items;

    public GildedRoseService(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        foreach (var item in _items)
        {
            UpdateItemQuality(item);
        }
    }

    private void UpdateItemQuality(Item item)
    {
        if (IsSulfuras(item))
        {
            return;
        }

        if (IsAgedBrie(item))
        {
            UpdateAgedBrieQuality(item);
        }
        else if (IsBackstagePasses(item))
        {
            UpdateBackstagePassesQuality(item);
        }
        else if (IsConjuring(item))
        {
            UpdateConjuringQuality(item);
        }
        else
        {
            UpdateNormalItemQuality(item);
        }

        item.SellIn--;
    }

    private void UpdateAgedBrieQuality(Item item)
    {
        if (item.Quality < ServiceConstants.MaximumQuality)
        {
            item.Quality++;
        }

        if (item.SellIn <= 0 && item.Quality < ServiceConstants.MaximumQuality)
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

        if (item.SellIn <= 0)
        {
            item.Quality = 0;
        }
    }

    /// <summary>
    /// New functional
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
    
    private bool IsSulfuras(Item item)
    {
        return item.Name == ProductNames.SulfurasRagnarosHand;
    }

    private bool IsBackstagePasses(Item item)
    {
        return item.Name == ProductNames.BackstagePasses;
    }

    private bool IsAgedBrie(Item item)
    {
        return item.Name == ProductNames.AgedBrie;
    }

    private bool IsConjuring(Item item)
    {
        return item.Name == ProductNames.Conjured;
    }
}