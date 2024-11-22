using FluffyDisket.UI;
using FluffyDisket.UI.Inven;
using UnityEditor;

namespace FluffyDisket
{
    public static class UITest
    {
        [MenuItem("Fluffy Extension/Unit Trait Tester _F10")]
        public static void OpenInvenPopup()
        {
            if (PopupManager.ExistInstance())
            {
                UIInvenPopup.OpenPopup();
            }
        }
    }
}