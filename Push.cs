using CommandSystem;
using Exiled.API.Features;
using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using MEC;

namespace Push
{
    [CommandHandler(typeof(ClientCommandHandler))]
    
    public class Push : ParentCommand
    {

        public Push() => LoadGeneratedCommands();

        public override string Command => ".push";

        public override string[] Aliases => new string[] { "push", ".p" };

        public override string Description => "pushes someone in front of you.";

        public override void LoadGeneratedCommands() { }

        public static Dictionary<Exiled.API.Features.Player, DateTime> Cooldowns = new Dictionary<Exiled.API.Features.Player, DateTime>();

        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Exiled.API.Features.Player Instigator = Exiled.API.Features.Player.Get(sender);

            if (Cooldowns.TryGetValue(Instigator, out DateTime value))
            {
                if (DateTime.Now < value)
                {
                    response = "Cooldown Active";
                    return false;
                }
            }

            var ray = new Ray(Instigator.CameraTransform.position + (Instigator.CameraTransform.forward * 0.1f), Instigator.CameraTransform.forward);


            if (!Physics.Raycast(ray, out RaycastHit hit, Plugin.Instance.Config.PushRange))
            {
                response = "No one to push.";
                return false;
            }
            var Victim = Exiled.API.Features.Player.Get(hit.collider);
            if (Victim == null)
            {
                response = "Can't push nothing";
                return false;
            }
            if ( Victim == Instigator)
            {
                response = "Can't push self";
                return false;
            }
            if (!Cooldowns.TryGetValue(Instigator, out _))
            {
                Cooldowns.Add(Instigator, DateTime.Now.AddSeconds(Plugin.Instance.Config.PushCooldown));
            }
            else
            {
                Cooldowns[Instigator] = DateTime.Now.AddSeconds(Plugin.Instance.Config.PushCooldown);
            }
            Timing.RunCoroutine(PushPlayer(Instigator, Victim));
            Victim.ShowHint(Plugin.Instance.Config.PushHintVictim.Replace("{player}", Instigator.DisplayNickname));
            Instigator.ShowHint(Plugin.Instance.Config.PushHintInstigator.Replace("{player}", Victim.DisplayNickname));
            response = "true";
            return true;
        }
        private IEnumerator<float> PushPlayer(Exiled.API.Features.Player Instigator, Exiled.API.Features.Player Victim)
        {
            Vector3 pushed = Instigator.CameraTransform.forward * Plugin.Instance.Config.PushForce;

            for (int i = 1; i < Plugin.Instance.Config.Iterations; i++)
            {

                float movementAmount = Plugin.Instance.Config.PushForce / Plugin.Instance.Config.Iterations;


                Vector3 newPos = Vector3.MoveTowards(Victim.Position, Victim.Position + new Vector3(pushed.x, 0, pushed.z), movementAmount);

                var ray = new Ray(Victim.Position, Instigator.CameraTransform.forward);


                for (int x = 1; x < 15; x++) // I don't know why this works :)
                {
                    if (Physics.Linecast(Victim.Position, newPos, x))
                    {
                        Exiled.API.Features.Log.Info("Woopsies wall in the way " + x.ToString());
                        yield break;
                    }
                }

                Victim.Position = newPos;


                yield return Timing.WaitForOneFrame;
            }

        }
    }
}
