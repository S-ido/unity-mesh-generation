public class Circle {
	private Point center;
	private float radius;

	public Circle() {
		center = new Point();
		radius = 0.0f;
	}

	// Compute the circle defined by three points
	public void Circumcircle(Point p1, Point p2, Point p3) {
		float orientation;

		orientation = PointExtension.Cross(p1, p2, p3);
		if (orientation != 0.0) {
			float p1Sq, p2Sq, p3Sq;
			float num;
			float cx, cy;

			p1Sq = p1.x * p1.x + p1.y * p1.y;
			p2Sq = p2.x * p2.x + p2.y * p2.y;
			p3Sq = p3.x * p3.x + p3.y * p3.y;
			num = p1Sq * (p2.y - p3.y) + p2Sq * (p3.y - p1.y) + p3Sq * (p1.y - p2.y);
			cx = num / (2.0f * orientation);
			num = p1Sq * (p3.x - p2.x) + p2Sq * (p1.x - p3.x) + p3Sq * (p2.x - p1.x);
			cy = num / (2.0f * orientation);

			center.x = cx;
			center.y = cy;
		}

		radius = center.Distance(p1);
	}

	// Is point lies inside the circle
	public bool Inside(Point p) {
		if (center.DistanceSquared(p) < radius * radius)
			return true;
		else
			return false;
	}
}