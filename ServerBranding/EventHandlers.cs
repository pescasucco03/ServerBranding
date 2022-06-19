using Exiled.API.Features;
using Exiled.Events.EventArgs;

namespace ServerBranding
{
    internal class EventHandlers
    {
        public void OnVerified(VerifiedEventArgs ev)
        {
            ev.Player.SendFakeUnitName(Server.Name);
        }
    }
}
