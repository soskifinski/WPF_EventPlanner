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


        //protected override IEnumerable<string> ValidateProperty(string propertyName)
        //{
        //    switch (propertyName)
        //    {
        //        case nameof(Email):
        //            if (string.Equals(FirstName, "Robot", StringComparison.OrdinalIgnoreCase))
        //            {
        //                yield return "Robots are not valid friends";
        //            }
        //            break;
        //    }
        //}
    }
}
