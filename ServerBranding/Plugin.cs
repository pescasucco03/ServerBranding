using Exiled.API.Features;
using Server = Exiled.Events.Handlers.Server;
using System;

namespace ServerBranding
{
    public class Plugin : Plugin<Config>
    {
        private EventHandlers _events;

        public static Plugin Instance;

        public override string Name { get; } = "ServerBranding";
        public override string Author { get; } = "Rowpann's Emporium";
        public override Version Version { get; } = new Version(1, 0, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(5, 2, 0);

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

        public void RegisterEvents()
        {
            Server.RoundStarted += _events.OnRoundStarted;
            //Server.RoundEnded += _events.OnRoundEnded;
        }

        public void UnregisterEvents()
        {
            Server.RoundStarted -= _events.OnRoundStarted;
            //Server.RoundEnded -= _events.OnRoundEnded;
        }
    }
}
