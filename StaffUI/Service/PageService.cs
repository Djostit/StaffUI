namespace StaffUI.Service
{
    public class PageService
    {
        public event Action<Page>? OnPageChanged;
        public void ChangePage(Page page) => OnPageChanged?.Invoke(page);
    }
}
