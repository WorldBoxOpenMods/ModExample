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
            group_id = "ExampleTraitGroup",
            path_icon = "ui/icons/neomodloader",
            birth = 50
        };
        trait.base_stats[S.health] = 100;
        // Just for display mod reloading effect. You can replace _reload_switch with manual modifying.
        // 仅用于展示显示模组重载效果(因为代码是静态的, 不能自动修改), 你可以将 _reload_switch 替换为手动修改
        trait.action_special_effect =
            ExampleModMain._reload_switch ? ExampleActions.example_trait_special_effect : null;
        AssetManager.traits.add(trait);


        ActorTraitGroupAsset group = new ActorTraitGroupAsset()
        {
            id = "ExampleTraitGroup",
            name = "trait_group_example",
            color = Color.cyan
        };
        AssetManager.trait_groups.add(group);
    }
}