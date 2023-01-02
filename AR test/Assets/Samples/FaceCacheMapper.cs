using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class FaceCacheMapper : IDisposable
{
    /// <summary>
    /// Has this cache been disposed.
    /// </summary>
    public bool Disposed { get; private set; }

    /// <summary>
    /// The finalizer that attempts to dispose the cache if it is not properly disposed.
    /// </summary>
    ~FaceCacheMapper()
    {
        Dispose(false);
    }

    /// <summary>
    /// Disposes the cache.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    void Dispose(bool disposing)
    {
        if (Disposed)
            return;

        OnDispose(disposing);

        Disposed = true;
    }

    /// <summary>
    /// Frees the resources held by the cache.
    /// </summary>
    /// <param name="disposing">True when dispose was called; false when invoked by the finalizer.</param>
    protected virtual void OnDispose(bool disposing) { }
}