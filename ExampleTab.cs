using System.Collections.Generic;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;

namespace ExampleMod;

internal static class ExampleTab
{
    public const string INFO = "info";
    public const string DISPLAY = "display";
    public const string CREATURE = "creature";
    public static PowersTab tab;
    public static void Init()
    {
        tab = TabManager.CreateTab("Example", "tab_example", "hotkey_tip_tab_other",
            SpriteTextureLoader.getSprite("ui/icons/iconSteam"));
        tab.SetLayout(new List<string>()
        {
            INFO, DISPLAY, CREATURE
        });

        _addButtons();
        
        tab.UpdateLayout();
    }

    private static void _addButtons()
    {
        tab.AddPowerButton(INFO, PowerButtonCreator.CreateWindowButton("test_1", "", SpriteTextureLoader.getSprite("ui/icons/iconSteam")));
        
        tab.AddPowerButton(INFO, PowerButtonCreator.CreateGodPowerButton("ExampleGodPower1", SpriteTextureLoader.getSprite("ui/icons/iconAttack")));
        tab.AddPowerButton(INFO, PowerButtonCreator.CreateToggleButton("ExampleGodPower2", SpriteTextureLoader.getSprite("ui/icons/iconArmor")));
        tab.AddPowerButton(DISPLAY, PowerButtonCreator.CreateWindowButton("test_4", "", SpriteTextureLoader.getSprite("ui/icons/iconArrowUP")));
        tab.AddPowerButton(CREATURE, PowerButtonCreator.CreateWindowButton("test_5", "", SpriteTextureLoader.getSprite("ui/icons/iconArrowDOWN")));
    }
}