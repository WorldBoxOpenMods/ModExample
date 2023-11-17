using NeoModLoader.api;
using NeoModLoader.General;
using NeoModLoader.General.UI.Tab;
using UnityEngine;

namespace ExampleMod;

public class ExampleModMain : BasicMod<ExampleModMain>{
    protected override void OnModLoad(){
        LogInfo("Hello World!");
        ExampleGodPowers.init();
        ExampleTab.Init();
        ExampleTraits.Init();
    }
    public static void Called(){
        LogInfo("Hello World From Another!");
    }
}