using System;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using Rocket.Unturned.Events;
using SDG.Unturned;
using Rocket.API.Collections;
using UnityEngine;

namespace Kill4XP
{
    public class Kill4XP : RocketPlugin<Configuration>
    {
        public static Kill4XP Instance;

        protected override void Load()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Rocket.Core.Logging.Logger.Log("Kill4XP Loaded!");
            Console.WriteLine("Revived and fixed by Plugin4U.cf");
            Console.ResetColor();
            UnturnedPlayerEvents.OnPlayerDeath += Events_OnPlayerDeath;
        }
        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= Events_OnPlayerDeath;
            Rocket.Core.Logging.Logger.Log("Kill4XP Unloaded!");
        }

        public void Events_OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, SDG.Unturned.ELimb limb, Steamworks.CSteamID murderer)
        {
            UnturnedPlayer killer = UnturnedPlayer.FromCSteamID(murderer);
            switch (limb)
            {
                case ELimb.SKULL:
                    GiveXP(Instance.Configuration.Instance.Skull_XP, killer, player, Translate("skull_name"));
                    break;
                case ELimb.LEFT_ARM:
                    GiveXP(Instance.Configuration.Instance.Arm_XP, killer, player, Translate("arm_name"));
                    break;
                case ELimb.RIGHT_ARM:
                    GiveXP(Instance.Configuration.Instance.Arm_XP, killer, player, Translate("arm_name"));
                    break;
                 case ELimb.LEFT_FOOT:
                    GiveXP(Instance.Configuration.Instance.Foot_XP, killer, player, Translate("foot_name"));
                    break;
                case ELimb.RIGHT_FOOT:
                    GiveXP(Instance.Configuration.Instance.Foot_XP, killer, player, Translate("foot_name"));
                    break;
                case ELimb.RIGHT_FRONT:
                    GiveXP(Instance.Configuration.Instance.Front_XP, killer, player, Translate("front_name"));
                    break;
                case ELimb.LEFT_FRONT:
                    GiveXP(Instance.Configuration.Instance.Front_XP, killer, player, Translate("front_name"));
                    break;
                case ELimb.SPINE:
                    GiveXP(Instance.Configuration.Instance.Spine_XP, killer, player, Translate("spine_name"));
                    break;
                case ELimb.LEFT_LEG:
                    GiveXP(Instance.Configuration.Instance.Leg_XP, killer, player, Translate("leg_name"));
                    break;
                case ELimb.RIGHT_LEG:
                    GiveXP(Instance.Configuration.Instance.Leg_XP, killer, player, Translate("leg_name"));
                    break;
                case ELimb.LEFT_BACK:
                    GiveXP(Instance.Configuration.Instance.Back_XP, killer, player, Translate("back_name"));
                    break;
                case ELimb.RIGHT_BACK:
                    GiveXP(Instance.Configuration.Instance.Back_XP, killer, player, Translate("back_name"));
                    break;

        }
    }

        public void GiveXP(uint amount, UnturnedPlayer player, UnturnedPlayer killed, string limbName)
        {
            player.Experience += amount;
            Color c = new Color();
            c.r = Instance.Configuration.Instance.plrMsg_r;
            c.g = Instance.Configuration.Instance.plrMsg_g;
            c.b = Instance.Configuration.Instance.plrMsg_b;
            UnturnedChat.Say(player, Translate("msg_playermsg", amount, killed, limbName), c);
            ConsoleColor c2 = ConsoleColor.White;
            try
            {
                c2 = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), Instance.Configuration.Instance.consoleMsgColor, true);
                Rocket.Core.Logging.Logger.Log(Translate("msg_consolemsg"), c2);
            }
            catch
            {
                Rocket.Core.Logging.Logger.Log("[Kill4XP] Please check your color syntax!");
                Rocket.Core.Logging.Logger.Log(Translate("msg_consolemsg"), ConsoleColor.Red);
            }
        }

        public new TranslationList DefaultTranslations
        {
            get
            {
                return new TranslationList(){
                    {"msg_playermsg","You received {0} experience for killing {1} in the {2}."},
                    {"msg_consolemsg","{0} received {1} experience for killing {2} in the {3}."},
                    {"spine_name", "spine"},
                    {"arm_name", "arm"},
                    {"leg_name", "leg"},
                    {"back_name", "back"},
                    {"front_name", "front"},
                    {"foot_name", "foot"},
                    {"skull_name", "skull"}
                };
            }
        }
    }
}
