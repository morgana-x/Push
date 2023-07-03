using Exiled.API.Features;

namespace Push
{
    public sealed class Plugin : Plugin<Config>
    {
        public override string Author => "atomsnow, morgana";

        public override string Name => "Push";

        public override string Prefix => Name;

        public static Plugin Instance;

        private EventHandlers _handlers;

        public override void OnEnabled()
        {
            Instance = this;

            RegisterEvents();

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();

            Instance = null;

            base.OnDisabled();
        }

        private void RegisterEvents()
        {
            _handlers = new EventHandlers();
            Exiled.Events.Handlers.Server.RoundEnded += _handlers.OnRoundEnd;
        }

        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundEnded -= _handlers.OnRoundEnd;
            _handlers = null;
        }
    }
}