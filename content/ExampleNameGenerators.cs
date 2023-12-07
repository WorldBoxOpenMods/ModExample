#if 一米_中文名
using System.IO;
using Chinese_Name;
#endif

namespace ExampleMod.Content;

internal static class ExampleNameGenerators
{
    public static void init()
    {
#if 一米_中文名
        init_chinese_name_generators();
#else
        init_vanilla_name_generators();
#endif
    }
#if 一米_中文名
    private static void init_chinese_name_generators()
    {
        WordLibraryManager.SubmitDirectoryToLoad(Path.Combine(ExampleModMain.Instance.GetDeclaration().FolderPath,
            "additional_resources/word_libraries"));
        CN_NameGeneratorLibrary.SubmitDirectoryToLoad(Path.Combine(ExampleModMain.Instance.GetDeclaration().FolderPath,
            "additional_resources/name_generators"));
    }
#endif
    private static void init_vanilla_name_generators()
    {
    }
}