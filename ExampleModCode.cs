using NeoModLoader.api;
using UnityEngine;

namespace ExampleMod;

public class ExampleModMain : BasicMod<ExampleModMain>{
    // public static ExampleModMain Instance { get; }
    protected override void OnModLoad(){
        ExampleModMain.LogInfo("Hello World!");
    }
    public static void Called(){
        ExampleModMain.LogInfo("Hello World From Another!");
    }
}