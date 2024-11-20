using CargoHub.Models;

public class MigrationService
{
    private DatabaseContext _context;
    public MigrationService(DatabaseContext context)
    {
        _context = context;
    }

    private async Task MigrateClients()
    {
        // read json addRangw() saveChanges()
    }

}