/// <summary>
/// Takes the value of a <see cref="FaceShapeBlend"/> from a pose and computes the
/// influence for a blend shape on a skinned mesh.
/// </summary>
interface IEvaluator2 : IDrawable2
{
    /// <summary>
    /// Compute the skinned mesh blend shape influence.
    /// </summary>
    /// <param name="value">The normalized value of the face blend shape.</param>
    /// <returns>The skinned mesh blend shape influence.</returns>
    float Evaluate(float value);
}