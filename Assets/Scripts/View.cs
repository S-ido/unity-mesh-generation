using UnityEngine;

public class View : MonoBehaviour {
	public bool uniqueVertices = true;

	private Grid grid;
	private Mesh mesh;
	private Triangulation triangulation;

	void Awake() {
		grid = new Grid(Application.dataPath + "/grid.dat");
		grid.Load();
	}

	void Start() {
		if (grid != null) {
			Generate(false);
		}
	}

	private void Generate(bool convexHull) {
		if (!grid.IsValid()) {
			Debug.Log("You must add at least 3 points");
			return;
		}

		mesh = new Mesh();
		Triangulate(convexHull);
		SetVertices();
		SetTriangles();
		SetUVs();
		if (uniqueVertices) {
			MakeVerticesUnique();
			SetColor();
		}

		SetMesh();
	}

	private void Triangulate(bool convexHull) {
		triangulation = new Triangulation(grid.points);
		triangulation.Triangulate(convexHull);
	}

	private void SetVertices() {
		int pointsCount = triangulation.points.Count;
		Vector3[] vertices3D = new Vector3[pointsCount];

		for (int i = 0; i < pointsCount; i++) {
			vertices3D[i] = triangulation.points[i].ToVector3();
		}

		mesh.vertices = vertices3D;
	}

	private void SetTriangles() {
		mesh.triangles = triangulation.indices.ToArray();
	}

	private void SetUVs() {
		int pointsCount = triangulation.points.Count;
		Vector2[] vertices2D = new Vector2[pointsCount];

		for (int i = 0; i < pointsCount; i++) {
			vertices2D[i] = triangulation.points[i].ToVector2();
		}

		mesh.uv = vertices2D;
	}

	private void MakeVerticesUnique() {
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector3[] verticesModified = new Vector3[triangles.Length];
		int[] trianglesModified = new int[triangles.Length];

		for (int i = 0; i < trianglesModified.Length; i++) {
			verticesModified[i] = vertices[triangles[i]];
			trianglesModified[i] = i;
		}

		mesh.vertices = verticesModified;
		mesh.triangles = trianglesModified;
	}

	private void SetColor() {
		Color32 leftColor = new Color(0.09f, 0.2f, 0.27f);
		Color32 rightColor = new Color(0.99f, 0.27f, 0.16f);
		Color32 currentColor = new Color32();
		int trianglesLength = mesh.triangles.Length;
		Color32[] colors = new Color32[trianglesLength];

		for (int i = 0; i < trianglesLength; i++) {
			if (i % 3 == 0)
				currentColor = Color32.Lerp(leftColor, rightColor, (float)i / trianglesLength);

			colors[i] = currentColor;
		}

		mesh.colors32 = colors;
	}

	private void SetMesh() {
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		mesh.name = "NewMesh";
		GetComponent<MeshFilter>().mesh = mesh;
	}

	public void OnDelaunayButtonClick() {
		Generate(false);
	}

	public void OnConvexHullButtonClick() {
		Generate(true);
	}
}
