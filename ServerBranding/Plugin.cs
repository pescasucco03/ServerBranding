using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using System;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Respawning;
using Respawning.NamingRules;

namespace ServerBranding
{
    public class Plugin : Plugin<Config>
    {
        public override string Name { get; } = "ServerBranding";
        public override string Author { get; } = "Rowpann's Emporium dev team";
        public override Version Version { get; } = new Version(1, 1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 0);
        
        private EventHandlers _events;
        public static Plugin Instance;

        public override void OnEnabled()
        {
            Instance = this;
            _events = new EventHandlers();
            RegisterEvents();

            foreach (var type in Enum.GetValues(typeof(RoleType)).Cast<RoleType>()
                         .Where(x => x.GetTeam() != Team.MTF))
            {
                UnitNamingManager.RolesWithEnforcedDefaultName.Add(type,
                    type.GetSide() is Side.Scp or Side.ChaosInsurgency
                        ? SpawnableTeamType.ChaosInsurgency
                        : SpawnableTeamType.NineTailedFox);
            }

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
}
