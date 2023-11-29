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
        Reload();
    }
    public static void Called(){
        LogInfo("Hello World From Another!");
    }

    public void Reload()
    {
        ExampleHotfixClass.HotfixExample();
        HotfixExample();
    }
    [Hotfixable]
    public static void HotfixExample()
    {
        LogInfo("Hello World after Hotfix! at 2 from Main");
    }
}

public class ExampleHotfixClass
{
    [Hotfixable]
    public static void HotfixExample()
    {
        ExampleModMain.LogInfo("Hello World after Hotfix! at 2");
    }
}