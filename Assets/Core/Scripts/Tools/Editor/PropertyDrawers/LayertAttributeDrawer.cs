namespace TopDownShooter.Tools.Editor {
    using UnityEngine;
    using UnityEditor;
    using TopDownShooter.Tools;

    /// <summary>
    /// A dropdown field to select a target mask
    /// </summary>
    [CustomPropertyDrawer(typeof(LayerFieldAttribute))]
    public class LayertAttributeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            property.intValue = EditorGUI.LayerField(position, label, property.intValue);
        }
    }
}