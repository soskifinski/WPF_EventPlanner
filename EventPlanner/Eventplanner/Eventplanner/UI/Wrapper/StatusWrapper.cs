using Eventplanner.Model;

namespace Eventplanner.UI.Wrapper
{
    public class StatusWrapper : ModelWrapper<Status>
    {
        public StatusWrapper(Status model) : base(model)
        {
        }
        Status status;

        //public string GetGermanTranslation()
        //{
        //    switch (this.status)
        //    {
        //        case status.REQUESTED:
        //            return "Angefragt";
        //        case status.PLANNED:
        //            return "Geplant";
        //        case status.PUBLISHED:
        //            return "Veröffentlicht";
        //        case Status.BOOKEDUP:
        //            return "Buchung bestätigt";
        //        case Status.TOOKPLACE:
        //            return "Tatort";
        //        case Status.BILLED:
        //            return "Rechnung erstellt";
        //        default:
        //            return "Unbekannt";
        //    }
        //}
        public string Status
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
