namespace StaffUI.ViewModels
{
    public class AddStaffViewModel : BindableBase
    {
        private readonly PageService _pageService;
        private readonly AddStaffService _addStaffService;
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
        public AddStaffViewModel(PageService pageService, AddStaffService addStaffService)
        {
            _pageService = pageService;
            _addStaffService = addStaffService;
        }
        public AsyncCommand AddStaffCommand => new(async () =>
        {
            await _addStaffService.AddStaffAsync(IdNumber, FirstName, LastName, MiddleName);
            _pageService.ChangePage(new MainPage());
        }, bool () =>
        {
            if (string.IsNullOrWhiteSpace(IdNumber)
                ||  IdNumber.Contains(" ")
                || !IdNumber.All(char.IsDigit))
                ErrorMessage = "Неверный формат";

            else if (string.IsNullOrWhiteSpace(FirstName)
                     ||  FirstName.Contains(" ")
                     || !FirstName.All(char.IsLetter))
                ErrorMessage = "Неверный формат";

            else if (string.IsNullOrWhiteSpace(LastName)
                     ||  LastName.Contains(" ")
                     || !LastName.All(char.IsLetter))
                ErrorMessage = "Неверный формат";

            else if (string.IsNullOrWhiteSpace(MiddleName)
                     ||  MiddleName.Contains(" ")
                     || !MiddleName.All(char.IsLetter))
                ErrorMessage = "Неверный формат";
            else
                ErrorMessage = string.Empty;

            if (ErrorMessage.Equals(string.Empty))
                return true; return false;
        });
    }
}
