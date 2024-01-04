using System.Collections.Generic;
using ExampleMod.UI.Windows;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;

namespace ExampleMod.UI;

internal static class ExampleTab
{
    public const string INFO = "info";
    public const string DISPLAY = "display";
    public const string CREATURE = "creature";
    public static PowersTab tab;

    public static void Init()
    {
        // Create a tab with id "Example", title key "tab_example", description key "hotkey_tip_tab_other", and icon "ui/icons/iconSteam".
        // 创建一个id为"Example", 标题key为"tab_example", 描述key为"hotkey_tip_tab_other", 图标为"ui/icons/iconSteam"的标签页.
        tab = TabManager.CreateTab("Example", "tab_example", "hotkey_tip_tab_other",
            SpriteTextureLoader.getSprite("ui/icons/iconSteam"));
        // Set the layout of the tab. The layout is a list of strings, each string is a category. Names of each category are not important.
        // 设置标签页的布局. 布局是一个字符串列表, 每个字符串是一个分类. 每个分类的名字不重要.
        tab.SetLayout(new List<string>()
        {
            INFO,
            DISPLAY,
            CREATURE
        });
        // Create windows.
        // 创建窗口.
        _createWindows();
        // Add buttons to the tab.
        // 向标签页添加按钮.
        _addButtons();
        // Update the layout of the tab.
        // 更新标签页的布局.
        tab.UpdateLayout();
    }

    private static void _createWindows()
    {
        ExampleAutoLayoutWindow.CreateWindow(nameof(ExampleAutoLayoutWindow),
            nameof(ExampleAutoLayoutWindow) + " Title");
        ExampleWideWindow.CreateAndInit(nameof(ExampleWideWindow));
    }

    private static void _addButtons()
    {
        tab.AddPowerButton(INFO,
            PowerButtonCreator.CreateWindowButton("test_1", nameof(ExampleAutoLayoutWindow),
                SpriteTextureLoader.getSprite("ui/icons/iconSteam")));

        tab.AddPowerButton(INFO,
            PowerButtonCreator.CreateGodPowerButton("ExampleGodPower1",
                SpriteTextureLoader.getSprite("ui/icons/iconAttack")));
        tab.AddPowerButton(INFO,
            PowerButtonCreator.CreateToggleButton("ExampleGodPower2",
                SpriteTextureLoader.getSprite("ui/icons/iconArmor")));
        tab.AddPowerButton(DISPLAY,
            PowerButtonCreator.CreateWindowButton("test_4", nameof(ExampleWideWindow),
                SpriteTextureLoader.getSprite("ui/icons/iconArrowUP")));
        tab.AddPowerButton(CREATURE,
            PowerButtonCreator.CreateWindowButton("test_5", "",
                SpriteTextureLoader.getSprite("ui/icons/iconArrowDOWN")));
    }
}