using Exiled.API.Features;
using Exiled.Events.EventArgs;
using Mirror;
using Respawning;
using Respawning.NamingRules;
using System.Linq;
using Exiled.API.Extensions;

namespace ServerBranding
{
    internal class EventHandlers
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            ev.Player.SendFakeUnitName("Rowpann's Emporium");
        }
    }
}
