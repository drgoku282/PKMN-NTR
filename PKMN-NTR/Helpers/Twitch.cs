using System;
using TwitchLib;
using TwitchLib.Models.Client;

namespace pkmn_ntr.Helpers
{
    class Twitch
    {
        TwitchClient client;
        private ConnectionCredentials credentials;
        public void Connect(string botname, string authToken, string channelName)
        {
            credentials = new ConnectionCredentials(botname, authToken);
            client = new TwitchClient(credentials, channelName);
            client.Connect();
        }

        public void sendMessage(String text)
        {
            client.SendMessage(text);
        }
    }
}
