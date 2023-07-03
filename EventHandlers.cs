using Exiled.Events.EventArgs;

namespace Push
{
    internal class EventHandlers
    {
        public void OnRoundEnd(Exiled.Events.EventArgs.Server.RoundEndedEventArgs _)
        {
            Push.Cooldowns.Clear();
        }
    }
}