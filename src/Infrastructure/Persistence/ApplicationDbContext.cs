using Tawala.Domain.Common;
using Tawala.Infrastructure.Common.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Tawala.Domain.Entities.Identity;
using Tawala.Domain.Entities;
using Tawala.Domain.Entities.Settings.OptionSetsEntities;
using Tawala.Domain.Entities.Settings;
using Tawala.Domain.Entities.Notifications;
using Tawala.Domain.Entities.Settings.AdminSettings;
using Tawala.Domain.Entities.Settings.ServiceProvider;
using Tawala.Domain.Entities.Settings.Other;
using Tawala.Domain.Entities.menu;
using Tawala.Domain.Entities.CategoryEni;
using Tawala.Domain.Entities.TawalhDesignWithConfig;
using Tawala.Domain.Entities.Reservations;
using System.Reflection.Emit;
using Tawala.Domain.Entities.Settings.EvaluationEN;

namespace Tawala.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;
        private readonly IDomainEventService _domainEventService;
        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService,
            IDomainEventService domainEventService,
            IDateTime dateTime) : base(options)
        {
            _currentUserService = currentUserService;
            _domainEventService = domainEventService;
            _dateTime = dateTime;
        }


        public DbSet<AppAttachment> AppAttachment { get; set; }
        public DbSet<OptionSetItem> OptionSetItem { get; set; }
        public DbSet<OptionSet> OptionSet { get; set; }
        public DbSet<Settings> Settings { get; set; }

        //-----------------Admin
        public DbSet<AdminNotifications> AdminNotifications { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Device> Device { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<OptionSet> OptionSets { get; set; }
        public DbSet<OptionSetItem> OptionSetItems { get; set; }

        //------------------
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<OpenDayes> OpenDayes { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<Complain> Complains { get; set; }
        //---------------menu
        public DbSet<Items> Items { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuItems> MenuItems { get; set; }
        //--------------------bui
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<RoomTable> RoomTables { get; set; }
        public DbSet<RestOccasions> RestOccasions { get; set; }
        public DbSet<OccasionsReservation> OccasionsReservation { get; set; }
        public DbSet<RestCommissionConfig> RestCommissionConfigs { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<ClientNotification> ClientNotifications { get; set; }



        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;

                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            await DispatchEvents();

            return result;
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;

                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            var result = base.SaveChanges(true);

            //DispatchEvents();

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

        }

        private async Task DispatchEvents()
        {
            while (true)
            {
                var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                    .Select(x => x.Entity.DomainEvents)
                    .SelectMany(x => x)
                    .Where(domainEvent => !domainEvent.IsPublished)
                    .FirstOrDefault();
                if (domainEventEntity == null) break;

                domainEventEntity.IsPublished = true;
                await _domainEventService.Publish(domainEventEntity);
            }
        }


    }
}
