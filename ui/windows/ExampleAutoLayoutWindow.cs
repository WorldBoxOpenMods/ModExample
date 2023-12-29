/*  Example code from https://github.com/goatdie/AncientWarfare2.0
 *  示例代码来自 https://github.com/goatdie/AncientWarfare2.0
 *  Actual author of this part code is Inmny. Last update: 2023/12/29
 *  该部分代码的实际作者是 Inmny. 最后更新: 2023/12/29
 */

using ExampleMod.UI.Prefabs;
using NeoModLoader.General;
using NeoModLoader.General.UI.Window;
using NeoModLoader.General.UI.Window.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;
namespace ExampleMod.UI.Windows;

/// <summary>
///     This is an example of window with auto layout.
///     <para>这是一个自动布局的窗口示例</para>
/// </summary>
public class ExampleAutoLayoutWindow : AutoLayoutWindow<ExampleAutoLayoutWindow>
{
    private Transform grid1;
    private Transform grid2;
    private Text line1_text;
    private Text line2_text;
    private UiUnitAvatarElement unit_avatar;
    // [Hotfixable] is not available in methods provided by MonoBehaviour. If you want to use it, please use it in the the method `update` and call `update` in `Update`
    // [Hotfixable] 不可用于 MonoBehaviour 提供的方法. 如果你想使用它, 请在 `update` 方法中使用, 并在 `Update` 中调用 `update`
    private void Update()
    {
        if (!Initialized || !IsOpened) return;
    }

    protected override void Init()
    {
        GetLayoutGroup().spacing = 3;
        #region Top part 顶部部分

        var top = this.BeginHoriGroup(new Vector2(200, 40), pSpacing: 5, pPadding: new RectOffset(3, 3, 0, 5));
        unit_avatar = Instantiate(
            Resources.Load<GameObject>("windows/inspect_unit")
                .transform.Find("Background/Scroll View/Viewport/Content/Part 1/BackgroundAvatar")
                .GetComponent<UiUnitAvatarElement>(),
            null);
        unit_avatar.GetComponent<RectTransform>().sizeDelta = new Vector2(36, 36);
        top.AddChild(unit_avatar.gameObject);

        #region MultiText group 多行文本组

        var multi_text_group = top.BeginVertGroup(new Vector2(150, 40), pSpacing: 3);
        SimpleText line1 = Instantiate(SimpleText.Prefab, null);
        line1.Setup("", TextAnchor.MiddleCenter, new Vector2(150, 18));
        line1.text.resizeTextMaxSize = 10;
        line1_text = line1.text;

        SimpleText line2 = Instantiate(SimpleText.Prefab, null);
        line2.Setup("", TextAnchor.MiddleCenter, new Vector2(150, 18));
        line2.text.resizeTextMaxSize = 10;
        line2_text = line2.text;

        multi_text_group.AddChild(line1.gameObject);
        multi_text_group.AddChild(line2.gameObject);

        #endregion

        #endregion

        #region Inline text 1 内联文本 1

        SimpleText inline_text1 = Instantiate(SimpleText.Prefab, null);
        inline_text1.Setup("", TextAnchor.MiddleCenter, new Vector2(150, 11));
        inline_text1.background.enabled = false;
        var auto_localized_text = inline_text1.text.gameObject.AddComponent<LocalizedText>();
        auto_localized_text.key = "auto_layout_window_text_1_key";
        auto_localized_text.autoField = true;
        LocalizedTextManager.addTextField(auto_localized_text);

        AddChild(inline_text1.gameObject);

        #endregion

        #region Grid group 1 网格组 1

        var grid1_group = this.BeginHoriGroup(new Vector2(200, 24), pSpacing: 5, pPadding: new RectOffset(3, 3, 0, 0));
        grid1 = grid1_group.transform;
        Image grid1_group_background = grid1_group.gameObject.AddComponent<Image>();
        grid1_group_background.sprite = SpriteTextureLoader.getSprite("ui/special/windowInnerSliced");
        grid1_group_background.type = Image.Type.Sliced;
        grid1_group_background.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        #endregion

        #region Inline text 2 内联文本 2

        SimpleText inline_text2 = Instantiate(SimpleText.Prefab, null);
        inline_text2.Setup("", TextAnchor.MiddleCenter, new Vector2(150, 11));
        inline_text2.background.enabled = false;
        auto_localized_text = inline_text2.text.gameObject.AddComponent<LocalizedText>();
        auto_localized_text.key = "auto_layout_window_text_2_key";
        auto_localized_text.autoField = true;
        LocalizedTextManager.addTextField(auto_localized_text);
        AddChild(inline_text2.gameObject);

        #endregion

        #region Grid group 2 网格组 2

        var grid2_group = this.BeginGridGroup(5, GridLayoutGroup.Constraint.FixedColumnCount, new Vector2(200, 50), new Vector2(24, 24), new Vector2(4, 2));
        grid2 = grid2_group.transform;
        Image grid2_group_background = grid2_group.gameObject.AddComponent<Image>();
        grid2_group_background.sprite = SpriteTextureLoader.getSprite("ui/special/windowInnerSliced");
        grid2_group_background.type = Image.Type.Sliced;
        grid2_group_background.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);

        #endregion
    }
    public override void OnFirstEnable()
    {
        base.OnFirstEnable();
    }
    public override void OnNormalEnable()
    {
        base.OnNormalEnable();
        var unit = World.world.units.GetRandom();
        if (unit == null || !unit.isAlive())
        {
            Clean();
            return;
        }
        unit_avatar.show(World.world.units.GetRandom());
        line1_text.text = unit.coloredName;
        line2_text.text = LM.Get(unit.asset.nameLocale);
    }
    public override void OnNormalDisable()
    {
        base.OnNormalDisable();
        Clean();
    }
    private void Clean()
    {
        line1_text.text = "NONE";
        line2_text.text = "NONE";
    }
}