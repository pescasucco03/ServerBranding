using Exiled.API.Extensions;
using Exiled.API.Features;
using Mirror;
using Respawning;
using Respawning.NamingRules;

namespace ServerBranding
{
    public static class Extensions
    {
        // stolen from joker
        public static void SendFakeUnitName(this Player target, string name, SpawnableTeamType spawnableTeamType = SpawnableTeamType.NineTailedFox)
        {
            // Log.Debug($"{nameof(SendFakeUnitName)}: Sending {target.Nickname} a fake unit name: {name}", plugin.Config.Debug);
            MirrorExtensions.SendFakeSyncObject(target, RespawnManager.Singleton.NamingManager.netIdentity, typeof(UnitNamingManager), writer =>
            {
                writer.WriteUInt64(1ul);
                writer.WriteUInt32(1);
                writer.WriteByte((byte)SyncList<SyncUnit>.Operation.OP_INSERT);
                writer.WriteUInt32(0);
                writer.WriteByte((byte)spawnableTeamType);
                writer.WriteString(name);
            });

            target.SendFakeSyncVar(Server.Host.ReferenceHub.networkIdentity,
                typeof(CharacterClassManager),
                nameof(CharacterClassManager.NetworkCurClass),
                (sbyte)RoleType.NtfCaptain);
            target.UnitName = target.Role.ToString();
            target.SendFakeSyncVar(Server.Host.ReferenceHub.networkIdentity,
                typeof(CharacterClassManager),
                nameof(CharacterClassManager.NetworkCurClass),
                (sbyte)RoleType.NtfCaptain);
        }
    }
}
