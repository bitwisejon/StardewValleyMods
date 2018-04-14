using StardewValley.Buildings;
using System.Collections.Generic;
using System.Linq;

namespace BitwiseJonMods
{
    class BuildingContentsInfo
    {
        public int NumberOfContainers { get; set; }
        public int NumberReadyToHarvest { get; set; }
        public int NumberReadyToLoad { get; set; }

        public IEnumerable<StardewValley.Object> Containers { get; set; }
        public IEnumerable<StardewValley.Object> ReadyToHarvestContainers { get; set; }
        public IEnumerable<StardewValley.Object> ReadyToLoadContainers { get; set; }


        public BuildingContentsInfo(Building building, List<string> supportedContainerTypes)
        {
            if (building == null || building.indoors == null)
            {
                Containers = null;
                NumberOfContainers = 0;
                NumberReadyToHarvest = 0;
                NumberReadyToLoad = 0;
            }
            else
            {
                Containers = building.indoors.Objects.Where(o => supportedContainerTypes.Any(c => o.Value.Name == c)).Select(o => o.Value);
                ReadyToHarvestContainers = Containers.Where(c => c.heldObject != null && c.readyForHarvest == true);
                ReadyToLoadContainers = Containers.Where(c => c.heldObject == null && c.readyForHarvest == false);

                NumberOfContainers = Containers.Count();
                NumberReadyToHarvest = ReadyToHarvestContainers.Count();
                NumberReadyToLoad = NumberReadyToHarvest + ReadyToLoadContainers.Count();
            }
        }
    }
}
