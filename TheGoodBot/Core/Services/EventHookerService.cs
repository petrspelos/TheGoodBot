﻿using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using TheGoodBot.Core.Services.Accounts;
using TheGoodBot.Entities;
using TheGoodBot.Guilds;
using TheGoodBot.Languages;
using TheGoodOne.DataStorage;

namespace TheGoodBot.Core.Services
{
    public class EventHookerService
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private LoggerService _logger;
        private GuildAccountService _guildAccount;
        private GuildUserAccountService _guildUser;
        private GlobalUserAccountService _user;
        private CreateLanguageFilesService _language;
        private CooldownService _cooldown;
        private BotConfig _config;

        public EventHookerService(DiscordSocketClient client, CommandService command, LoggerService logger,
            GuildUserAccountService guildUser, GlobalUserAccountService user, CreateLanguageFilesService language,
            GuildAccountService guildAccount, CooldownService cooldown, BotConfig config)
        {
            _client = client;
            _commands = command;
            _logger = logger;
            _guildUser = guildUser;
            _user = user;
            _language = language;
            _guildAccount = guildAccount;
            _cooldown = cooldown;
            _config = config;
        }

        public void HookEvents()
        {
            _client.Ready += Ready;
            _client.JoinedGuild += GuildJoined;
        }

        private async Task GuildJoined(SocketGuild guild)
        {
            _guildAccount.AddGuild(guild.Id);
            await Task.CompletedTask;
        }

        private Task Ready()
        {
            _language.CreateAllLanguageFiles();
            _guildAccount.CreateAllGuildAccounts();
            _guildAccount.CreateAllGuildCooldownsAndInvocations();
            _client.SetStatusAsync(UserStatus.DoNotDisturb);
            _client.SetGameAsync(_config.GameStatus);
            Console.WriteLine("Ready, sir.");
            return Task.CompletedTask;
        }
    }
}