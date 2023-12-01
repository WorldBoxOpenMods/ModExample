using System.IO;
using NeoModLoader.api;
using ExampleMod.UI;
using ExampleMod.Content;
using NeoModLoader.api.attributes;
using NeoModLoader.General;

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
    [Hotfixable]
    public void Reload()
    {
        // Reload locales when mod loaded, it's optional.
        // 重载模组时重新加载语言文件, 不是必需的
        var locale_dir = GetLocaleFilesDirectory(GetDeclaration());
        foreach(var file in Directory.GetFiles(locale_dir, "*.json")){
            LM.LoadLocale(Path.GetFileNameWithoutExtension(file), file);
        }
        
        // 实现了重载切换ExampleTrait的action_special_effect
        _reload_switch = !_reload_switch;
        var example_trait_to_update = AssetManager.traits.get("ExampleTrait");
        example_trait_to_update.action_special_effect = _reload_switch ? ExampleActions.example_trait_special_effect : null;
        example_trait_to_update.special_effect_interval = 1f;
        
        // 将特质的效果更新即时应用于所有单位. 
        foreach(var actor in World.world.units){
            if(actor!=null && actor.isAlive() && actor.hasTrait("ExampleTrait")){
                actor.setStatsDirty();
            }
        }
    }
}