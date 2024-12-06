using System;
using System.IO;
using ExampleMod.Content;
using ExampleMod.UI;
using NeoModLoader.api;
using NeoModLoader.api.attributes;
using NeoModLoader.General;
using UnityEngine;

namespace ExampleMod;

/// <summary>
///     <para>
///         This class is the main class of the mod. It can be named as you like, be placed in any namespace, be placed
///         in any folder and be internal.
///     </para>
///     <para><see cref="BasicMod{T}" /> is a common mod class, it implements some useful functions.</para>
///     <para>
///         <see cref="IReloadable" /> let the mod can be reloaded. It's optional. If you implement this interface, you
///         can see reload button in mod list.
///     </para>
///     <para>
///         <see cref="IUnloadable"/> let the mod can be unloaded. It's optional. If you implement this interface, you
///         can "unload" this mod when disable this mod.
///     </para>
///     <para>这个类是模组的主类, 你可以把它放到任何地方, 也可以起任意的名字, 也可以设置为internal</para>
///     <para><see cref="BasicMod{T}" /> 是一个比较通用的模组基类, 它实现了一些有用的函数</para>
///     <para><see cref="IReloadable" /> 让模组可以重载. 是可选的. 如果你实现了这个接口, 你可在模组列表里看到模组重载按钮</para>
///     <para><see cref="IUnloadable"/> 让模组可以卸载. 是可选的. 如果你实现了这个接口, 你可以在禁用模组时即使"卸载"该模组</para>
/// </summary>
public class ExampleModMain : BasicMod<ExampleModMain>, IReloadable, IUnloadable
{
    // Just for displaying mod reloading effect. To emulate the effect of reloading mod, you can replace _reload_switch with manual modifying.
    // 仅用于展示显示模组重载效果(因为代码是静态的, 不能自动修改), 你可以将 _reload_switch 替换为手动修改
    internal static bool _reload_switch;

    // It is used for storing self made prefab, avoiding prefab objects under root scene. It's optional.
    // 用于存储自制的预制体, 避免预制体对象直接暴露在场景根节点下. 不是必需的.
    internal static Transform prefab_library;

    /// <summary>
    ///     <para>
    ///         To test reloading function, you can modify traits in <see cref="ExampleTraits" /> or trait action in
    ///         <see cref="ExampleActions" /> then click Reload button in mod list
    ///     </para>
    ///     <para>You can modify them both in once reloading.</para>
    ///     <para>
    ///         为了测试重载功能, 你可以修改<see cref="ExampleTraits" />中的traits, 你也可以在里面添加新的trait, 或者修改<see cref="ExampleActions" />
    ///         中的特质行为, 然后点击模组列表中该模组的重载按钮
    ///     </para>
    ///     <para>你可以在一次重载中进行多次修改</para>
    /// </summary>
    // Let the method can be hotfixed when it is modified and after the mod is reloaded. You can add this attribute at runtime.
    // 让方法在修改后和模组重载后可以被热更新. 你可以在游戏运行时添加这个属性
    [Hotfixable]
    // This method will be called when the mod is reloaded. "OnModLoad" won't be called when mod reloaded. To test it, you can modify the code in 
    // 这个函数会在模组函数热更新后被调用. "OnModLoad"仅在模组加载时被调用, 重载时不会被调用
    public void Reload()
    {
        // Reload logic here. It mainly reloads traits added and applies traits' modification to every units immediately here.
        // 重载逻辑在这里. 这里的功能主要是重新初始化特质, 并将特质的修改立即应用于所有单位.

        // Reload locales when mod reloaded, it's optional.
        // 重载模组时重新加载语言文件, 不是必需的
        var locale_dir = GetLocaleFilesDirectory(GetDeclaration());
        foreach (var file in Directory.GetFiles(locale_dir))
        {
            if (file.EndsWith(".json"))
            {
                LM.LoadLocale(Path.GetFileNameWithoutExtension(file), file);
            }
            else if (file.EndsWith(".csv"))
            {
                LM.LoadLocales(file);
            }
        }

        LM.ApplyLocale();
        // Reload mod resources when mod reloaded, it's optional.
        // 重载模组时重新加载模组资源, 不是必需的
        // Code is coming soon.
        // 代码很快就有了

        // Emulate methods modified.(Because code is static, it can't be modified automatically. I mean the example code is static and it cannot modify it automatically.)
        // 用于模拟函数被修改(因为代码是静态的, 不能自动修改, 我的意思是示例代码是静态的, 它不会自动修改)
        _reload_switch = !_reload_switch;
        // Reload Example Trait. It's optional.
        // 重载示例特质, 不是必需的.
        ExampleTraits.Init();

        // Apply traits' modification to every units immediately here.
        // 将特质的效果更新即时应用于所有单位. 
        foreach (var actor in World.world.units)
        {
            // Search all units and apply traits' modification to them.
            // 搜索所有拥有ExampleTrait的单位并将特质的效果更新即时应用于它们.
            if (actor != null && actor.isAlive() && actor.hasTrait("ExampleTrait"))
            {
                actor.setStatsDirty();
            }
        }
    }

    // This method will be called when the mod is unloaded. You can unload part of the mod.
    // 这个函数会在模组"卸载"时被调用. 你可以只卸载模组的一部分.
    public void OnUnload()
    {
        // We only unload a trait here. You can unload more things.
        // 我们只卸载一个特性. 你可以卸载更多东西.

        // Apply traits' modification to every units immediately here.
        // 先移除所有单位的ExampleTrait即时应用于所有单位. 
        foreach (var actor in World.world.units)
        {
            // Search all units and apply traits' modification to them.
            // 搜索所有拥有ExampleTrait的单位并移除这个特质即时应用于它们.
            if (actor != null && actor.isAlive() && actor.hasTrait("ExampleTrait"))
            {
                actor.removeTrait("ExampleTrait");
                actor.setStatsDirty();
            }
        }

        // Remove trait from trait library
        // 从特性库移除特性
        if (AssetManager.traits.get("ExampleTrait") != null)
        {
            AssetManager.traits.list.Remove(AssetManager.traits.get("ExampleTrait"));
            AssetManager.traits.dict.Remove("ExampleTrait");
        }
    }

    /// <summary>
    ///     <para>
    ///         You can initialize you mod here, some methods called in order OnModLoad -> Awake -> OnEnable -> Start ->
    ///         Update -> Update -> ...
    ///     </para>
    ///     <para>你可以在这里初始化你的模组, MonoBehaviour的函数调用顺序 OnModLoad -> Awake -> OnEnable -> Start -> Update -> Update -> ...</para>
    /// </summary>
    protected override void OnModLoad()
    {
        // Hello world
        // 打印 Hello World
        LogInfo("Hello World!");
        // Create a prefab library to store prefabs. It's optional.
        // 创建一个预制体存储库. 不是必需的.
        prefab_library = new GameObject("PrefabLibrary").transform;
        prefab_library.SetParent(transform);
        // Example of enabling mod reload feature(You need to replace the user name with your own one)
        // Use this avoiding players from using mod reloading
        // 启用模组重载功能的示例(你需要把名字换成自己的电脑用户)
        // 避免玩家使用模组重载
        if (Environment.UserName == "Inmny")
        {
            Config.isEditor = true;
        }

        // Example of add name generators and example of mod optional dependencies.
        // 添加名字生成器的示例和模组可选依赖的示例.
        ExampleNameGenerators.init();
        // Example of new tab and buttons.
        // 新建标签页和按钮的示例.
        ExampleGodPowers.init();
        ExampleTab.Init();
        // Example of adding traits, traits group and mod reloading and unloading.
        // 添加特性, 特性组和模组重载的示例.
        ExampleTraits.Init();
        // Example of adding items, item modifiers and item materials.
        // 添加装备, 装备词条和装备材料的示例.
        ExampleItems.Init();
        // Example of creating event handlers and add new world log message.
        // It implements two event handlers to handle Plot Start event and World Log Message format event to add new world log message type for tipping plot start.
        // 创建事件处理器和添加新的世界日志消息的示例.
        // 它实现了两个事件处理器来处理plot开始事件和世界日志消息格式事件, 以添加新的世界日志消息类型来提示一场plot的开始.
        ExampleEventHandlers.init();
        ExampleActorOverrideSprite.init();
    }

    // Example code for mod be called by other mods. You can test mod dependencies by calling this method in your mod. If no other mods call this method, this method is simply useless.
    // 被其他模组调用的示例代码. 你可以在你的模组中调用这个方法来测试模组依赖. 如果没有其他模组调用这个方法, 这个方法就是无用的.
    public static void Called()
    {
        LogInfo("Hello World From Another!");
    }
}