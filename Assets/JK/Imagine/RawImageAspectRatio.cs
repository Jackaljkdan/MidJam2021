using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JK.Imagine
{
    /// <summary>
    /// https://forum.unity.com/threads/code-snippet-size-rawimage-to-parent-keep-aspect-ratio.381616/#post-2728682
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(RawImage))]
    public class RawImageAspectRatio : MonoBehaviour
    {
        #region Inspector Fields

        public float padding = 0;

        public bool isParentMinSize = false;

        #endregion

        /// <summary>
        /// Fits the raw image in the parent, filling it vertically or horizontally or both if possible
        /// </summary>
        public void Fit()
        {
            RawImage image = GetComponent<RawImage>();

            float w = 0, h = 0;
            RectTransform parentRt = transform.parent.GetComponent<RectTransform>();
            RectTransform rt = GetComponent<RectTransform>();

            // check if there is something to do
            if (image.texture != null && parentRt != null)
            {
                float multPadding = 1 - padding;
                float ratio = image.texture.width / (float)image.texture.height;
                var bounds = new Rect(0, 0, parentRt.rect.width, parentRt.rect.height);

                if (!isParentMinSize)
                {
                    if (Mathf.RoundToInt(rt.eulerAngles.z) % 180 == 90)
                    {
                        //Invert the bounds if the image is rotated
                        bounds.size = new Vector2(bounds.height, bounds.width);
                    }
                    //Size by height first
                    h = bounds.height * multPadding;
                    w = h * ratio;
                    if (w > bounds.width * multPadding)
                    { //If it doesn't fit, fallback to width;
                        w = bounds.width * multPadding;
                        h = w / ratio;
                    }
                }
                else
                {
                    if (bounds.width < bounds.height)
                    {
                        w = bounds.width;
                        h = w / ratio;
                    }
                    else
                    {
                        h = bounds.height;
                        w = h * ratio;
                    }
                }
            }
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
            rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        }
    }
}