using System;
using System.Collections.Generic;
using NeoModLoader.General.Game;

namespace ExampleMod.Content;

internal static class ExampleItems
{
    public static void Init()
    {
        // 添加示例装备材料
        add_example_item_material();
        // 添加示例装备词条
        add_example_item_modifier();
        // 添加示例装备
        add_example_item();
        // 为人类添加示例装备锻造
        // Add example item creating to human
        add_example_item_to_human();
    }

    private static void add_example_item_to_human()
    {
        // 获取人类种族信息
        // Get information of human
        Race human = AssetManager.raceLibrary.get(S.human);
        // 添加示例装备, 20是权重(是一个偏大的权重, 原版多数集中于5左右), 这里是为了更容易见到效果
        // Add example item, 20 is weight (it's a big weight, vanilla is mostly around 5), here is for easier to see the effect
        for(int i= 0; i < 20; i++)
        {
            human.preferred_weapons.Add("example_range_item");
        }
    }

    private static void add_example_item_modifier()
    {
        BaseStats stats = new BaseStats();
        stats[S.attack_speed] = 10;
        ItemAsset example_item_modifier = ItemAssetCreator.CreateAndAddModifier(
            id: "example_item_modifier1",               // 示例词条的ID, 唯一
                                                        // ID of example modifier, unique
            mod_type: "example_item_modifier",          // 示例词条的类型, 可相同
                                                        // Type of example modifier, repeatable
            mod_rank: 1,                                // 示例词条的等级, 用于区分同类型词条
                                                        // Rank of example modifier, used to distinguish modifiers of the same type
            translation_key: "example_item_modifier",   // 示例词条的翻译键, 用于显示, 需要在翻译文件中添加
                                                        // Translation key of example modifier, used to display, need to add in translation file
            pools: new []{"weapon"},                    // 示例词条的池, 用于决定词条的应用范围, 例如weapon池的词条只能用于武器
                                                        // Pools of example modifier, used to decide the scope of modifier, for example, modifier in weapon pool can only be used on weapon
            rarity: 5,                                  // 示例词条的稀有度, 用于决定词条的出现概率, 值越大出现概率越高, 原版多数集中于1或3左右
                                                        // Rarity of example modifier, used to decide the probability of modifier, the bigger the value, the higher the probability, vanilla is mostly around 1 or 3
            equipment_value: 1,                         // 不想写注释了, 自己去看文档 https://worldboxopenmods.gitbook.io/mod-tutorial-zh/you-xi-nei-rong-tian-jia/itemcreator
            // Don't want to write comments, go see the document yourself https://worldboxopenmods.gitbook.io/mod-tutorial-zh/you-xi-nei-rong-tian-jia/itemcreator
            quality: ItemQuality.Epic,
            base_stats: stats,
            action_attack_target: null,
            action_special_effect: null,
            special_effect_interval: 1
        );
        // 复制创建一个等级为2级的示例词条
        // Clone a modifier with different rank
        var modifier2 = AssetManager.items_modifiers.clone("example_item_modifier2", example_item_modifier.id);
        modifier2.quality = ItemQuality.Legendary;
        modifier2.mod_rank = 2;
        modifier2.base_stats[S.attack_speed] = 20;
    }

    private static void add_example_item_material()
    {
        // 不想写注释了, 自己去看文档 https://worldboxopenmods.gitbook.io/mod-tutorial-zh/you-xi-nei-rong-tian-jia/itemcreator
        // Don't want to write comments, go see the document yourself https://worldboxopenmods.gitbook.io/mod-tutorial-zh/you-xi-nei-rong-tian-jia/itemcreator
        BaseStats stats = new BaseStats();
        stats[S.armor] = 10;
        ItemAsset example_item_material = ItemAssetCreator.CreateWeaponMaterial(
            id: "example_item_material",
            base_stats: stats,
            cost_gold: 1,
            cost_resources: new KeyValuePair<string, int>[]
            {
                new(SR.common_metals, 10), new(SR.wood, 4)
            },
            equipment_value: 10,
            metallic: false,
            minimum_city_storage_resource_1: 12,
            mod_rank: 1,
            quality: ItemQuality.Legendary,
            tech_needed: null
        );
        AssetManager.items_material_weapon.add(example_item_material);
    }

    private static void add_example_item()
    {
        // 不想写注释了, 自己去看文档 https://worldboxopenmods.gitbook.io/mod-tutorial-zh/you-xi-nei-rong-tian-jia/itemcreator
        // Don't want to write comments, go see the document yourself https://worldboxopenmods.gitbook.io/mod-tutorial-zh/you-xi-nei-rong-tian-jia/itemcreator
        BaseStats stats = new BaseStats();
        stats[S.projectiles] = 3;
        stats[S.range] = 10;
        stats[S.damage] = 100;
        ItemAsset example_item = ItemAssetCreator.CreateRangeWeapon(
            id: "example_range_item",
            projectile: "bone",
            base_stats: stats,
            materials: new List<string> { "example_item_material" },
            item_modifiers: new List<string> { "example_item_modifier1" },
            name_class: "item_class_example_range_item",
            name_templates: new List<string> { "example_item_name_template" },
            tech_needed: "weapon_bow",
            action_attack_target: null,
            action_special_effect: null,
            special_effect_interval: 1,
            equipment_value: 10,
            path_slash_animation: "effects/slashes/slash_bow"
        );
    }
}