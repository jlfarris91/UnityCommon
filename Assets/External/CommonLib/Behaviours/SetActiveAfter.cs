using System.Collections;
using UnityEngine;

namespace CommonLib.Behaviours
{
    /// <summary>
    /// Sets a GameObject active after waiting a given number of seconds.
    /// </summary>
    public class SetActiveAfter : MonoBehaviour
    {
        public void SetActive(float seconds, bool active)
        {
            this.StopAllCoroutines();
            this.StartCoroutine(this.SetActiveAfterCoroutine(seconds, active));
        }

        private IEnumerator SetActiveAfterCoroutine(float seconds, bool active)
        {
            yield return new WaitForSeconds(seconds);
            this.gameObject.SetActive(active);
        }
    }
}