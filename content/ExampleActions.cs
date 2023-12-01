using NeoModLoader.api.attributes;

namespace ExampleMod.Content;

internal static class ExampleActions
{
    [Hotfixable]
    public static bool example_trait_special_effect(BaseSimObject pTarget, WorldTile pTile){
        if (pTarget == null || !pTarget.isAlive()) return false;
        // You can modify the method when game running. And click the reload button in the mod menu to reload the method.
        // 你可以在游戏运行时修改方法，然后点击mod菜单中的reload按钮来重新加载方法。
        ExampleModMain.LogInfo($"{pTarget.base_data.name} is being affected by ExampleTrait");
        return true;
    }
}