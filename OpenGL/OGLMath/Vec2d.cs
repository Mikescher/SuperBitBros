﻿using OpenTK;
using System;

namespace SuperBitBros.OpenGL.OGLMath {
    class Vec2d {
        public static Vec2d Zero { get { return new Vec2d(); } private set { } }

        public double X;
        public double Y;

        public Vec2d() {
            X = 0;
            Y = 0;
        }

        public Vec2d(double pX, double pY) {
            X = pX;
            Y = pY;
        }

        public Vec2d(Vec2d v) {
            X = v.X;
            Y = v.Y;
        }

        #region Operators

        public static implicit operator Vector2d(Vec2d instance) {
            return new Vector2d(instance.X, instance.Y);
        }

        public static explicit operator Vec2i(Vec2d instance) {
            return new Vec2i((int)instance.X, (int)instance.Y);
        }

        public static Vec2d operator +(Vec2d v1, Vec2d v2) {
            return new Vec2d(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2d operator +(Vec2d v1, double v2) {
            return new Vec2d(v1.X + v2, v1.Y + v2);
        }

        public static Vec2d operator -(Vec2d v1, Vec2d v2) {
            return new Vec2d(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vec2d operator -(Vec2d v1, double v2) {
            return new Vec2d(v1.X - v2, v1.Y - v2);
        }

        public static Vec2d operator *(Vec2d v1, Vec2d v2) {
            return new Vec2d(v1.X * v2.X, v1.Y * v2.Y);
        }

        public static Vec2d operator *(Vec2d v1, double v2) {
            return new Vec2d(v1.X * v2, v1.Y * v2);
        }

        public static Vec2d operator /(Vec2d v1, Vec2d v2) {
            return new Vec2d(v1.X / v2.X, v1.Y / v2.Y);
        }

        public static Vec2d operator /(Vec2d v1, double v2) {
            return new Vec2d(v1.X / v2, v1.Y / v2);
        }

        public static Vec2d operator -(Vec2d v) {
            return new Vec2d(-v.X, -v.Y);
        }

        public static bool operator ==(Vec2d mySizeA, Vec2d mySizeB) {
            return (mySizeA.X == mySizeB.X &&
                    mySizeA.Y == mySizeB.Y);
        }
        public static bool operator !=(Vec2d mySizeA, Vec2d mySizeB) {
            return !(mySizeA == mySizeB);
        }
        public override bool Equals(object obj) {
            if (obj is Vec2d)
                return this == (Vec2d)obj;
            return false;
        }
        public override int GetHashCode() {
            return (X * 1024 + Y).GetHashCode();
        }

        #endregion

        public double GetLength() {
            return Math.Sqrt(X * X + Y * Y);
        }

        public bool isZero() {
            return X == 0 && Y == 0;
        }

        public void Normalize() {
            if (!isZero()) {
                double w = GetLength();
                X = X / w;
                Y = Y / w;
            }
        }

        public void SetLength(double len) {
            Normalize();

            X = X * len;
            Y = Y * len;
        }

        public void Set(double px, double py) {
            X = px;
            Y = py;
        }
    }
}
