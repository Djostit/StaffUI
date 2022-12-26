namespace StaffUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly PageService _pageService;
        public Page? PageSource 
        {
            get { return GetValue<Page>(); }
            set { SetValue(value); }
        }
        public MainWindowViewModel(PageService pageService)
        {
            _pageService = pageService;

            _pageService.OnPageChanged += (page) => PageSource = page;

            _pageService.ChangePage(new MainPage());
        }
    }
}
