﻿using StardewValley;
using StardewValley.Buildings;
using StardewValley.Locations;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;

namespace BitwiseJonMods
{
    class BuildingContentsInfo
    {
        public int NumberOfContainers { get; set; }
        public int NumberOfItems { get; set; }
        public int NumberReadyToHarvest { get; set; }
        public int NumberReadyToLoad { get; set; }

        public IEnumerable<StardewValley.Object> Containers { get; set; }
        public IEnumerable<StardewValley.Object> ReadyToHarvestContainers { get; set; }
        public IEnumerable<StardewValley.Object> ReadyToLoadContainers { get; set; }

        public bool IsCellar { get; set; }

        public BuildingContentsInfo(GameLocation location, List<string> supportedContainerTypes)
        {
            if (location == null)
            {
                Containers = null;
                NumberOfItems = 0;
                NumberOfContainers = 0;
                NumberReadyToHarvest = 0;
                NumberReadyToLoad = 0;
                IsCellar = false;
            }
            else
            {
                var objects = location.objects.Values;
                Containers = objects.Where(o => supportedContainerTypes.Any(c => o.Name == c)).Select(o => o);
                ReadyToHarvestContainers = Containers.Where(c => c.heldObject.Value != null && (c.readyForHarvest.Value == true || (c.heldObject.Value is Chest && (c.heldObject.Value as Chest).items.Count() > 0)));
                ReadyToLoadContainers = Containers.Where(c => c.heldObject.Value == null && c.readyForHarvest.Value == false && c.name != "Mushroom Box" && c.name != "Auto-Grabber");

                NumberOfItems = Containers.Count(c => c.heldObject.Value != null && c.readyForHarvest.Value == true) + Containers.Where(c => c.heldObject.Value is Chest).Sum(c => (c.heldObject.Value as Chest).items.Sum(i => i.Stack));
                NumberOfContainers = Containers.Count();
                NumberReadyToHarvest = ReadyToHarvestContainers.Count();
                NumberReadyToLoad = ReadyToHarvestContainers.Where(c => c.name != "Mushroom Box" && c.name != "Auto-Grabber").Count() + ReadyToLoadContainers.Count();

                IsCellar = location is Cellar;
            }
        }
    }
}
