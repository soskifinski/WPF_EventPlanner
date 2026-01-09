using Eventplanner.Model;

namespace Eventplanner.UI.Wrapper
{
    public class InstitutionWrapper : ModelWrapper<Location>
    {
        public InstitutionWrapper(Location model) : base(model)
        {
        }
        public int Id { get { return Model.Id; } }
        public string Name
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}
