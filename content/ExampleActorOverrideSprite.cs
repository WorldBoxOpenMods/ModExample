using System.Linq;
using System.Text;
using UnityEngine;

namespace ExampleMod.Content;

internal static class ExampleActorOverrideSprite
{
    private static readonly string[] walk_sprites =
    {
        "actors/races/human/unit_male_1/walk_0", "actors/races/human/unit_male_1/walk_1",
        "actors/races/human/unit_male_1/walk_2", "actors/races/human/unit_male_1/walk_3"
    };

    private static readonly string[] swim_sprites =
    {
        "actors/races/human/unit_male_2/swim_0", "actors/races/human/unit_male_2/swim_1",
        "actors/races/human/unit_male_2/swim_2", "actors/races/human/unit_male_2/swim_3"
    };

    private static readonly string[] idle_sprites =
    {
        "actors/races/human/unit_male_1/walk_3"
    };

    private static readonly string[] attack_sprites =
    {
        "actors/t_sheep/walk_0", "actors/t_sheep/walk_1"
    };

    public static void init()
    {
        ActorAsset asset = AssetManager.actor_library.get(SA.unit_human);
        asset.has_override_sprite = true;
        asset.get_override_sprite = get_human_override_sprite;
    }

    private static Sprite get_human_override_sprite(Actor actor)
    {
        actor.data.get("last_anim_frame_idx", out int last_frame);
        actor.data.get("last_anim_state",     out string last_anim_state);
        var frame_path = idle_sprites[0];
        var curr_frame = last_frame + 1;
        var curr_anim_state = last_anim_state;

        // Check attack at first
        if (Mathf.Abs(actor.attackTimer - actor.s_attackSpeed_seconds) < 0.01f)
        {
            // Just begin attack action
            curr_frame = 0;
            curr_anim_state = "attack";
        }

        if (actor.attackTimer > 0 && curr_anim_state == "attack")
        {
            // Continue play the attack animation
            if (curr_frame >= attack_sprites.Length)
            {
                // attack animation finishes. Turn to idle
                // do nothing, it can be done below
            }
            else
            {
                frame_path = attack_sprites[curr_frame];

                actor.data.set("last_anim_frame_idx", curr_frame);
                actor.data.set("last_anim_state",     curr_anim_state);
                return get_single_sprite(frame_path);
            }
        }

        if (actor.isAffectedByLiquid())
            curr_anim_state = "swim";
        else if (actor.is_moving)
            curr_anim_state = "walk";
        else
            curr_anim_state = "idle";

        switch (curr_anim_state)
        {
            case "walk":
                curr_frame %= walk_sprites.Length;
                frame_path = walk_sprites[curr_frame];
                break;
            case "swim":
                curr_frame %= swim_sprites.Length;
                frame_path = swim_sprites[curr_frame];
                break;
            case "idle":
                curr_frame %= idle_sprites.Length;
                frame_path = idle_sprites[curr_frame];
                break;
        }

        actor.data.set("last_anim_frame_idx", curr_frame);
        actor.data.set("last_anim_state",     curr_anim_state);
        return get_single_sprite(frame_path);
    }

    private static Sprite get_single_sprite(string path)
    {
        var components = path.Split('/');

        var folder_builder = new StringBuilder();
        folder_builder.Append(components[0]);
        for (var i = 1; i < components.Length - 1; i++)
        {
            folder_builder.Append('/');
            folder_builder.Append(components[i]);
        }

        var sprite_list = SpriteTextureLoader.getSpriteList(folder_builder.ToString());
        return sprite_list.FirstOrDefault(x => x.name == components.Last());
    }
}