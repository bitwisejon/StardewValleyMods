using StardewModdingAPI;
using BitwiseJonMods.Common;


namespace BitwiseJonMods
{
    class BuildingContentsHandler
    {
        private BuildingContentsInfo _buildingInfo;

        public BuildingContentsHandler(BuildingContentsInfo buildingInfo)
        {
            _buildingInfo = buildingInfo;
        }

        public int HarvestContents(StardewValley.Farmer player)
        {
            int numItemsHarvested = 0;

            //Harvest items into inventory
            foreach (var container in _buildingInfo.ReadyToHarvestContainers)
            {
                //Get the item stored in the container
                var item = (StardewValley.Item)container.heldObject;

                //Make sure player can collect item and inventory is not already full
                if (player.couldInventoryAcceptThisItem(item))
                {
                    //this.Monitor.Log($"  Harvesting item {item.Name} from container {container.Name} and placing in {player.Name}'s inventory.");
                    if (player.IsMainPlayer && !player.addItemToInventoryBool(item, false))
                    {
                        //Inventory was full - throw exception so we can show a message
                        Utility.Log($"  {player.Name} has run out of inventory space. Stopping harvest.");
                        throw new InventoryFullException();
                    }
                    numItemsHarvested++;

                    //Remove this item permanently from the container
                    container.heldObject = (StardewValley.Object)null;
                    container.readyForHarvest = false;
                    container.showNextIndex = false;
                }
                else
                {
                    //Inventory was full - throw exception so we can show a message
                    Utility.Log($"  {player.Name} has run out of inventory space. Stopping harvest.");
                    throw new InventoryFullException();
                }
            }

            return numItemsHarvested;
        }

        public int LoadContents(StardewValley.Farmer player)
        {
            int numItemsLoaded = 0;

            if (player.ActiveObject != null)
            {
                foreach (var container in _buildingInfo.ReadyToLoadContainers)
                {
                    if (player.ActiveObject != null)
                    {
                        //this.Monitor.Log($"  {player.Name} is holding {player.ActiveObject.Name} so placing it in container {container.Name}.");
                        if (container.performObjectDropInAction(player.ActiveObject, false, player))
                        {
                            player.reduceActiveItemByOne();
                            numItemsLoaded++;
                        }
                        else
                        {
                            Utility.Log($"  Unable to load item. Container {container.Name} does not accept items of type {player.ActiveObject.Name}.");
                        }
                    }
                    else
                    {
                        Utility.Log($"  {player.Name} has run out of items to load. Stopping load.");
                        break;
                    }
                }
            }
            else
            {
                Utility.Log($"  {player.Name} is not holding an item, so not loading containers.");
            }

            return numItemsLoaded;
        }
    }
}
