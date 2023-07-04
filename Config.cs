using Exiled.API.Interfaces;
using Exiled.API.Features;
using Exiled.API.Enums;
using System.ComponentModel;
using System.Collections.Generic;

namespace Push
{
    public class Config : IConfig
    {

        public bool IsEnabled { get; set; } = true;

        public bool Debug { get; set; } = false;



        [Description("Range at which the pusher can push someone")]
        public float PushRange { get; set; } = 1f;



        [Description("Cooldown in seconds between each push")]
        public float PushCooldown { get; set; } = 2f;



        [Description("Push Force, how much they get pushed")]
        public float PushForce { get; set; } = 1.5f;


        [Description("More iterations = more smoother push at cost of performance")]
        public int Iterations { get; set; } = 15;


        [Description("Message showed to victim when pushed.")]
        public string PushHintVictim { get; set; } = "You have been pushed by {player}!";


        [Description("Message showed to instigator when victim is pushed.")]
        public string PushHintInstigator { get; set; } = "You pushed {player}!";
    }
}