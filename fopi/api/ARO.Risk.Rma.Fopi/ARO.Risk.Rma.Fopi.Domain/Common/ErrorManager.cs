using ARO.Risk.Rma.Fopi.Domain.Common.Interface;
using System.Reflection;

namespace ARO.Risk.Rma.Fopi.Domain.Common
{
    public class ErrorManager<T> where T : class, IHaveError
    {
        private T That;
        private Type ThatType;
        public ErrorManager(T that)
        {
            this.That = that;
            this.ThatType = this.That.GetType();
        }

        public void CheckProperty(string propertyName)
        {
            PropertyInfo? property = ThatType.GetProperty(propertyName);
            if (property == null)
            {
                throw new InvalidOperationException($"{propertyName} doesn't exists in type {ThatType.Name}");
            }
        }

        public void AddPropertyError(string propertyName, ErrorCode code)
        {
            var errors = code.ToString().Split(",").Select(i => i.Trim());
            foreach (var error in errors)
            {
                this.AddPropertyError(propertyName, error);
            }
        }

        private void AddPropertyError(string propertyName, string errorMessage)
        {
            CheckProperty(propertyName);

            if (That.Error.ContainsKey(propertyName))
            {
                That.Error[propertyName].Add(errorMessage);
            }
            else
            {
                That.Error.Add(propertyName, new HashSet<string> { errorMessage });
            }
        }
    }
}
