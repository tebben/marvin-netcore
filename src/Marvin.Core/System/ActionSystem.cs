using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Marvin.Core.Models;
using Marvin.Core.Module;

namespace Marvin.Core.System
{
    public class ActionSystem
    {
        private readonly ConcurrentDictionary<string, IAction> _actions;

        /// <summary>
        /// Action System is used to easily trigger actions defined in the modules
        /// </summary>
        public ActionSystem()
        {
            _actions = new ConcurrentDictionary<string, IAction>();
        }

        /// <summary>
        /// Add a list of actions to the action system
        /// </summary>
        /// <param name="actions"></param>
        public void Add(List<IAction> actions)
        {
            foreach (var action in actions)
            {
                Add(action);
            }
        }

        /// <summary>
        /// Add a new action to the Action System so it can be triggered
        /// </summary>
        /// <param name="action"></param>
        public void Add(IAction action)
        {
            if (_actions.ContainsKey(action.GetAction()))
            {
                Console.Out.WriteLine($"Trying to add duplicate action: {action.GetAction()}");
                return;
            }

            _actions.TryAdd(action.GetAction(), action);
        }

        /// <summary>
        /// Remove an action from the system
        /// </summary>
        /// <param name="action"></param>
        public void Remove(IAction action)
        {
            if (!_actions.ContainsKey(action.GetAction()))
                return;

            _actions.TryRemove(action.GetAction(), out action);
        }

        /// <summary>
        /// Fire a new action by shooting in an ActionMessage, based on ActionMessage.Action
        /// the IAction is retrieved and Execute will be called on the object 
        /// </summary>
        /// <param name="actionMessage"></param>
        public void Fire(ActionMessage actionMessage)
        {
            if (!_actions.ContainsKey(actionMessage.Action))
                return;

            _actions[actionMessage.Action].Execute(actionMessage);
        }
    }
}
