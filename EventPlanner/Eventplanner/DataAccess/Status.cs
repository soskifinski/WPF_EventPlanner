using System.ComponentModel;

namespace Eventplanner.Model
{
    public enum Status
    {
        [Description("Angefragt")]
        REQUESTED,
        [Description("Geplant")]
        PLANNED,
        [Description("Veröffentlicht")]
        PUBLISHED,
        [Description("Gebucht")]
        BOOKEDUP,
        [Description("In Vorbereitung")]
        INPREPARATION,
        [Description("Stattgefunden")]
        TOOKPLACE,
        [Description("Angerechnet")]
        BILLED,
        [Description("Archiviert")]
        ARCHIVED
    }

    public enum PriceCategory
    {
        [Description("Kind")]
        CHILD,
        [Description("Erwachsener")]
        ADULT,
        [Description("Ermäßigt")]
        DISCOUNT
    }
    public enum ServiceRole
    {
        [Description("Veranstaltungsleitung")]
        SUPERVISOR,
        [Description("Veranstalter*")]
        ORGANIZER,
        [Description("Künstler*")]
        ARTIST,
        [Description("Veranstaltungsaufsicht")]
        GUARD,
        [Description("Reinigungsfachkraft")]
        CLEANER,
        [Description("Veranstaltungstechniker*")]
        TECHNICIAN,
        [Description("Aufbauhelfer*")]
        CONSTRUCTIONHELPER
    }

    public enum WeekDays
    {
        [Description("Montag")]
        MONDAY,
        [Description("Dienstag")]
        TUESDAY,
        [Description("Mittwoch")]
        WEDNESDAY,
        [Description("Donnerstag")]
        THURSDAY,
        [Description("Freitag")]
        FRIDAY,
        [Description("Samstag")]
        SATURDAY,
        [Description("Sonntag")]
        SUNDAY
    }
    public enum Gender
    {
        [Description("Mann")]
        MALE,
        [Description("Frau")]
        FEMALE,
        [Description("Divers")]
        DIVERS
    }
}
