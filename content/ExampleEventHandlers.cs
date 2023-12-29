using ExampleMod.Content.Handlers;
using NeoModLoader.General.Event.Listeners;
namespace ExampleMod.Content;

internal static class ExampleEventHandlers
{
    public static void init()
    {
        WorldLogMessageListener.RegisterHandler(new ExampleWorldLogMessageHandler());
        PlotStartListener.RegisterHandler(new ExamplePlotStartLogHandler());
    }
}