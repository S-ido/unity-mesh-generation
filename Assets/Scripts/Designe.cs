using UnityEngine;
using UnityEngine.EventSystems;

public class Designe : MonoBehaviour {
	public float minPointsDistance = 0.3f;
	public float removePointDistance = 0.15f;
	public Color markerColor;
	public float markerSize = 0.1f;

	private Grid grid;

	static Material mat;
	static void CreateMaterial() {
		if (!mat) {
			var shader = Shader.Find("Hidden/Internal-Colored");
			mat = new Material(shader);
			mat.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			mat.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			mat.SetInt("_ZWrite", 1);
		}
	}

	void Awake() {
		grid = new Grid();
		grid.Load();
	}

	void Update() {
		if (!EventSystem.current.IsPointerOverGameObject()) {
			if (Input.GetButtonUp("Fire1")) {
				Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				grid.AddPoint(point, minPointsDistance);
				grid.Save();
			}

			if (Input.GetButtonUp("Fire2")) {
				Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				grid.RemovePoint(point, removePointDistance);
				grid.Save();
			}
		}
	}

	public void ResetPoints() {
		grid.Reset();
	}

	public void OnRenderObject() {
		if (grid != null && grid.points.Count > 0) {
			CreateMaterial();
			mat.SetPass(0);
			GL.PushMatrix();
			GL.Begin(GL.QUADS);
			GL.Color(markerColor);
			for (int i = 0; i < grid.points.Count; i++) {
				DrawQuad(grid.points[i].ToVector2(), 0);
			}
			GL.Color(Color.white);
			GL.End();
			GL.PopMatrix();
		}
	}

	void DrawQuad(Vector2 position, float depth) {
		GL.Vertex3(position.x - markerSize, position.y - markerSize, depth);
		GL.Vertex3(position.x - markerSize, position.y + markerSize, depth);
		GL.Vertex3(position.x + markerSize, position.y + markerSize, depth);
		GL.Vertex3(position.x + markerSize, position.y - markerSize, depth);
	}
}
