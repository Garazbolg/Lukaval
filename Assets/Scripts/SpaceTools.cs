using UnityEngine;
using System.Collections;

/*
 * Used mathematics :
 *    - Dot product
 *        Between : 2 Vectors (v1 and v2)
 *        Expression : v1.x*v2.x + v1.y*v2.y + v1.z*v2.z
 *        Output : A scalar.
 *        Properties : If above 0, the angle from v2 to v1 is between [0, 90[ U ]270, 360] degrees (or [-360, -270[ U ]-90, 0] degrees).
 *                     If 0, v1 and v2 are perpendicular (90/-270 or 270/-90 degrees).
 *                     If below 0, the angle from v2 to v1 is between ]90, 270[ degrees (or ]-270, -90[ degrees).
 *
 *    - Cross product
 *        Between : 2 Vectors (v1 and v2)
 *        Expression : (v1.y*v2.z - v1.z*v2.y, v1.z*v2.x - v1.x*v2.z, v1.x*v2.y - v1.y*v2.x)
 *        Output : For 3D vectors, a 3D vector.
 *                 For 2D vectors, a scalar. 
 *        Properties : In 3D, the vector is the rotation axis to get from v1 to v2 with the smallest rotation angle possible (less than 180 degrees).
 *                     In 2D, the scalar sign tells if v2 is on the left or right of v1 (v1 direction considered as forward).
 *                     Null (0, 0, 0) only if v1 and v2 parallel (angle between them is 0 or 180 degrees).
 *                     Its length is the area of the parallelogram determined by v1 and v2, halve it to get the area of the corresponding triangle.
 *                 
 *    - Scalar triple product
 *        Between : 3 Vectors (v1, v2 and v3)
 *        Expression : Dot(v1, Cross(v2, v3)) = Dot(Cross(v1, v2), v3)
 *        Output : A scalar.
 *        Properties : Its absolute value is the volume of the parallelepiped determined by v1, v2 and v3.
 *    
 *
 *    - References
 *        Dot product :
 *            https://betterexplained.com/articles/vector-calculus-understanding-the-dot-product/
 *            http://math.oregonstate.edu/home/programs/undergrad/CalculusQuestStudyGuides/vcalc/dotprod/dotprod.html
 *
 *        Cross product :
 *            http://math.oregonstate.edu/home/programs/undergrad/CalculusQuestStudyGuides/vcalc/crossprod/crossprod.html
 *            http://allenchou.net/2013/07/cross-product-of-2d-vectors/
 *        
 *        Scalar triple product :
 *            http://mathinsight.org/scalar_triple_product
 */

/// <summary>
/// Class holding tools to deal with space transformations and operations such as projection, rotation, etc.
/// </summary>
public class SpaceTools {
    private static Vector3 m_InfiniteVect = float.PositiveInfinity * Vector3.one;

    /// <summary>
    /// A 4x4 basic TRS matrix (no translation, no orientation, scale one).
    /// </summary>
    public static Matrix4x4 NeutralTRSMatrix
    {
        get { return Matrix4x4.TRS(Vector3.zero, Quaternion.identity, Vector3.one); }
    }

    /// <summary>
    /// An infinite vector such as (Inf, Inf, Inf).
    /// </summary>
    public static Vector3 InfiniteVector
    {
        get { return m_InfiniteVect; }
    }

    /// <summary>
    /// Returns projection of the given point on the given plane.
    /// </summary>
    /// <param name="toProject">Coordinates of the Point to project.</param>
    /// <param name="planePoint">Coordinates of any point of the plane to project on.</param>
    /// <param name="planeNormal">Coordinates of the normal vector of the plane to project on (does not have to be normalized, but can be if you want).</param>
    /// <returns>Projection point.</returns>
    public static Vector3 ProjectPointOnPlane(Vector3 toProject, Vector3 planePoint, Vector3 planeNormal)
    {
        return toProject - Vector3.Dot(toProject - planePoint, planeNormal.normalized) * planeNormal.normalized;
    }

    /// <summary>
    /// Extracts the rotation from the given 4x4 TRS Matrix as a Quaternion.
    /// </summary>
    /// <param name="m">TRS Matrix to extract the rotation from.</param>
    /// <returns>Rotation stored in the TRS Matrix.</returns>
    public static Quaternion TRSRotation(Matrix4x4 m)
    {
        return Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
    }

    /// <summary>
    /// Extracts the translation from the given 4x4 TRS Matrix as a Vector3.
    /// </summary>
    /// <param name="m">TRS Matrix to extract the translation from.</param>
    /// <returns>Translation stored in the TRS Matrix.</returns>
    public static Vector3 TRSTranslation(Matrix4x4 m)
    {
        return m.GetColumn(3);
    }

    /// <summary>
    /// Extracts the scale from the given 4x4 TRS Matrix as a Vector3.
    /// </summary>
    /// <param name="m">TRS Matrix to extract the scale from.</param>
    /// <returns>Scale stored in the TRS Matrix.</returns>
    public static Vector3 TRSScale(Matrix4x4 m)
    {
        return new Vector3(m.GetColumn(0).magnitude, m.GetColumn(1).magnitude, m.GetColumn(2).magnitude);
    }

    /// <summary>
    /// Returns the rotation to get from Origin to Target as a Quaternion.
    /// </summary>
    /// <param name="originOrientation">Start reference orientation.</param>
    /// <param name="targetOrientation">End reference orientation.</param>
    /// <returns>Rotation from Origin to Target.</returns>
    public static Quaternion RotationBetween(Quaternion originOrientation, Quaternion targetOrientation)
    {
        return Quaternion.Inverse(originOrientation) * targetOrientation;
    }

    /// <summary>
    /// Returns the translation to get from Origin to Target as a Vector3.
    /// </summary>
    /// <param name="originPosition">Start reference position.</param>
    /// <param name="targetPosition">End reference position.</param>
    /// <returns>Translation from Origin to Target.</returns>
    public static Vector3 TranslationBetween(Vector3 originPosition, Vector3 targetPosition)
    {
        return targetPosition - originPosition;
    }

    /// <summary>
    /// Returns the relative position of Target according to the plane defined by Forward and Up (-1 if on left, 1 if on right, 0 if on plane).
    /// </summary>
    /// <param name="target">Target's position.</param>
    /// <param name="forward">Forward vector of the reference object used to construct the plane.</param>
    /// <param name="up">Up vector of the reference object used to construct the plane.</param>
    /// <returns>Relative position of the Target from the plane perspective.</returns>
    public static float RelativePosition(Vector3 target, Vector3 forward, Vector3 up)
    {
        //Cross between Forward and Target build a vector coplanar to the plane defined by Forward and Right.
        //Dot between Cross and Up says if they are perpendicular or not.
        float dir = Vector3.Dot(Vector3.Cross(forward, target), up);

        return (dir > 0) ? 1 : (dir < 0) ? -1 : 0;
    }

    /// <summary>
    /// Returns a Vector3 representing the rotation defined by the given Quaternion with signed angles.
    /// </summary>
    /// <param name="q">Quaternion to convert into a rotation represented as a Vector3.</param>
    /// <returns>Vector3 representing the rotation defined by the given Quaternion.</returns>
    public static Vector3 QuaternionToEulerSigned(Quaternion q)
    {
        float sqx = q.x * q.x,
              sqy = q.y * q.y,
              sqz = q.z * q.z,
              sqw = q.w * q.w,
              correctionFactor = sqx + sqy + sqz + sqw,
              singularityCheck = q.x * q.y + q.z * q.w;

        // Singularity at North Pole
        Vector3 euler = new Vector3(0, 2f * Mathf.Atan2(q.x, q.w), Mathf.PI / 2f);
        // Singularity at South Pole
        if (singularityCheck < -0.499f * correctionFactor)
            euler *= -1;
        // No singularity
        else if (singularityCheck >= -0.499f * correctionFactor && singularityCheck <= 0.499f * correctionFactor)
        {
            euler.x = Mathf.Atan2(2f * (q.x * q.w - q.y * q.z), -sqx + sqy - sqz + sqw);
            euler.y = Mathf.Atan2(2f * (q.y * q.w - q.x * q.z), sqx - sqy - sqz + sqw);
            euler.z = Mathf.Asin(2f * singularityCheck / correctionFactor);
        }

        return euler * Mathf.Rad2Deg;
    }
}
