using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        INPREPARATION,
        TOOKPLACE,
        BILLED,
        CLOSED
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
        CLEANER,
        TECHNICIAN,
        CONSTRUCTIONHELPER
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
    public enum Gender
    {
        MALE,
        FEMALE,
        DIVERS
    }
}
