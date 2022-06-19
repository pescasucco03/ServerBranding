using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using Exiled.API.Extensions;
using Mirror;
using Respawning;
using Respawning.NamingRules;
using Player = Exiled.Events.Handlers.Player;
using Version = System.Version;

namespace ServerBranding
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "ServerBranding";
        public override string Author { get; } = "Rowpann's Emporium dev team";
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 0);
        
        private EventHandlers _events;
        public static Plugin Instance;

        public override void OnEnabled()
        {
            Instance = this;
            _events = new EventHandlers();
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            _events = null;
            Instance = null;
            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            Player.Verified += _events.OnVerified;
        }

        private void UnregisterEvents()
        {
            Player.Verified -= _events.OnVerified;
        }
    }

    public static class Extensions
    {
        // stolen from joker
        public static void SendFakeUnitName(this Exiled.API.Features.Player target, string name, SpawnableTeamType spawnableTeamType = SpawnableTeamType.NineTailedFox)
        {
            // Log.Debug($"{nameof(SendFakeUnitName)}: Sending {target.Nickname} a fake unit name: {name}", plugin.Config.Debug);
            MirrorExtensions.SendFakeSyncObject(target, RespawnManager.Singleton.NamingManager.netIdentity, typeof(UnitNamingManager), writer =>
            {
                writer.WriteUInt64(1ul);
                writer.WriteUInt32(1);
                writer.WriteByte((byte)SyncList<SyncUnit>.Operation.OP_ADD);
                writer.WriteByte((byte)spawnableTeamType);
                writer.WriteString(name);
            });
            target.SendFakeSyncVar(Exiled.API.Features.Server.Host.ReferenceHub.networkIdentity, typeof(CharacterClassManager), nameof(CharacterClassManager.NetworkCurClass), (sbyte)RoleType.NtfCaptain);
            target.UnitName = target.Role.ToString();
            target.SendFakeSyncVar(Exiled.API.Features.Server.Host.ReferenceHub.networkIdentity, typeof(CharacterClassManager), nameof(CharacterClassManager.NetworkCurClass), (sbyte)RoleType.NtfCaptain);
        }
    }
}
