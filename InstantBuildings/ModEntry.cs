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

        public override void Entry(IModHelper helper)
        {
            _config = helper.ReadConfig<ModConfig>();

            BitwiseJonMods.Common.Utility.InitLogging(this.Monitor);
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
                    Game1.activeClickableMenu = (IClickableMenu)new InstantBuildMenu(_config);
                }
                else if (Game1.activeClickableMenu is InstantBuildMenu)
                {
                    Game1.displayFarmer = true;
                    ((InstantBuildMenu)Game1.activeClickableMenu).exitThisMenu();
                }
            }

        }
    }
}
