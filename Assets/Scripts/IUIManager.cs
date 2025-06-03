using System;

public interface IUIManager 
{
    public event EventHandler<OnUIManagerEventArgs> OnUIManager;
    public class OnUIManagerEventArgs : EventArgs
    {
        public float uiBar;
        public float timer;
        public float battleTimer;
        public float playerHealth;
        public float enemyHealth;
        
    }


}
