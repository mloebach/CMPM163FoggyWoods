// SphericalFog.cs
//
// By: Ismael Cortez
// Date: 06-02-2020
// CMPM 163 Final Project: Foggy Forest
//
// Adapted from:
//	https://forum.unity.com/threads/spherical-fog-shader-shared-project.269771/
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SphericalFog : MonoBehaviour
{
	protected MeshRenderer sphericalFogObject;
	public Material sphericalFogMaterial;
	public float scaleFactor = 1;

	void OnEnable()
	{
		sphericalFogObject = gameObject.GetComponent<MeshRenderer>();
		if (sphericalFogObject == null)
			Debug.LogError("Volume Fog Object must have a MeshRenderer Component!");
		
		//Note: In forward lightning path, the depth texture is not automatically generated.
		if (Camera.main.depthTextureMode == DepthTextureMode.None)
			Camera.main.depthTextureMode = DepthTextureMode.Depth;
		
		sphericalFogObject.material = sphericalFogMaterial;
		
	}

	// Update() is called once per frame
	void Update()
	{
		float radius = (transform.lossyScale.x + transform.lossyScale.y + transform.lossyScale.z) / 6;
		Material mat = Application.isPlaying ? sphericalFogObject.material : sphericalFogObject.sharedMaterial;
		if (mat)
			mat.SetVector ("FogParam", new Vector4(transform.position.x, transform.position.y, transform.position.z, radius * scaleFactor));
	}
}

