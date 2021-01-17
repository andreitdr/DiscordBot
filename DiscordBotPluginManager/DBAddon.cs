namespace DiscordBotPluginManager
{
    public interface DBAddon
    {
        string Name { get; }

        string Description { get; }

        int Execute(System.Windows.Forms.Form formToChange);
    }
}