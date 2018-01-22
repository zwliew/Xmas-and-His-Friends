using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MinimalistFreeSimpleMat : ShaderGUI {

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
	{
		MaterialProperty _MainTexture = ShaderGUI.FindProperty("_MainTexture", properties);
		MaterialProperty _MainTexturePower = ShaderGUI.FindProperty ("_MainTexturePower", properties);

		MaterialProperty _Color_F = ShaderGUI.FindProperty("_Color_F", properties);

		MaterialProperty _Color_B = ShaderGUI.FindProperty("_Color_B", properties);

		MaterialProperty _Color_L = ShaderGUI.FindProperty("_Color_L", properties);

		MaterialProperty _Color_R = ShaderGUI.FindProperty("_Color_R", properties);

		MaterialProperty _Color_T = ShaderGUI.FindProperty("_Color_T", properties);
		MaterialProperty _Color_D = ShaderGUI.FindProperty("_Color_D", properties);

		MaterialProperty _AmbientColor = ShaderGUI.FindProperty("_AmbientColor", properties);
		MaterialProperty _AmbientPower = ShaderGUI.FindProperty("_AmbientPower", properties);

		MaterialProperty _DontMix = ShaderGUI.FindProperty("_DontMix", properties);
        MaterialProperty _Fog = ShaderGUI.FindProperty("_Fog", properties);
        MaterialProperty _RealtimeShadow = ShaderGUI.FindProperty("_RealtimeShadow", properties);

        MaterialProperty _LM = ShaderGUI.FindProperty("_LM", properties);
		MaterialProperty _LMColor = ShaderGUI.FindProperty("_LMColor", properties);
		MaterialProperty _LMPower = ShaderGUI.FindProperty("_LMPower", properties);

        //Displaying Properties.....................
        EditorGUI.BeginDisabledGroup(true);
        materialEditor.ShaderProperty(_MainTexture, _MainTexture.displayName);
		materialEditor.ShaderProperty(_MainTexturePower, _MainTexturePower.displayName);
        EditorGUI.EndDisabledGroup();

		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Color of 6 sides", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color_F, "Forward");
		materialEditor.ShaderProperty (_Color_B, "Backward");
		materialEditor.ShaderProperty (_Color_L, "Left");
		materialEditor.ShaderProperty (_Color_R, "Right");
		materialEditor.ShaderProperty (_Color_T, "Top");
		materialEditor.ShaderProperty (_Color_D, "Bottom");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();


        //Ambient
        EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Ambient Color", EditorStyles.boldLabel);
        EditorGUILayout.Space();

		materialEditor.ShaderProperty (_AmbientColor, "Color");
		materialEditor.ShaderProperty (_AmbientPower, "Power");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();
        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();
        EditorGUI.BeginDisabledGroup(true);
        materialEditor.ShaderProperty (_DontMix, "Don't mix color of sides");
        materialEditor.ShaderProperty(_Fog, "Fog");
        materialEditor.ShaderProperty(_RealtimeShadow, "Realtime Shadow");
        EditorGUI.EndDisabledGroup();
		EditorGUILayout.Space();
        //LightMap
        EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Lightmap Settings", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_LM, "Enable");
		if (_LM.floatValue != 0) {
            EditorGUI.BeginDisabledGroup(true);
            materialEditor.ShaderProperty (_LMColor, "Color");
			materialEditor.ShaderProperty (_LMPower, "Power");
            EditorGUI.EndDisabledGroup();
        }

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

        EditorGUILayout.HelpBox("Some features are not available in the free edition of Minimalist", MessageType.Warning);
        if (GUILayout.Button("Get Full version of Minimalist"))
        {
            Application.OpenURL("http://u3d.as/R85");
        }
    }
}
