
using TMPro;
using UnityEngine;

namespace UI.Extension
{
    public static class UIExtension
    {
        public static void SetUIPos_WorldToScreenPos(this RectTransform rectTransform, Vector3 originPos)
        {
            Camera cam = GetHighestPriorityCamera();

            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            Vector3 screenPosition = cam.WorldToScreenPoint(originPos);
            bool isOutsideOfCamera = (screenPosition.z < 0);// ||
                                                            //screenPosition.x < 0 || screenPosition.x > screenSize.x ||
                                                            //screenPosition.y < 0 || screenPosition.y > screenSize.y);

            if (isOutsideOfCamera)
            {
                rectTransform.anchoredPosition = new Vector2(-3000, -3000);
            }
            else
            {
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, originPos);
                Vector2 position = screenPoint - screenSize * 0.5f;
                rectTransform.anchoredPosition = position;
            }
        }

        public static Camera GetHighestPriorityCamera()
        {
            Camera[] allCameras = Camera.allCameras;
            Camera highestPriorityCamera = null;
            float maxDepth = float.MinValue;

            foreach (Camera cam in allCameras)
            {
                if (cam.depth > maxDepth)
                {
                    maxDepth = cam.depth;
                    highestPriorityCamera = cam;
                }
            }

            return highestPriorityCamera;
        }

        

        public static string SimplifyNumber(this float number)
        {
            if (number >= 1_000_000_000_000_000_000)
            {
                return $"{number / 1_000_000_000_000_000_000.0:F1}E";
            }
            else if (number >= 1_000_000_000_000_000)
            {
                return $"{number / 1_000_000_000_000_000.0:F1}P";
            }
            else if (number >= 1_000_000_000_000)
            {
                return $"{number / 1_000_000_000_000.0:F1}T";
            }
            else if (number >= 1_000_000_000)
            {
                return $"{number / 1_000_000_000.0:F1}G";
            }
            else if (number >= 1_000_000)
            {
                return $"{number / 1_000_000.0:F1}M";
            }
            else if (number >= 1_000)
            {
                return $"{number / 1_000.0:F1}k";
            }
            else
            {
                return $"{number:F0}";
            }
        }
    }


}
