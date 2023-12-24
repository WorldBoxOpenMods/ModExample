// Wrap every thing for "optional" dependencies with macro. "一米_中文名" is the mod's id. Chinese_Name is the namespace provided by mod "一米_中文名".
// 用宏包裹所有"可选"依赖. "一米_中文名"是这个可选依赖的模组id. Chinese_Name是模组"一米_中文名"提供的命名空间.
using System.IO;
#if 一米_中文名
using Chinese_Name;
#endif

namespace ExampleMod.Content;

internal static class ExampleNameGenerators
{
    public static void init()
    {
        // If mod "一米_中文名" is compiled, init chinese name generators. Otherwise, init vanilla name generators.
        // 如果模组"一米_中文名"被编译, 初始化中文名生成器. 否则, 初始化原版名字生成器.
#if 一米_中文名
        init_chinese_name_generators();
#else
        init_vanilla_name_generators();
#endif
    }
#if 一米_中文名
    private static void init_chinese_name_generators()
    {
        // Because the following methods and classes are provided by mod "一米_中文名", you should use macro to wrap them.
        // 因为下面的方法和类是由模组"一米_中文名"提供的, 你应该用宏包裹它们.
        WordLibraryManager.SubmitDirectoryToLoad(Path.Combine(ExampleModMain.Instance.GetDeclaration().FolderPath,
            "additional_resources/word_libraries"));
        CN_NameGeneratorLibrary.SubmitDirectoryToLoad(Path.Combine(ExampleModMain.Instance.GetDeclaration().FolderPath,
            "additional_resources/name_generators"));
    }
#endif
    private static void init_vanilla_name_generators()
    {
        /// Add Name Generator for Example Item <see cref= "ExampleItems"/>
        /// 给示例物品添加名字生成器 <see cref= "ExampleItems"/>
        NameGeneratorAsset example_item_gn = new NameGeneratorAsset()
        {
            id = "example_item_name_template",
            use_dictionary = true
        };
        example_item_gn.motto_parts.Add("first", "Scruffy,Shabby,Sturdy,Bloody,Smooth,Worn,Savage,Broken,Suede,Fancy,Classic,Breezy,Elegant,Rugged,Ruched,Superior,Treaded");
        example_item_gn.motto_parts.Add("space", " ");
        example_item_gn.motto_parts.Add("second", "Bone Bow,Great Bone");
        example_item_gn.templates.Add("first,space,second");
        example_item_gn.templates.Add("first,space,second");
        example_item_gn.templates.Add("first,space,second");
        example_item_gn.templates.Add("second");

    }
}