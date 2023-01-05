using Tawala.Domain.Common;
using Microsoft.Extensions.Options;

namespace Tawala.Infrastructure.Services
{
    public class MyConfigService
    {
        public MyConfig options { get; private set; }

        public MyConfigService(IOptionsSnapshot<MyConfig> optionsSnapshot )
        {
            options = optionsSnapshot.Value;
        }
    }
}
