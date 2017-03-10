
//====================================================================================
//
// CloudsToyEditor.cs
//
// Name: Julian Oliden "Jocyf"
// Date: 18-04-2015
// v1.1 - Unity 5
//
//=====================================================================================

using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor(typeof(CloudsToy))]
public class CloudsToyEditor : Editor {

	private CloudsToy CloudSystem;	// Pointer to CloudsToy.cs
	
	private SerializedProperty maximunClouds;
	private SerializedProperty cloudPreset;
	private SerializedProperty cloudRender;
	private SerializedProperty typeClouds;
	private SerializedProperty CloudDetail;

	private SerializedProperty realisticShader;
	private SerializedProperty brightShader;
	private SerializedProperty projectorMaterial;

	private SerializedProperty positionCheckerTime;
	private SerializedProperty sizeFactorPart;
	private SerializedProperty emissionMult;

	private SerializedProperty softClouds;
	private SerializedProperty spreadDir;
	private SerializedProperty lengthSpread;
	private SerializedProperty numberClouds;
	private SerializedProperty side;
	private SerializedProperty dissapearMultiplier;
	private SerializedProperty maximunVelocity;
	private SerializedProperty velocityMultipier;

	private SerializedProperty paintType;
	private SerializedProperty cloudColor;
	private SerializedProperty mainColor;
	private SerializedProperty secondColor;
	private SerializedProperty tintStrength;
	private SerializedProperty offset;

	private SerializedProperty maxWithCloud;
	private SerializedProperty maxTallCloud;
	private SerializedProperty maxDepthCloud;
	private SerializedProperty fixedSize;

	private SerializedProperty isAnimate;
	private SerializedProperty animationVelocity;
	private SerializedProperty numberOfShadows;

	// Clouds textures
	private SerializedProperty cloudsTextAdd;
	private SerializedProperty cloudsTextBlended;


	private ProceduralCloudTexture ProcText = null; // Pointer to ProceduralTexture.cs object reference.

	private SerializedProperty PT1TextureWidth;		// Texture initial size and ramdom seed.
	private SerializedProperty PT1TextureHeight;
	private SerializedProperty PT1TypeNoise;
	private SerializedProperty PT1Seed;

	private SerializedProperty PT1ScaleWidth;		// Texture size multipiers
	private SerializedProperty PT1ScaleHeight;
	private SerializedProperty PT1ScaleFactor;

	private SerializedProperty PT1TurbSize; 		// Cloud params
	private SerializedProperty PT1TurbLacun;
	private SerializedProperty PT1TurbGain;
	private SerializedProperty PT1turbPower;
	private SerializedProperty PT1xyPeriod;

	private SerializedProperty PT1Lacunarity; 		// Perlin params
	private SerializedProperty PT1FractalIncrement;
	private SerializedProperty PT1Octaves;
	private SerializedProperty PT1Offset;

	private SerializedProperty PT1HaloEffect; 		// Texture Adjustements
	private SerializedProperty PT1HaloInsideRadius;
	private SerializedProperty PT1InvertColors;
	private SerializedProperty PT1ContrastMult;
	private SerializedProperty PT1UseAlphaTexture;
	private SerializedProperty PT1AlphaIndex;



	private int i = 0;
	private int textDrawSize = 10;
	private int MyWidth = 100;
	private int toolbarOptions = 0;				// Option picked in the top toolbar (Clouds / Proc Texture).
	private bool showMaximumClouds = false;		// Check if the 'Maximum Clouds' Foldout is clicked.
	private bool showShaderSettings = false;	// Check if the 'Shader' Foldout is clicked.
	private bool showAdvancedSettings = false;	// Check if the 'Advanced Settings' Foldout is clicked.
	private bool showTextures = false;			// Check if the 'CloudToy Textures' Foldout is clicked.




	// Styles
	private GUIStyle redFoldoutStyle;
	private Color myColor;

	public void OnEnable(){
		//drawIt = serializedObject.FindProperty ("drawIt");
		maximunClouds = serializedObject.FindProperty ("MaximunClouds");
		cloudPreset = serializedObject.FindProperty ("CloudPreset");
		cloudRender = serializedObject.FindProperty ("CloudRender");
		typeClouds = serializedObject.FindProperty ("TypeClouds");
		CloudDetail = serializedObject.FindProperty ("CloudDetail");
		realisticShader = serializedObject.FindProperty ("realisticShader");
		brightShader = serializedObject.FindProperty ("brightShader");
		projectorMaterial = serializedObject.FindProperty ("projectorMaterial");
		positionCheckerTime = serializedObject.FindProperty ("PositionCheckerTime");
		sizeFactorPart = serializedObject.FindProperty ("SizeFactorPart");
		emissionMult = serializedObject.FindProperty ("EmissionMult");
		softClouds = serializedObject.FindProperty ("SoftClouds");
		spreadDir = serializedObject.FindProperty ("SpreadDir");
		lengthSpread = serializedObject.FindProperty ("LengthSpread");
		numberClouds = serializedObject.FindProperty ("NumberClouds");
		side = serializedObject.FindProperty ("Side");
		dissapearMultiplier = serializedObject.FindProperty ("DisappearMultiplier");
		maximunVelocity = serializedObject.FindProperty ("MaximunVelocity");
		velocityMultipier = serializedObject.FindProperty ("VelocityMultipier");
		paintType = serializedObject.FindProperty ("PaintType");
		cloudColor = serializedObject.FindProperty ("CloudColor");
		mainColor = serializedObject.FindProperty ("MainColor");
		secondColor = serializedObject.FindProperty ("SecondColor");
		tintStrength = serializedObject.FindProperty ("TintStrength");
		offset = serializedObject.FindProperty ("offset");
		maxWithCloud = serializedObject.FindProperty ("MaxWithCloud");
		maxTallCloud = serializedObject.FindProperty ("MaxTallCloud");
		maxDepthCloud = serializedObject.FindProperty ("MaxDepthCloud");
		fixedSize = serializedObject.FindProperty ("FixedSize");
		isAnimate = serializedObject.FindProperty ("IsAnimate");
		animationVelocity = serializedObject.FindProperty ("AnimationVelocity");
		numberOfShadows = serializedObject.FindProperty ("NumberOfShadows");
		cloudsTextAdd = serializedObject.FindProperty ("CloudsTextAdd");
		cloudsTextBlended = serializedObject.FindProperty ("CloudsTextBlended");

		PT1TextureWidth = serializedObject.FindProperty ("PT1TextureWidth");
		PT1TextureHeight = serializedObject.FindProperty ("PT1TextureHeight");
		PT1TypeNoise = serializedObject.FindProperty ("PT1TypeNoise");
		PT1Seed = serializedObject.FindProperty ("PT1Seed");
		PT1ScaleWidth = serializedObject.FindProperty ("PT1ScaleWidth");
		PT1ScaleHeight = serializedObject.FindProperty ("PT1ScaleHeight");
		PT1ScaleFactor = serializedObject.FindProperty ("PT1ScaleFactor");

		PT1TurbSize = serializedObject.FindProperty ("PT1TurbSize");
		PT1TurbLacun = serializedObject.FindProperty ("PT1TurbLacun");
		PT1TurbGain = serializedObject.FindProperty ("PT1TurbGain");
		PT1turbPower = serializedObject.FindProperty ("PT1turbPower");
		PT1xyPeriod = serializedObject.FindProperty ("PT1xyPeriod");

		PT1Lacunarity = serializedObject.FindProperty ("PT1Lacunarity");
		PT1FractalIncrement = serializedObject.FindProperty ("PT1FractalIncrement");
		PT1Octaves = serializedObject.FindProperty ("PT1Octaves");
		PT1Offset = serializedObject.FindProperty ("PT1Offset");

		PT1HaloEffect = serializedObject.FindProperty ("PT1HaloEffect");
		PT1HaloInsideRadius = serializedObject.FindProperty ("PT1HaloInsideRadius");
		PT1InvertColors = serializedObject.FindProperty ("PT1InvertColors");
		PT1ContrastMult = serializedObject.FindProperty ("PT1ContrastMult");
		PT1UseAlphaTexture = serializedObject.FindProperty ("PT1UseAlphaTexture");
		PT1AlphaIndex = serializedObject.FindProperty ("PT1AlphaIndex");
	}

    public override void OnInspectorGUI(){
		//EditorGUIUtility.LookLikeInspector();
		EditorGUIUtility.LookLikeControls();

		CloudSystem = (CloudsToy) target;
		if (!CloudSystem.gameObject) return; // If there isn't any cloudstoy gameobject in your scene, just return and do nothing at all.

		ProcText = (ProceduralCloudTexture) CloudSystem.ProceduralTexture; // Get the pointer to the ProceduralTexture object

		// Definition of a red foldout (how it will looks like)
		redFoldoutStyle = new GUIStyle(EditorStyles.foldout);
		redFoldoutStyle.normal.textColor = Color.red;
		redFoldoutStyle.focused.textColor = Color.red;
		redFoldoutStyle.hover.textColor = Color.red;
		redFoldoutStyle.active.textColor = Color.red;

		myColor = GUI.color; // Get the Current(default) GUI Color.

		// Update the serializedProperty - always do this in the beginning of OnInspectorGUI.
		serializedObject.Update ();

		// Starts Drawing
		EditorGUILayout.BeginVertical();
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		string[] MenuOptions = new string[] { "    Clouds    ", "Proc Texture" };
		toolbarOptions = GUILayout.Toolbar(toolbarOptions, MenuOptions);
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		EditorGUILayout.EndVertical();
		EditorGUILayout.BeginVertical();
		switch (toolbarOptions){
			
			
		case 0: 
			// Is the CloudsToy being executed in Unity? If not, show the Maximun Clouds parameter.
			GUIContent contentFoldout = new GUIContent(" Maximun Clouds (DO NOT change while executing)", "Set the maximun clouds number that" +
			                                           "the CloudsToy system will handle. Changing this variable in runtime will crash the application.");
			if(!ProcText)
			{
				GUI.color = Color.red;
				showMaximumClouds = EditorGUILayout.Foldout(showMaximumClouds, contentFoldout, redFoldoutStyle);
				if(showMaximumClouds)
					EditorGUILayout.PropertyField(maximunClouds, new GUIContent(" "));
				GUI.color = myColor; 

				if (GUI.changed){
					EditorUtility.SetDirty(target);
					GUI.changed = false;
				}
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();
			}

			GUIContent contentCloud = new GUIContent("  Cloud Presets: ", "Cloud pressets to quickly start configuring the clouds style");
			GUI.changed = false;
			EditorGUILayout.PropertyField(cloudPreset, contentCloud);
			if (GUI.changed)
			{
				switch(cloudPreset.enumValueIndex){
				case (int)CloudsToy.TypePreset.Stormy:
					CloudSystem.SetPresetStormy();
					break;
				case (int)CloudsToy.TypePreset.Sunrise:
					CloudSystem.SetPresetSunrise();
					break;
				case (int)CloudsToy.TypePreset.Fantasy:
					CloudSystem.SetPresetFantasy();
					break;
				}
				EditorUtility.SetDirty(target);
				//GUI.changed = false;
				return;
			}

			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Cloud Render: ", "Assign the shader that will be used to render the clouds particles.");
			EditorGUILayout.PropertyField(cloudRender, contentCloud);
			contentCloud = new GUIContent("  Cloud Type: ", "Assign the texture that will be used to draw the clouds.");
			EditorGUILayout.PropertyField(typeClouds, contentCloud);
			contentCloud = new GUIContent("  Cloud Detail: ", "Cloud complexity that will created more populated clouds. " +
										  "Be aware that higher levels can drop your FPS drasticaly if there are a lot of clouds.");
			EditorGUILayout.PropertyField(CloudDetail, contentCloud);
			if (GUI.changed)
			{
				CloudSystem.SetCloudDetailParams();
				EditorUtility.SetDirty(target);
			}
			GUI.changed = false;
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			GUI.color = Color.red; 
			contentFoldout = new GUIContent(" Particles Shader Settings", "You can change the shaders used in the clouds." +
				"It can be used, for example, to use different shaders in mobile applications.");
			showShaderSettings = EditorGUILayout.Foldout(showShaderSettings, contentFoldout, redFoldoutStyle);
			if(showShaderSettings)
			{
				EditorGUILayout.Separator();
				contentCloud = new GUIContent("Realistic Cloud Shader:", "Shader that will be used when selecting Realistic Clouds. This shader will use" +
					"the blended textures that can be assigned in the CloudsToy Texture paragraph. It is an alpha blended shader.");
				EditorGUILayout.PropertyField(realisticShader, contentCloud);
				contentCloud = new GUIContent("Bright Cloud Shader:", "Shader that will be used when selecting Bright Clouds. This shader will use" +
				                              "the add textures that can be assigned in the CloudsToy Texture paragraph. It is an alpha additive shader.");
				EditorGUILayout.PropertyField(brightShader, contentCloud);
				contentCloud = new GUIContent("Projector Material:", "The projector material will be used to create the clouds shadows. " +
											  "By default it is usedthe Unity's default projector material.");
				EditorGUILayout.PropertyField(projectorMaterial, contentCloud);
				EditorGUILayout.Separator();
			}
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			contentFoldout = new GUIContent(" Particles Advanced Settings", "This section provides of two parameters to tweak your clouds. " +
				"It will be applied to all the cloud system. Those values can make your FPS drop drastically is you select very high values. Take it into account!");
			showAdvancedSettings = EditorGUILayout.Foldout(showAdvancedSettings, contentFoldout, redFoldoutStyle);
			if(showAdvancedSettings)
			{
				EditorGUILayout.Separator();
				contentCloud = new GUIContent("  Position Checker Time: ", "The time period to check the cloud position.");
				positionCheckerTime.floatValue = EditorGUILayout.Slider(contentCloud, positionCheckerTime.floatValue, 0f, 2.0f);
				contentCloud = new GUIContent("  Size Factor: ", "Modify the initial ellipsoid from wich the cloud particle is generated, so smaller (or bigger) clouds will be created.");
				sizeFactorPart.floatValue = EditorGUILayout.Slider(contentCloud, sizeFactorPart.floatValue, 0.1f, 4.0f);
				contentCloud = new GUIContent("  Emitter Mult: ", "Modify the minimun/maximun emission particle cloud, so more (or less) populated clouds will be created.");
				emissionMult.floatValue = EditorGUILayout.Slider(contentCloud, emissionMult.floatValue, 0.1f, 4.0f);
				EditorGUILayout.Separator();
			}
			GUI.color = myColor; 
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			Rect buttonRect = EditorGUILayout.BeginHorizontal();
			buttonRect.x = buttonRect.width / 2 - 100;
			buttonRect.width = 200;
			buttonRect.height = 30;

			GUI.color = Color.red; 
			contentCloud = new GUIContent("Repaint Clouds", "Unity scene cloud regeneration and repainting. Use it when you want to be sure that all your tweaked adjustments are being applied to your clouds in the scene." +
			                              "It's ment to be used only in Unity while adjusting your clouds. DO NOT USE IT in your game in realtime execution; you will be recreating your clouds in your game just for nothing.");
			if(GUI.Button(buttonRect, contentCloud))
				CloudSystem.EditorRepaintClouds();
			GUI.color = myColor;

			EditorGUILayout.Separator();
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Soft Clouds", "Modify particle render to stretch mode instead of the regular billboard mode.");
			EditorGUILayout.PropertyField(softClouds, contentCloud);
			if(softClouds.boolValue)
			{
				contentCloud = new GUIContent("  Spread Direction: ", "The world particle velocity that will be applied to the stretched clouds particles.");
				EditorGUILayout.PropertyField(spreadDir, contentCloud);
				contentCloud = new GUIContent("  Length Spread: ", "The scale lenght to which the clouds will be stretched to.");
				lengthSpread.floatValue = EditorGUILayout.Slider(contentCloud, lengthSpread.floatValue, 1.0f, 30.0f);
			}
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Clouds Num: ", "Number of clouds that will be created. The maximum number of clouds that CloudsToy will handle" +
				"can be configured in the Maximum clouds parameter, the first cloudsToy parameter");
			numberClouds.intValue = EditorGUILayout.IntSlider(contentCloud, numberClouds.intValue, 1, maximunClouds.intValue);
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Cloud Creation Size: ", "The scene blue box size where the clouds will be created into initially.");
			EditorGUILayout.PropertyField(side, contentCloud);
			contentCloud = new GUIContent("  Dissapear Mult: ", "The scene yellow box will be calculated as a multiplier of the blue box. It is used" +
				"to know when to remove a cloud. So, when clouds are moving in any direction, as soon as they reach the yellow box border, they will be moved to the other side of the box");
			dissapearMultiplier.floatValue = EditorGUILayout.Slider(contentCloud, dissapearMultiplier.floatValue, 1.0f, 10.0f);
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Maximum Velocity: ", "The clouds maximum velocity. Bigger clouds will be slower than the smaller ones.");
			EditorGUILayout.PropertyField(maximunVelocity, contentCloud);
			contentCloud = new GUIContent("  Velocity Mult: ", "A velocity multiplier to quickly tweak the clouds velocity without modifying the previous parameter.");
			velocityMultipier.floatValue = EditorGUILayout.Slider(contentCloud, velocityMultipier.floatValue, 0.01f, 20.0f);
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Paint Type: ", "The clouds can be colorized with different adjustable colors using two diferent presets. Below paint type will be try to change the color of the" +
				"lower cloud particles to simulate how real clouds look like.");
			EditorGUILayout.PropertyField(paintType, contentCloud);
			contentCloud = new GUIContent("  Cloud Color: ", "Main cloud color used only in Realistic render mode. This color" +
				"will be directly assigned to the cloud realistic shader Tint color used by realistic particle clouds.");
			EditorGUILayout.PropertyField(cloudColor, contentCloud);
			contentCloud = new GUIContent("  Main Color: ", "This is the main color used when trying colorize the cloud.");
			EditorGUILayout.PropertyField(mainColor, contentCloud);
			contentCloud = new GUIContent("  Secondary Color: ", "This is the second color used when trying colorize the cloud.");
			EditorGUILayout.PropertyField(secondColor, contentCloud);
			contentCloud = new GUIContent("  Tint Strength: ", "Higher strenth will tint more cloud particles in the cloud.");
			tintStrength.intValue = EditorGUILayout.IntSlider(contentCloud, tintStrength.intValue, 1, 100);
			if(paintType.enumValueIndex == (int)CloudsToy.TypePaintDistr.Below)
			{
				contentCloud = new GUIContent("  Offset: ", "Will be used in the below paint type to tint the cloud particles depending on" +
					"their relative position inside the cloud. Higher values will paint particles that are in high local positions inside the cloud");
				offset.floatValue = EditorGUILayout.Slider(contentCloud, offset.floatValue, 0.0f, 1.0f);
			}
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Width: ", "Maximum width of each cloud");
			maxWithCloud.intValue = EditorGUILayout.IntSlider(contentCloud, maxWithCloud.intValue, 10, 1000);
			contentCloud = new GUIContent("  Height: ", "Maximum height of each cloud");
			maxTallCloud.intValue = EditorGUILayout.IntSlider(contentCloud, maxTallCloud.intValue, 5, 500);
			contentCloud = new GUIContent("  Depth: ", "Maximum depth of each cloud");
			maxDepthCloud.intValue = EditorGUILayout.IntSlider(contentCloud, maxDepthCloud.intValue, 5, 1000);
			contentCloud = new GUIContent("  Fixed Size", "The size of the clouds will be exactly the same depending on their cloud" +
				"size type (big, medium, small). If fixed size is disabled all the big clouds (for example) will not have the exact same size");
			EditorGUILayout.PropertyField(fixedSize, contentCloud);
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Animate Cloud", "Each cloud can be animated making it to rotate over itself.");
			EditorGUILayout.PropertyField(isAnimate, contentCloud);
			if(isAnimate.boolValue)
			{
				contentCloud = new GUIContent("  Animation Velocity: ", "Cloud rotation velocity.");
				animationVelocity.floatValue = EditorGUILayout.Slider(contentCloud, animationVelocity.floatValue, 0.0f, 1.0f);
			}
			contentCloud = new GUIContent("  Shadows: ", "Clouds can have a shadow. It is made using a Unity's projector that creates a shadow" +
				"taking into account the layer the clouds are in (so they will ignore the cloud particle itself when drawing their own shadow");
			EditorGUILayout.PropertyField(numberOfShadows, contentCloud);
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			contentFoldout = new GUIContent(" CloudsToy Textures", "This section show all textures used in cloudstoy to generate clouds");
			showTextures = EditorGUILayout.Foldout(showTextures, contentFoldout);

			if(showTextures){
				GUI.color = Color.green;
				contentCloud = new GUIContent("  Texture Size", " ");
				textDrawSize = EditorGUILayout.IntSlider(contentCloud, textDrawSize, 60, 90);
				GUI.color = myColor;

				EditorGUILayout.Separator();
				EditorGUILayout.Separator();

				contentCloud = new GUIContent("  Texture Add", "These are the textures that will be used by bright type clouds. Bright clouds " +
					"use a particle additive kind of shader.");
				GUIContent contentCloud2 = new GUIContent("(Used for Bright Clouds)", "");
				EditorGUILayout.LabelField(contentCloud, contentCloud2);



				EditorGUILayout.BeginHorizontal();
				for(i = 0; i < cloudsTextAdd.arraySize; i++)
				{
					if(i == cloudsTextAdd.arraySize*0.5f)
					{
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Separator();
						EditorGUILayout.BeginHorizontal();
					}
					else
					if(i != 0 && i != 3)
						EditorGUILayout.Separator();

					contentCloud = new GUIContent("", "");
					cloudsTextAdd.GetArrayElementAtIndex(i).objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField(cloudsTextAdd.GetArrayElementAtIndex(i).objectReferenceValue, typeof(Texture2D), false, GUILayout.Width(textDrawSize), GUILayout.Height(textDrawSize));
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();

				contentCloud = new GUIContent("  Texture Blended", "These are the textures that will be used by realistic type clouds. Bright clouds " +
				                              "use a particle blended additive kind of shader.");
				contentCloud2 = new GUIContent("(Used for Realistic Clouds)", "");
				EditorGUILayout.LabelField(contentCloud, contentCloud2);
				EditorGUILayout.BeginHorizontal();
				for(i = 0; i < cloudsTextBlended.arraySize; i++)
				{
					if(i == cloudsTextBlended.arraySize*0.5f)
					{
						EditorGUILayout.EndHorizontal();
						EditorGUILayout.Separator();
						EditorGUILayout.BeginHorizontal();
					}
					else
					if(i != 0 && i != 3)
						EditorGUILayout.Separator();

					contentCloud = new GUIContent("", "");
					cloudsTextBlended.GetArrayElementAtIndex(i).objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField(cloudsTextBlended.GetArrayElementAtIndex(i).objectReferenceValue, typeof(Texture2D), false, GUILayout.Width(textDrawSize), GUILayout.Height(textDrawSize));
				}
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Separator();
			}

			EditorGUILayout.Separator();
			if (GUI.changed)
				EditorUtility.SetDirty (target);
			GUI.changed = false;
			break;
			
			
		case 1:
			if(!ProcText)
			{
				GUI.color = Color.red; 
				contentCloud = new GUIContent("  Texture Width: ", "Texture width used to create the runtime noise based texture. Because the texture is generated at runtime, " +
					"big texture sizes will slow the process. The texture is generated pixel by pixel so an 256x256 texture will be FOUR TIMES SLOWER than a 128x128 texture");
				EditorGUILayout.PropertyField(PT1TextureWidth, contentCloud);
				contentCloud = new GUIContent("  Texture Height: ", "Texture height used to create the runtime noise based texture. Because the texture is generated at runtime, " +
				                              "big texture sizes will slow the process. The texture is generated pixel by pixel so an 256x256 texture will be FOUR TIMES SLOWER than a 128x128 texture");
				EditorGUILayout.PropertyField(PT1TextureHeight, contentCloud);
				if (GUI.changed && ProcText)
					EditorUtility.SetDirty(target);
				GUI.color = myColor; 
			}
			GUI.changed = false;
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Type of Noise: ", "There are two different noise generator algorithms: The standard noise generation (Cloud) and the usual perlin noise generator.");
			EditorGUILayout.PropertyField(PT1TypeNoise, contentCloud);
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Seed: ", "This value is the basic seed value to generate any ramdom noise Diferent values will generate different noise patterns.");
			PT1Seed.intValue = EditorGUILayout.IntSlider(contentCloud, PT1Seed.intValue, 1, 10000);
			EditorGUILayout.Separator();
			if (GUI.changed && ProcText)
			{
				CloudSystem.PT1NewRandomSeed();
				EditorUtility.SetDirty(target);
			}
			GUI.changed = false;

			contentCloud = new GUIContent("  Scale Width: ", "Internal noise scale width used when generating the noise pattern.");
			PT1ScaleWidth.floatValue = EditorGUILayout.Slider(contentCloud, PT1ScaleWidth.floatValue, 0.1f, 50.0f);
			contentCloud = new GUIContent("  Scale Height: ", "Internal noise scale height used when generating the noise pattern.");
			PT1ScaleHeight.floatValue = EditorGUILayout.Slider(contentCloud, PT1ScaleHeight.floatValue, 0.1f, 50.0f);
			contentCloud = new GUIContent("  Scale Factor: ", "Scale multiplier used to quickly tweak the scale width/height at once.");
			PT1ScaleFactor.floatValue = EditorGUILayout.Slider(contentCloud, PT1ScaleFactor.floatValue, 0.1f, 2.0f);
			EditorGUILayout.Separator();

			switch(PT1TypeNoise.enumValueIndex){
			case (int)CloudsToy.NoisePresetPT1.Cloud:
				contentCloud = new GUIContent("  Turb Size: ", "Turbulence parameter used by SimpleNoise Turbulence generator. Internally, this is the octaves parameter" +
				                              "used to calculate the turbulence of an already created SimpleNoise texture.");
				PT1TurbSize.intValue = EditorGUILayout.IntSlider(contentCloud, PT1TurbSize.intValue, 1, 256);
				contentCloud = new GUIContent("  Turb Lacun: ", "Lacunarity parameter used by SimpleNoise Turbulence generator. Internally, this is the lacunarity parameter" +
				                              "used to calculate the turbulence of an already created SimpleNoise texture.");
				PT1TurbLacun.floatValue = EditorGUILayout.Slider(contentCloud, PT1TurbLacun.floatValue, 0.01f, 0.99f);
				contentCloud = new GUIContent("  Turb Gain: ", "Gain parameter used by SimpleNoise Turbulence generator. Internally, this is the gain parameter" +
				                              "used to calculate the turbulence of an already created SimpleNoise texture. Higher values will generate brighter textures.");
				PT1TurbGain.floatValue = EditorGUILayout.Slider(contentCloud, PT1TurbGain.floatValue, 0.01f, 2.99f);
				contentCloud = new GUIContent("  Radius: ", "Used to adjust the noise turbulence. Lower values will generate darker textures because the resulting texture" +
				                              "will dark the pixels outside that radious.");
				PT1xyPeriod.floatValue = EditorGUILayout.Slider(contentCloud, PT1xyPeriod.floatValue, 0.1f, 2.0f);
				contentCloud = new GUIContent("  Turb Power: ", "Turbulence multipler that will affect the pixels inside the turbulence radious. " +
				                              "Higher values will generate brighter results BUT it will only affect the pixels inside a given Radious.");
				PT1turbPower.floatValue = EditorGUILayout.Slider(contentCloud, PT1turbPower.floatValue, 1.0f, 60.0f);
				break;
			case (int)CloudsToy.NoisePresetPT1.PerlinCloud:
				contentCloud = new GUIContent("  Lacunarity: ", "Lacunarity parameter used by Perlin noise generator.");
				PT1Lacunarity.floatValue = EditorGUILayout.Slider(contentCloud, PT1Lacunarity.floatValue, 0.0f, 10.0f);
				contentCloud = new GUIContent("  FractalIncrement: ", "FractalIncrement parameter used by Perlin noise generator.");
				PT1FractalIncrement.floatValue = EditorGUILayout.Slider(contentCloud, PT1FractalIncrement.floatValue, 0.0f, 2.0f);
				contentCloud = new GUIContent("  Octaves: ", "Octave parameter used by Perlin noise generator.");
				PT1Octaves.floatValue = EditorGUILayout.Slider(contentCloud, PT1Octaves.floatValue, 0.0f, 10.0f);
				contentCloud = new GUIContent("  Offset: ", "Offset parameter used by Perlin noise generator (HybridMultifractal noise functions).");
				PT1Offset.floatValue = EditorGUILayout.Slider(contentCloud, PT1Offset.floatValue, 0.1f, 3.0f);
				break;
			}
			EditorGUILayout.Separator();

			/*CloudSystem.PT1IsHalo = EditorGUILayout.Toggle("  Halo Active:", CloudSystem.PT1IsHalo);
			if(CloudSystem.PT1IsHalo)*/
			contentCloud = new GUIContent("  HaloEffect: ", "Will create a dark halo around the texture, used to make rounded textures that can be used to draw the clouds.");
			PT1HaloEffect.floatValue = EditorGUILayout.Slider(contentCloud, PT1HaloEffect.floatValue, 0.1f, 1.7f);
			contentCloud = new GUIContent("  Inside Radius: ", "Will dark the pixels inside the Halo, used to teawk rounded textures that can be used to draw the clouds.");
			PT1HaloInsideRadius.floatValue = EditorGUILayout.Slider(contentCloud, PT1HaloInsideRadius.floatValue, 0.1f, 3.5f);
			EditorGUILayout.Separator();
			/*CloudSystem.PT1BackgroundColor = EditorGUILayout.ColorField("  Back Color: ", CloudSystem.PT1BackgroundColor);
			CloudSystem.PT1FinalColor = EditorGUILayout.ColorField("  Front Color: ", CloudSystem.PT1FinalColor);*/
			contentCloud = new GUIContent("  Invert Colors:", "Invert the texture colors.");
			EditorGUILayout.PropertyField(PT1InvertColors, contentCloud);
			contentCloud = new GUIContent("  Contrast Mult: ", "Higher values will create brighter textures.");
			PT1ContrastMult.floatValue = EditorGUILayout.Slider(contentCloud, PT1ContrastMult.floatValue, 0.0f, 2.0f);
			EditorGUILayout.Separator();
			contentCloud = new GUIContent("  Alpha Texture:", "It will create a second texture with transparency so the alpha channel can be tweaked." +
				"This new alpha texture will be draw in the inspector so you can see the alpha channel (the alpha values will be shown in green color.");
			EditorGUILayout.PropertyField(PT1UseAlphaTexture, contentCloud);
			if(PT1UseAlphaTexture.boolValue)
			{
				contentCloud = new GUIContent("  Alpha Index: ", "This value 0-1 will be used to increase/decrease the texture's alpha channel.");
				PT1AlphaIndex.floatValue = EditorGUILayout.Slider(contentCloud, PT1AlphaIndex.floatValue, 0.0f, 1.0f);
			}
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			
			// Be sure that the Texture1 class exists before try to paint the textures.
			if(ProcText)
			{
				contentCloud = new GUIContent("  InEditor Text Size ", "Texture size used only in the inspector window.");
				MyWidth = EditorGUILayout.IntSlider(contentCloud, MyWidth, 50, 105);
				EditorGUILayout.Separator();
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				ProcText.MyTexture = (Texture2D)EditorGUILayout.ObjectField(ProcText.MyTexture, typeof(Texture2D), false, GUILayout.Width(MyWidth), GUILayout.Height(MyWidth));
				EditorGUILayout.Separator();
				EditorGUILayout.Space();
				if(CloudSystem.PT1UseAlphaTexture)
					ProcText.MyAlphaDrawTexture = (Texture2D)EditorGUILayout.ObjectField(ProcText.MyAlphaDrawTexture, typeof(Texture2D), false, GUILayout.Width(MyWidth), GUILayout.Height(MyWidth));
				EditorGUILayout.Separator();
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();
				EditorGUILayout.Separator();
			}
			
			Rect buttonRectPT1 = EditorGUILayout.BeginHorizontal();
			buttonRectPT1.x = buttonRectPT1.width / 2 - 100;
			buttonRectPT1.width = 200;
			buttonRectPT1.height = 30;

			GUI.color = Color.red; 
			contentCloud = new GUIContent("Reset Parameters", "Reset the noise parameters to their default values.");
			if(GUI.Button(buttonRectPT1, contentCloud))
			{
				CloudSystem.ResetCloudParameters();
				if(ProcText)
					CloudSystem.PT1CopyParameters();
			}
			EditorGUILayout.EndHorizontal();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			// Is the program being executed? If so, show the 'Save Params' button.
			GUI.color = Color.yellow;
			if(ProcText)
			{
				Rect buttonRectPrint = EditorGUILayout.BeginHorizontal();
				buttonRectPrint.x = buttonRectPrint.width * 0.5f - 100;
				buttonRectPrint.width = 200;
				buttonRectPrint.height = 30;

				contentCloud = new GUIContent("Save Texture", "Save the generated texture to a file. CAUTION: This funcion can not be used in Web Player targeted projects.");
				if(GUI.Button(buttonRectPrint, contentCloud))
					CloudSystem.SaveProceduralTexture();
				EditorGUILayout.EndHorizontal();
			}

			GUI.color = myColor; 
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();

			if (GUI.changed)
			{
				if(ProcText)
				{
					CloudSystem.PT1CopyParameters();
					CloudSystem.ModifyPTMaterials();
				}
				EditorUtility.SetDirty (target);
			}
			GUI.changed = false;
			
			break;
		
		}
		EditorGUILayout.EndVertical();

		// Apply changes to the serializedProperty - always do this in the end of OnInspectorGUI.
		serializedObject.ApplyModifiedProperties ();
    }
}
