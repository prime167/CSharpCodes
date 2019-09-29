using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace AspNetCoreGrpcService.Services
{
    public class LuCatService : LuCat.LuCatBase
    {
        private static readonly List<string> Cats = new List<string> { "英短银渐层", "英短金渐层", "美短", "蓝猫", "狸花猫", "橘猫" };
        private static readonly Random Rand = new Random(DateTime.Now.Millisecond);

        public override Task<SuckingCatResult> SuckingCat(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new SuckingCatResult
            {
                Message = $"您吸了一只{Cats[Rand.Next(0, Cats.Count)]}"
            });
        }
    }
}
