using GalaSoft.MvvmLight.Command;
using TheCreationist.App.ViewModel;
using System;

namespace TheCreationist.App.Managers
{
    public class RuleManager : IDisposable
    {
        public RuleManager(MainViewModel mainViewModel)
        {
            MainViewModel = mainViewModel;

            CreateRuleCommand = new RelayCommand(
                () => CreateRule(),
                () => CanCreateRule());

            DeleteRuleCommand = new RelayCommand(
                () => DeleteRule(),
                () => CanDeleteRule());

            ProcessRuleCommand = new RelayCommand<ProjectViewModel>(
                (p) => ProcessRule(p),
                (p) => CanProcessRule(p));
        }

        public MainViewModel MainViewModel { get; private set; }

        public RelayCommand CreateRuleCommand { get; set; }

        public RelayCommand DeleteRuleCommand { get; set; }

        public RelayCommand<ProjectViewModel> ProcessRuleCommand { get; set; }

        private void CreateRule()
        {
            throw new NotImplementedException();
        }

        private bool CanCreateRule()
        {
            throw new NotImplementedException();
        }

        private void DeleteRule()
        {
            throw new NotImplementedException();
        }

        private bool CanDeleteRule()
        {
            throw new NotImplementedException();
        }

        private void ProcessRule(ProjectViewModel projectViewModel)
        {
            throw new NotImplementedException();
        }

        private bool CanProcessRule(ProjectViewModel projectViewModel)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Logger.Log.Debug("Disposing");

            MainViewModel = null;

            Logger.Log.Debug("Disposed");
        }
    }
}
