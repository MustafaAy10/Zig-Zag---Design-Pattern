
namespace Game.DevelopmentKit.ObjectPool 
{
    public interface IPoolable
    {
        void Initialize();
        void Activate();
        void Deactivate();

    }

}

