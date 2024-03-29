﻿using System.Diagnostics;
using System.Windows.Input;
using WgServerforWindows.Properties;

namespace WgServerforWindows.Models
{
    public class OpenClientConfigDirectorySubCommand : PrerequisiteItem
    {
        public OpenClientConfigDirectorySubCommand() : base
        (
            title: string.Empty,
            successMessage: string.Empty,
            errorMessage: string.Empty,
            resolveText: string.Empty,
            configureText: Resources.OpenClientConfigDirectoryConfigureText
        )
        {
            AppSettings.Instance.PropertyChanged += (_, args) =>
            {
                if (args.PropertyName == nameof(AppSettings.Instance.CustomClientConfigDirectory))
                {
                    RaisePropertyChanged(nameof(SuccessMessage));
                    RaisePropertyChanged(nameof(CanConfigure));
                }
            };
        }

        #region PrerequisiteItem members

        public override string SuccessMessage
        {
            get => string.Format(Resources.OpenClientConfigDirectorySuccessMessage, ClientConfigurationsPrerequisite.ClientConfigDirectory);
            set { }
        }

        public override void Configure()
        {
            if (CanConfigure)
            {
                WaitCursor.SetOverrideCursor(Cursors.Wait);

                Process.Start(new ProcessStartInfo
                {
                    FileName = ClientConfigurationsPrerequisite.ClientConfigDirectory,
                    UseShellExecute = true
                });

                WaitCursor.SetOverrideCursor(null);
            }
        }

        #endregion
    }
}
