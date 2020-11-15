namespace MuulfzInteroperation.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using Core;

    public class MuulfzInteroperationDebug : BaseScript
    {
        public MuulfzInteroperationDebug()
        {
            Proxy.FivemApi = FivemServerLibrary.Instance;
            API.RegisterCommand("vrp_client_proxy", new Action<int, List<object>, string>(((i, list, arg3) =>
            {
                string first = list.First().ToString();
                string function = list[1].ToString();


                list.RemoveAt(0);
                list.RemoveAt(0);

                RunProxy(first, function, list.ToArray());
            })), false);
        }

        public async void RunProxy(string resource, string function, params object[] args)
        {
            Console.WriteLine("RunCommand");
            Proxy proxy = new Proxy(resource, new ClientLog());
            object result = await proxy.TriggerFunction<object>(function, args);
            Console.WriteLine(result);
        }
    }
}