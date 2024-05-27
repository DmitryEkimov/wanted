namespace Wanted.Persistence.Configurations.Seeds;

using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class CompanySeeding : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder) =>
        builder.HasData(
            new Company(Guid.Parse("26f40fdf-8f92-4c4f-80c1-71090d86aef4"), "Gazprom"),
            new Company(Guid.Parse("1b0f8308-feb0-4d55-93ec-0765971e0bb7"), "Ozon"),
            new Company(Guid.Parse("5f7f47e3-610f-499c-9119-b73e1df23b62"), "Wildberries"),
            new Company(Guid.Parse("804B81BF-1730-43C8-AEEB-DE6685E41CC3"), "Yandex")
        );
}
