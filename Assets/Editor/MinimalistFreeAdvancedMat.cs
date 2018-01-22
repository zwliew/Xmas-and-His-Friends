using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MinimalistFreeAdvancedMat : ShaderGUI {

	public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
	{
		MaterialProperty _MainTexture = ShaderGUI.FindProperty("_MainTexture", properties);
		MaterialProperty _MainTexturePower = ShaderGUI.FindProperty ("_MainTexturePower", properties);

		MaterialProperty _Color1_F = ShaderGUI.FindProperty("_Color1_F", properties);
		MaterialProperty _Color2_F = ShaderGUI.FindProperty("_Color2_F", properties);
		MaterialProperty _UseGradient_F = ShaderGUI.FindProperty("_UseGradient_F", properties);
		MaterialProperty _GradientYStartPos_F = ShaderGUI.FindProperty("_GradientYStartPos_F", properties);
		MaterialProperty _GradientHeight_F = ShaderGUI.FindProperty("_GradientHeight_F", properties);

		MaterialProperty _Color1_B = ShaderGUI.FindProperty("_Color1_B", properties);
		MaterialProperty _Color2_B = ShaderGUI.FindProperty("_Color2_B", properties);
		MaterialProperty _UseGradient_B = ShaderGUI.FindProperty("_UseGradient_B", properties);
		MaterialProperty _GradientYStartPos_B = ShaderGUI.FindProperty("_GradientYStartPos_B", properties);
		MaterialProperty _GradientHeight_B = ShaderGUI.FindProperty("_GradientHeight_B", properties);

		MaterialProperty _Color1_L = ShaderGUI.FindProperty("_Color1_L", properties);
		MaterialProperty _Color2_L = ShaderGUI.FindProperty("_Color2_L", properties);
		MaterialProperty _UseGradient_L = ShaderGUI.FindProperty("_UseGradient_L", properties);
		MaterialProperty _GradientYStartPos_L = ShaderGUI.FindProperty("_GradientYStartPos_L", properties);
		MaterialProperty _GradientHeight_L = ShaderGUI.FindProperty("_GradientHeight_L", properties);

		MaterialProperty _Color1_R = ShaderGUI.FindProperty("_Color1_R", properties);
		MaterialProperty _Color2_R = ShaderGUI.FindProperty("_Color2_R", properties);
		MaterialProperty _UseGradient_R = ShaderGUI.FindProperty("_UseGradient_R", properties);
		MaterialProperty _GradientYStartPos_R = ShaderGUI.FindProperty("_GradientYStartPos_R", properties);
		MaterialProperty _GradientHeight_R = ShaderGUI.FindProperty("_GradientHeight_R", properties);

		MaterialProperty _Color_T = ShaderGUI.FindProperty("_Color_T", properties);
		MaterialProperty _Color_D = ShaderGUI.FindProperty("_Color_D", properties);

		MaterialProperty _AmbientColor = ShaderGUI.FindProperty("_AmbientColor", properties);
		MaterialProperty _AmbientPower = ShaderGUI.FindProperty("_AmbientPower", properties);

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
        EditorGUI.EndDisabledGroup();
        materialEditor.ShaderProperty(_UseGradient_F, "Use Gradient");
        _UseGradient_F.floatValue = 1;
        EditorGUI.BeginDisabledGroup(true);
        if (_UseGradient_F.floatValue != 0) {
            materialEditor.ShaderProperty (_Color1_F, "Color 1");
			materialEditor.ShaderProperty (_Color2_F, "Color 2");
			materialEditor.ShaderProperty (_GradientYStartPos_F, "Gradient Start Position");
			materialEditor.ShaderProperty (_GradientHeight_F, "Gradient FallOff");
		} else {
            materialEditor.ShaderProperty (_Color1_F, "Color");
			_Color2_F.colorValue = _Color1_F.colorValue;
            
        }
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

        //Backward Properties
        EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Backward", EditorStyles.boldLabel);
		EditorGUILayout.Space();
        EditorGUI.EndDisabledGroup();
		materialEditor.ShaderProperty(_UseGradient_B, "Use Gradient");
        EditorGUI.BeginDisabledGroup(true);
        if (_UseGradient_B.floatValue != 0) {
			materialEditor.ShaderProperty (_Color1_B, "Color 1");
			materialEditor.ShaderProperty (_Color2_B, "Color 2");
			materialEditor.ShaderProperty (_GradientYStartPos_B, "Gradient Start Position");
			materialEditor.ShaderProperty (_GradientHeight_B, "Gradient FallOff");
		} else {
			materialEditor.ShaderProperty (_Color1_B, "Color");
			_Color2_B.colorValue = _Color1_B.colorValue;
		}
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();


        //Left Properties
        EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Left", EditorStyles.boldLabel);
		EditorGUILayout.Space();
        EditorGUI.EndDisabledGroup();
		materialEditor.ShaderProperty(_UseGradient_L, "Use Gradient");
        EditorGUI.BeginDisabledGroup(true);
        if (_UseGradient_L.floatValue != 0) {
			materialEditor.ShaderProperty (_Color1_L, "Color 1");
			materialEditor.ShaderProperty (_Color2_L, "Color 2");
			materialEditor.ShaderProperty (_GradientYStartPos_L, "Gradient Start Position");
			materialEditor.ShaderProperty (_GradientHeight_L, "Gradient FallOff");
		} else {
			materialEditor.ShaderProperty (_Color1_L, "Color");
			_Color2_L.colorValue = _Color1_L.colorValue;
		}
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();


        //Right Properties
        EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Right", EditorStyles.boldLabel);
		EditorGUILayout.Space();
        EditorGUI.EndDisabledGroup();
        materialEditor.ShaderProperty(_UseGradient_R, "Use Gradient");
        EditorGUI.BeginDisabledGroup(true);
        if (_UseGradient_R.floatValue != 0) {
			materialEditor.ShaderProperty (_Color1_R, "Color 1");
			materialEditor.ShaderProperty (_Color2_R, "Color 2");
			materialEditor.ShaderProperty (_GradientYStartPos_R, "Gradient Start Position");
			materialEditor.ShaderProperty (_GradientHeight_R, "Gradient FallOff");
		} else {
			materialEditor.ShaderProperty (_Color1_R, "Color");
			_Color2_R.colorValue = _Color1_R.colorValue;
		}
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();


        //Top
        EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Top", EditorStyles.boldLabel);
		EditorGUILayout.Space();
		materialEditor.ShaderProperty(_Color_T, "Color");
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

		//Down
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Bottom", EditorStyles.boldLabel);
		EditorGUILayout.Space();
		materialEditor.ShaderProperty(_Color_D, "Color");
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

		//Ambient
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("Ambient Lighting", EditorStyles.boldLabel);
		EditorGUILayout.Space();
		materialEditor.ShaderProperty(_AmbientColor, "Color");
		materialEditor.ShaderProperty(_AmbientPower, "Power");
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();

		EditorGUILayout.Space();
		materialEditor.ShaderProperty(_LocalSpace, "Gradient in Local Space");
		materialEditor.ShaderProperty(_DontMix, "Don't mix color of sides");

        materialEditor.ShaderProperty(_Fog, "Fog");
        materialEditor.ShaderProperty(_RealtimeShadow, "Realtime Shadow");
        materialEditor.ShaderProperty(_ShadowColor, "Shadow Color");
        EditorGUILayout.Space();

		//Lightmap
		EditorGUILayout.Space();
		EditorGUILayout.BeginVertical("Box");
		EditorGUILayout.LabelField("LightMap Settings", EditorStyles.boldLabel);
		EditorGUILayout.Space();
        EditorGUI.EndDisabledGroup();
		materialEditor.ShaderProperty(_LM, "Enable");
        EditorGUI.BeginDisabledGroup(true);
		if (_LM.floatValue != 0) {
			materialEditor.ShaderProperty(_LMColor, "Color");
			materialEditor.ShaderProperty(_LMPower, "Power");
		}
		EditorGUILayout.Space();
		EditorGUILayout.EndVertical();
        EditorGUI.EndDisabledGroup();
	}


}
