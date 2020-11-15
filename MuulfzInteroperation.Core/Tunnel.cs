namespace MuulfzInteroperation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Tunnel
    {
        private readonly IInteroperatingLog _log;
        public static IFivemLibrary FivemApi { get; set; }
        private string ResourceName { get; set; }
        private string Prefix { get; set; } = FivemApi.ResourceName();

        public Tunnel(string resourceName, IInteroperatingLog log = null)
        {
            _log = log;
            ResourceName = resourceName;
        }

        public void TriggerAction(string functionName, params object[] args)
        {
            TriggerAction(functionName, null, args);
        }

        public void TriggerAction(string functionName, string targetId = null, params object[] args)
        {
            TriggerTunnelEvent(functionName, ResourceName, CallbackStatus.NoCallBack, targetId, args);
        }

        public async Task<List<object>> TriggerFunction(string functionName,
            params object[] args)
        {
            return await TriggerFunction(functionName, null, args);
        }

        public async Task<List<object>> TriggerFunction(string functionName, string targetId = null,
            params object[] args)
        {
            string identifier = Prefix + Guid.NewGuid();

            string responseEventName = $"{ResourceName}:{identifier}:tunnel_res";

            TaskCompletionSource<List<object>> source = new TaskCompletionSource<List<object>>();

            void ActionResult(int i, List<object> list)
            {
                Console.WriteLine(i);
                source.SetResult(list);
            }

            Action<int, List<object>> action = ActionResult;

            FivemApi.Add(responseEventName, action);

            TriggerTunnelEvent(functionName, identifier, CallbackStatus.WaitForCallback, targetId, args);

            List<object> sourceTask = await source.Task;

            FivemApi.Remove(responseEventName, action);

            return sourceTask;
        }

        public async Task<Tuple<T, T2>> TriggerFunction<T, T2>(string functionName,
            params object[] args)
        {
            return await TriggerFunction<T, T2>(functionName, null, args);
        }

        public async Task<Tuple<T, T2>> TriggerFunction<T, T2>(string functionName, string targetId = null,
            params object[] args)
        {
            List<object> objects = await TriggerFunction(functionName, args);

            T first = objects[0] is T ? (T) objects.First() : default;
            T2 second = objects[1] is T2 ? (T2) objects.First() : default;

            return new Tuple<T, T2>(first, second);
        }


        public async Task<Tuple<T, T2, T3>> TriggerFunction<T, T2, T3>(string functionName,
            params object[] args)
        {
            return await TriggerFunction<T, T2, T3>(functionName, null, args);
        }

        public async Task<Tuple<T, T2, T3>> TriggerFunction<T, T2, T3>(string functionName, string targetId = null,
            params object[] args)
        {
            List<object> objects = await TriggerFunction(functionName, args);

            T first = objects[0] is T ? (T) objects[0] : default;
            T2 second = objects[1] is T2 ? (T2) objects[1] : default;
            T3 third = objects[2] is T3 ? (T3) objects[2] : default;


            return new Tuple<T, T2, T3>(first, second, third);
        }

        public void TriggerTunnelEvent(string functionName, string identifier, CallbackStatus callbackStatus,
            string targetId = null,
            params object[] args)
        {
            if (FivemApi != null)
            {
                _log?.Log($"Trigger Tunnel Event | target:{functionName} callbackStatus:{callbackStatus}");

                int status = (int) callbackStatus;

                FivemApi.TriggerRemoteEvent($"{ResourceName}:tunnel_req", targetId, functionName, args, identifier,
                    status);
            }
            else
            {
                _log?.Error("Fivem Api not found");
            }
        }
    }
}