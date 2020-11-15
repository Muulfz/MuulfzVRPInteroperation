namespace MuulfzInteroperationClientDebug
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using MuulfzInteroperation.Client;
    using MuulfzInteroperation.Core;

    public class MuulfzInteroperationDebug : BaseScript
    {
        public MuulfzInteroperationDebug()
        {
            API.RegisterCommand("vrp_client_proxy", new Action<int, List<object>, string>(((i, list, arg3) =>
            {
                string first = list.First().ToString();
                string function = list[1].ToString();


                list.RemoveAt(0);
                list.RemoveAt(0);

                RunProxy(first, function, list.ToArray());
            })), false);


            API.RegisterCommand("vrp_tunnel_test",
                new Action<int, List<object>, string>(((i, list, arg3) =>
                {
                    string first = list[0].ToString();
                    string function = list[1].ToString();


                    list.RemoveAt(0);
                    list.RemoveAt(0);

                    RunTunnel(first, function, list.ToArray());
                })), false);
        }

        public async void RunTunnel(string resource, string function, params object[] args)
        {
            Debug.WriteLine("RunCommand");
            Tunnel tunnel = new Tunnel(resource, new ClientLog());
            List<object> triggerFunction = await tunnel.TriggerFunction(function, args);
            foreach (var o in triggerFunction)
            {
                Debug.WriteLine(o.ToString());
            }
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