using Exiled.API.Extensions;
using Exiled.API.Features;
using Mirror;
using Respawning;
using Respawning.NamingRules;

namespace ServerBranding
{
    public static class Extensions
    {
        public static void SendFakeUnitName(this Player target, string name, SpawnableTeamType spawnableTeamType = SpawnableTeamType.NineTailedFox)
        {
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
        
        public static void ClearUnitNames(this Player target)
        {
            MirrorExtensions.SendFakeSyncObject(target, RespawnManager.Singleton.NamingManager.netIdentity, typeof(UnitNamingManager),
                writer
                    =>
                {
                    writer.WriteUInt64(1ul);
                    writer.WriteUInt32(1);
                    writer.WriteByte((byte)SyncList<byte>.Operation.OP_CLEAR);
                });
            
            // if (target.Role.Team != Team.SCP)
            // {
            //     target.SendFakeSyncVar(Server.Host.ReferenceHub.networkIdentity, typeof(CharacterClassManager), nameof(CharacterClassManager.NetworkCurClass), (sbyte)RoleType.NtfCaptain);
            //     target.UnitName = string.Empty;
            // }
        }
    }
}
