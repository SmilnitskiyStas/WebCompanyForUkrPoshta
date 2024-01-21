using WebCompany.Repositiories;
using WebCompany.Repositiories.IRepository;
using WebCompany.Repositories;
using WebCompany.Repositories.IRepository;

namespace WebCompany
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddTransient<IAddressRepository, AddressRepository>(provider => new AddressRepository(connectionString));
            builder.Services.AddTransient<ICityRepository, CityRepository>(provider => new CityRepository(connectionString));
            builder.Services.AddTransient<ICompanyRepository, CompanyRepository>(provider => new CompanyRepository(connectionString));
            builder.Services.AddTransient<ICountryRepository, CountryRepository>(provider => new CountryRepository(connectionString));
            builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>(provider => new DepartmentRepository(connectionString));
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>(provider => new EmployeeRepository(connectionString));
            builder.Services.AddTransient<IJobRepository, JobRepository>(provider => new JobRepository(connectionString));
            builder.Services.AddTransient<IPhoneNmbRepository, PhoneNmbRepository>(provider => new PhoneNmbRepository(connectionString));
            builder.Services.AddTransient<IFilterEmployeeRepository, FilterEmployeeRepository>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
