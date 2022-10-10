namespace Assets.Scripts.UI
{
    public class PopUp : BaseWindow 
    {
        private void Awake()
        {
            _closeWindowButton.onClick.AddListener(CloseWindow);
        }
        public override void OpenWindow()
        {
            base.OpenWindow();
        }

        public override void CloseWindow()
        {
            base.CloseWindow();
        }
    }
}
