namespace MuulfzInteroperation.Server
{
    using System;
    using System.Collections.Generic;
    using CitizenFX.Core;
    using CitizenFX.Core.Native;
    using Core;

    public class MuulfzInteroperationDebug : BaseScript
    {
        public MuulfzInteroperationDebug()
        {
            Console.WriteLine("Script Start");
            Proxy.FivemApi = FivemServerLibrary.Instance;
            API.RegisterCommand("vrp_proxy_test", new Action<int, List<object>, string>(((i, list, arg3) =>
            {
                RunProxy();
            })), false);

            
        }
        [EventHandler("vRP:muulfz_interoperation:proxy_res")]
        public void Test(int rid, List<object> rest )
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
            Console.WriteLine("RunCommand");
            Proxy proxy = new Proxy("vRP", new ServerLog());
            string result = await proxy.TriggerFunction<string>("getLastLogin", 1);
            Console.WriteLine(result);
        }

        [Command("vrp_kick")]
        public void KickPlayerFromCsharp()
        {
            Proxy proxy = new Proxy("vRP");
            proxy.TriggerProxyEvent("getLastLogin", "muulfz_interoperation", CallbackStatus.WaitForCallback, 1);
            proxy.TriggerProxyEvent("kick", "muulfz_interoperation", CallbackStatus.NoCallBack,1, "Teste" );
        }

    }
}