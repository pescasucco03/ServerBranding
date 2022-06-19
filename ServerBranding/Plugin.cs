using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using System;

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
}
