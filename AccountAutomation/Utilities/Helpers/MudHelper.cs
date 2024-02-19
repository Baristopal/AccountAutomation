using MudBlazor;

namespace WebUI.Utilities.Helpers;

public static class MudHelper
{
    public static class MudDialogHelper
    {
        public static DialogOptions DialogOptionsLarge => new()
        { CloseButton = true, CloseOnEscapeKey = false, FullScreen = false, FullWidth = true, MaxWidth = MaxWidth.ExtraLarge, Position = DialogPosition.TopCenter, DisableBackdropClick = false, NoHeader = false };
        public static DialogOptions DialogOptionsMedium => new()
        { CloseButton = true, CloseOnEscapeKey = false, FullScreen = false, FullWidth = true, MaxWidth = MaxWidth.Large, Position = DialogPosition.TopCenter, DisableBackdropClick = false, NoHeader = false };

        public static DialogOptions DialogOptionsSmall => new()
        { CloseButton = true, CloseOnEscapeKey = false, FullScreen = false, FullWidth = true, MaxWidth = MaxWidth.Medium, Position = DialogPosition.TopCenter, DisableBackdropClick = false, NoHeader = false };
        public static DialogOptions DialogOptionsExtraSmall => new()
        { CloseButton = true, CloseOnEscapeKey = false, FullScreen = false, FullWidth = true, MaxWidth = MaxWidth.Small, Position = DialogPosition.TopCenter, DisableBackdropClick = false, NoHeader = false };
    }
}
