using ReactiveUI;

namespace Gymnasiearbete.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase _content;
        public ViewModelBase Content
        {
            get => _content;
            private set => this.RaiseAndSetIfChanged(ref _content, value);
        }
        public MainWindowViewModel()
        {
            Content = new MainViewModel();
        }
    }
}
