﻿using GildedRose.ConsoleApp.Models;
using GildedRose.ConsoleApp.Resources;
using GildedRose.ConsoleApp.Services;

namespace GildedRose.ConsoleApp;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(@"OMGHAI!");

        IList<Item> items = new List<Item>
        {
            new() { Name = ProductNames.DexterityVest, SellIn = 10, Quality = 20 },
            new() { Name = ProductNames.AgedBrie, SellIn = 2, Quality = 0 },
            new() { Name = ProductNames.MongooseElixir, SellIn = 5, Quality = 7 },
            new() { Name = ProductNames.SulfurasRagnarosHand, SellIn = 0, Quality = 80 },
            new() { Name = ProductNames.SulfurasRagnarosHand, SellIn = -1, Quality = 80 },
            new() { Name = ProductNames.TAFKAL80ETC, SellIn = 15, Quality = 20 },
            new() { Name = ProductNames.TAFKAL80ETC, SellIn = 10, Quality = 49 },
            new() { Name = ProductNames.TAFKAL80ETC, SellIn = 5, Quality = 49 },
            // this conjured item does not work properly yet
            new() { Name = ProductNames.ConjuredManaCake, SellIn = 3, Quality = 6 }
        };

        var gildedRoseService = new GildedRoseService(items);

        for (var i = 0; i < 31; i++)
        {
            Console.WriteLine(ProgramMessages.DayFormat, i);
            Console.WriteLine(ProgramMessages.ProductProperties);
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("");
            gildedRoseService.UpdateQuality();
        }
    }
}