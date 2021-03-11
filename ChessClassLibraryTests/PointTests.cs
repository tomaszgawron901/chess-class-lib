﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessClassLibrary.Tests
{
    [TestClass()]
    public class PointTests
    {
        [TestMethod()]
        public void constructor_0_arguments_corect()
        {
            Point p = new Point();
            Assert.AreEqual(p.X, 0);
            Assert.AreEqual(p.Y, 0);
        }

        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-1, 2)]
        [DataRow(-500, -500)]
        public void constructor_2_arguments_correct(int x, int y)
        {
            Point p = new Point(x, y);
            Assert.AreEqual(p.X, x);
            Assert.AreEqual(p.Y, y);
        }

        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-1, 2)]
        [DataRow(-500, -500)]
        public void equals_true(int x, int y)
        {
            Point p1 = new Point(x, y);
            Point p2 = new Point(x, y);
            Assert.IsTrue(p1.Equals(p2));
            Assert.AreNotSame(p1, p2);
        }

        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-1, 2)]
        [DataRow(-500, -500)]
        public void equals_false(int x, int y)
        {
            Point p1 = new Point(x, y);
            Point p2 = new Point(x+1, y+1);
            Assert.IsFalse(p1.Equals(p2));
            Assert.AreNotSame(p1, p2);
        }

        [TestMethod()]
        public void equals_object_true()
        {
            Assert.IsTrue(new Point().Equals((object)new Point()));
        }

        [TestMethod()]
        public void equals_object_null()
        {
            Assert.IsFalse(new Point().Equals(null));
        }


        [DataTestMethod()]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 1, 2, 3)]
        [DataRow(-1, 2, -1, 2)]
        [DataRow(-500, -500, 0, 0)]
        public void plus_method_correct(int x1, int y1, int x2, int y2)
        {
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = p1.Plus(p2);
            Assert.AreNotSame(p3, p1);
            Assert.AreNotSame(p3, p2);
            Assert.AreEqual(p3.X, x1 + x2);
            Assert.AreEqual(p3.Y, y1 + y2);
        }

        [DataTestMethod()]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 1, 2, 3)]
        [DataRow(-1, 2, -1, 2)]
        [DataRow(-500, -500, 0, 0)]
        public void minus_method_correct(int x1, int y1, int x2, int y2)
        {
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = p1.Minus(p2);
            Assert.AreNotSame(p3, p1);
            Assert.AreNotSame(p3, p2);
            Assert.AreEqual(p3.X, x1 - x2);
            Assert.AreEqual(p3.Y, y1 - y2);
        }

        [DataTestMethod()]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 1, 2, 3)]
        [DataRow(-1, 2, -1, 2)]
        [DataRow(-500, -500, 0, 0)]
        public void plus_operator_correct(int x1, int y1, int x2, int y2)
        {
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = p1 + p2;
            Assert.AreNotSame(p3, p1);
            Assert.AreNotSame(p3, p2);
            Assert.AreEqual(p3.X, x1 + x2);
            Assert.AreEqual(p3.Y, y1 + y2);
        }

        [DataTestMethod()]
        [DataRow(0, 0, 0, 0)]
        [DataRow(1, 1, 2, 3)]
        [DataRow(-1, 2, -1, 2)]
        [DataRow(-500, -500, 0, 0)]
        public void minus_operator_correct(int x1, int y1, int x2, int y2)
        {
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = p1 - p2;
            Assert.AreNotSame(p3, p1);
            Assert.AreNotSame(p3, p2);
            Assert.AreEqual(p3.X, x1 - x2);
            Assert.AreEqual(p3.Y, y1 - y2);
        }

        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-1, 2)]
        [DataRow(-500, -500)]
        public void equals_operator_correct(int x, int y)
        {
            Point p1 = new Point(x, y);
            Point p2 = new Point(x, y);
            Assert.IsTrue(p1==p2);
            Assert.AreNotSame(p1, p2);
        }

        [DataTestMethod()]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(-1, 2)]
        [DataRow(-500, -500)]
        public void not_equals_operator_correct(int x, int y)
        {
            Point p1 = new Point(x, y);
            Point p2 = new Point(x + 1, y + 1);
            Assert.IsTrue(p1!=p2);
            Assert.AreNotSame(p1, p2);
        }
    }
}