using System.Collections.Generic;

public class Triangulation {
	public List<Point> points { get; private set; }
	public List<int> indices { get; private set; }
	public List<Triangle> triangles { get; private set; }

	public Triangulation(List<Point> points) {
		this.points = points;
		triangles = new List<Triangle>();
		indices = new List<int>();
	}

	public void Triangulate(bool convexHull) {
		SortPoints();
		if (convexHull)
			ComputeConvexHull();
		PointsIndexing();
		ConnectPoints();
	}

	void SortPoints() {
		points.Sort((a, b) =>
		  a.x == b.x ? a.y.CompareTo(b.y) : (a.x > b.x ? 1 : -1));
	}

	void PointsIndexing() {
		int pointsCount = points.Count;
		for (int i = 0; i < pointsCount; i++) {
			points[i].index = i;
		}
	}

	void ConnectPoints() {
		Circle circle = new Circle();
		bool correctCandidate;
		int pointsCount = points.Count;
		Point[] pointsArray = points.ToArray();

		// Take three points
		for (int i = 0; i < pointsCount - 2; i++) {
			for (int j = i + 1; j < pointsCount - 1; j++) {
				for (int k = j + 1; k < pointsCount; k++) {
					// Make circle passing through three points
					circle.Circumcircle(pointsArray[i], pointsArray[j], pointsArray[k]);
					correctCandidate = true;
					for (int l = 0; l < pointsCount; l++) {
						if (l != i && l != j && l != k) {
							// Check whether there is a point lying inside the circle
							if (circle.Inside(pointsArray[l])) {
								// No point can lie in the interior of any circle
								correctCandidate = false;
								break;
							}
						}
					}

					if (correctCandidate)
						AddTriangle(pointsArray[i], pointsArray[j], pointsArray[k]);
				}
			}
		}
	}

	void AddTriangle(Point p1, Point p2, Point p3) {
		triangles.Add(new Triangle(p1, p2, p3));
		float orientation = PointExtension.Cross(p1, p2, p3);
		// Add indices in clockwise order
		if (orientation < 0) {
			indices.Add(p1.index);
			indices.Add(p2.index);
			indices.Add(p3.index);
		} else if (orientation > 0) {
			indices.Add(p3.index);
			indices.Add(p2.index);
			indices.Add(p1.index);
		}
	}

	void ComputeConvexHull() {
		List<Point> hull = new List<Point>();
		int lowerHull = 0, upperHull = 0; // Size of lower and upper hulls
		int pointsCount = points.Count;

		// Builds a hull from the leftmost point.
		for (int i = 0; i < pointsCount; i++) {
			Point p = points[i];
			Point p1;

			// Build lower hull
			while (lowerHull >= 2 && (p1 = hull[hull.Count - 1]).Subtract(hull[hull.Count - 2]).Cross(p.Subtract(p1)) >= 0) {
				hull.RemoveAt(hull.Count - 1);
				lowerHull--;
			}
			hull.Add(p);
			lowerHull++;

			// Build upper hull
			while (upperHull >= 2 && (p1 = hull[0]).Subtract(hull[1]).Cross(p.Subtract(p1)) <= 0) {
				hull.RemoveAt(0);
				upperHull--;
			}
			if (upperHull != 0) // When upperHull=0, share the point added above
				hull.Insert(0, p);
			upperHull++;
		}
		hull.RemoveAt(hull.Count - 1);

		points = hull;
	}
}
