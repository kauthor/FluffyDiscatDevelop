

namespace FluffyDisket
{
    public enum Contents
    {
        Lobby=0,
        Field=1
    }
    public class GameManager : CustomSingleton<GameManager>
    {
        private Contents currentContents;
        public Contents CurrentContents => currentContents;

        private bool isAuto = false;

        public bool SetAuto(bool isauto) => isAuto = isauto;
        public bool IsAuto => isAuto;
    }
}