using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MinimalistFreeStandardMat : ShaderGUI {

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
	{
		MaterialProperty _MainTexture = ShaderGUI.FindProperty("_MainTexture", properties);
		MaterialProperty _MainTexturePower = ShaderGUI.FindProperty ("_MainTexturePower", properties);

		MaterialProperty _Color1_F = ShaderGUI.FindProperty("_Color1_F", properties);
		MaterialProperty _Color2_F = ShaderGUI.FindProperty("_Color2_F", properties);

		MaterialProperty _Color1_B = ShaderGUI.FindProperty("_Color1_B", properties);
		MaterialProperty _Color2_B = ShaderGUI.FindProperty("_Color2_B", properties);

		MaterialProperty _Color1_L = ShaderGUI.FindProperty("_Color1_L", properties);
		MaterialProperty _Color2_L = ShaderGUI.FindProperty("_Color2_L", properties);

		MaterialProperty _Color1_R = ShaderGUI.FindProperty("_Color1_R", properties);
		MaterialProperty _Color2_R = ShaderGUI.FindProperty("_Color2_R", properties);

		MaterialProperty _Color_T = ShaderGUI.FindProperty("_Color_T", properties);
		MaterialProperty _Color_D = ShaderGUI.FindProperty("_Color_D", properties);

		MaterialProperty _AmbientColor = ShaderGUI.FindProperty("_AmbientColor", properties);
		MaterialProperty _AmbientPower = ShaderGUI.FindProperty("_AmbientPower", properties);

		MaterialProperty _GradientYStartPos = ShaderGUI.FindProperty("_GradientYStartPos", properties);
		MaterialProperty _GradientHeight = ShaderGUI.FindProperty("_GradientHeight", properties);

		MaterialProperty _LocalSpace = ShaderGUI.FindProperty("_LocalSpace", properties);
		MaterialProperty _DontMix = ShaderGUI.FindProperty("_DontMix", properties);
        MaterialProperty _Fog = ShaderGUI.FindProperty("_Fog", properties);
        MaterialProperty _RealtimeShadow = ShaderGUI.FindProperty("_RealtimeShadow", properties);
        MaterialProperty _ShadowColor = ShaderGUI.FindProperty("_ShadowColor", properties);

        MaterialProperty _LM = ShaderGUI.FindProperty("_LM", properties);
		MaterialProperty _LMColor = ShaderGUI.FindProperty("_LMColor", properties);
		MaterialProperty _LMPower = ShaderGUI.FindProperty("_LMPower", properties);


        //Displaying Properties.....................

        EditorGUILayout.Space();
        EditorGUILayout.HelpBox("This shader is not available to use in the free version of Minimalist", MessageType.Error);
        if (GUILayout.Button("Get full version of Minimalist"))
        {
            Application.OpenURL("http://u3d.as/R85");
        }
        EditorGUILayout.Space();
        EditorGUILayout.Space();



        EditorGUI.BeginDisabledGroup(true);
        materialEditor.ShaderProperty(_MainTexture, _MainTexture.displayName);
		materialEditor.ShaderProperty(_MainTexturePower, _MainTexturePower.displayName);
        

		//forward Properties
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Forward", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color1_F, "Color 1");
		materialEditor.ShaderProperty (_Color2_F, "Color 2");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

		//Backward Properties
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Backward", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color1_B, "Color 1");
		materialEditor.ShaderProperty (_Color2_B, "Color 2");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

		//Backward Properties
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Left", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color1_L, "Color 1");
		materialEditor.ShaderProperty (_Color2_L, "Color 2");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

		//Right Properties
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Right", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color1_R, "Color 1");
		materialEditor.ShaderProperty (_Color2_R, "Color 2");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

		//Top Properties
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Top", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color_T, "Color");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

		//bottom Properties
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Bottom", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_Color_D, "Color");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

        //Gradient Settings
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Gradient Settings", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        materialEditor.ShaderProperty(_GradientYStartPos, "Gradient Start Position");
        materialEditor.ShaderProperty(_GradientHeight, "Gradient Falloff");

        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();

        //Ambient
        EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Ambient Color", EditorStyles.boldLabel);
		EditorGUILayout.Space();

		materialEditor.ShaderProperty (_AmbientColor, "Color");
		materialEditor.ShaderProperty (_AmbientPower, "Power");

		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

		EditorGUILayout.Space();
		materialEditor.ShaderProperty (_LocalSpace, "Gradient in Local Space");
		materialEditor.ShaderProperty (_DontMix, "Don't mix color of sides");
        materialEditor.ShaderProperty(_Fog, "Fog");
        materialEditor.ShaderProperty(_RealtimeShadow, "Realtime Shadow");
        materialEditor.ShaderProperty(_ShadowColor, "Shadow Color");
        EditorGUILayout.Space();



		//LightMap
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Lightmap Settings", EditorStyles.boldLabel);
		EditorGUILayout.Space();
        EditorGUI.EndDisabledGroup();
		materialEditor.ShaderProperty (_LM, "Enable");
        EditorGUI.BeginDisabledGroup(true);
		if (_LM.floatValue != 0) {
			materialEditor.ShaderProperty (_LMColor, "Color");
			materialEditor.ShaderProperty (_LMPower, "Power");
		}


		EditorGUILayout.Space ();
		EditorGUILayout.EndVertical ();

        EditorGUI.EndDisabledGroup();
	
	
	
	}
}
