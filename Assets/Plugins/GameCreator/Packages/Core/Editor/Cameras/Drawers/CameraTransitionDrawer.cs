using System.Collections;
using System.Collections.Generic;
using GameCreator.Editor.Common;
using GameCreator.Runtime.Cameras;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameCreator.Editor.Cameras
{
    [CustomPropertyDrawer(typeof(CameraTransition))]
    public class CameraTransitionDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            VisualElement root = new VisualElement();

            StyleSheet[] styleSheets = StyleSheetUtils.Load();
            foreach (StyleSheet styleSheet in styleSheets)
            {
                root.styleSheets.Add(styleSheet);
            }
            
            SerializedProperty currentShot = property.FindPropertyRelative("m_CurrentShotCamera");
            SerializedProperty smoothPosition = property.FindPropertyRelative("m_SmoothTimePosition");
            SerializedProperty smoothRotation = property.FindPropertyRelative("m_SmoothTimeRotation");
            
            PropertyTool fieldCurrentShot = new PropertyTool(currentShot);
            PropertyTool fieldSmoothPosition = new PropertyTool(smoothPosition);
            PropertyTool fieldSmoothRotation = new PropertyTool(smoothRotation);
            
            Label labelTitle = new Label("Shot");
            labelTitle.AddToClassList("gc-label-title");
            
            root.Add(labelTitle);
            root.Add(fieldCurrentShot);
            root.Add(fieldSmoothPosition);
            root.Add(fieldSmoothRotation);

            return root;
        }
    }
}
