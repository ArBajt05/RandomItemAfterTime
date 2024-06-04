using Rocket.Core.Plugins;
using Rocket.Unturned.Player;
using Rocket.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using System.Threading;
using Rocket.Core.Logging;
using System.Collections;
using UnityEngine;
using Steamworks;
using SDG.Unturned;
using Rocket.API.Collections;

namespace RandomItemAfterTime
{
    public class Class1 : RocketPlugin<Configuration>
    {
        public static Class1 Instance;
        private Dictionary<CSteamID, Coroutine> PlayerTimer = new Dictionary<CSteamID, Coroutine>();

        protected override void Load()
        {
            Instance = this;
            U.Events.OnPlayerConnected += OnPlayerConnected;
            U.Events.OnPlayerDisconnected += OnPlayerDisconnected;

            Rocket.Core.Logging.Logger.Log("RandomItemAfterTime has been loaded");
            Rocket.Core.Logging.Logger.Log("Created By ArBajt");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            U.Events.OnPlayerDisconnected -= OnPlayerDisconnected;

            Rocket.Core.Logging.Logger.Log("RandomItemAfterTime has been unloaded");
            Rocket.Core.Logging.Logger.Log("Created By ArBajt");
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            Coroutine coroutine = StartCoroutine(ItemGiveCoroutine(player));
            PlayerTimer[player.CSteamID] = coroutine;
        }

        private void OnPlayerDisconnected(UnturnedPlayer player)
        {
            if (PlayerTimer.TryGetValue(player.CSteamID, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                PlayerTimer.Remove(player.CSteamID);
            }
        }

        private IEnumerator ItemGiveCoroutine(UnturnedPlayer player)
        {
            while (true)
            {
                yield return new WaitForSeconds(Class1.Instance.Configuration.Instance.TimeToGiveItem);

                if (player != null)
                {
                    GiveRandomItem(player);
                }
                else
                {

                }

                PlayerTimer.Remove(player.CSteamID);
            }
        }

        private void GiveRandomItem(UnturnedPlayer player)
        {
            ushort randomItem = Configuration.Instance.Items[new System.Random().Next(Configuration.Instance.Items.Length)];
            player.GiveItem(randomItem, Class1.Instance.Configuration.Instance.Amount);
        }
    }
}
