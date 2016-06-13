using System;
using System.Collections.Generic;
using Marvin.Core.Models;
using Marvin.Core.Module;

namespace Marvin.Logger.Actions
{
    public class WriteAction : MarvinAction
    {
        public WriteAction() : base(
            "WriteAction",
            "Write a message to the log",
            "LogWrite")
        {
            Sample = new ActionMessage
            {
                Action = GetAction(),
                Payload = new Dictionary<string, object>
                {
                    { "message", "Hello Logger!!"}
                }
            };
        }

        public override void Execute(ActionMessage msg)
        {
            Console.Out.WriteLine(msg.Payload);
        }
    }
}
