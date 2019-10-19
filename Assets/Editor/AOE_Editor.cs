using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AOE_Ability))]
public class AOE_Editor : Editor
{
    AOE_Ability script;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        script = target as AOE_Ability;
        //((AOE_Ability)target).shape
        script.shape = (AbilityShape)EditorGUILayout.EnumPopup("Shape", script.shape);
        switch (script.shape)
        {
            case AbilityShape.Box:
                script.boxSize = EditorGUILayout.Vector2Field("Box Size", script.boxSize);

                break;
            case AbilityShape.Sphere:
                script.angle = EditorGUILayout.Vector2Field("Angle", script.angle);
                script.radius = EditorGUILayout.FloatField("Radius", script.radius);
                break;
            default:
                Debug.LogError("Shape unhandled in AOE_Editor");
                break;
                
        }
        //script.animation = (AnimationClip)EditorGUILayout.ObjectField("Ability Animation", script.animation, typeof(AnimationClip), false);



    }
}
