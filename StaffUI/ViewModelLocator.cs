namespace StaffUI
{
    internal class ViewModelLocator
    {
        private static ServiceProvider _provider;
        public static void Init()
        {
            var services = new ServiceCollection();

            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<MainPageViewModel>();
            services.AddTransient<AddStaffViewModel>();
            services.AddTransient<AddProjectViewModel>();
            services.AddTransient<EditProjectViewModel>();

            services.AddSingleton<PageService>();
            services.AddSingleton<AddStaffService>();
            services.AddSingleton<AddProjectService>();

            _provider = services.BuildServiceProvider();
            foreach(var service in services) 
            {
                _provider.GetRequiredService(service.ServiceType);
            }
        }

        public MainWindowViewModel MainWindowViewModel => _provider.GetRequiredService<MainWindowViewModel>();
        public MainPageViewModel MainPageViewModel => _provider.GetRequiredService<MainPageViewModel>();
        public AddStaffViewModel AddStaffViewModel => _provider.GetRequiredService<AddStaffViewModel>();
        public AddProjectViewModel AddProjectViewModel => _provider.GetRequiredService<AddProjectViewModel>();
        public EditProjectViewModel EditProjectViewModel => _provider.GetRequiredService<EditProjectViewModel>();

    }
}
