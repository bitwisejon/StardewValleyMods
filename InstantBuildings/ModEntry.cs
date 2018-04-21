using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitwiseJonMods
{
    public class ModEntry : Mod
    {
        private ModConfig _config;
        private bool _tractorModFound = false;

        public override void Entry(IModHelper helper)
        {
            BitwiseJonMods.Common.Utility.InitLogging(this.Monitor);

            _config = helper.ReadConfig<ModConfig>();
            _tractorModFound = helper.ModRegistry.IsLoaded("Pathoschild.TractorMod");

            BitwiseJonMods.Common.Utility.Log(string.Format("Config BuildUsesResources={0}", _config.BuildUsesResources));
            BitwiseJonMods.Common.Utility.Log(string.Format("Config ToggleInstantBuildMenuButton={0}", _config.ToggleInstantBuildMenuButton));
            BitwiseJonMods.Common.Utility.Log(string.Format("Tractor Mod Found={0}", _tractorModFound));

            InputEvents.ButtonPressed += this.InputEvents_ButtonPressed;
        }

        /// <summary>The event called when the player presses a keyboard button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event arguments.</param>
        private void InputEvents_ButtonPressed(object sender, EventArgsInput e)
        {
            if (e.Button == _config.ToggleInstantBuildMenuButton && Game1.currentLocation is Farm)
            {
                if (Context.IsPlayerFree && Game1.activeClickableMenu == null)
                {
                    if (_tractorModFound)
                    {
                        //Get tractor blueprint from carpenter menu
                        var carpenterMenu = new CarpenterMenu();
                        Game1.activeClickableMenu = (IClickableMenu)carpenterMenu;
                        Game1.delayedActions.Add(new DelayedAction(100, new DelayedAction.delayedBehavior(this.getTractorBlueprintFromCarpenterMenu)));
                    }
                    else
                    {
                        activateInstantBuildMenu();
                    }
                }
                else if (Game1.activeClickableMenu is InstantBuildMenu)
                {
                    Game1.displayFarmer = true;
                    ((InstantBuildMenu)Game1.activeClickableMenu).exitThisMenu();
                }
            }
        }

        private void activateInstantBuildMenu(BluePrint tractorBlueprint = null)
        {
            Game1.activeClickableMenu = (IClickableMenu)new InstantBuildMenu(_config, tractorBlueprint);
        }

        private void getTractorBlueprintFromCarpenterMenu()
        {
            var menu = ((CarpenterMenu)Game1.activeClickableMenu);

            var blueprints = this.Helper.Reflection
                .GetField<List<BluePrint>>(menu, "blueprints")
                .GetValue();

            var tractorBlueprint = blueprints.SingleOrDefault(b => b.name == "TractorGarage");
            menu.exitThisMenu();

            activateInstantBuildMenu(tractorBlueprint);
        }
    }
}
