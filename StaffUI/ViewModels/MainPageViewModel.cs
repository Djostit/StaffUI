namespace StaffUI.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly AddStaffService _addStaffService;
        public List<Staff> ItemsStaffs
        {
            get { return GetValue<List<Staff>>(); }
            set { SetValue(value); }
        }
        public Staff SelectedStaff
        {
            get { return GetValue<Staff>(); }
            set { SetValue(value, changedCallback: OnStaffChanged); }
        }
        private void OnStaffChanged()
        {
            Global.CurrentStaff = SelectedStaff;

            _pageService.ChangePage(new AddProjectPage());
        }
        public MainPageViewModel(PageService pageService, AddStaffService addStaffService)
        {
            _pageService = pageService;
            _addStaffService = addStaffService;
        }
        public DelegateCommand AddStaffCommand => new(() =>
        {
            _pageService.ChangePage(new AddStaffPage());
        });
        public AsyncCommand RefreshCommand => new(async () => 
        {
            ItemsStaffs = await _addStaffService.GetStaff();
        });
        
    }
}
