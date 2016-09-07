using System;
using UnityEngine;

[Serializable]
public class Point {
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
	public int index { get; set; }

	public Point() { }

	public Point(float x, float y, float z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public Vector2 ToVector2() {
		return new Vector2(x, y);
	}

	public Vector3 ToVector3() {
		return new Vector3(x, y, z);
	}

	public override string ToString() {
		return "Index: " + index + "   X: " + x + " Y: " + y;
	}
}