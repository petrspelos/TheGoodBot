﻿using System;
using System.Data.Common;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TheGoodBot.Entities;

namespace TheGoodBot.Guilds
{
    public class GuildUserAccountService
    {
        public GuildUserAccountStruct GetOrCreateGuildUserAccount(ulong guildID, ulong userID)
        {
            CreateGuildUserAccount(guildID, userID);
            var guild = GetAccount(guildID, userID);
            return guild;
        }

        public void SaveGuildUserAccount(GuildUserAccountStruct guildUser, string filePath)
        {
            var rawData = JsonConvert.SerializeObject(guildUser, Formatting.Indented);
            File.WriteAllText(filePath, rawData);
        }

        private void CreateGuildUserAccount(ulong guildID, ulong userID)
        {
            string filePath = "GuildUserAccounts/" + guildID + "/" + userID + ".json";
            string directory = "GuildUserAccounts/" + guildID;
            if (!FileExists(filePath, directory))
            {
                var rawData = JsonConvert.SerializeObject(GenerateBlankGuildUserConfig(guildID, userID), Formatting.Indented);
                File.WriteAllText(filePath, rawData);
            }
            else { return; }

        }

        private GuildUserAccountStruct GenerateBlankGuildUserConfig(ulong guildID, ulong userID) => new GuildUserAccountStruct()
        {
            UserId = userID,
            GuildId = guildID

        };

        private static bool FileExists(string filePath, string directory)
        {
            if (File.Exists(filePath)) { return true; }

            Directory.CreateDirectory(directory);
            return false;
        }

        private GuildUserAccountStruct GetAccount(ulong guildID, ulong userID)
        {
            string filePath = "GuildUserAccounts/" + guildID + "/" + userID + ".json";
            var rawData = File.ReadAllText(filePath);
            var guildUser = JsonConvert.DeserializeObject<GuildUserAccountStruct>(rawData);
            return guildUser;
        }
    }
}