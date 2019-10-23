using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Melee_Ability))]
public class Melee_Editor : Editor
{
    Melee_Ability script;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        script = target as Melee_Ability;
        //((AOE_Ability)target).shape
        script.shape = (Melee_Ability.AbilityShape)EditorGUILayout.EnumPopup("Shape", script.shape);
        switch (script.shape)
        {
            case Melee_Ability.AbilityShape.Box:
                script.boxSize = EditorGUILayout.Vector2Field("Box Size", script.boxSize);
                break;
            case Melee_Ability.AbilityShape.Ray:
                break;
            default:
                Debug.LogError("Shape unhandled in AOE_Editor");
                break;
                
        }
        //script.animation = (AnimationClip)EditorGUILayout.ObjectField("Ability Animation", script.animation, typeof(AnimationClip), false);



    }
}
