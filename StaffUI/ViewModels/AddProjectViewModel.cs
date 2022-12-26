using StaffUI.Service;

namespace StaffUI.ViewModels
{
    public class AddProjectViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly AddStaffService _addStaffService;
        private readonly AddProjectService _addProjectService;
        public List<Project> ItemsProjects
        {
            get { return GetValue<List<Project>>(); }
            set { SetValue(value); }
        }
        public Project SelectedProject
        {
            get { return GetValue<Project>(); }
            set { SetValue(value, changedCallback: OnProjectChanged); }
        }
        private void OnProjectChanged()
        {
             Global.CurrentProject = SelectedProject;
            _pageService.ChangePage(new EditProject());
        }
        public string IdNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string MiddleName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        private string ErrorMessage
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public AddProjectViewModel(PageService pageService, AddStaffService addStaffService, AddProjectService addProjectService)
        {
            _pageService = pageService;
            _addStaffService = addStaffService;
            _addProjectService = addProjectService;
            IdNumber = Global.CurrentStaff?.Id.ToString();
            FirstName = Global.CurrentStaff?.FirstName;
            LastName = Global.CurrentStaff?.LastName;
            MiddleName = Global.CurrentStaff?.MiddleName;
            ItemsProjects = _addProjectService.GetStaff();
        }
        public AsyncCommand SaveStaffCommand => new(async () =>
        {
            await _addStaffService.SaveCurrentStaff(IdNumber, FirstName, LastName, MiddleName);
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(IdNumber)
                || IdNumber.Contains(" ")
                || !IdNumber.All(char.IsDigit))
                ErrorMessage = "Неверный формат";

            else if (string.IsNullOrWhiteSpace(FirstName)
                     || FirstName.Contains(" ")
                     || !FirstName.All(char.IsLetter))
                ErrorMessage = "Неверный формат";

            else if (string.IsNullOrWhiteSpace(LastName)
                     || LastName.Contains(" ")
                     || !LastName.All(char.IsLetter))
                ErrorMessage = "Неверный формат";

            else if (string.IsNullOrWhiteSpace(MiddleName)
                     || MiddleName.Contains(" ")
                     || !MiddleName.All(char.IsLetter))
                ErrorMessage = "Неверный формат";
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;
        });
        public DelegateCommand ReturnBackCommand => new(() =>
        {
            _pageService.ChangePage(new MainPage());
        });
        public DelegateCommand AddProjectCommand => new(() => 
        {
            _pageService.ChangePage(new EditProject());
        });
        public AsyncCommand DeleteStaffCommand => new(async () => 
        {
            await _addProjectService.DeleteFromStaffProject(Global.CurrentStaff.ProjectName);
            await _addStaffService.DeleteStaff();
            _pageService.ChangePage(new MainPage());
        });
    }
}
