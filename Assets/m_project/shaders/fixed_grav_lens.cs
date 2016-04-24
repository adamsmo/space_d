using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class fixed_grav_lens : MonoBehaviour {
	public Shader  shader;

	private Material _material; // Material which is located shader
	protected Material material {
		get {
			if (_material == null) {
				_material = new Material (shader);
				_material.hideFlags = HideFlags.HideAndDontSave;
			}
			return _material;
		} 
	}

	protected virtual void OnDisable() {
		if( _material ) {
			DestroyImmediate( _material );
		}
	}

	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		if (shader && material) {
			Graphics.Blit(source, destination, material);
		}
	}
}
