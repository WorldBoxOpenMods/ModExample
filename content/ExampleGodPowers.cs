namespace ExampleMod.Content;

internal static class ExampleGodPowers
{
    public static void init()
    {
        GodPower power = new GodPower();
        power.id = "ExampleGodPower1";
        power.name = "Example God Power 1";
        AssetManager.powers.add(power);

        power = new GodPower();
        power.id = "ExampleGodPower2";
        power.name = "Example God Power 2";
        power.toggle_name = "ExampleGodPowerToggle";
        AssetManager.powers.add(power);
    }
}