using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// The face capture data that can be recorded in a take and played back.
/// </summary>
[Flags]
enum FaceFlagsChannel
{
    /// <summary>
    /// The flags used to indicate no channels.
    /// </summary>
    None = 0,

    /// <summary>
    /// The channel used for the blend shape pose of the face.
    /// </summary>
    BlendShapes = 1 << 0,

    /// <summary>
    /// The channel used for the position of the head.
    /// </summary>
    HeadPosition = 1 << 1,

    /// <summary>
    /// The channel used for the orientation of the head.
    /// </summary>
    HeadRotation = 1 << 2,

    /// <summary>
    /// The channel used for the orientations of the eyes.
    /// </summary>
    Eyes = 1 << 3,

    /// <summary>
    /// The flags used to indicate all channels.
    /// </summary>
    All = ~0,
}

