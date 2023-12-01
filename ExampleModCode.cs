using NeoModLoader.api;
using ExampleMod.UI;
using ExampleMod.Content;
using NeoModLoader.api.attributes;

namespace ExampleMod;

public class ExampleModMain : BasicMod<ExampleModMain>, IReloadable{
    protected override void OnModLoad(){
        LogInfo("Hello World!");
        ExampleGodPowers.init();
        ExampleTab.Init();
        ExampleTraits.Init();
    }
    public static void Called(){
        LogInfo("Hello World From Another!");
    }

    private static bool _reload_switch;
    public void Reload()
    {
        _reload_switch = !_reload_switch;
        var example_trait_to_update = AssetManager.traits.get("ExampleTrait");
        example_trait_to_update.action_special_effect = _reload_switch ? ExampleActions.example_trait_special_effect : null;
        example_trait_to_update.special_effect_interval = 1f;
        
        foreach(var actor in World.world.units){
            if(actor!=null && actor.isAlive() && actor.hasTrait("ExampleTrait")){
                actor.setStatsDirty();
            }
        }
    }
}