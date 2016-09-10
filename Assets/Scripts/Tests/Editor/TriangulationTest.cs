using NUnit.Framework;
using UnityEngine;

namespace Picadilla.Zad1 {
    public class TriangulationTest {
        [Test]
        public void PointsSorted_AfterTriangulation() {
            //Arrange
            Grid grid = new Grid(Application.dataPath + "/grid.dat");
            grid.AddPoint(new Vector3(1, 0, 0), 0.1f);
            grid.AddPoint(new Vector3(0, 1, 0), 0.1f);
            grid.AddPoint(new Vector3(-1, 0, 0), 0.1f);
            Triangulation triangulation = new Triangulation(grid.points);

            //Act
            triangulation.Triangulate(false);

            //Assert
            Assert.That(triangulation.points[0].x < triangulation.points[2].x);
        }

        [Test]
        public void PointsIndexed_AfterTriangulation() {
            //Arrange
            Grid grid = new Grid(Application.dataPath + "/grid.dat");
            grid.AddPoint(new Vector3(1, 0, 0), 0.1f);
            grid.AddPoint(new Vector3(0, 1, 0), 0.1f);
            grid.AddPoint(new Vector3(-1, 0, 0), 0.1f);
            Triangulation triangulation = new Triangulation(grid.points);

            //Act
            triangulation.Triangulate(false);

            //Assert
            Assert.That(triangulation.points[2].index == 2);
        }

        [Test]
        public void TrianglesCreated_AfterTriangulation() {
            //Arrange
            Grid grid = new Grid(Application.dataPath + "/grid.dat");
            grid.AddPoint(new Vector3(1, 0, 0), 0.1f);
            grid.AddPoint(new Vector3(0, 1, 0), 0.1f);
            grid.AddPoint(new Vector3(-1, 0, 0), 0.1f);
            Triangulation triangulation = new Triangulation(grid.points);

            //Act
            triangulation.Triangulate(false);

            //Assert
            Assert.That(triangulation.triangles.Count > 0);
        }

        [Test]
        public void IndicesCreated_AfterTriangulation() {
            //Arrange
            Grid grid = new Grid(Application.dataPath + "/grid.dat");
            grid.AddPoint(new Vector3(1, 0, 0), 0.1f);
            grid.AddPoint(new Vector3(0, 1, 0), 0.1f);
            grid.AddPoint(new Vector3(-1, 0, 0), 0.1f);
            Triangulation triangulation = new Triangulation(grid.points);

            //Act
            triangulation.Triangulate(false);

            //Assert
            Assert.That(triangulation.indices.Count > 0);
        }

        [Test]
        public void RemovingUnwantedPoints_AfterConvexHull() {
            //Arrange
            Grid grid = new Grid(Application.dataPath + "/grid.dat");
            grid.AddPoint(new Vector3(1, 0, 0), 0.1f);
            grid.AddPoint(new Vector3(0, 1, 0), 0.1f);
            grid.AddPoint(new Vector3(-1, -1, 0), 0.1f);
            grid.AddPoint(new Vector3(0, 0, 0), 0.1f);
            Triangulation triangulation = new Triangulation(grid.points);

            //Act
            triangulation.Triangulate(true);

            //Assert
            Assert.That(triangulation.points.Count == 3);
        }
    }
}