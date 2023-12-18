using NeoModLoader.api.attributes;

namespace ExampleMod.Content;

internal static class ExampleActions
{
    [Hotfixable]
    public static bool example_trait_special_effect(BaseSimObject pTarget, WorldTile pTile)
    {
        if (pTarget == null || !pTarget.isAlive()) return false;
        // You can modify the method when game running. And click the reload button in the mod menu to reload the method.
        // 你可以在游戏运行时修改方法，然后点击mod菜单中的reload按钮来重新加载方法。
        ExampleModMain.LogInfo($"{pTarget.base_data.name} is being affected by ExampleTrait");
        return true;
    }

    // This method will be called when config value set. ATTENTION: It might be called when game start.
    // 这个方法会在配置值被设置时调用。注意：这个方法会在模组被加载时调用。
    public static void ExampleSwitchConfigCallBack(bool pCurrentValue)
    {
        ExampleModMain.LogInfo($"Current value of a switch config is '{pCurrentValue}'");
    }

    // This method will be called when config value set. ATTENTION: It might be called when game start.
    // 这个方法会在配置值被设置时调用。注意：这个方法会在模组被加载时调用。
    public static void ExampleSliderConfigCallback(float pCurrentValue)
    {
        ExampleModMain.LogInfo($"Current value of a slider config is '{pCurrentValue}'");
    }
}