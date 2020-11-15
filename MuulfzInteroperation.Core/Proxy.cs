namespace MuulfzInteroperation.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Proxy
    {
        private readonly IInteroperatingLog _log;
        public static IFivemLibrary FivemApi { get; set; }
        private string ResourceName { get; set; }
        private string Prefix { get; set; } = FivemApi.ResourceName();

        public Proxy(string resourceName, IInteroperatingLog log = null)
        {
            _log = log;
            ResourceName = resourceName;
        }

        public void TriggerAction(string functionName, params object[] args)
        {
            _log?.Log($"Proxy Trigger action to function {functionName}");

            TriggerProxyEvent(functionName, ResourceName, CallbackStatus.NoCallBack, args);
        }

        public async Task<T> TriggerFunction<T>(string functionName, params object[] args)
        {
            List<object> objects = await TriggerFunction(functionName, args);

            if (_log != null)
            {
              _log.Log($"Function {functionName} returns : {objects.Count} items");
              foreach (var o in objects)
              {
                  _log.Log($"Operation results: {o}");
              }
            }

            return objects[0] is T ? (T) objects[0] : default;
        }

        public async Task<Tuple<T, T2>> TriggerFunction<T, T2>(string functionName, params object[] args)
        {
            List<object> objects = await TriggerFunction(functionName, args);

            T first = objects[0] is T ? (T) objects.First() : default;
            T2 second = objects[1] is T2 ? (T2) objects.First() : default;

            return new Tuple<T, T2>(first, second);
        }

        private async Task<List<object>> TriggerFunction(string functionName, params object[] args)
        {
            string identifier = Prefix + Guid.NewGuid();

            string responseEventName = $"{ResourceName}:{identifier}:proxy_res";

            TaskCompletionSource<List<object>> source = new TaskCompletionSource<List<object>>();

            void ActionResult(int i, List<object> list)
            {
                source.SetResult(list);
            }

            Action<int, List<object>> action = ActionResult;

            FivemApi.Add(responseEventName, action );

            TriggerProxyEvent(functionName, identifier, CallbackStatus.WaitForCallback, args);

            List<object> sourceTask = await source.Task;

            FivemApi.Remove(responseEventName, action);

            return sourceTask;
        }

        public void TriggerProxyEvent(string functionName, string identifier, CallbackStatus callbackStatus,
            params object[] args)
        {
            _log?.Log($"Trigger Proxy Event | target:{functionName} callbackStatus:{callbackStatus}");

            int status = (int) callbackStatus;

            FivemApi.TriggerEvent($"{ResourceName}:proxy", functionName, args, identifier, status);
        }
    }
}