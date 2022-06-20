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

            // This shows the Mtf units to people who are not Mtf.
            // Do this for each RoleType (excluding Mtf/Guards).
            // This also shows all of the other Mtf units to the person with that RoleType.
            UnitNamingManager.RolesWithEnforcedDefaultName.Add(RoleType.ClassD, SpawnableTeamType.NineTailedFox);
        }
    }
}
