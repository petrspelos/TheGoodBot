﻿using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using TheGoodBot.Core.Preconditions;
using TheGoodBot.Core.Services.Languages;
using TheGoodOne.DataStorage;

namespace TheGoodBot.Core.Modules
{
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        private CustomEmbedService _customEmbedService;
        private CreateGuildAccountFilesService _createGuildAccountFiles;
        private GuildAccountService _guildAccountService;

        public TestModule(CustomEmbedService customEmbedService = null, CreateGuildAccountFilesService createGuildAccountFiles = null,
            GuildAccountService guildAccountService = null)
        {
            _customEmbedService = customEmbedService;
            _createGuildAccountFiles = createGuildAccountFiles;
            _guildAccountService = guildAccountService;
        }

        [Cooldown]
        [Command("test"), Alias()]
        [Summary("Simple test command that does nothing but posting a message.")]
        public async Task TestAndStuff() =>
            await _customEmbedService.CreateAndPostEmbed(Context, "test");

        [Cooldown]
        [Command("guild"), Alias()]
        [Summary("Simple test command that does nothing but posting a message.")]
        public async Task Guild() =>
            await _customEmbedService.CreateAndPostEmbed(Context, "guild");
    }
}
