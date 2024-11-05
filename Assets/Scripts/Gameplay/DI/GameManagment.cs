using UnityEngine;
using System.Collections.Generic;
using Zenject;

namespace GameSystem
{
    [System.Serializable]
    public class GameManagment
    {
        [SerializeField] private DiContainer container;
        [field:SerializeField] public bool IsPause { get; private set; }
        
        public List<ICustomTickable> TickablesObjects { get; private set; } = new();
        public List<ICustomPause> PauseObjects { get; private set; } = new();

        public event System.Action<bool> OnPauseChanged;

        #region Instatiate/Destroy Metods

        public bool TryInstatiate<T>(T prefab, out T outItem) where T : Object
        {
            outItem = null;

            if (!prefab.IsNotNullUniversal()) return false;

            GameObject item = container.InstantiatePrefab(prefab);
            item.TryAddObject(TickablesObjects);
            outItem = item.GetComponent<T>();
            return true;
        }

        public void Destroy(GameObject item)
        {
            if (item == null) return;
            item.TryRemoveObject(TickablesObjects);
            Object.Destroy(item);
        }

        #endregion

        #region Tick Metods

        public void SetPauseState(bool state)
        {
            IsPause = state;
            OnPauseChanged?.Invoke(IsPause);
            PauseObjects.ProcessObjects(item => item.IsNotNullUniversal(), trueAction: (item) => item.SetPauseState(state), falseAction: (item) => item.TryRemoveItem(PauseObjects));
        }

        public void OnTick()
        {
            if (IsPause) return;
            TickablesObjects.ProcessObjects(item => item.IsNotNullUniversal(), trueAction: (item) => item.OnTick(), falseAction: (item) => item.TryRemoveItem(TickablesObjects));
        }

        public void OnFixedTick()
        {
            if (IsPause) return;
            TickablesObjects.ProcessObjects(item => item.IsNotNullUniversal(), trueAction: (item) => item.OnFixedTick(), falseAction: (item) => item.TryRemoveItem(TickablesObjects));
        }

        #endregion
    }
}