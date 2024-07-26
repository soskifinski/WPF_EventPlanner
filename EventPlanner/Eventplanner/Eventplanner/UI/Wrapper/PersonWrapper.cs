using Eventplanner.Model;

namespace Eventplanner.UI.Wrapper
{
    public class PersonWrapper : ModelWrapper<Person>
    {
        public PersonWrapper(Person model) : base(model)
        {
        }

        public int Id { get { return Model.Id; } }

        public string FirstName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string LastName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }


        public string TelephoneNumber
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Email
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool IsEmployee
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public Gender Gender
        {
            get { return GetValue<Gender>(); }
            set { SetValue(value); }
        }

        public Address Address
        {
            get { return GetValue<Address>(); }
            set { SetValue(value); }
        }        
    }
}
