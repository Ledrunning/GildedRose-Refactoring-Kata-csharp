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
            if (_items[i].Name != ProductNames.AgedBrie && _items[i].Name != ProductNames.TAFKAL80ETC)
            {
                if (_items[i].Quality > ServiceConstants.ZeroQuality)
                {
                    if (_items[i].Name != ProductNames.SulfurasRagnarosHand)
                    {
                        _items[i].Quality = _items[i].Quality - 1;
                    }
                }
            }
            else
            {
                if (_items[i].Quality < ServiceConstants.StandardQuality)
                {
                    _items[i].Quality = _items[i].Quality + 1;

                    if (_items[i].Name == ProductNames.TAFKAL80ETC)
                    {
                        if (_items[i].SellIn < 11)
                        {
                            if (_items[i].Quality < 50)
                            {
                                _items[i].Quality = _items[i].Quality + 1;
                            }
                        }

                        if (_items[i].SellIn < ServiceConstants.SellInSixDays)
                        {
                            if (_items[i].Quality < ServiceConstants.StandardQuality)
                            {
                                _items[i].Quality = _items[i].Quality + 1;
                            }
                        }
                    }
                }
            }

            if (_items[i].Name != ProductNames.SulfurasRagnarosHand)
            {
                _items[i].SellIn = _items[i].SellIn - 1;
            }

            if (_items[i].SellIn < 0)
            {
                if (_items[i].Name != ProductNames.AgedBrie)
                {
                    if (_items[i].Name != ProductNames.TAFKAL80ETC)
                    {
                        if (_items[i].Quality > 0)
                        {
                            if (_items[i].Name != ProductNames.SulfurasRagnarosHand)
                            {
                                _items[i].Quality = _items[i].Quality - 1;
                            }
                        }
                    }
                    else
                    {
                        _items[i].Quality = _items[i].Quality - _items[i].Quality;
                    }
                }
                else
                {
                    if (_items[i].Quality < ServiceConstants.StandardQuality)
                    {
                        _items[i].Quality = _items[i].Quality + 1;
                    }
                }
            }
        }
    }
}