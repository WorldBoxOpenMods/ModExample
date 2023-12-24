using NeoModLoader.api.attributes;
using UnityEngine;

namespace ExampleMod.Content;

internal static class ExampleTraits
{
    [Hotfixable]
    public static void Init()
    {
        ActorTrait trait = new ActorTrait()
        {
            id = "ExampleTrait",
            group_id = "ExampleTraitGroup", // Trait group id
            // 特性组 id
            path_icon = "ui/icons/neomodloader",
            birth = 50 // 50% chance to born with this trait
            // 50% 几率出生时拥有该特性
        };
        trait.base_stats[S.health] = 100;
        // Just for display mod reloading effect. You can replace _reload_switch with manual modifying.
        // 仅用于展示显示模组重载效果(因为代码是静态的, 不能自动修改), 你可以将 _reload_switch 替换为手动修改
        trait.action_special_effect =
            ExampleModMain._reload_switch
                ? ExampleActions.example_trait_special_effect
                : [Hotfixable](pTarget, pTile) =>
                {
                    if (Toolbox.randomChance(0.99f)) return false;
                    ExampleModMain.LogInfo("Before hotfixed trait action");
                    return true;
                };
        // Add trait to trait library
        // 将特性添加到特性库
        AssetManager.traits.add(trait);


        ActorTraitGroupAsset group = new ActorTraitGroupAsset()
        {
            id = "ExampleTraitGroup", // Trait group id
            // 特性组 id
            name = "trait_group_example", // Trait group name's locale key
            // 特性组名称本地化的key
            color = Color.cyan
        };
        // Add trait group to trait group library
        // 将特性组添加到特性组库
        AssetManager.trait_groups.add(group);
    }
}