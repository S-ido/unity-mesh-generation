using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class Grid {

	public List<Point> points { get; set; }

	private FileManager fileManager;
	private string savePath;

	public Grid(string path) {
		points = new List<Point>();
		savePath = path;
		fileManager = new FileManager();
	}

	public void AddPoint(Vector3 position, float minDistance) {
		if (points.Count > 0) {
			Point point = GetClosestPoint(position);
			if (CheckDistance(position, point, minDistance))
				return;
		}

		points.Add(new Point(position.x, position.y, 0));
	}

	public void RemovePoint(Vector3 position, float removeDistance) {
		if (points.Count > 0) {
			Point point = GetClosestPoint(position);
			if (CheckDistance(position, point, removeDistance)) {
				points.Remove(point);
			}
		}
	}

	public void Save() {
		fileManager.Save(savePath, points);
	}

	public void Load() {
		List<Point> data = (List<Point>)fileManager.Load(savePath);
		if (data != null)
			points = data;
	}

	public void Reset() {
		points.Clear();
		Save();
	}

	public Point GetClosestPoint(Vector3 newPoint) {
		var closestPoint = points.Select(p => new { point = p, distance = GetDistance(new Vector3(p.x, p.y, p.z), newPoint) })
			.Aggregate((p1, p2) => p1.distance < p2.distance ? p1 : p2).point;

		return closestPoint;
	}

	public bool CheckDistance(Vector3 mousePoint, Point closestPoint, float minDistance) {
		mousePoint.z = 0;
		return GetDistance(new Vector3(closestPoint.x, closestPoint.y, closestPoint.z), mousePoint) < minDistance;
	}

	private float GetDistance(Vector3 p1, Vector3 p2) {
		return Vector3.Distance(p1, p2);
	}

	public bool IsValid() {
		return points.Count > 2;
	}
}
