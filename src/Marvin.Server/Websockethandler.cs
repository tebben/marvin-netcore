using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using Marvin.Core.Models;
using Marvin.Core.System;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json;

namespace Marvin.Server
{
    public class Websockethandler
    {
        private readonly ConcurrentDictionary<int, WebSocket> _actionSockets;
        private readonly ConcurrentDictionary<int, WebSocket> _eventSockets;
        private readonly ActionSystem _actionSystem;

        public Websockethandler(IApplicationBuilder app, ActionSystem actionSystem)
        {
            _actionSystem = actionSystem;
            _actionSockets = new ConcurrentDictionary<int, WebSocket>();
            _eventSockets = new ConcurrentDictionary<int, WebSocket>();
            SetupWsActionsListener(app);
        }

        private void SetupWsActionsListener(IApplicationBuilder app)
        {
            app.Map("/actions", w =>
            {
                w.Use(async (http, next) =>
                {
                    if (http.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await http.WebSockets.AcceptWebSocketAsync();
                        if (webSocket != null && webSocket.State == WebSocketState.Open)
                        {
                            if (_actionSockets.TryAdd(webSocket.GetHashCode(), webSocket))
                            {
                                while (webSocket.State == WebSocketState.Open)
                                {
                                    var token = CancellationToken.None;
                                    var buffer = new ArraySegment<byte>(new byte[4096]);
                                    var received = await webSocket.ReceiveAsync(buffer, token);

                                    switch (received.MessageType)
                                    {
                                        case WebSocketMessageType.Text:
                                            var request = Encoding.UTF8.GetString(buffer.Array, buffer.Offset, buffer.Count).TrimEnd((char)0);
                                            
                                            try
                                            {
                                                var o = JsonConvert.DeserializeObject<ActionMessage>(request);
                                                await Console.Out.WriteLineAsync("Message Received: " + request);
                                                _actionSystem.Fire(o);
                                            }
                                            catch (Exception)
                                            {
                                                
                                            }
                                            break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        await next();
                    }
                });
            });
        }

        /* SENDING
         *  var varToken = CancellationToken.None;
            var type = WebSocketMessageType.Text;
            var data = Encoding.UTF8.GetBytes("You told ASP.NET " + request);
            var varBuffer = new ArraySegment<byte>(data);
            await webSocket.SendAsync(varBuffer, type, true, varToken);
         */
    }
}
