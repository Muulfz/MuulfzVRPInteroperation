namespace MuulfzInteroperationServerDebug
{
    using System;
    using System.Collections.Generic;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using MuulfzInteroperation.Core;
    using MuulfzInteroperation.Server;

    public class MuulfzInteroperationDebug : BaseScript
    {
        public MuulfzInteroperationDebug()
        {
            Console.WriteLine("Script Start");

            API.RegisterCommand("vrp_proxy_test",
                new Action<int, List<object>, string>(((i, list, arg3) => { RunProxy(); })), false);

            API.RegisterCommand("vrp_tunnel_test",
                new Action<int, List<object>, string>(((i, list, arg3) => { RunTunnel(); })), false);
        }

        [EventHandler("vRP:muulfz_interoperation:proxy_res")]
        public void Test(int rid, List<object> rest)
        {
            Console.WriteLine(rid);
            foreach (var o in rest)
            {
                Console.WriteLine("Item");
                Console.WriteLine(o);
            }
        }

        public async void RunProxy()
        {
            Console.WriteLine("RunCommand Test");
            Proxy proxy = new Proxy("vRP", new ServerLog());
            string result = await proxy.TriggerFunction<string>("getLastLogin", 1);
            Console.WriteLine(result);
        }

        public async void RunTunnel()
        {
            Console.WriteLine("RunCommand");
            Tunnel tunnel = new Tunnel("vRP", new ServerLog());
            List<object> triggerFunction = await tunnel.TriggerFunction("getPosition", "1");
            foreach (var o in triggerFunction)
            {
                Console.WriteLine(o);
            }
        }

        [Command("vrp_kick")]
        public void KickPlayerFromCsharp()
        {
            Proxy proxy = new Proxy("vRP");
            proxy.TriggerProxyEvent("getLastLogin", "muulfz_interoperation", CallbackStatus.WaitForCallback, 1);
            proxy.TriggerProxyEvent("kick", "muulfz_interoperation", CallbackStatus.NoCallBack, 1, "Teste");
        }
    }
}