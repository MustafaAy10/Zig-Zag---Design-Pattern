using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DevelopmentKit.Services
{
    public interface IServiceLocator
    {
        void Add(string key, object value);
        T Get<T>(string key);
        void Remove(string key);
    }
}
