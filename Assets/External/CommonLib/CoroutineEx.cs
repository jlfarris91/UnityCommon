namespace CommonLib
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public static class CoroutineEx
    {
        public static IEnumerator FadeIn(CanvasGroup canvasGroup, float duration)
        {
            yield return new Wait(Fade(new[] { canvasGroup }, 0.0f, 1.0f, duration));
        }

        public static IEnumerator FadeIn(IEnumerable<CanvasGroup> canvasGroups, float duration)
        {
            yield return new Wait(Fade(canvasGroups, 0.0f, 1.0f, duration));
        }

        public static IEnumerator FadeOut(CanvasGroup canvasGroup, float duration)
        {
            yield return new Wait(Fade(new[] { canvasGroup }, 1.0f, 0.0f, duration));
        }

        public static IEnumerator FadeOut(IEnumerable<CanvasGroup> canvasGroups, float duration)
        {
            yield return new Wait(Fade(canvasGroups, duration, 1.0f, 0.0f));
        }

        private static IEnumerator Fade(IEnumerable<CanvasGroup> canvasGroups, float startAlpha, float endAlpha, float duration)
        {
            List<CanvasGroup> canvasGroupList = canvasGroups.ToList();

            canvasGroupList.Foreach(_ =>
            {
                _.alpha = startAlpha;
            });

            yield return new WaitFor(duration, t =>
            {
                canvasGroupList.Foreach(cg => cg.alpha = Mathf.Lerp(startAlpha, endAlpha, t));
            });

            canvasGroupList.Foreach(_ =>
            {
                _.alpha = endAlpha;
            });
        }
    }
}
