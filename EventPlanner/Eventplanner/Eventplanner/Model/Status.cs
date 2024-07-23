using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventplanner.Model
{
    public enum Status
    {
        REQUESTED,
        PLANNED,
        PUBLISHED,
        BOOKEDUP,
        TOOKPLACE,
        BILLED
    }

    public enum TicketCategory
    {
        CHILD,
        ADULT,
        DISCOUNT
    }
    public enum Role
    {
        SUPERVISOR,
        ORGANIZER,
        ARTIST,
        GUARD,
        CLEANER
    }

    public enum WeekDays
    {
        MONDAY,
        TUESDAY,
        WEDNESDAY,
        THURSDAY,
        FRIDAY,
        SATURDAY,
        SUNDAY
    }
}
