namespace MauiPopup
{
    public enum PopupAnimation
    {
        /// <summary>
        /// No animation — popup appears/disappears instantly.
        /// </summary>
        None,

        /// <summary>
        /// Smooth fade in/out (default).
        /// </summary>
        Fade,

        /// <summary>
        /// Scales up from center on appear, scales down on dismiss.
        /// </summary>
        Scale,

        /// <summary>
        /// Slides up from the bottom on appear, slides down on dismiss.
        /// </summary>
        SlideUp,

        /// <summary>
        /// Slides down from the top on appear, slides up on dismiss.
        /// </summary>
        SlideDown
    }
}
