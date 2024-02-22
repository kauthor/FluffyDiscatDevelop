using UnityEngine;
using UnityEngine.UI;

namespace FluffyDisket.UI
{
    public class UILogSentence:MonoBehaviour
    {
        [SerializeField] private Text txtLog;

        public void SetLog(string log)
        {
            txtLog.text = log;
        }
    }
}