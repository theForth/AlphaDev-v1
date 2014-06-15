using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BaseProjectileController))] 
public class BaseProjectileController_Editor : Editor
{
		public SerializedProperty test;
		public SerializedProperty projectile;
		public SerializedProperty projectileSpeed;
		public SerializedProperty projectileSpeedDelta;
		public SerializedProperty projectileLife;
		public SerializedProperty projectileAcceleration;
		public SerializedProperty Offset;
		public SerializedProperty displayScale;
		public SerializedProperty displayOffset;
		public SerializedProperty gizmosFlag;
		public SerializedProperty clipSize;
		public SerializedProperty horizontalSpread;
		public SerializedProperty verticalSpread;
		public SerializedProperty projectileCount;
		public SerializedProperty showDisplayHandles;
		public SerializedProperty projectileGlobalAcceleration;
		public SerializedProperty rateOfFire;
		public SerializedProperty reloadTime;
		public SerializedProperty isSemiAutomatic;
	
		public SerializedProperty shootSound;
		public SerializedProperty reloadingSound;
		public SerializedProperty reloadedSound;
		public SerializedProperty emptyClickSound;
	
		public bool speedEditMode;
		public bool lifeEditMode;
	
		void SerializeProperties ()
		{
				test = this.serializedObject.FindProperty ("test");
				projectile = this.serializedObject.FindProperty ("projectile");
				horizontalSpread = this.serializedObject.FindProperty ("horizontalSpread");
				verticalSpread = this.serializedObject.FindProperty ("verticalSpread");
				displayOffset = this.serializedObject.FindProperty ("displayOffset");
				projectileAcceleration = this.serializedObject.FindProperty ("projectileAcceleration");
				projectileSpeed = this.serializedObject.FindProperty ("projectileSpeed");
				projectileSpeedDelta = this.serializedObject.FindProperty ("projectileSpeedDelta");
				projectileLife = this.serializedObject.FindProperty ("projectileLife");
				rateOfFire = this.serializedObject.FindProperty ("rateOfFire");
				clipSize = this.serializedObject.FindProperty ("clipSize");
				displayScale = this.serializedObject.FindProperty ("displayScale");
				projectileCount = this.serializedObject.FindProperty ("projectileCount");
				showDisplayHandles = this.serializedObject.FindProperty ("showDisplayHandles");
				projectileGlobalAcceleration = this.serializedObject.FindProperty ("projectileGlobalAcceleration");
				reloadTime = this.serializedObject.FindProperty ("reloadTime");
				gizmosFlag = this.serializedObject.FindProperty ("gizmosFlag");
				isSemiAutomatic = this.serializedObject.FindProperty ("isSemiAutomatic");
		
				shootSound = this.serializedObject.FindProperty ("shootSound");
				reloadingSound = this.serializedObject.FindProperty ("reloadingSound");
				reloadedSound = this.serializedObject.FindProperty ("reloadedSound");
				emptyClickSound = this.serializedObject.FindProperty ("emptyClickSound");
		}
	
		override public void OnInspectorGUI ()
		{
				BaseProjectileController tW = (BaseProjectileController)target;
				tW.accelerationScale = 0.01f;
				if (projectile != null) {
						if (projectile.objectReferenceValue == null) {
								projectile.objectReferenceValue = (GameObject)Resources.Load ("Defaultprojectile");
						}
				}
		
				SerializeProperties ();
		
				//General settings editor
				EditorGUILayout.Space ();
				EditorGUILayout.BeginHorizontal ();
				bool autofire = EditorGUILayout.Toggle ("Autofire test", test.boolValue);
				test.boolValue = autofire;
				if (!autofire && Application.isPlaying) {
						if (GUILayout.Button ("Test shoot")) {
								tW.GetComponent<BaseProjectileController> ().Shoot ();
						}
				}
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.TextField ("Trigger Axis", tW.triggerAxis);
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("projectile prefab");
				projectile.objectReferenceValue = (GameObject)EditorGUILayout.ObjectField (projectile.objectReferenceValue, typeof(GameObject), false);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				bool automatic = EditorGUILayout.Toggle ("Auto-fire trigger", isSemiAutomatic.boolValue);
				isSemiAutomatic.boolValue = automatic;
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
		
				//projectile values editor
				EditorGUILayout.Slider (horizontalSpread, 0, 100, "Horizontal Spread");
				EditorGUILayout.Slider (verticalSpread, 0, 100, "Vertical Spread");
				EditorGUILayout.Slider (projectileLife, 1, 60);
				EditorGUILayout.IntSlider (projectileCount, 1, 21, "projectiles per shot");
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("projectile speed");
				projectileSpeed.floatValue = EditorGUILayout.FloatField (projectileSpeed.floatValue);
				GUILayout.Label ("±");
				projectileSpeedDelta.floatValue = EditorGUILayout.FloatField (projectileSpeedDelta.floatValue);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
		
				//projectile acceleration editor
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("Acceleration");
				EditorGUILayout.BeginVertical ();
				projectileAcceleration.vector3Value = EditorGUILayout.Vector3Field ("Local (Shoot shape)", projectileAcceleration.vector3Value);
				projectileGlobalAcceleration.vector3Value = EditorGUILayout.Vector3Field ("Global (Gravity + wind)", projectileGlobalAcceleration.vector3Value);
				EditorGUILayout.EndVertical ();
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();
		
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("Display options");
				EditorGUILayout.BeginVertical ();
				EditorGUILayout.BeginHorizontal ();				
				gizmosFlag.boolValue = EditorGUILayout.Toggle ("", gizmosFlag.boolValue, GUILayout.Width (12));
				EditorGUILayout.LabelField ("Always show gizmos");
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				showDisplayHandles.boolValue = EditorGUILayout.Toggle ("", showDisplayHandles.boolValue, GUILayout.Width (12));
				displayOffset.vector3Value = EditorGUILayout.Vector3Field ("Show display offset handles", displayOffset.vector3Value);
				EditorGUILayout.EndHorizontal ();
				displayScale.floatValue = EditorGUILayout.FloatField ("Display scale", displayScale.floatValue);
				EditorGUILayout.EndVertical ();
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.Space ();

				//Sound values
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("Shoot sound");
				shootSound.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField (shootSound.objectReferenceValue, typeof(AudioClip), false);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("Reloading sound");
				reloadingSound.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField (reloadingSound.objectReferenceValue, typeof(AudioClip), false);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("Reloaded sound");
				reloadedSound.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField (reloadedSound.objectReferenceValue, typeof(AudioClip), false);
				EditorGUILayout.EndHorizontal ();
				EditorGUILayout.BeginHorizontal ();
				EditorGUILayout.PrefixLabel ("Empty click sound");
				emptyClickSound.objectReferenceValue = (AudioClip)EditorGUILayout.ObjectField (emptyClickSound.objectReferenceValue, typeof(AudioClip), false);
				EditorGUILayout.EndHorizontal ();

				//Value clamps & update
				projectileSpeed.floatValue = (float)Mathf.Clamp (projectileSpeed.floatValue, 1, Mathf.Infinity);
				projectileSpeedDelta.floatValue = (float)Mathf.Clamp (projectileSpeedDelta.floatValue, 0, Mathf.Infinity);
				projectileLife.floatValue = (float)Mathf.Clamp (projectileLife.floatValue, 1, Mathf.Infinity);
				horizontalSpread.floatValue = (float)Mathf.Clamp (horizontalSpread.floatValue, 0, Mathf.Infinity);
				verticalSpread.floatValue = (float)Mathf.Clamp (verticalSpread.floatValue, 0, Mathf.Infinity);
				clipSize.intValue = (int)Mathf.Clamp (clipSize.intValue, 1, Mathf.Infinity);
				this.serializedObject.ApplyModifiedProperties ();
				if (GUI.changed) {
						SceneView.RepaintAll ();
				}
		}
	
		void OnSceneGUI ()
		{
				Vector3 offset, vec;
				BaseProjectileController tW = (BaseProjectileController)target;
		
				if (tW.enabled) {
			
						SerializeProperties ();
			
						//Display position handle
						if (showDisplayHandles.boolValue) {
								offset = tW.transform.position;
								vec = -offset + Handles.PositionHandle (offset + tW.DisplayOffset (), tW.transform.rotation);
								displayOffset.vector3Value = tW.transform.worldToLocalMatrix.MultiplyVector (vec);

								displayScale.floatValue = Mathf.Max (0.1f, 
					Handles.ScaleSlider (displayScale.floatValue, 
						offset + tW.DisplayOffset (),
						-tW.transform.up,
						tW.transform.rotation,
						0.75f * HandleUtility.GetHandleSize (offset + tW.DisplayOffset ()), 0));
						}
			
						//Rate of fire handle (time scale)
						Handles.color = Color.white;
						offset = tW.DisplayPosition () + tW.transform.up * displayScale.floatValue / 2 + tW.transform.forward * 0.5f * displayScale.floatValue;
						vec = -offset + Handles.Slider (offset + tW.transform.forward * 1 / rateOfFire.floatValue * tW.clipSize * displayScale.floatValue, tW.transform.forward, 0.75f * displayScale.floatValue, Handles.ConeCap, 0);
						rateOfFire.floatValue = tW.clipSize / (vec.magnitude / displayScale.floatValue);
			
						//Clip size handle
						Handles.color = Color.white;
						offset = tW.DisplayPosition () - tW.transform.up * displayScale.floatValue / 2 + tW.transform.forward * 0.75f * displayScale.floatValue;
						vec = -offset + Handles.Slider (offset + tW.transform.forward * 1 / tW.rateOfFire * tW.clipSize * displayScale.floatValue, tW.transform.forward, 0.75f * displayScale.floatValue, Handles.SphereCap, 0);
						offset = tW.DisplayPosition () - tW.transform.up * displayScale.floatValue / 2 + tW.transform.forward * 0.5f * displayScale.floatValue;
						vec = -offset + Handles.Slider (offset + tW.transform.forward * 1 / tW.rateOfFire * tW.clipSize * displayScale.floatValue, tW.transform.forward, 0.75f * displayScale.floatValue, Handles.CylinderCap, 0);
						clipSize.intValue = Mathf.RoundToInt (tW.rateOfFire * (vec.magnitude / displayScale.floatValue));
			
						//Reload time handle
						Handles.color = Color.gray;
						offset = tW.DisplayPosition () + tW.transform.up * displayScale.floatValue / 2 - tW.transform.forward * 0.5f * displayScale.floatValue;
						vec = -offset + Handles.Slider (offset - tW.transform.forward * tW.reloadTime * displayScale.floatValue, -tW.transform.forward, 0.75f * displayScale.floatValue, Handles.ConeCap, 0);
						reloadTime.floatValue = vec.magnitude / displayScale.floatValue;
			
						//Update values
						this.serializedObject.ApplyModifiedProperties ();
			
						//Lifetime display
						Handles.color = Color.white;
						Vector3 position = tW.ShotEnd (tW.projectileSpeed * tW.transform.forward, tW.projectileLife, Color.white);
						Handles.Label (position, Mathf.Round (100 * tW.projectileLife) / 100 + " secs" + " | " + Mathf.Round (100 * Vector3.Distance (tW.transform.position, position)) / 100 + " m");
				}
		}
	
}
