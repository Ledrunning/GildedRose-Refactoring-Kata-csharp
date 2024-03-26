using GildedRose.ConsoleApp.Constants;
using GildedRose.ConsoleApp.Models;
using GildedRose.ConsoleApp.Resources;

namespace GildedRose.ConsoleApp.Services;

public class GildedRoseService
{
    private readonly IList<Item> _items;

    public GildedRoseService(IList<Item> items) => _items = items;

    public void UpdateQuality()
    {
        for (var i = 0; i < _items.Count; i++)
        {
            var item = _items[i];
            if (item.Name != ProductNames.AgedBrie && item.Name != ProductNames.TAFKAL80ETC)
            {
                if (item.Quality > ServiceConstants.ZeroQuality)
                {
                    if (item.Name != ProductNames.SulfurasRagnarosHand)
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

                    if (item.Name == ProductNames.TAFKAL80ETC)
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

            if (item.Name != ProductNames.SulfurasRagnarosHand)
            {
                item.SellIn = item.SellIn - 1;
            }

            if (item.SellIn < ServiceConstants.SellInZeroDay)
            {
                if (item.Name != ProductNames.AgedBrie)
                {
                    if (item.Name != ProductNames.TAFKAL80ETC)
                    {
                        if (item.Quality > ServiceConstants.ZeroQuality)
                        {
                            if (item.Name != ProductNames.SulfurasRagnarosHand)
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
}