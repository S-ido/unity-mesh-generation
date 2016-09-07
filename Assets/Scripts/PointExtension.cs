public static class PointExtension {

	public static Point Subtract(this Point p1, Point p2) {
		return new Point(p1.x - p2.x, p1.y - p2.y, 0);
	}

	public static float Cross(this Point p1, Point p2) {
		return p1.x * p2.y - p1.y * p2.x;
	}

	// =0 - Collinear, >0 - Counterclockwise, <0 - Clockwise
	public static float Cross(Point p1, Point p2, Point p3) {
		return (p2.x - p1.x) * (p3.y - p1.y) - (p2.y - p1.y) * (p3.x - p1.x);
	}

	public static float Distance(this Point p1, Point p2) {
		float v1 = p1.x - p2.x, v2 = p1.y - p2.y;
		return UnityEngine.Mathf.Sqrt((v1 * v1) + (v2 * v2));
	}

	public static float DistanceSquared(this Point p1, Point p2) {
		float v1 = p1.x - p2.x, v2 = p1.y - p2.y;
		return (v1 * v1) + (v2 * v2);
	}
}
