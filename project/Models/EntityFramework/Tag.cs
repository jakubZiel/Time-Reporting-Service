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

                builder.HasData(
                        new Tag() 
                        {
                            ID = 1,
                            ProjectID = 1,
                            Name = "coding"
                        },
                        new Tag() 
                        {
                            ID = 2,
                            ProjectID = 1,
                            Name = "debuging"
                        },
                        new Tag()
                        {
                            ID = 3,
                            ProjectID = 2,
                            Name = "database"
                        },
                        new Tag()
                        {
                            ID = 4,
                            ProjectID = 2,
                            Name = "coding"
                        },
                        new Tag()
                        {
                            ID = 5,
                            ProjectID = 3,
                            Name = "drinking"
                        },
                        new Tag()
                        {
                            ID = 6,
                            ProjectID = 3,
                            Name = "coding"
                        }
                    );
            }
        }

    }
}