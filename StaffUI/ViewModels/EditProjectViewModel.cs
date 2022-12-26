namespace StaffUI.ViewModels
{
    public class EditProjectViewModel : BindableBase
    {
        public string NameProject
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string StartDate
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string EndDate
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public Visibility IsVisible
        {
            get { return GetValue<Visibility>(); }
            set { SetValue(value); }
        }
        private readonly PageService _pageService;
        private readonly AddProjectService _addProjectService;
        private readonly AddStaffService _addStaffService;
        public EditProjectViewModel(PageService pageService, AddProjectService addProjectService, AddStaffService addStaffService)
        {
            _pageService = pageService;
            _addProjectService = addProjectService;
            _addStaffService = addStaffService;

            NameProject = Global.CurrentProject?.Name;
            StartDate = Global.CurrentProject?.StartDate;
            EndDate = Global.CurrentProject?.EndDate;

            if (Global.CurrentProject == null)
            {
                IsVisible = Visibility.Collapsed;
            }
            else
            {
                IsVisible = Visibility.Visible;
            }
        }
        public AsyncCommand SaveProjectCommand => new(async () => 
        {
            if(Global.CurrentProject != null) 
            {
                await _addProjectService.SaveCurrentProject(NameProject, StartDate, EndDate);
            }
            else
            {
                await _addStaffService.AddProjectCurrnetStaff(await _addProjectService.AddProjectAsync(NameProject, StartDate, EndDate));
            }
            _pageService.ChangePage(new AddProjectPage());
            Global.CurrentProject = null;
        });
        public DelegateCommand ReturnBackCommand => new(() => 
        {
            Global.CurrentProject = null;
            _pageService.ChangePage(new AddProjectPage());
        });
        public AsyncCommand DeleteProjectCommand => new(async () =>
        {
            await _addStaffService.DeleteProjectFromStaff();
            await _addProjectService.DeleteProject(Global.CurrentProject);
            _pageService.ChangePage(new AddProjectPage());
        });
    }
}
