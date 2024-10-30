using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.DevelopmentKit.Services
{
    public interface IService
    {
        void Initialize(IServiceLocator serviceLocator);
    }
}
