namespace TopDownShooter.Gameplay {
    using UnityEngine;

    /// <summary>
    /// Base class for logic execution system when object dies
    /// </summary>
    public abstract class DeathBehaviour : ScriptableObject{
        public abstract void Execute(Object target);
    }

}