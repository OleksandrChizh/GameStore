using System.Linq;
using System.Reflection;
using Ninject.Extensions.Factory;
using Ninject.Parameters;

namespace GameStore.WebUI
{
    public class FilterProvider : StandardInstanceProvider
    {
        protected override string GetName(MethodInfo methodInfo, object[] arguments)
        {
            string methodName = methodInfo.Name;
            return methodName.Replace("Make", string.Empty);
        }

        protected override IConstructorArgument[] GetConstructorArguments(MethodInfo methodInfo, object[] arguments)
        {
            return base.GetConstructorArguments(methodInfo, arguments).ToArray();
        }
    }
}