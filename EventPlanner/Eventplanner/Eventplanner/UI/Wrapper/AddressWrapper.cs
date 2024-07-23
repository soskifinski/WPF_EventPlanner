using Eventplanner.Model;

namespace Eventplanner.UI.Wrapper
{
    public class AddressWrapper : ModelWrapper<Address>
    {
        public AddressWrapper(Address model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string StreetHouseNr
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string PostalCode
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string PLZ
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string City
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Country
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Details
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
