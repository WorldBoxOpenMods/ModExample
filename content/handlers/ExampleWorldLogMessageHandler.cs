using NeoModLoader.General.Event.Handlers;
using UnityEngine;
namespace ExampleMod.Content.Handlers;

internal class ExampleWorldLogMessageHandler : WorldLogMessageHandler
{
    public override void Handle(ref WorldLogMessage pMessage, ref string pText, ref Color pColor, ref bool pColorField, bool pColorTags)
    {
        switch (pMessage.text)
        {
            case "example_plot_start":
                pText = pText.Replace("$name$", coloredText(ref pMessage, pMessage.special1, pColorTags));
                pText = pText.Replace("$plot_type$", coloredText(ref pMessage, pMessage.special2, pColorTags));
                break;
        }
    }
    private static string coloredText(ref WorldLogMessage pMessage, string pText, bool pColorTags, int pColorId = -1)
    {
        Color color = Color.clear;
        switch (pColorId)
        {
            case 1:
                color = pMessage.color_special1;
                break;
            case 2:
                color = pMessage.color_special2;
                break;
            case 3:
                color = pMessage.color_special3;
                break;
        }

        if (color == Color.clear)
        {
            pColorTags = false;
        }

        if (pColorTags)
        {
            string text = Toolbox.colorToHex(color);
            return "<color=" + text + ">" + pText + "</color>";
        }

        return pText;
    }
}