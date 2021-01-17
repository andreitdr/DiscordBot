using Discord;
using Discord.WebSocket;

using System.Linq;

namespace DiscordBotPluginManager
{
    public static class Permissions
    {
        public static bool hasPermission(this IRole role, GuildPermission permission)
        {
            return role.Permissions.Has(permission);
        }

        public static bool hasRole(this SocketGuildUser user, IRole role)
        {
            return user.Roles.Contains(role);
        }

        public static bool hasPermission(this SocketGuildUser user, GuildPermission permission)
        {
            var roles = user.Roles;

            foreach (var role in roles)
                if (role.hasPermission(permission))
                    return true;
            return false;
        }
    }
}