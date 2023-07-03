using Exiled.API.Interfaces;

namespace Push
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;

        public float PushRange { get; set; } = 1f;

        public float PushCooldown { get; set; } = 2f;

        public float PushForce { get; set; } = 1.5f;

        public int Iterations { get; set; } = 15;

        public string PushHintVictim { get; set; } = "You have been pushed by {player}!";
        public string PushHintInstigator { get; set; } = "You pushed {player}!";
    }
}