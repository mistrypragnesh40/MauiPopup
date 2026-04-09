namespace MauiPopup
{
    public class PopupDismissedEventArgs : EventArgs
    {
        public object Result { get; }

        public PopupDismissedEventArgs(object result)
        {
            Result = result;
        }
    }
}
