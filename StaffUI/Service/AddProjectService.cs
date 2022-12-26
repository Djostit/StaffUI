using StaffUI.Models;

namespace StaffUI.Service
{
    public class AddProjectService
    {
        private static List<Project> Projects = new();
        private const string PATH = "project.json";
        private async static Task ReadProjects()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            Projects = JsonConvert.DeserializeObject<List<Project>>(await File.ReadAllTextAsync(PATH));
        }
        private async static Task SaveProject()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            await File.WriteAllTextAsync(PATH, JsonConvert.SerializeObject(Projects, Formatting.Indented));
        }
        public async Task<int> AddProjectAsync(string NameProject, string StartDate, string EndDate)
        {
            await ReadProjects();
            int maxcount = Projects.Count + 1;
            Projects.Add(new Project
            {
                Id = maxcount,
                Name = NameProject,
                EndDate = EndDate,
                StartDate = StartDate,
            });
            await SaveProject();
            return maxcount;
        }
        public List<Project> GetStaff()
        {
            ReadProjects().GetAwaiter();
            List<Project> a = new();
            if (Global.CurrentStaff == null
                || Global.CurrentStaff.ProjectName == null)
                return a;
            for (int i = 0; i < Projects.Count; i++)
            {
                var proj = Global.CurrentStaff.ProjectName.SingleOrDefault(p => p.Equals(Projects[i].Id));
                if (proj != 0)
                {
                    a.Add(Projects[i]);
                }
            }
            return a;
        }
        public async Task SaveCurrentProject(string NameProject, string StartDate, string EndDate)
        {
            if (Global.CurrentProject == null)
                return;

            int index = Projects.FindIndex(s => s.Id.Equals(Global.CurrentProject.Id));
            Debug.WriteLine(index);

            Global.CurrentProject = new Project
            {
                Name = NameProject,
                StartDate = StartDate,
                EndDate = EndDate,
                Id = Global.CurrentProject.Id,
            };

            Projects[index] = Global.CurrentProject;
            await SaveProject();
        }
        public async Task DeleteProject(Project project)
        {
            await ReadProjects();

            Projects.RemoveAt(Projects.FindIndex(p => p.Id.Equals(project.Id)));

            await SaveProject();
        }
        public async Task DeleteFromStaffProject(List<int> project)
        {
            await ReadProjects();

            for(int i = 0; i < project.Count; i++) 
            {
                Projects.RemoveAt(Projects.FindIndex(p => p.Id.Equals(project[i])));
            }

            await SaveProject();
        }
    }
}
