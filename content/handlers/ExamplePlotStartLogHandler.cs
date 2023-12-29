using NeoModLoader.api.attributes;
using NeoModLoader.General;
using NeoModLoader.General.Event.Handlers;
namespace ExampleMod.Content.Handlers;

internal class ExamplePlotStartLogHandler : PlotStartHandler
{
    [Hotfixable]
    public override void Handle(Plot pPlot, Actor pActor, PlotAsset pAsset)
    {
        WorldLogMessage plot_start = new("example_plot_start", pActor.getName(), string.IsNullOrEmpty(pAsset.translation_key) ? LM.Get("unknown_plot") : LM.Get(pAsset.translation_key));
        plot_start.icon = $"../../{pAsset.path_icon}";
        plot_start.add();
    }
}