using StaffUI.Models;
using System.Diagnostics;

namespace StaffUI.Service
{
    public class AddStaffService
    {
        private static List<Staff> Staffs = new();
        private const string PATH = "staff.json";
        private async static Task ReadStaffs()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            Staffs = JsonConvert.DeserializeObject<List<Staff>>(await File.ReadAllTextAsync(PATH));
        }
        private async static Task SaveStaff()
        {
            if (!File.Exists(PATH))
            {
                await File.WriteAllTextAsync(PATH, "[]");
            }
            await File.WriteAllTextAsync(PATH, JsonConvert.SerializeObject(Staffs, Formatting.Indented));
        }
        public async Task AddStaffAsync(string Id, string Firstname, string Lastname, string Middlename)
        {
            await ReadStaffs();
            Staffs.Add(new Staff
            {
                Id = int.Parse(Id),
                FirstName = Firstname,
                LastName = Lastname,
                MiddleName = Middlename,
                ProjectName = new List<int>(),
            });
            await SaveStaff();
        }
        public async Task<List<Staff>> GetStaff()
        {
            await ReadStaffs();
            return Staffs;
        }
        public async Task SaveCurrentStaff(string IdNumber, string FirstName, string LastName, string MiddleName)
        {
            if (Global.CurrentStaff == null)
                return;
            int index = Staffs.FindIndex(s => s.Equals(Global.CurrentStaff));

            Global.CurrentStaff = new Staff
            {
                Id = int.Parse(IdNumber),
                FirstName = FirstName,
                LastName = LastName,
                MiddleName = MiddleName,
                ProjectName = Global.CurrentStaff.ProjectName,
            };
            
            Staffs[index] = Global.CurrentStaff;
            await SaveStaff();
        }
        public async Task AddProjectCurrnetStaff(int Id)
        {
            if (Global.CurrentStaff == null)
                return;

            int index = Staffs.FindIndex(s => s.Equals(Global.CurrentStaff));
            if (Global.CurrentStaff.ProjectName == null)
                Global.CurrentStaff.ProjectName = new List<int> { Id };
            else
                Global.CurrentStaff.ProjectName.Add(Id);

            Staffs[index] = Global.CurrentStaff;
            await SaveStaff();
        }

        public async Task DeleteProjectFromStaff()
        {
            await ReadStaffs();

            Global.CurrentStaff.ProjectName.RemoveAt(Global.CurrentStaff.ProjectName.FindIndex(p => p.Equals(Global.CurrentProject.Id)));

            int index = Staffs.FindIndex(s => s.Id.Equals(Global.CurrentStaff.Id));
            Debug.WriteLine(index);
            Staffs[index] = Global.CurrentStaff;
            await SaveStaff();
            //await SaveCurrentStaff(Global.CurrentStaff.Id.ToString(), Global.CurrentStaff.FirstName, Global.CurrentStaff.LastName, Global.CurrentStaff.MiddleName);
        }

        public async Task DeleteStaff()
        {
            await ReadStaffs();

            Staffs.RemoveAt(Staffs.FindIndex(p => p.Id.Equals(Global.CurrentStaff.Id)));

            await SaveStaff();
        }
    }
}
