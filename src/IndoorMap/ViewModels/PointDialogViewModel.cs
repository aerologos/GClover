using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IndoorMap.ViewModels.Base;
using IndoorMap.Services.Abstract;
using NLog;
using Extype;
using Prism.Commands;
using Prism.Dialogs;
using DomainModel;


namespace IndoorMap.ViewModels;

public class PointDialogViewModel : DialogViewModelBase
{
    private readonly Logger _logger = LogManager.GetLogger(nameof(PointDialogViewModel));
    private readonly INotificationService _notificationService;

    /// <summary>
    /// Gets the title of the dialog window.
    /// </summary>
    public override string Title => "Полетная высота";

    private float _altitude = 2;
    /// <summary>
    /// Gets or sets the altitude of the point.
    /// </summary>
    public float Altitude
    {
        get => _altitude;
        set => SetProperty(ref _altitude, value);
    }

    /// <summary>
    /// Gets the command to save the data.
    /// </summary>
    public DelegateCommand SaveCommand { get; }

    /// <summary>
    /// Instantiates the new <see cref="ConnectionViewModel"/> object.
    /// </summary>
    /// <param name="notificationService">The user notification service.</param>
    public PointDialogViewModel(
        INotificationService notificationService)
    {
        _notificationService = notificationService.ThrowIfNull(nameof(notificationService));

        SaveCommand = new DelegateCommand(Save, CanExecuteSaveCommand())
            .ObservesProperty(() => Altitude);
    }

    private ArucoMarker _editedMarker;
    public override void OnDialogOpened(IDialogParameters parameters)
    {
        _editedMarker = parameters[nameof(ArucoMarker)] as ArucoMarker;
        base.OnDialogOpened(parameters);
    }

    private async void Save()
    {
        try
        {
            _editedMarker.Altitude = Altitude;
            var dialogParameters = new DialogParameters
            {
                { nameof(ArucoMarker), _editedMarker } 
            };

            RaiseRequestClose(new DialogResult(ButtonResult.OK) { Parameters =  dialogParameters });
        }
        catch (Exception ex)
        {
            await _notificationService.NotifyAboutErrorAsync("Что-то не так. Проверьте логи.");
            _logger.Error(ex);
        }
    }

    private Func<bool> CanExecuteSaveCommand()
    {
        return () => Altitude > 0;
    }
}
