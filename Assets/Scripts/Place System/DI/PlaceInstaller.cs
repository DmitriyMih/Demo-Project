using UnityEngine;
using Zenject;

namespace GameSystem
{
    public class PlaceInstaller:MonoInstaller
    {
        [SerializeField] private PlaceManagment placeManagment;

        public override void InstallBindings()
        {
            Container.Bind<PlaceManagment>().FromInstance(placeManagment).AsSingle();
        }
    }
}