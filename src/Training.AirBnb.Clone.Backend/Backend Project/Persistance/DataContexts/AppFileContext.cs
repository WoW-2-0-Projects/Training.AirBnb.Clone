using Backend_Project.Domain.Entities;
using FileBaseContext.Abstractions.Models.Entity;
using FileBaseContext.Abstractions.Models.FileContext;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Context.Models.Configurations;
using FileBaseContext.Context.Models.FileContext;

namespace Backend_Project.Persistance.DataContexts;

public class AppFileContext : FileContext, IDataContext
{
    public IFileSet<EmailTemplate, Guid> EmailTemplates => Set<EmailTemplate>(nameof(EmailTemplates));
    public IFileSet<Reservation, Guid> Reservations => Set<Reservation>(nameof(Reservations));
    public IFileSet<City, Guid> Cities => Set<City>(nameof(Cities));
    public IFileSet<Country, Guid> Countries => Set<Country>(nameof(Countries));
    public IFileSet<User, Guid> Users => Set<User>(nameof(Users));
    public IFileSet<Address, Guid> Addresses => Set<Address>(nameof(Addresses));
    public IFileSet<ReservationOccupancy,Guid> ReservationOccupancies => Set<ReservationOccupancy>(nameof(ListingOccupancies));
    public IFileSet<ListingComment, Guid> ListingComments => Set<ListingComment>(nameof(ListingComments));
    public IFileSet<UserCredentials,Guid> UserCredentials => Set<UserCredentials>(nameof(UserCredentials));

    public AppFileContext(IFileContextOptions<IFileContext> fileContextOptions) : base(fileContextOptions)
    {
        OnSaveChanges += AddPrimaryKeys;
    }


    public virtual ValueTask AddPrimaryKeys(IEnumerable<IFileSetBase> fileSets)
    {
        foreach (var fileSetBase in fileSets)
        {
            if (fileSetBase is not IFileSet<IFileSetEntity<Guid>, Guid> fileSet) continue;

            foreach (var entry in fileSet.Where(entry => entry.Id == default))
                entry.Id = Guid.NewGuid();
        }

        return new ValueTask(Task.CompletedTask);
    }

    public ValueTask DisposeAsync()
    {
        return new ValueTask(Task.CompletedTask);
    }
}