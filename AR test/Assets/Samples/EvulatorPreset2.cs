using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Stores an <see cref="IEvaluator2"/> in an asset to it can be shared by many mappings.
/// </summary>
abstract class EvulatorPreset2 : ScriptableObject
{
    /// <summary>
    /// The evaluator stored in this asset.
    /// </summary>
    public abstract IEvaluator2 Evaluator { get; }
}
