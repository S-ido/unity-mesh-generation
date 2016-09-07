using UnityEngine;
using NUnit.Framework;

namespace Picadilla.Zad1 {
    public class GridTest {

        [Test]
        public void OnePointAvailable_AfterPointIsAdded() {
            //Arrange
            Grid grid = new Grid();

            //Act
            grid.AddPoint(Vector3.zero, 0.1f);

            //Assert
            Assert.That(grid.points.Count, Is.EqualTo(1));
        }

        [Test]
        public void NoPointsAvailable_AfterReset() {
            //Arrange
            Grid grid = new Grid();
            grid.AddPoint(Vector3.zero, 0.1f);

            //Act
            grid.Reset();

            //Assert
            Assert.That(grid.points.Count, Is.EqualTo(0));
        }

        [Test]
        public void NoPointsAvailable_AfterAddAndRemovePoint() {
            //Arrange
            Grid grid = new Grid();

            //Act
            grid.AddPoint(Vector3.zero, 0.1f);
            grid.RemovePoint(Vector3.zero, 1);

            //Assert
            Assert.That(grid.points.Count, Is.EqualTo(0));
        }

        [Test]
        public void NoPointsRemoved_IfDistanceIsGreaterThanRemoveDistance() {
            //Arrange
            Grid grid = new Grid();
            grid.AddPoint(-Vector3.one, 0.1f);

            //Act
            grid.RemovePoint(Vector3.one, 1);

            //Assert
            Assert.That(grid.points.Count, Is.EqualTo(1));
        }

        [Test]
        public void GridValid_AfterAddThreePoints() {
            //Arrange
            Grid grid = new Grid();

            //Act
            grid.AddPoint(Vector3.zero, 0.1f);
            grid.AddPoint(Vector3.one, 0.1f);
            grid.AddPoint(-Vector3.one, 0.1f);

            //Assert
            Assert.That(grid.IsValid());
        }

        [Test]
        public void GridInvalid_AfterCtreate() {
            //Arrange
            Grid grid = new Grid();

            //Assert
            Assert.IsFalse(grid.IsValid());
        }
    }
}