using UnityEngine;
using Zenject;

namespace GameSystem.PlayerSystem
{
    public class PlayerInstaller : MonoInstaller
    {
        [Inject] private GameManagment gameManagment;

        [SerializeField] private PlayerManagment playerManagment;

        public override void InstallBindings()
        {
            Container.Bind<PlayerManagment>().FromInstance(playerManagment).AsSingle();
        }

        private void Awake() => Initialization();
        private void Initialization() => gameManagment.TickablesObjects.Add(playerManagment);
    }
}