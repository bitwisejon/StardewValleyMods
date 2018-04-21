using StardewModdingAPI;

namespace BitwiseJonMods
{
    public class ModConfig
    {
        /// <summary>
        /// Indicates if buildings should cost their usual resources. Set to false to build in "sandbox" mode.
        /// </summary>
        public bool BuildUsesResources { get; set; } = true;

        /// <summary>
        /// Button to open and close the Instant Build menu
        /// </summary>
        public SButton ToggleInstantBuildMenuButton { get; set; } = SButton.B;
    }
}
