using MudBlazor;

namespace WebUI.Utilities.Helpers;

using Entities.Models;

public class MessageboxHelper
{
    private readonly IDialogService dialogService;
    private readonly ISnackbar snackbar;
    private DialogOptions dialogOptions;

    public MessageboxHelper(IDialogService dialogService, ISnackbar snackbar)
    {
        this.dialogService = dialogService;
        this.snackbar = snackbar;

        dialogOptions = new()
        {
            FullWidth = true,
            CloseButton = true,
            CloseOnEscapeKey = false,
            DisableBackdropClick = true,
        };
    }

    public async Task ShowDialogMessage(string title, string message)
    {
        await dialogService.ShowMessageBox(
            title: title,
             message: message,
             yesText: "Tamam", options: dialogOptions);
    }
    public async Task<bool?> ShowDialog(string title, string message)
    {
        return await dialogService.ShowMessageBox(

            title: title,
             message: message,
             yesText: "Evet", noText: "Hayır", options: dialogOptions);
    }
    public async Task<bool?> ShowDeleteDialog()
    {
        return await dialogService.ShowMessageBox(

            title: "Uyarı!",
             message: "Silmek istediğinize emin misiniz?",
             yesText: "Evet", noText: "Hayır", options: dialogOptions);
    }
    public void ShowSnackMessage(string message, Severity severity = Severity.Success)
    {
        snackbar.Add(severity: severity, message: message);
    }
    public void ShowSuccessSnackMessage()
    {
        snackbar.Add("İşlem başarılı", Severity.Success);
    }

    public BaseResponse<IEnumerable<T>> MessageBoxErrorCheckerIEnumerable<T>(BaseResponse<IEnumerable<T>> result)
        where T : new()
    {
        if (result.Success == false)
        {
            ShowSnackMessage(result.Message, Severity.Error);
            return default;
        }

        return result;
    }

    public BaseResponse<T> MessageBoxErrorChecker<T>(BaseResponse<T> result) where T : new()
    {
        if (result.Success == false)
        {
            ShowSnackMessage(result.Message, Severity.Error);
            return default;
        }

        return result;
    }
}
