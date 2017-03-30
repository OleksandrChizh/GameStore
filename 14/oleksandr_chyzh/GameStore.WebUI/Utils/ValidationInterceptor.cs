using System.Linq;
using System.Reflection;
using GameStore.Services.Interfaces.Exceptions;
using GameStore.Services.Interfaces.Validators;
using Ninject.Extensions.Interception;
using Ninject.Extensions.Interception.Request;
using WebGrease.Css.Extensions;

namespace GameStore.WebUI.Utils
{
    // This class checks arguments validity of services by calling method ValidateArgument of argument validators
    public class ValidationInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            if (invocation?.Request?.Method == null)
            {
                return;
            }

            IProxyRequest request = invocation.Request;
            MethodInfo method = request.Method;
            string methodName = method.Name;
            string className = method.DeclaringType?.FullName ?? string.Empty;
            ParameterInfo[] parameters = method.GetParameters();

            parameters
                .SelectMany(
                GetAttributes,
                    (p, a) => new
                    {
                        Parameter = p,
                        Attrribute = a
                    })
                .Select(d => new
                {
                    Details = new ArgumentExceptionDetails
                    {
                        ArgumentName = d.Parameter.Name,
                        Value = request.Arguments[d.Parameter.Position],
                        ClassName = className,
                        MethodName = methodName,
                    },
                    Attribute = d.Attrribute
                })
                .ForEach(d =>
                {
                    d.Attribute.ValidateArgument(d.Details);
                });

            invocation.Proceed();
        }

        private ArgumentValidationAttribute[] GetAttributes(ParameterInfo parameterInfo)
        {
            return (ArgumentValidationAttribute[])parameterInfo.GetCustomAttributes(typeof(ArgumentValidationAttribute), true);
        }
    }
}