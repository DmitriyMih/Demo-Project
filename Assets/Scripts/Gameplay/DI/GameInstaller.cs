using UnityEngine;
using Zenject;

namespace GameSystem
{
    public class GameInstaller : MonoInstaller, ITickable, IFixedTickable
    {
        [SerializeField] private GameManagment gameManagment;

        public override void InstallBindings()
        {
            Container.Bind<GameManagment>().FromInstance(gameManagment).AsSingle();
            Container.Bind<ITickable>().FromInstance(this).AsSingle();
            Container.Bind<IFixedTickable>().FromInstance(this).AsSingle();
        }

        public void Tick() => gameManagment.OnTick();
        public void FixedTick() => gameManagment.OnFixedTick();
    }
}