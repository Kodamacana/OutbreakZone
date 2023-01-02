using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ViewerProperty
{
    /// <summary>
    /// Adds a property from a specified Component.
    /// </summary>
    /// <param name="component">The Component to register the property from.</param>
    /// <param name="propertyName">The name of the property to register.</param>
    void Register(Component component, string propertyName);

    /// <summary>
    /// Adds a property from a specified GameObject.
    /// </summary>
    /// <param name="go">The GameObject to register the property from.</param>
    /// <param name="propertyName">The name of the property to register.</param>
    void Register(GameObject go, string propertyName);
}
