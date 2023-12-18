using System.IO;
using ExampleMod.Content;
using ExampleMod.UI;
using NeoModLoader.api;
using NeoModLoader.api.attributes;
using NeoModLoader.General;

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
///     <para>这个类是模组的主类, 你可以把它放到任何地方, 也可以起任意的名字, 也可以设置为internal</para>
///     <para><see cref="BasicMod{T}" /> 是一个比较通用的模组基类, 它实现了一些有用的函数</para>
///     <para><see cref="IReloadable" /> 让模组可以重载. 是可选的. 如果你实现了这个接口, 你可在模组列表里看到模组重载按钮</para>
/// </summary>
public class ExampleModMain : BasicMod<ExampleModMain>, IReloadable
{
    // Just for displaying mod reloading effect. To emulate the effect of reloading mod, you can replace _reload_switch with manual modifying.
    // 仅用于展示显示模组重载效果(因为代码是静态的, 不能自动修改), 你可以将 _reload_switch 替换为手动修改
    internal static bool _reload_switch;

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
        foreach (var file in Directory.GetFiles(locale_dir, "*.json"))
        {
            LM.LoadLocale(Path.GetFileNameWithoutExtension(file), file);
        }
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
            if (actor != null && actor.isAlive() && actor.hasTrait("ExampleTrait"))
            {
                actor.setStatsDirty();
            }
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
        LogInfo("Hello World!");
        // Example of add name generators and example of mod optional dependencies.
        ExampleNameGenerators.init();
        // Example of new tab and buttons.
        ExampleGodPowers.init();
        ExampleTab.Init();
        // Example of adding traits, traits group and mod reloading.
        ExampleTraits.Init();
    }

    // Example code for mod be called by other mods. You can test mod dependencies by calling this method in your mod. If no other mods call this method, this method is simply useless.
    // 被其他模组调用的示例代码. 你可以在你的模组中调用这个方法来测试模组依赖. 如果没有其他模组调用这个方法, 这个方法就是无用的.
    public static void Called()
    {
        LogInfo("Hello World From Another!");
    }
}