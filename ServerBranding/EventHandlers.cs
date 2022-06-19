using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Exiled.
using Mirror;
using Respawning;
using Respawning.NamingRules;
using System.Linq;

namespace ServerBranding
{
    internal class EventHandlers
    {
        public void OnRoundStarted()
        {
            UnitNamingManager unitNamingManager = RespawnManager.Singleton.NamingManager;
            SyncList<SyncUnit> units = unitNamingManager.AllUnitNames;

            SyncUnit serverUnit = new SyncUnit()
            {
                UnitName = Server.Name
            };

            units.Insert(0, serverUnit);
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            UnitNamingManager unitNamingManager = RespawnManager.Singleton.NamingManager;
            SyncList<SyncUnit> units = unitNamingManager.AllUnitNames;

            SyncUnit serverUnit = units.FirstOrDefault(x => x.UnitName == Server.Name);
            units.Remove(serverUnit);
        }
    }
}
