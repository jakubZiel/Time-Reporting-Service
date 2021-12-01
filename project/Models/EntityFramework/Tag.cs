using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace project.Models.EntityFramework
{
    public class Tag
    {
        public int ID { get; set; }
        public int ProjectID { get; set; }

        public string Name { get; set; }

        public class EmployeeConfig : IEntityTypeConfiguration<Tag>
        {
            public void Configure(EntityTypeBuilder<Tag> builder)
            {
                builder.Property(s => s.Name)
                    .HasMaxLength(40);
            }
        }

    }
}