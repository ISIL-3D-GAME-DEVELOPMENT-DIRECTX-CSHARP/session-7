﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;

namespace Sesion2_Lab01 {
    public class SimpleMatrix {

        private static Matrix identity = new Matrix(1f, 0f, 0f, 0f,
                                                    0f, 1f, 0f, 0f,
                                                    0f, 0f, 1f, 0f,
                                                    0f, 0f, 0f, 1f);

        public static Matrix Identity { get { return identity; } }

        public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector) {
            Vector3 NVector3_1 = Vector3.Normalize(cameraPosition - cameraTarget);
            Vector3 NVector3_2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, NVector3_1));
            Vector3 vector1 = Vector3.Cross(NVector3_1, NVector3_2);
            Matrix NMatrix;
            NMatrix.M11 = NVector3_2.X;
            NMatrix.M12 = vector1.X;
            NMatrix.M13 = NVector3_1.X;
            NMatrix.M14 = 0.0f;
            NMatrix.M21 = NVector3_2.Y;
            NMatrix.M22 = vector1.Y;
            NMatrix.M23 = NVector3_1.Y;
            NMatrix.M24 = 0.0f;
            NMatrix.M31 = NVector3_2.Z;
            NMatrix.M32 = vector1.Z;
            NMatrix.M33 = NVector3_1.Z;
            NMatrix.M34 = 0.0f;
            NMatrix.M41 = -Vector3.Dot(NVector3_2, cameraPosition);
            NMatrix.M42 = -Vector3.Dot(vector1, cameraPosition);
            NMatrix.M43 = -Vector3.Dot(NVector3_1, cameraPosition);
            NMatrix.M44 = 1f;
            return NMatrix;
        }

        public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane) {
            Matrix matrix;
            matrix.M11 = (float)(2.0 / ((double)right - (double)left));
            matrix.M12 = 0.0f;
            matrix.M13 = 0.0f;
            matrix.M14 = 0.0f;
            matrix.M21 = 0.0f;
            matrix.M22 = (float)(2.0 / ((double)top - (double)bottom));
            matrix.M23 = 0.0f;
            matrix.M24 = 0.0f;
            matrix.M31 = 0.0f;
            matrix.M32 = 0.0f;
            matrix.M33 = (float)(1.0 / ((double)zNearPlane - (double)zFarPlane));
            matrix.M34 = 0.0f;
            matrix.M41 = (float)(((double)left + (double)right) / ((double)left - (double)right));
            matrix.M42 = (float)(((double)top + (double)bottom) / ((double)bottom - (double)top));
            matrix.M43 = (float)((double)zNearPlane / ((double)zNearPlane - (double)zFarPlane));
            matrix.M44 = 1.0f;
            return matrix;
        }

        public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance) {
            Matrix NMatrix;

            if ((fieldOfView <= 0f) || (fieldOfView >= 3.141593f)) {
                throw new ArgumentException("fieldOfView <= 0 O >= PI");
            }

            if (nearPlaneDistance <= 0f) {
                throw new ArgumentException("nearPlaneDistance <= 0");
            }

            if (farPlaneDistance <= 0f) {
                throw new ArgumentException("farPlaneDistance <= 0");
            }

            if (nearPlaneDistance >= farPlaneDistance) {
                throw new ArgumentException("nearPlaneDistance >= farPlaneDistance");
            }

            float num = 1f / ((float)Math.Tan((double)(fieldOfView * 0.5f)));
            float num9 = num / aspectRatio;
            NMatrix.M11 = num9;
            NMatrix.M12 = NMatrix.M13 = NMatrix.M14 = 0f;
            NMatrix.M22 = num;
            NMatrix.M21 = NMatrix.M23 = NMatrix.M24 = 0f;
            NMatrix.M31 = NMatrix.M32 = 0f;
            NMatrix.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            NMatrix.M34 = -1f;
            NMatrix.M41 = NMatrix.M42 = NMatrix.M44 = 0f;
            NMatrix.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            return NMatrix;
        }
    }
}
