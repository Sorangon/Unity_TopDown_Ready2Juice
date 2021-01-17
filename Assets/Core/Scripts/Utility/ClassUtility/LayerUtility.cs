namespace TopDownShooter.Utility {
    using UnityEngine;

    /// <summary>
    /// Utility methods for Layers
    /// </summary>
    public static class LayerUtility {
        #region Constants
        private const int LayersCount = 16;
        #endregion

        #region Methods
        /// <summary>
        /// Returns the collisions allowed with a layer
        /// </summary>
        /// <returns></returns>
        public static LayerMask GetLayerMask(int layerId) {
            LayerMask mask = new LayerMask();

            for (int i = 0; i < LayersCount; i++) {
                if(!Physics.GetIgnoreLayerCollision(i, layerId) && LayerMask.LayerToName(i).Length > 0){
                    int layerValue = 1 << i;
                    mask.value += layerValue;
                }
            }

            return mask;
        } 
        #endregion
    }
}