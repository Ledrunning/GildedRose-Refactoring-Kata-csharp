using GildedRose.ConsoleApp.Constants;
using GildedRose.ConsoleApp.Models;
using GildedRose.ConsoleApp.Resources;

namespace GildedRose.ConsoleApp.Services;

public class GildedRoseService
{
    private readonly IList<Item> _items;

    public GildedRoseService(IList<Item> items)
    {
        _items = items;
    }

    public void UpdateQuality()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            if (!IsAgedBrie(item) && !IsBackstagePasses(item))
            {
                if (item.Quality > ServiceConstants.ZeroQuality)
                {
                    if (!IsSulfuras(item))
                    {
                        item.Quality = item.Quality - 1;
                    }
                }
            }
            else
            {
                if (item.Quality < ServiceConstants.MaximumQuality)
                {
                    item.Quality = item.Quality + 1;

                    if (IsBackstagePasses(item))
                    {
                        if (item.SellIn < ServiceConstants.BackstagePassesThreshold)
                        {
                            if (item.Quality < ServiceConstants.MaximumQuality)
                            {
                                item.Quality = item.Quality + 1;
                            }
                        }

                        if (item.SellIn < ServiceConstants.SellInSixDays)
                        {
                            if (item.Quality < ServiceConstants.MaximumQuality)
                            {
                                item.Quality = item.Quality + 1;
                            }
                        }
                    }
                }
            }

            if (!IsSulfuras(item))
            {
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < ServiceConstants.SellInZeroDay)
            {
                if (!IsAgedBrie(item))
                {
                    if (!IsBackstagePasses(item))
                    {
                        if (item.Quality > ServiceConstants.ZeroQuality)
                        {
                            if (!IsSulfuras(item))
                            {
                                item.Quality = item.Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        item.Quality = item.Quality - item.Quality;
                    }
                }
                else
                {
                    if (item.Quality < ServiceConstants.MaximumQuality)
                    {
                        item.Quality = item.Quality + 1;
                    }
                }
            }
        }
    }

    private static bool IsSulfuras(Item item)
    {
        return item.Name == ProductNames.SulfurasRagnarosHand;
    }

    private static bool IsBackstagePasses(Item item)
    {
        return item.Name == ProductNames.BackstagePasses;
    }

    private static bool IsAgedBrie(Item item)
    {
        return item.Name == ProductNames.AgedBrie;
    }
}