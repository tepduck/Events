using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasData
                (
                    new Event
                    {
                        Id = Guid.NewGuid(),
                        Name = "Tech. Interview",
                        Description = "description-filler",
                        Speeker = "me",
                        EventDateAndPlace = "17.09 at Main Building"
                    },
                    new Event
                    {
                        Id = Guid.NewGuid(),
                        Name = "for test",
                        Description = "filler-descr",
                        Speeker = "not me",
                        EventDateAndPlace = "18.09 at BSTU" 
                    }
                );
        }
    }
}
