/*
The MIT License (MIT)

Copyright (c) 2016 Jonathan Parham

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the “Software”), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using UnityEngine;
using System;
using System.Collections;

public abstract class Tweener : MonoBehaviour
{
	#region Properties
	public static float DefaultDuration = 1f;
	public static Func<float, float, float, float> DefaultEquation = EasingEquations.EaseInOutQuad;

	public EasingControl easingControl;
	public bool destroyOnComplete = true;
	#endregion

	#region MonoBehaviour
	protected virtual void Awake ()
	{
		easingControl = gameObject.AddComponent<EasingControl>();
	}

	protected virtual void OnEnable ()
	{
		easingControl.updateEvent += OnUpdate;
		easingControl.completedEvent += OnComplete;
	}

	protected virtual void OnDisable ()
	{
		easingControl.updateEvent -= OnUpdate;
		easingControl.completedEvent -= OnComplete;
	}

	protected virtual void OnDestroy ()
	{
		if (easingControl != null)
			Destroy(easingControl);
	}
	#endregion

	#region Event Handlers
	protected abstract void OnUpdate (object sender, EventArgs e);

	protected virtual void OnComplete (object sender, EventArgs e)
	{
		if (destroyOnComplete)
			Destroy(this);
	}
	#endregion
}
