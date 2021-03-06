﻿using System.Linq;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace TheGoodBot.Core.Extensions
{
    public static class GetTopColourOfUser
    {
        public static Color GetUserTopColour(this IGuildUser user, SocketCommandContext context)
        {
            var hierarchyOrderedRoleList =
                ((SocketGuildUser) context.User).Roles.OrderByDescending(x => x.Position).ToList();
            int y = 0;
            foreach (var role in hierarchyOrderedRoleList)
            {
                if (hierarchyOrderedRoleList[y].Color == Color.Default) y++;
                else break;
            }

            return hierarchyOrderedRoleList[y].Color;
        }
    }
}