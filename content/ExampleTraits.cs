using UnityEngine;

namespace ExampleMod.Content;

internal static class ExampleTraits
{
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